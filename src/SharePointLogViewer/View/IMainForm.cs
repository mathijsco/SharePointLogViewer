using SharePointLogViewer.DataSources.Uls;
using SharePointLogViewer.EventArguments;
using SharePointLogViewer.Mvp;
using System;

namespace SharePointLogViewer.View
{
    public interface IMainForm : IView
    {
        event EventHandler<DefaultLoadingEventArgs> DefaultLoading;
        event EventHandler RefreshRequest;
        event EventHandler<TimeLimitChangedEventArgs> TimeLimitChanged;
        event EventHandler<SimpleFilterOpeningEventArgs> FilterOpening;
        event EventHandler<SimpleFilterChangedEventArgs> FilterChanged;
        event EventHandler<SimpleFilterHistoryEventArgs> FilterPreviousRequest;
        event EventHandler<SourceChangedEventArgs> SourceChanged;
        event EventHandler<UlsLogEntryEventArgs> UlsEntryRequest;
        event EventHandler<UlsLogEntryEventArgs> FindUlsEntryRequest;
        event EventHandler<WhatsWrongEventArgs> WhatsWrongRequest;
        event EventHandler<ExportEntriesEventArgs> ExportEntriesRequest;

        void LoadUlsEntries(int entryCount, string openedHostName, string openedLocation);

        void ExportEntriesCompleted(bool success, Exception exception);

        void ShowException(Exception ex);

        void StartLoading();

        void StopLoading();
    }
}
