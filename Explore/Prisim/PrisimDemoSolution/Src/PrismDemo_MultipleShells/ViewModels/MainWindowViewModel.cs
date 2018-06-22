using Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismDemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        //public MainWindowViewModel()
        //{

        //}

        // ViewModel locator approach

        IRegionManager _regionManager;
        private readonly IShellService _service;

        public DelegateCommand<string> OpenShellCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager, IShellService service)
        {
            _regionManager = regionManager;
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
            _regionManager.RequestNavigate(KnownRegionNames.ContentRegion, viewName);
        }

    }
}
