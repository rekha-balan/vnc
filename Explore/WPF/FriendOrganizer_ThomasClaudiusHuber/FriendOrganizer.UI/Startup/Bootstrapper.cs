using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Practices.Unity;
using Prism.Modularity;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.Startup
{
    class Bootstrapper : UnityBootstrapper
    {
        //protected override void ConfigureModuleCatalog()
        //{
        //    var moduleCatalog = (ModuleCatalog)ModuleCatalog;
        //    moduleCatalog.AddModule(typeof(ModuleAModule));
        //}
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IFriendDataService, FriendDataService>();
        }

        //protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        //{
        //    RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
        //    mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
        //    return mappings;
        //}

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
