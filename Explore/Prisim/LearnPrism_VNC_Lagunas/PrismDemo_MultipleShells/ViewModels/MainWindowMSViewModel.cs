using Infrastructure;
using Infrastructure.Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismDemo.ViewModels
{
    public class MainWindowMSViewModel : BindableBase, IRegionManagerAware
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        // ViewModel locator approach.  Got here because of AutoWireUp

        IRegionManager _regionManager;
        private readonly IShellService _service;

        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }

        #region IRegionManagerAware

        public IRegionManager RegionManager
        {
            get;
            set;
        }

        #endregion

        // The region gets passed in.  Problem is it is always the global RegionManager.
        // Which means the Navigate() method, infra, always is navigating on the first shell.

        //public MainWindowMSViewModel(IRegionManager regionManager, IShellService service)
        //{
        //    _regionManager = regionManager;
        //    _service = service;

        //    OpenShellCommand = new DelegateCommand<string>(OpenShell);
        //    NavigateCommand = new DelegateCommand<string>(Navigate);
        //}

    public MainWindowMSViewModel(IShellService service)
    {
        _service = service;

        OpenShellCommand = new DelegateCommand<string>(OpenShell);
        NavigateCommand = new DelegateCommand<string>(Navigate);
    }

    void OpenShell(string viewName)
        {
            _service.ShowShell(viewName);
        }

        void Navigate(string viewName)
        {
        // Should do null check.  This gets set in BootStrapper.

        //_regionManager.RequestNavigate(KnownRegionNames.ContentRegion, viewName);
        RegionManager.RequestNavigate(KnownRegionNames.ContentRegion, viewName);
    }

}
}
