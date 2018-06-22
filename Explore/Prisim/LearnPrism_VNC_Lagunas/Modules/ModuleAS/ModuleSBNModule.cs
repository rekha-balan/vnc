using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Infrastructure;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleSBN
{
    public class ModuleSBNModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _manager;

        public ModuleSBNModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _manager = manager;
        }

        public void Initialize()
        {
            _container.RegisterType<IContentSBN, ContentSBN>();
            _container.RegisterType<IContentSBNViewModel, ContentSBNViewModel>();

            _manager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentSBN));
        }
    }
}
