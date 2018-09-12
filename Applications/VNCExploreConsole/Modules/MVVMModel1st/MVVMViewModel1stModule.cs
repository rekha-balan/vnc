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

namespace MVVMViewModel1st
{
    public class MVVMViewModel1stModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        // Need a container so we can register our views
        // and a RegionManager so we can compose views and perform View discovery.
        // Prism will pass in when this module is created.

        public MVVMViewModel1stModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _regionManager = manager;
        }

        public void Initialize()
        {
            // 1. Register Views and ViewModels

            // ViewModel first approach.

            // ViewModel is responsible for instantiating the View
            // When container creates ViewModel is sees a View is required in constructor.
            // Container resolves the View and passes it into the ViewModel's constructor.

            // Register a View as type IContentA_VM1_View.  Container will return ContentA_VM1
            // because ContentA_VM1 implements IContentA_VM1_View
            // Do same for IContentA_VM1_ViewViewModel, ContentA_VM1_ViewViewModel

            _container.RegisterType<ToolbarA>();
            _container.RegisterType<IContentA_VM1, ContentA_VM1>();
            _container.RegisterType<IContentA_VM1_ViewModel, ContentA_VM1_ViewModel>();

            // 2. Compose Application views using registered Views and ViewModels

            // Enable view discovery for toolbar
            // Not clear if need to RegisterType with container, supra, if using region manager

            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegionM1, typeof(ToolbarA));

            // Enable view discovery for content

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionM1, typeof(ContentA_VM1));

            // Problem here is we get the View but no ViewModel

            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentA_VM1));

            // Enable view Injection

            //var vm = _container.Resolve<IContentA_VM1_ViewModel>();
            ////vm.Message = "Prism Rocks!";

            ////Now inject in a region the view that viewmodel created. (16)
            //_regionManager.Regions[RegionNames.ContentRegionM1].Add(vm.View);

            //// If don't deactivate view the second view is covered up.

            //_regionManager.Regions[RegionNames.ContentRegionM1].Deactivate(vm.View);

            //var vm2 = _container.Resolve<IContentA_VM1_ViewModel>();
            //vm2.Message = "Prism Rocks! Second ViewModel";

            //_regionManager.Regions[RegionNames.ContentRegionM1].Add(vm2.View);

            // If you need more control of region,

            //try
            //{
            //    IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
            //    // Can get list of Views, ActiveViews, Activate, Deactivate, Add, Remove, Activate, Deactivate, etc.
            //    region.Add(vm2.View);
            //}
            //catch (Exception ex)
            //{
            //    // This gets thrown because we left the first view in place. (5)
            //    MessageBox.Show(ex.ToString());
            //}

            // TODO(crhodes)
            // Show deactivating or removing view

            // TODO(crhodes)
            // Play with switch views and the model associated with view
            //try
            //{
            //    //IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
            //    //// Can get list of Views, ActiveViews, Activate, Deactivate, Add, Remove, Activate, Deactivate, etc.
            //    ////region.Deactivate(vm.View); // Doing this still through exception
            //    ////region.Deactivate(vm2.View);
            //    //region.Activate(vm2.View);
            //}
            //catch (Exception ex)
            //{
            //    // This gets thrown because we left the first view in place. (5)
            //    MessageBox.Show(ex.ToString());
            //}
        }
    }
}
