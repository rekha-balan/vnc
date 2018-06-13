using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
//using Microsoft.Practices.Prism.Regions;
using Prism.Regions;

namespace ModuleB1
{
    public class ViewB1ViewModel : ViewModelBase, IViewB1ViewModel, INavigationAware, IRegionMemberLifetime
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

        public ViewB1ViewModel()
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
            get { return true; }
        }
    }
}
