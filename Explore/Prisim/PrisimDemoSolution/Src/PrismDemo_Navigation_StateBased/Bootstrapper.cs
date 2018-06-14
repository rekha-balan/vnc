using PrismDemo.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using ModuleA1;
using ModuleB1;
using Prism.Regions;
using System.Windows.Controls;
using Infrastructure;
using System;
using PrismDemo.ViewModels;
using Services.PersonService;
using ModuleSBN;

namespace PrismDemo
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            catalog.AddModule(typeof(PersonServiceModule));
            catalog.AddModule(typeof(ModuleSBNModule));

            return catalog;
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }
    }
}
