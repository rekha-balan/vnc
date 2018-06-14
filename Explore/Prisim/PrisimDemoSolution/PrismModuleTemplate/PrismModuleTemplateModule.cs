using PrismModuleTemplate.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace PrismModuleTemplate
{
    public class PrismModuleTemplateModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public PrismModuleTemplateModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<ViewA>();
        }
    }
}