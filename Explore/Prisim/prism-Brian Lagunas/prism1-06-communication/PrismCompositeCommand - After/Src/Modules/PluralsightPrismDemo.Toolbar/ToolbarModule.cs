using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PluralsightPrismDemo.Infrastructure;

namespace PluralsightPrismDemo.Toolbar
{
    public class ToolbarModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ToolbarModule(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            var vm = _container.Resolve<IToolbarViewModel>();
            _regionManager.Regions[RegionNames.ToolbarRegion].Add(vm.View);
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IToolbarViewModel, ToolbarViewModel>();
            _container.RegisterType<IToolbarView, ToolbarView>();
        }
    }
}
