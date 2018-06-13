using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using PluralsightPrismDemo.Infrastructure;

namespace ModuleB
{
    /// <summary>
    /// Interaction logic for ViewB.xaml
    /// </summary>
    public partial class ViewB : UserControl, IView
    {
        public ViewB(IViewBViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
