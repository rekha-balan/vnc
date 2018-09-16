using System;
using Microsoft.Practices.Unity;
using Infrastructure;
using Prism.Regions;
using Prism.Modularity;
using ModuleInterfaces;

namespace StatusBar
{
    public class StatusBarModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public StatusBarModule(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            var vm = _container.Resolve<IStatusBarViewModel>();
            _regionManager.Regions[RegionNames.StatusBarRegionC_DC].Add(vm.View);
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IStatusBarViewModel, StatusBarViewModel>();
            _container.RegisterType<IStatusBar, StatusBar>();
        }
    }
}
