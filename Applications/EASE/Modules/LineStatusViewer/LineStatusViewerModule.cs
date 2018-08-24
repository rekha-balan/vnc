using LineStatusViewer.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Infrastructure;
using LineStatusViewer.Data;
using LineStatusViewer.ViewModels;

namespace LineStatusViewer
{
    public class LineStatusViewerModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public LineStatusViewerModule(IUnityContainer container, IRegionManager regionManager)
        {
            try
            {
                _container = container;
                _regionManager = regionManager;
            }
            catch (Exception ex)
            {
                var foo = ex;
            }
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusV, typeof(LineStatusView));

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusG, typeof(LineStatusGridView));

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusNVDV, typeof(LineStatusNVDVView));

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusNV, typeof(LineStatusNavigationView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusDV, typeof(LineStatusDetailView));

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusNV2, typeof(LineStatusNavigationView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatusDV2, typeof(LineStatusDetailView));
        }

        void RegisterViewsAndServices()
        {
            _container.RegisterType<ILineStatusDataService, LineStatusDataService>();

            // TODO(crhodes)
            // Learn how to do the Autofac .AsImplementedInterfaces();

            _container.RegisterType<ILookupBuildsService, LookupDataService>();


            _container.RegisterType<ILineStatusNavigationViewModel, LineStatusNavigationViewModel>();
            _container.RegisterType<ILineStatusDetailViewModel, LineStatusDetailViewModel>();
        }
    }
}