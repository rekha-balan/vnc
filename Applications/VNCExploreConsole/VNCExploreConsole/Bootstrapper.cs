using VNCExploreConsole.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Infrastructure;
using System.Windows.Controls;
using Prism.Regions;
using ModuleA;
using SimpleView;
using PeopleViewer;
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.CSV;
using PersonRepository.SQL;
using System;
using MVVMView1st;
using MVVMViewModel1st;
using StatusBar;
using PeopleCC;
using PeopleDC;
using Toolbar;

namespace VNCExploreConsole
{
    class Bootstrapper : UnityBootstrapper
    {
        // Step 1a - Create the catalog of Modules
        //
        // This is called when you do not directly reference the Assembly
        // containing the module.

        // TODO(crhodes)
        // Figure out how you can do more than one of these creates at a time
        // e.g. App.Config and look in Directory

        // To load modules from directory
        //
        // NB. ModuleB.dll and ModuleD.dll have not been referenced
        // but appears in .\bin\Modules folder

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
        //        new Uri("/VNCExploreConsole;component/XamlCatalog.xaml", UriKind.Relative));
        //}

        // To load from an App.Config file
        //
        // NB. ModuleD.dll has not been referenced
        // but appears in .\bin\Modules folder

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        // Step 1b - Configure the catalog of modules
        // Modules are loaded at Startup and must be a project reference

        protected override void ConfigureModuleCatalog()
        {
            Type moduleAType = typeof(ModuleAModule);
            ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = moduleAType.Name,
                ModuleType = moduleAType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
                //    InitializationMode = InitializationMode.OnDemand
            });

            var moduleCatalog = (ModuleCatalog)ModuleCatalog;

            //moduleCatalog.AddModule(typeof(ModuleAModule));

            // View Examples

            moduleCatalog.AddModule(typeof(SimpleViewModule));

            moduleCatalog.AddModule(typeof(MVVMView1stModule));

            moduleCatalog.AddModule(typeof(MVVMViewModel1stModule));

            // Commanding Examples

            moduleCatalog.AddModule(typeof(StatusBarModule));
            moduleCatalog.AddModule(typeof(PeopleDC.PeopleModule));
            moduleCatalog.AddModule(typeof(PeopleCC.PeopleModule));
            moduleCatalog.AddModule(typeof(ToolbarModule));


            moduleCatalog.AddModule(typeof(PeopleViewerDIModule));
            moduleCatalog.AddModule(typeof(PeopleViewerTightCouplingModule));
            moduleCatalog.AddModule(typeof(PeopleViewerLooseCouplingModule));
        }

        // Step 2 - Configure the container

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            // Use the ServiceRepository
            //Container.RegisterType<PersonRepository.Interface.IPersonRepository, ServiceRepository>();
            // Use the CSVRepository
            //Container.RegisterType<PersonRepository.Interface.IPersonRepository, CSVRepository>();
            // Use the SQLRepository
            Container.RegisterType<PersonRepository.Interface.IPersonRepository, SQLRepository>();
        }

        // Step 3 - Configure the RegionAdapters if any custom ones have been created

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();

            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            return mappings;
        }

        // Step 4 - Create the Shell that will hold the modules in designated regions.

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        // Step 5 - Show the MainWindow

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
