using Infrastructure;
using PrismDemo.ViewModels;
using System.Windows;

namespace PrismDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowViewNavigation : Window
    {
        public MainWindowViewNavigation(IMainWindowViewNavigationViewModel viewModel)
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
