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
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IFriendDataService, FriendDataService>();

            // TODO(crhodes)
            // Need to figure out how to get Unity to do this which AutoFac supports.
            // builder.RegisterType<FriendOrganizerDbContext>().AsSelf()

            Container.RegisterType<FriendOrganizerDbContext>();

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
