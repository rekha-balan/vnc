using Infrastructure;
using Infrastructure.Prism;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;

namespace PrismDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //public partial class MainWindowMS : Window, IView2
    //public partial class MainWindowMS : Window, IView
    //public partial class MainWindowMS : Window, IView
    //public partial class MainWindowMS : Window, IRegionManagerAware
    public partial class MainWindowMS : Window
    {
        public MainWindowMS()
        {
            InitializeComponent();
        }

        #region IRegionManagerAware

        // Example shows this implemented by ViewModel but
        // had to put it here

        //public IRegionManager RegionManager
        //{
        //    get;
        //    set;
        //}

        #endregion

        //public IViewModel ViewModel
        //{
        //    get;
        //    set;
        //}
        //IViewModel2 IView2.ViewModel
        //{
        //    get;
        //    set;
        //}
    }
}
