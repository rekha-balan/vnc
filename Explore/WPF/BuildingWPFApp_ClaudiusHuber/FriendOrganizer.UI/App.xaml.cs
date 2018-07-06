using System.Windows;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModel;
using FriendOrganizer.UI.Startup;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
  {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Xaml knows how to call the default, parameterless constructor
            // We need to pass a ViewModel into the MainWindow
            // and the ViewModel needs a DataService.

            // Do the work ourselves then show the window.

            //var mainWindow = new MainWindow(
            //    new MainViewModel(
            //        new FriendDataService()));

            //mainWindow.Show();

            // Or be cool and use Prism

            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
