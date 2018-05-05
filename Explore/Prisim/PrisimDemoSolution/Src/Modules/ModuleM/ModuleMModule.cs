using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Prism.Regions;
using PrismDemo.Infrastructure;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleM
{
    public class ModuleMModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        //public ModuleMModule(IUnityContainer container, IRegionManager regionManager)
        //{
        //    _container = container;
        //    _regionManager = regionManager;
        //}

        public ModuleMModule(IUnityContainer container)
        {
            _container = container;
        }
        // Register our Views with our container

        public void Initialize()
        {
            _container.RegisterType<ToolbarA>();
            _container.RegisterType<ContentA>();

            //_regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ToolbarA));
            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentA));
        }
    }
}
