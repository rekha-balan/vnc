//using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Infrastructure;
using Prism.Regions;
using Prism.Unity;

namespace ModuleB1
{
    public class ModuleB1Module : ModuleBase
    {
        public ModuleB1Module(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager) { }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ViewB1Button));
        }

        protected override void RegisterTypes()
        {
            // This is supposed to work but doesn't.
            //Container.RegisterTypeForNavigation<ViewB1>();

            // This longer form does work.
            Container.RegisterType<object, ViewB1>(typeof(ViewB1).FullName);
        }
    }
}
