﻿//using People.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Infrastructure;
using ModuleInterfaces;

namespace PeopleDC
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

            // DelegateCommand Example
            // This is for single content not tab view

            var vm = _container.Resolve<IPersonViewModel>();
            _regionManager.Regions[RegionNames.ContentRegionC_DC].Add(vm.View);
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IPersonViewModel, PersonViewModel>();
            _container.RegisterType<IPerson, Person>();
        }
    }
}