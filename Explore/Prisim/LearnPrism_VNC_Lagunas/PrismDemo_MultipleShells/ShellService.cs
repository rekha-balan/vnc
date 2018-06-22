using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Infrastructure;
using Infrastructure.Prism;
using Prism.Regions;

namespace PrismDemo
{
    public class ShellService : IShellService
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        // Takes in a RegionManager

        public ShellService(IUnityContainer container, IRegionManager regionManager)
        {
            // To resolve actual instances
            _container = container;
            // To avoid duplication exceptions
            _regionManager = regionManager;
        }

        public void ShowShell(string uri)
        {
            //var shell = _container.Resolve<Views.MainWindow>();
            var shell = _container.Resolve<Views.MainWindowMS>();

            // Avoid duplicate registration exception, create a local region.

            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);

            // This makes the shell aware of the scoped region versions the global one.

            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            // This is for the initial View
            scopedRegion.RequestNavigate(KnownRegionNames.ContentRegion, uri);

            shell.Show();
        }
    }
}
