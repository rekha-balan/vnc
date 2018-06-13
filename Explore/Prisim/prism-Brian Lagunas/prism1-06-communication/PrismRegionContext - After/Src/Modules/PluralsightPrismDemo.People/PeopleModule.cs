using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PluralsightPrismDemo.Infrastructure;

namespace PluralsightPrismDemo.People
{
    public class PeopleModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public PeopleModule(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            var vm = this._container.Resolve<IPeopleViewModel>();
            _regionManager.Regions[RegionNames.ContentRegion].Add(vm.View);

            _regionManager.RegisterViewWithRegion("PersonDetailsRegion", typeof(PersonDetailsView));
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IPeopleViewModel, PeopleViewModel>();
            _container.RegisterType<IPeopleView, PeopleView>();
            _container.RegisterType<IPersonDetailsView, PersonDetailsView>();
            _container.RegisterType<IPersonDetailsViewModel, PersonDetailsViewModel>();
        }
    }
}
