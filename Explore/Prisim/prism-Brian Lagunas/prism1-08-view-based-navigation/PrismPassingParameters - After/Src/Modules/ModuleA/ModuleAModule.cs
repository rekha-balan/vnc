using Microsoft.Practices.Prism.Regions;
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
            ApplicationCommands.NavigateCommand.Execute(typeof(ViewA));
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<IViewAViewModel, ViewAViewModel>();
            Container.RegisterTypeForNavigation<ViewA>();
            Container.RegisterType<IEmailViewViewModel, EmailViewViewModel>();
            Container.RegisterTypeForNavigation<EmailView>();
        }
    }
}
