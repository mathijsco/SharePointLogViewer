using System;
using System.Linq;
using System.Windows.Forms;
using SharePointLogViewer.Common.Enums;
using SharePointLogViewer.Common.Filters;

namespace SharePointLogViewer.View.Controls
{
    public partial class SimpleFilterUserControl : UserControl
    {
        private bool _filterSet;
        private SimpleFilter _filter;

        public SimpleFilterUserControl()
        {
            InitializeComponent();

            LoadLevelFilters();
        }

        public SimpleFilter Filter
        {
            get
            {
                return CreateFilter();
            }
            set
            {
                txtInclude.Text = value.IncludeText;
                txtExclude.Text = value.ExcludeText;
                foreach (int level in Enum.GetValues(typeof (ErrorLevel)))
                {
                    if (level == 0) continue;
                    if (value.ErrorLevel.HasFlag((ErrorLevel)level))
                        lstErrorLevels.SelectedItems.Add((ErrorLevel)level);
                }

                if (!_filterSet)
                {
                    _filterSet = true;
                    _filter = value;
                }
            }
        }

        public bool FilterChanged
        {
            get { return !CreateFilter().Equals(_filter); }
        }

        public bool InitialFilterIsEmpty
        {
            get { return _filter.IsEmpty; }
        }

        private SimpleFilter CreateFilter()
        {
            var include = txtInclude.Text.Trim();
            var exclude = txtExclude.Text.Trim();

            return new SimpleFilter
            {
                IncludeText = !string.IsNullOrEmpty(include) ? include : null,
                ExcludeText = !string.IsNullOrEmpty(exclude) ? exclude : null,
                ErrorLevel = lstErrorLevels.SelectedItems.Count > 0
                    ? lstErrorLevels.SelectedItems.Cast<ErrorLevel>().Aggregate((e1, e2) => e1 | e2)
                    : ErrorLevel.Unknown
            };
        }

        private void LoadLevelFilters()
        {
            foreach (int id in ((int[])Enum.GetValues(typeof(ErrorLevel))).Reverse())
            {
                if (id == 0) continue;
                lstErrorLevels.Items.Add((ErrorLevel)id);
            }
        }
    }
}
