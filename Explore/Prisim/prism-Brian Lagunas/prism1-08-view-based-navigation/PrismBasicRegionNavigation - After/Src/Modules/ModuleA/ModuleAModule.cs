﻿using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PluralsightPrismDemo.Infrastructure;

namespace ModuleA
{
    public class ModuleAModule : ModuleBase
    {
        public ModuleAModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager) { }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ViewAButton));
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<ViewA>();
        }
    }
}
