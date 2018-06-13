using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using PluralsightPrismDemo.Infrastructure;
using System.Text.RegularExpressions;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _manager;

        public ModuleAModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _manager = manager;
        }

        public void Initialize()
        {
            _container.RegisterType<ToolbarA>();
            _container.RegisterType<IContentAView, ContentA>();
            _container.RegisterType<IContentAViewViewModel, ContentAViewViewModel>();

            _manager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ToolbarA));

            var vm = _container.Resolve<IContentAViewViewModel>();
            vm.Message = "First View";

            IRegion region = _manager.Regions[RegionNames.ContentRegion];
            region.Add(vm.View);

            var vm2 = _container.Resolve<IContentAViewViewModel>();
            vm2.Message = "Second View";

            region.Deactivate(vm.View);
            region.Add(vm2.View);
        }
    }
}
