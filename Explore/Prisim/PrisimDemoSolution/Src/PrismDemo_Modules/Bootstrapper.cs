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

        // To load modules from code
        //
        // NB. ModuleA needs to be referenced

        protected override void ConfigureModuleCatalog()
        {
            Type moduleAType = typeof(ModuleAModule);
            ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = moduleAType.Name,
                ModuleType = moduleAType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }

        // To load modules from directory
        //
        // NB. ModuleB.dll and ModuleD.dll have not been referenced
        // but appear in .\bin\Modules folder

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        //}

        // To load modules from Xaml
        //
        // NB. ModuleC.dll has not been referenced
        // but appear in .\bin folder

        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return Prism.Modularity.ModuleCatalog.CreateFromXaml(
        //        new Uri("/PrismDemoRegisterModule;component/XamlCatalog.xaml", UriKind.Relative));
        //}

        // To load from an App.Config file
        //
        // NB. ModuleD.dll has not been referenced
        // but appears in .\bin\Modules folder

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
