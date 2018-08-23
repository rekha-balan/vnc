using LineStatusViewer.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace LineStatusViewer.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class LineStatusViewNVDV : UserControl
    {
        private LineStatusViewModel _viewModel;

        public LineStatusViewNVDV(LineStatusViewModel viewModel)
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
