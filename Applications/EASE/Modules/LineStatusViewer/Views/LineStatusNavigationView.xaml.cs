using LineStatusViewer.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace LineStatusViewer.Views
{

    public partial class LineStatusNavigationView : UserControl
    {
        private LineStatusViewModel _viewModel;

        public LineStatusNavigationView(LineStatusViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;

            Loaded += LineStatusView_Loaded;
        }

        //void LineStatusView_Loaded(object sender, RoutedEventArgs e)
        //{
        //    _viewModel.Load();
        //}

        async void LineStatusView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
