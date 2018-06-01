﻿using PrismDemo.Views;
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

namespace PrismDemo
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            //this is for the State-Based Navigation Demo
            //return Container.Resolve<MainWindow>();

            // This is for the View-Based Navigation Demo
            return Container.Resolve<MainWindowViewNavigation>();
        }

        protected override void InitializeShell()
        {
            // This is for the State-Based Navigation Demo
            //Application.Current.MainWindow.Show();

            // This is for the View-Based Navigation Demo
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IMainWindowViewNavigationViewModel, MainWindowViewNavigationViewModel>();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            return mappings;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            // This is for State-Based Navigation

            //catalog.AddModule(typeof(PersonServiceModule));
            //catalog.AddModule(typeof(ModuleASModule));

            // This is for View-Based Navigation

            catalog.AddModule(typeof(ModuleA1Module));
            catalog.AddModule(typeof(ModuleB1Module));

            return catalog;
        }
    }
}