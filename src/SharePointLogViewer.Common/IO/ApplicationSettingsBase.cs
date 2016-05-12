using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SharePointLogViewer.Common.IO
{
    /// <summary>
    /// The base class for application settings.
    /// </summary>
    /// <typeparam name="TSettings"></typeparam>
    public abstract class ApplicationSettingsBase<TSettings> : IDisposable where TSettings : class
    {
        private static TSettings _instance;

        /// <summary>
        /// Gets the settings instance with the key value container.
        /// </summary>
        protected static TSettings Instance
        {
            get { return _instance = (_instance ?? (TSettings)Activator.CreateInstance(typeof(TSettings), true)); }
        }

        public static event PropertyChangedEventHandler PropertyChanged;
        private static void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(null, e);
        }


        private Dictionary<string, object> _container;
        private readonly string _fileLocation;
        private Timer _saveTimer;
        private bool _disposed;
        private readonly object _syncRoot = new object();

        /// <summary>
        /// Creates a new application setting base class with the default setting file named 'settings.xml'.
        /// </summary>
        protected ApplicationSettingsBase()
            : this("settings.xml") { }

        /// <summary>
        /// Creates a new application setting base class with the XML file specified.
        /// </summary>
        /// <param name="fileLocation"></param>
        protected ApplicationSettingsBase(string fileLocation)
        {
            if (!Path.IsPathRooted(fileLocation))
                fileLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileLocation);

            _fileLocation = fileLocation;
        }

        /// <summary>
        /// Saves the settings to the XML.
        /// </summary>
        private void Save()
        {
            lock (_syncRoot)
            {
                if (_saveTimer == null)
                    return;

                _saveTimer.Dispose();
                _saveTimer = null;

                Trace.WriteLine("Saving the settings to: '" + _fileLocation + "'...");

                // Load the values from the XML if it's not done yet.
                Load();

                var doc = new XDocument();
                var root = new XElement("Settings");
                foreach (var setting in _container)
                {
                    Trace.Write("Process setting '" + setting.Key + "'... ");

                    var e = new XElement(setting.Key);

                    var serializedSetting = setting.Value as SerializedSettingWrapper;
                    if (serializedSetting != null)
                    {
                        e.Value = serializedSetting.Content;
                        e.SetAttributeValue("serialized", "true");
                    }
                    else if (!setting.Value.GetType().IsPrimitive && !(setting.Value is string))
                    {
                        e.Value = Serialize(setting.Value, setting.Value.GetType());
                        e.SetAttributeValue("serialized", "true");
                    }
                    else
                        e.Value = setting.Value.ToString();

                    root.Add(e);
                    Trace.WriteLine("done");

                }
                doc.Add(root);

                Trace.WriteLine("Save the settings...");
                doc.Save(_fileLocation);
            }
        }

        /// <summary>
        /// Loads the settings from the XML file.
        /// </summary>
        private void Load()
        {
            lock (_syncRoot)
            {
                if (_container != null)
                    return;

                Trace.WriteLine("Loading settings XML file from: '" + _fileLocation + "'...");

                _container = new Dictionary<string, object>();
                XDocument doc;
                try
                {
                    doc = XDocument.Load(_fileLocation);
                }
                catch (FileNotFoundException)
                {
                    Trace.WriteLine("Setting file does not exist.");
                    return;
                }

                var root = doc.Element("Settings");
                if (root != null)
                {
                    foreach (var element in root.Elements())
                    {
                        Trace.Write("Loading setting '" + element.Name.LocalName + "'... ");
                        var serializedAttribute = element.Attribute("serialized");
                        if (serializedAttribute != null &&
                            serializedAttribute.Value.Equals("true", StringComparison.OrdinalIgnoreCase))
                            _container[element.Name.LocalName] = new SerializedSettingWrapper(element.Value);
                        else
                            _container[element.Name.LocalName] = element.Value;

                        Trace.WriteLine("done");

                    }
                    Trace.WriteLine("Loading settings XML file from: '" + _fileLocation + "'...");
                }
                Trace.WriteLine("Done loading settings.");
            }
        }

        /// <summary>
        /// Gets a value from the container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        protected T Get<T>(string key = null)
        {
            // Resolve the property name
            if (key == null)
            {
                var trace = new StackTrace();
                var frame = trace.GetFrame(1);
                var method = frame.GetMethod();
                var methodName = method.Name;
                if (!methodName.StartsWith("get_"))
                    throw new InvalidOperationException("The current caller is not a property.");
                key = methodName.Remove(0, 4);
                Trace.WriteLine("Reading setting with key: '" + key + "'.");
            }

            Load();

            lock (_syncRoot)
            {
                if (_container.ContainsKey(key))
                {
                    var value = _container[key];

                    // Deserialize if possible
                    var serializedSetting = value as SerializedSettingWrapper;
                    if (serializedSetting != null)
                        value = _container[key] = Deserialize<T>(serializedSetting.Content);

                    // Return the real value
                    if (value is T) return (T)value;
                    // Return a converted value
                    return (T)(_container[key] = Convert.ChangeType(value, typeof(T)));
                }
                else
                {
                    Trace.Write("Existing key does not exists in the settings file. Trying to load the default value...");

                    var property = this.GetType().GetProperty(key, BindingFlags.Static | BindingFlags.Public);
                    if (property != null)
                    {
                        var attribute = property.GetCustomAttributes(typeof(DefaultValueAttribute), false).FirstOrDefault() as DefaultValueAttribute;
                        if (attribute != null)
                        {
                            Trace.WriteLine("Default value found.");
                            var value = (T)attribute.Value;
                            _container.Add(key, value);
                            return value;
                        }
                        Trace.WriteLine("No default value.");
                    }
                    else
                        Trace.WriteLine("Property could not be found.");

                    _container.Add(key, default(T));
                    return default(T);
                }
            }
        }

        /// <summary>
        /// Sets a value in the container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="key"></param>
        protected void Set<T>(T value, string key = null)
        {
            // Resolve the property name
            if (key == null)
            {
                var trace = new StackTrace();
                var frame = trace.GetFrame(1);
                var methodName = frame.GetMethod().Name;
                if (!methodName.StartsWith("set_"))
                    throw new InvalidOperationException("The current caller is not a property.");
                key = methodName.Remove(0, 4);
                Trace.Write("Writing setting with key: '" + key + "'.");
            }
            var changed = !_container.ContainsKey(key) || _container[key] == null || !_container[key].Equals(value);
            if (changed)
            {
                if (_container.ContainsKey(key))
                    _container[key] = value;
                else
                    _container.Add(key, value);

                OnPropertyChanged(new PropertyChangedEventArgs(key));

                lock (_syncRoot)
                {
                    // If an timer is running, reset it back to 5 seconds before saving.
                    if (_saveTimer != null)
                        _saveTimer.Dispose();
                    _saveTimer = new Timer(o => Save(), null, 5000, int.MaxValue);
                }
            }
        }

        #region Serialize and Deserialize

        private static string Serialize(object obj, Type type)
        {
            var builder = new StringBuilder();
            using (var stream = new StringWriter(builder))
            {
                var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                var serializer = new XmlSerializer(type);
                serializer.Serialize(stream, obj, emptyNs);
            }
            var resultIncXml = builder.ToString();
            return resultIncXml.Remove(0, resultIncXml.IndexOf("\n", StringComparison.Ordinal) + 1); // Remove the XML declaration
        }

        private static T Deserialize<T>(string content)
        {
            using (var stream = new StringReader(content))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Destructs the class.
        /// </summary>
        ~ApplicationSettingsBase()
        {
            ((IDisposable)this).Dispose();
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_saveTimer != null)
                    {
                        _saveTimer.Dispose();
                        Save();
                    }
                }

                _saveTimer = null;
                _disposed = true;
            }
        }

        #endregion
    }
}
