using System;
using System.Windows.Forms;
using SharePointLogViewer.Common.Utilities;
using SharePointLogViewer.View;
using System.Diagnostics;

namespace SharePointLogViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var enabled = TraceManager.Enabled = Settings.EnableDiagnostics;
            if (Settings.EnableDiagnostics)
            {
                AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Trace.Listeners.Add(new TextWriterTraceListener("Trace.log", "Trace") { TraceOutputOptions = TraceOptions.DateTime });
                Trace.AutoFlush = true;

                //TraceManager.WriteNewFile();
                Trace.WriteLine("");
                Trace.WriteLine("SharePoint log viewer started with loggin at " + DateTime.Now);
            }

            Application.Run(new MainForm());
        }

        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Trace.TraceError(e.ExceptionObject.ToString());
            Trace.Flush();
        }
    }
}
