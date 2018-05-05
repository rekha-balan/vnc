using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Prism.Regions;
using PrismDemo.Infrastructure;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;

namespace ModuleM
{
    public class ModuleMModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleMModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _regionManager = manager;
        }

        //public ModuleMModule(IUnityContainer container)
        //{
        //    _container = container;
        //}

        // Register our Views with our container

        public void Initialize()
        {
            // Not clear if need to call this if using region manager, infra
            //_container.RegisterType<ToolbarA>();

            // ViewModel first
            _container.RegisterType<IContentAView, ContentA>();
            _container.RegisterType<IContentAViewViewModel, ContentAViewViewModel>();

            // View first approach
            _container.RegisterType<ContentA2>();
            _container.RegisterType<IContentAViewViewModel2, ContentAViewViewModel2>();

            // Enable view discovery for toolbar
            // Not clear if need to RegisterType with container, supra, if using region manager

            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ToolbarA));

            // Enable view discovery for content

            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentA));

            // Enable view Injection for 

            var vm = _container.Resolve<IContentAViewViewModel>();
            vm.Message = "Prism Rocks!";

            // Now injection in region.  Add view that view model created. (5)
            _regionManager.Regions[RegionNames.ContentRegion].Add(vm.View);

            var vm2 = _container.Resolve<IContentAViewViewModel>();
            vm.Message = "Prism Rocks! Second View";

            _regionManager.Regions[RegionNames.ContentRegion].Add(vm2.View);

            // Notice how the second view appears! on top of first.
            // TODO(crhodes)
            // Use region manager to verify there are two views.  Might only be one

            // If you need more control of region

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

            try
            {
                IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
                // Can get list of Views, ActiveViews, Activate, Deactivate, Add, Remove, Activate, Deactivate, etc.
                region.Deactivate(vm.View); // Doing this still through exception
                //region.Deactivate(vm2.View);
                region.Activate(vm2.View);
            }
            catch (Exception ex)
            {
                // This gets thrown because we left the first view in place. (5)
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
