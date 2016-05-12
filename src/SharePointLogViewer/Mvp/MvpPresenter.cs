using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.Mvp
{
    public class MvpPresenter<TView, TModel>
        where TView : IView
    {
        protected TView View { get; private set; }

        protected TModel Model { get; private set; }

        public MvpPresenter(TView view, TModel model)
        {
            this.View = view;
            this.Model = model;
        }
    }
}
