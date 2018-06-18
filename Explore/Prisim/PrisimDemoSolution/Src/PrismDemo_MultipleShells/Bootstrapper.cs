using PrismDemo.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using ModuleR;
using ModuleRMS;
using Prism.Regions;
using System.Windows.Controls;
using Infrastructure;
using System;
using Prism.Mvvm;
using System.Reflection;
using System.Globalization;
using Infrastructure.Prism;

// PrismDemo_Views
namespace PrismDemo
{
    class Bootstrapper : UnityBootstrapper
    {
        public Bootstrapper()
        {
            //This is not needed.  Does not seem to work.  AutoWireUp works!
           //ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
           //{
           //    var viewName = viewType.FullName;
           //    var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
           //    var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
           //    return Type.GetType(viewModelName);
           //});
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(ModuleRModule));
            moduleCatalog.AddModule(typeof(ModuleRMSModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
            //{
            //    return Container.Resolve(type);
            //});

            // Register as a Singleton
            Container.RegisterType<IShellService, ShellService>(new ContainerControlledLifetimeManager());
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            return mappings;
        }

        // If have custom region adapter, have to tell bootstrapper

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            IRegionBehaviorFactory behaviors = base.ConfigureDefaultRegionBehaviors();
            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));

            return behaviors;
        }

        protected override DependencyObject CreateShell()
        {
            //return Container.Resolve<MainWindow>();
            return Container.Resolve<MainWindowMS>();
        }

        protected override void InitializeShell()
        {
            // Need to make first shell aware of Global Region manager
            // Example shows IRegionManagerAware on ViewModel.
            // Had to implement it on MainWindowMS to get it to work.
            // Need to understand.

            var regionManager = RegionManager.GetRegionManager(Shell);
            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);

            App.Current.MainWindow.Show();
        }
    }
}
