﻿using LineStatusViewer.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
