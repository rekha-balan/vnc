using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using PluralsightPrismDemo.Infrastructure;

namespace ModuleB
{
    public class ViewBViewModel : ViewModelBase, IViewBViewModel
    {
        private int _pageViews;
        public int PageViews
        {
            get { return _pageViews; }
            set
            {
                _pageViews = value;
                OnPropertyChanged("PageViews");
            }
        }       

        public ViewBViewModel()
        {

        }
    }
}
