using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using PluralsightPrismDemo.Infrastructure;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _manager;

        public ModuleAModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _manager = manager;
        }

        public void Initialize()
        {
            _container.RegisterType<ToolbarA>();
            _container.RegisterType<IContentAView, ContentA>();
            _container.RegisterType<IContentAViewViewModel, ContentAViewViewModel>();

            _manager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ToolbarA));
        }
    }
}
