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

//using EaseCore;
using VNC;

namespace TestLoggingSimple
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string LOG_APPNAME = "SIMPLE";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogSomething(object sender, RoutedEventArgs e)
        {
            //Log.Info("LogSomething for Me", LOG_APPNAME, 0);
            AppLog.Info("AppLogSomething for Me", LOG_APPNAME, 0);
        }
    }
}
