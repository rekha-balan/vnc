﻿using LineStatusViewer.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LineStatusViewer.Views
{

    public partial class LineStatusNavigationView : UserControl
    {
        private LineStatusNavigationViewModel _viewModel;

        //public LineStatusNavigationView()
        //{
        //}

        public LineStatusNavigationView(LineStatusNavigationViewModel viewModel)
        {
            try
            {
                InitializeComponent();

                _viewModel = viewModel;
                DataContext = _viewModel;
            }
            catch (Exception ex)
            {
                var foo = ex;
            }

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
