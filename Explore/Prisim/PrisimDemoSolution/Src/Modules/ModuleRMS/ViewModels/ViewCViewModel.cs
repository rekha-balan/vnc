using Infrastructure;
using Infrastructure.Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleRMS.ViewModels
{
    public class ViewCViewModel : BindableBase, IRegionManagerAware
    {
        public DelegateCommand NavigateCommand { get; set; }

        public ViewCViewModel()
        {
            NavigateCommand = new DelegateCommand(Navigate);
        }

        void Navigate()
        {
            RegionManager.RequestNavigate(KnownRegionNames.ContentRegion, "ViewA");
        }

        public IRegionManager RegionManager { get; set; }
    }
}
