namespace SharePointLogViewer.Common.IO
{
    /// <summary>
    /// Wrapper for the serialized settings.
    /// </summary>
    internal sealed class SerializedSettingWrapper
    {
        public string Content { get; private set; }

        public SerializedSettingWrapper(string content)
        {
            Content = content;
        }
    }
}
