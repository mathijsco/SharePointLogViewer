using System;
using SharePointLogViewer.Common.Filters;
using SharePointLogViewer.Model;
using SharePointLogViewer.Mvp;
using SharePointLogViewer.View;
using SharePointLogViewer.EventArguments;
using System.Threading.Tasks;

namespace SharePointLogViewer.Presenter
{
    public class MainFormPresenter : MvpPresenter<IMainForm, IMainFormModel>
    {
        public MainFormPresenter(IMainForm view)
            : base(view, new MainFormModel())
        {
            this.View.UlsEntryRequest += ViewUlsEntryRequest;
            this.View.FindUlsEntryRequest += View_FindUlsEntryRequest;
            this.View.DefaultLoading += View_DefaultLoading;
            this.View.RefreshRequest += View_RefreshRequest;
            this.View.FilterOpening += View_FilterOpening;
            this.View.TimeLimitChanged += View_TimeLimitChanged;
            this.View.FilterChanged += View_FilterChanged;
            this.View.FilterPreviousRequest += View_FilterPreviousRequest;
            this.View.SourceChanged += View_SourceChanged;
            this.View.WhatsWrongRequest += View_WhatsWrongRequest;
            this.View.ExportEntriesRequest += View_ExportEntriesRequest;
            this.Model.LoadingUlsEntriesCompleted += Model_LoadingUlsEntriesCompleted;
        }

        private void View_ExportEntriesRequest(object sender, ExportEntriesEventArgs e)
        {
            this.View.StartLoading();
            var task = new Task(() => this.Model.ExportEntries(e.Path));
            task.ContinueWith((t) =>
            {
                this.View.StopLoading();

                var ex = t.Exception != null ? t.Exception.GetBaseException() : null;
                this.View.ExportEntriesCompleted(!t.IsFaulted, ex);
            });
            task.Start();
        }

        private void View_TimeLimitChanged(object sender, EventArguments.TimeLimitChangedEventArgs e)
        {
            this.View.StartLoading();

            this.Model.ChangeTimeLimit(e.Time);
            this.Model.RefreshEntriesAsync(false);
        }

        private void View_FilterOpening(object sender, EventArguments.SimpleFilterOpeningEventArgs e)
        {
            e.Result = this.Model.Filter;
        }

        private void View_FilterChanged(object sender, EventArguments.SimpleFilterChangedEventArgs e)
        {
            this.View.StartLoading();

            this.Model.Filter = e.Filter;
            this.Model.RefreshEntriesAsync(false);
            e.HasFiltersInHistory = this.Model.HasFiltersInHistory;
        }

        private void View_FilterPreviousRequest(object sender, SimpleFilterHistoryEventArgs e)
        {
            this.Model.PreviousFilter();
            e.HasFiltersInHistory = this.Model.HasFiltersInHistory;
        }

        private void View_WhatsWrongRequest(object sender, EventArguments.WhatsWrongEventArgs e)
        {
            e.ResultIndex = this.Model.FindFirstException(e.StartIndex);
        }

        private void ViewUlsEntryRequest(object sender, EventArguments.UlsLogEntryEventArgs e)
        {
            e.Entry = this.Model.GetEntry(e.Index);
        }

        private void View_FindUlsEntryRequest(object sender, EventArguments.UlsLogEntryEventArgs e)
        {
            e.Index = this.Model.FindIndexFor(e.Entry);
        }

        private void Model_LoadingUlsEntriesCompleted(object sender, EventArguments.LoadingUlsEntriesCompletedEventArgs e)
        {
            if (e.Exception != null)
            {
                this.View.StopLoading();
                this.View.ShowException(e.Exception);
            }
            else
            {
                this.View.LoadUlsEntries(e.Entries.Count, e.OpenedHostName, e.OpenedLocation);
                this.View.StopLoading();
            }
        }

        private void View_SourceChanged(object sender, EventArguments.SourceChangedEventArgs e)
        {
            this.View.StartLoading();

            this.Model.ChangeSource(e);
            this.Model.RefreshEntriesAsync(true);
        }

        private void View_RefreshRequest(object sender, EventArgs e)
        {
            this.View.StartLoading();

            if (Settings.LoseFilterForRefresh)
                this.Model.Filter = new SimpleFilter();

            this.Model.RefreshEntriesAsync(true);
        }

        private void View_DefaultLoading(object sender, EventArguments.DefaultLoadingEventArgs e)
        {
            e.TimeLimitation = this.Model.GetTimeLimits();
        }
    }
}
