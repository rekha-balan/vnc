using PeopleViewer.Presentation;
using PersonRepository.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PeopleViewer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ComposeObjects();
            Application.Current.MainWindow.Show();
        }

        private static void ComposeObjects()
        {
            var repository = new ServiceRepository();
            var viewModel = new PeopleViewerViewModel(repository);
            Application.Current.MainWindow = new PeopleViewerWindow(viewModel);
            Application.Current.MainWindow.Title = "Loose Coupling - People Viewer";
        }
    }
}
