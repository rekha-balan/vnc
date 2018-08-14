﻿using PeopleViewer.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Infrastructure;

namespace PeopleViewer
{
    public class PeopleViewerLooseCouplingModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public PeopleViewerLooseCouplingModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionZ, typeof(Views.PeopleViewer));
        }
    }
}