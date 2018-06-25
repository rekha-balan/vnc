using PrismDemo.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using ModuleRibbon;
using Prism.Regions;
using System.Windows.Controls;
using Infrastructure;
using System;
using DevExpress.Xpf.Ribbon;
using PrismDemo.Prism;
// PrismDemo_Views
namespace PrismDemo
{
    class Bootstrapper : UnityBootstrapper
    {
        // Step 1
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(ModuleRibbonModule));
        }

        // Step 2
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            mappings.RegisterMapping(typeof(RibbonControl), Container.Resolve<RibbonControlRegionAdapter>());
            mappings.RegisterMapping(typeof(RibbonStatusBarControl), Container.Resolve<RibbonStatusBarControlRegionAdapter>());
            return mappings;
        }

        // Step 3
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        // Step 4
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
