using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Process_And_Threads
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var threads = currentProcess.Threads;

            var processID = currentProcess.Id;

            var currentThread = Thread.CurrentThread;
            var currentContext = Thread.CurrentContext;

            var appDomain = Thread.GetDomain();
            var appDomainID = Thread.GetDomainID();
        }
    }
}
