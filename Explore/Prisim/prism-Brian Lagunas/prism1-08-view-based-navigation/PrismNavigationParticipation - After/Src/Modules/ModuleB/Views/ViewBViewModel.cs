using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using PluralsightPrismDemo.Infrastructure;

namespace ModuleB
{
    public class ViewBViewModel : ViewModelBase, IViewBViewModel, INavigationAware, IRegionMemberLifetime
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

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            PageViews++;
        }

        public bool KeepAlive
        {
            get { return false; }
        }
    }
}
