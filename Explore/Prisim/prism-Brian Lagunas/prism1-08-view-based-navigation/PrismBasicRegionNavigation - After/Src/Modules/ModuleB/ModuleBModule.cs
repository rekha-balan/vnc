using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PluralsightPrismDemo.Infrastructure;

namespace ModuleB
{
    public class ModuleBModule : ModuleBase
    {
        public ModuleBModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager) { }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ViewBButton));
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<ViewB>();
        }
    }
}
