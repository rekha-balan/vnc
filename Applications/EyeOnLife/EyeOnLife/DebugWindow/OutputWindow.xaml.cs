using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using XamlExtentionMethods;

namespace DebugHelp
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class OutputWindow : Window
    {
        double frequency = Stopwatch.Frequency;

        public OutputWindow()
        {
            InitializeComponent();
        }

        public long Write(string message)
        {
            this.tbOutput.AppendText(Environment.NewLine);
            this.tbOutput.AppendText(message);
            tbOutput.Refresh();

            return Stopwatch.GetTimestamp();
        }

        public long Write(string message, long startTime)
        {
            this.tbOutput.AppendText(Environment.NewLine);
            this.tbOutput.AppendText(string.Format("{0:F4}s : {1}", ((double)Stopwatch.GetTimestamp() - (double)startTime) / frequency, message));
            tbOutput.Refresh();

            return Stopwatch.GetTimestamp();
        }

        private void OnClear_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Clear();
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {

        }
    }
}
