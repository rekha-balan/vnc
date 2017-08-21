using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VNC.AssemblyHelper;

namespace TestAssemblyHelperWPF
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

        private void btnDisplayAssemblyInfo_Click(object sender, RoutedEventArgs e)
        {
            DisplayAssemblyInfo();
        }

        private void DisplayAssemblyInfo()
        {
            Assembly asmb = Assembly.GetExecutingAssembly();
            AssemblyInformation info = new AssemblyInformation(asmb);
            txtOutput.Text = txtOutput.Text + System.Environment.NewLine + info.ToString();
        }

        private void btnDomainReflection_Click(object sender, RoutedEventArgs e)
        {
            var assemblyPath = txtAssemblyPath.Text;
            var manager = new AssemblyReflectionManager();
            var success = manager.LoadAssembly(assemblyPath, "VNCReflectionDomain");

            var results = manager.GetTypeInformation(assemblyPath);

            txtOutput.Clear();

            foreach (TypeInformation info in results)
            {
                txtOutput.Text += (System.Environment.NewLine + info.FullName);
            }

            manager.UnloadAssembly(assemblyPath);
        }

        private void btnAssemblyReflection_Click(object sender, RoutedEventArgs e)
        {
            var assemblyPath = txtAssemblyPath.Text;
            var manager = new AssemblyReflectionManager();
            var success = manager.LoadAssembly(assemblyPath, "VNCReflectionDomain");

            var results = manager.Reflect(assemblyPath, (a) =>
            {
                var names = new List<String>();
                var types = a.GetTypes();

                foreach (var t in types)
                {
                    names.Add(t.Name);
                }

                return names;
            });

            txtOutput.Clear();

            foreach (var name in results)
            {
                txtOutput.Text += (System.Environment.NewLine + name.ToString());
            }

            manager.UnloadAssembly(assemblyPath);
        }
    }
}
