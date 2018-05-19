using System;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.Practices.Prism.Regions;
using Infrastructure;
using Prism.Regions;

namespace ModuleA1
{
    public class ViewA1ViewModel : ViewModelBase, IViewA1ViewModel, INavigationAware
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

        public ViewA1ViewModel()
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
    }
}
