using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using PluralsightPrismDemo.Infrastructure;
using System.Windows;

namespace PluralsightPrismDemo
{
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<object> NavigateCommand { get; private set; }

        public ShellViewModel(IRegionManager regionManager)
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
            MessageBox.Show(String.Format("Navigation to {0} complete. ", result.Context.Uri));
        }
    }
}
