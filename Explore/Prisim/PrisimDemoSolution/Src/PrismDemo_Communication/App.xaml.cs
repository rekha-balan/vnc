using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PrismDemo
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var bootstrapper = new Bootstrapper();
            var bootstrapper = new BootstrapperRegionContext();
            bootstrapper.Run();
        }
    }
}
