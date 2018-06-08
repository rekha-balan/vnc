using PrismDemo.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using ModuleM;
using Prism.Regions;
using System.Windows.Controls;
using Infrastructure;
using System;
using People;
using StatusBar;
using Toolbar;
using PluralsightPrismDemo.Services;
//using Unity.Container;
// PrismDemo_Communication
namespace PrismDemo
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog moduleCatalog = new ModuleCatalog();

            moduleCatalog.AddModule(typeof(ServicesModule));
            moduleCatalog.AddModule(typeof(ToolbarModule));
            moduleCatalog.AddModule(typeof(PeopleModule));
            moduleCatalog.AddModule(typeof(StatusBarModule));

            return moduleCatalog;
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
