namespace SharePointLogViewer.DataSources.Uls
{
    internal class UlsLogFoundEntry : UlsLogEntry
    {
        public UlsLogEntry Entry { get; private set; }

        public int Index { get; private set; }

        public UlsLogFoundEntry(UlsLogEntry entry, int index)
        {
            this.Entry = entry;
            this.Index = index;
        }
    }
}
