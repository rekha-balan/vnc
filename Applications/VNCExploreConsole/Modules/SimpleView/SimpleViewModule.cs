using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Prism.Regions;
using Infrastructure;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;

namespace SimpleView
{
    public class SimpleViewModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        // Need a container so we can register our views
        // and a RegionManager so we can compose views and perform View discovery.
        // Prism will pass in when this module is created.

        public SimpleViewModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _regionManager = manager;
        }

        public void Initialize()
        {
            // 1. Register Views
            // Nothing happens other thn Unity knows about View

            //_container.RegisterType<ToolbarA>();  // If this is commented out, still works!
            //_container.RegisterType<ContentA>();  // If this is commented out, still works!

            // 2. Tell the Region where the View goes.
            // NB. This also registers the type

            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegionSV, typeof(ToolbarA));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionSV, typeof(ContentA));
        }
    }
}
