using LineStatusViewer.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Infrastructure;
using LineStatusViewer.Data;

namespace LineStatusViewer
{
    public class LineStatusViewerModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public LineStatusViewerModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusA, typeof(LineStatusView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusG, typeof(LineStatusGridView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusB, typeof(LineStatusNavigationView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusC, typeof(LineStatusDetailView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusA, typeof(LineStatusViewX));
        }

        void RegisterViewsAndServices()
        {
            _container.RegisterType<ILineStatusDataService, LineStatusDataService>();
        }
    }
}