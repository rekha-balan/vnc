using PrismDemo.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using ModuleA;
using ModuleB;
using ModuleA1;
using ModuleB1;
using Prism.Regions;
using System.Windows.Controls;
using Infrastructure;
using System;
using People;
using StatusBar;
using Toolbar;
using Services.PersonService;

namespace PrismDemo
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            catalog.AddModule(typeof(PersonServiceModule));

            catalog.AddModule(typeof(ModuleA1Module));
            catalog.AddModule(typeof(ModuleB1Module));

            return catalog;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<ViewModels.IMainWindowViewModel, ViewModels.MainWindowViewModel>();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
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
