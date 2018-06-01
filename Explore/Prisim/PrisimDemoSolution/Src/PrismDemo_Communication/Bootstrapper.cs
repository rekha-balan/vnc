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
//using Unity.Container;

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

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            catalog.AddModule(typeof(ToolbarModule));
            catalog.AddModule(typeof(PeopleModule));
            catalog.AddModule(typeof(StatusBarModule));

            return catalog;
        }
    }
}
