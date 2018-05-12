using PrismDemo.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using ModuleAS;
using Prism.Regions;
using System.Windows.Controls;
using Infrastructure;
using System;
using People;
using StatusBar;
using Toolbar;
using Services.PersonService;

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

            catalog.AddModule(typeof(PersonServiceModule));
            catalog.AddModule(typeof(ModuleASModule));

            return catalog;
        }
    }
}
