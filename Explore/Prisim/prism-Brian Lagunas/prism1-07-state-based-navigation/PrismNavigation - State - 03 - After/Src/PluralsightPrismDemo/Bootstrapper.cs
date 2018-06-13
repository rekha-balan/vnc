using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Modularity;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;
using PluralsightPrismDemo.Infrastructure;
using ModuleA;
using Services.PersonService;

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

            catalog.AddModule(typeof(PersonServiceModule));
            catalog.AddModule(typeof(ModuleAModule));

            return catalog;
        }
    }
}
