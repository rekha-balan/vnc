using People.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

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

        //public void Initialize()
        //{
        //    _container.RegisterTypeForNavigation<ViewA>();
        //}

        public void Initialize()
        {
            RegisterViewsAndServices();

            var vm = this._container.Resolve<IPersonViewModel>();
            _regionManager.Regions[RegionNames.ContentRegion].Add(vm.View);
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IPersonViewModel, PersonViewModel>();
            _container.RegisterType<IPersonView, PersonView>();
        }
    }
}