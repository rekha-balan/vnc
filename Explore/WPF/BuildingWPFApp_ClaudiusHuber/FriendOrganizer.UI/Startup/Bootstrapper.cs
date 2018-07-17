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
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.ViewModel;
using Prism.Events;

namespace FriendOrganizer.UI.Startup
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Register as a singleton
            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IFriendDataService, FriendDataService>();

            // TODO(crhodes)
            // Learn how to make this line work in Unity
            //Container.RegisterType<LookupDataService>().AsImplementedInterfaces();
            // For now just use the IFriendLookupDataService

            Container.RegisterType<IFriendLookupDataService, LookupDataService>();



            // TODO(crhodes)
            // Need to figure out how to get Unity to do this which AutoFac supports.
            // builder.RegisterType<FriendOrganizerDbContext>().AsSelf()

            Container.RegisterType<FriendOrganizerDbContext>();

            Container.RegisterType<INavigationViewModel, NavigationViewModel>();
            Container.RegisterType<IFriendDetailViewModel, FriendDetailViewModel>();
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
