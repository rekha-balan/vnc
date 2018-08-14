using EASECommandConsole.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Infrastructure;
using System.Windows.Controls;
using Prism.Regions;
using ModuleA;
using PeopleViewer;

namespace EASECommandConsole
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(ModuleAModule));
            moduleCatalog.AddModule(typeof(PeopleViewerTightCouplingModule));
            moduleCatalog.AddModule(typeof(PeopleViewerLooseCouplingModule));
        }

        protected override Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            return mappings;
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
