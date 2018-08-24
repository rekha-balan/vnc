using FriendOrganizer.UI.ViewModel;
using System;
using System.Windows;

namespace FriendOrganizer.UI
{
    public partial class MainWindowNVDV : Window
    {
        public MainWindowNVDVViewModel _viewModel;

        public MainWindowNVDV(MainWindowNVDVViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        // Load ViewModel asynchronously

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    _viewModel.Load();
        //}
    }
}
