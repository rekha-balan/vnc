using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Infrastructure;
using System.Windows;
using System;

namespace PrismDemo.ViewModels
{
    public class MainWindowViewNavigationViewModel : ViewModelBase, IMainWindowViewNavigationViewModel
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<object> NavigateCommand { get; private set; }

        public MainWindowViewNavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<object>(Navigate);
            ApplicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
        }

        private void Navigate(object navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath.ToString(), NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            // Can do work here after navigation complete

            MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
        }
    }
}
