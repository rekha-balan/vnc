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
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.CSV;
using PersonRepository.SQL;

namespace EASECommandConsole
{
    class Bootstrapper : UnityBootstrapper
    {
 
        // Step 1 - Configure the catalog of modules

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(ModuleAModule));
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

        // Step 3 - Configure the RegionAdapters

        protected override Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
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
