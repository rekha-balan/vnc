using PrismDemo.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using ModuleA;
using Prism.Regions;
using System.Windows.Controls;
using Infrastructure;
using System;

namespace PrismDemo
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        //// To load modules from code

        //protected override void ConfigureModuleCatalog()
        //{
        //    Type moduleAType = typeof(ModuleAModule);
        //    ModuleCatalog.AddModule(new ModuleInfo()
        //    {
        //        ModuleName = moduleAType.Name,
        //        ModuleType = moduleAType.AssemblyQualifiedName,
        //        InitializationMode = InitializationMode.WhenAvailable
        //    });
        //}

        //// To load modules from directory

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        //}

        //// To load modules from Xaml
        //// TODO(crhodes)
        //// This is not working

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return Prism.Modularity.ModuleCatalog.CreateFromXaml(
        //        new Uri("/PrismDemoRegisterModule;component/XamlCatalog.xaml", UriKind.Relative));
        //}

        // To load from an App.Config file

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            return mappings;
        }
    }
}
