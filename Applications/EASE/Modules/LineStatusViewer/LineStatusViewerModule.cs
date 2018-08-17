using LineStatusViewer.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Infrastructure;

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
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionLineStatus, typeof(LineStatusView));
        }
    }
}