using System;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Modularity;
using System.Windows;
using PluralsightPrismDemo.People;
using PluralsightPrismDemo.StatusBar;
using PluralsightPrismDemo.Toolbar;
using PluralsightPrismDemo.Services;

namespace PluralsightPrismDemo
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            catalog.AddModule(typeof(ServicesModule));
            catalog.AddModule(typeof(ToolbarModule));
            catalog.AddModule(typeof(PeopleModule));
            catalog.AddModule(typeof(StatusBarModule));

            return catalog;
        }
    }
}
