using Infrastructure;
//using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Infrastructure;
using Prism.Regions;
using Prism.Unity;

namespace ModuleA1
{
    public class ModuleA1Module : ModuleBase
    {
        public ModuleA1Module(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager) { }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ViewA1Button));
        }

        protected override void RegisterTypes()
        {
            //Container.RegisterTypeForNavigation<ViewA1>();
            Container.RegisterType<object, ViewA1>(typeof(ViewA1).FullName);
        }
    }
}
