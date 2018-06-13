using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using PluralsightPrismDemo.Infrastructure;
using Microsoft.Practices.ServiceLocation;

namespace ModuleA
{
    [ModuleExport(typeof(ModuleAModule), InitializationMode=InitializationMode.WhenAvailable)]
    public class ModuleAModule : IModule
    {
        IRegionManager _manager;

        [ImportingConstructor]
        public ModuleAModule(IRegionManager regionManager)
        {
            _manager = regionManager;
        }

        public void Initialize()
        {
            _manager.Regions[RegionNames.ToolbarRegion].Add(ServiceLocator.Current.GetInstance<ToolbarA>());

            IRegion region = _manager.Regions[RegionNames.ContentRegion];
            region.Add(ServiceLocator.Current.GetInstance<ContentA>());
        }
    }
}
