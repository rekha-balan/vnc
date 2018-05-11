using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Infrastructure;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleAS
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
            _container.RegisterType<IContentASView, ContentASView>();
            _container.RegisterType<IContentASViewViewModel, ContentASViewViewModel>();

            _manager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentAView));
        }
    }
}
