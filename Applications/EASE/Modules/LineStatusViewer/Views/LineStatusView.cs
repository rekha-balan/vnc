using LineStatusViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LineStatusViewer.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class LineStatusView : UserControl
    {
        private LineStatusViewModel _viewModel;

        public LineStatusView(LineStatusViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            Loaded += LineStatusView_Loaded;
        }
        void LineStatusView_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Load();
        }
    }
}
