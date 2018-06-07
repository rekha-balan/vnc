//using People.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Infrastructure;

namespace People
{
    public class PeopleModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public PeopleModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            // This is for single content not tab view

            //var vm = _container.Resolve<IPersonViewModel>();
            //_regionManager.Regions[RegionNames.ContentRegion].Add(vm.View);

            // This is for TabControl multiple view 

            IRegion region = _regionManager.Regions[RegionNames.ContentRegion];

            var vm = _container.Resolve<IPersonViewModel>();
            vm.CreatePerson("Bob", "Smith");

            region.Add(vm.View);
            region.Activate(vm.View);

            var vm2 = _container.Resolve<IPersonViewModel>();
            vm2.CreatePerson("Karl", "Sums");
            region.Add(vm2.View);

            var vm3 = _container.Resolve<IPersonViewModel>();
            vm3.CreatePerson("Jeff", "Lock");
            region.Add(vm3.View);

            //_regionManager.RegisterViewWithRegion("PersonDetailsRegion", typeof(PersonDetailsView));
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IPersonViewModel, PersonViewModel>();
            _container.RegisterType<IPersonView, PersonView>();

            //_container.RegisterType<IPeopleViewModel, PeopleViewModel>();
            //_container.RegisterType<IPeopleView, PeopleView>();
            //_container.RegisterType<IPersonDetailsView, PersonDetailsView>();
            //_container.RegisterType<IPersonDetailsViewModel, PersonDetailsViewModel>();
        }
    }
}