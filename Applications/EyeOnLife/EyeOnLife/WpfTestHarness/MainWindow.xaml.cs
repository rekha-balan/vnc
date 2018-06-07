using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WpfTestHarness
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void OnAD1(object sender, RoutedEventArgs e)
        {
            var foo = ADHelper.ADHelper.CheckGroupMembership("crhodes", "EyeOnLife_Admins", "Life.PacificLife.Net");

            ADHelper.ADHelper.GetAuthorizationGroupsMembership("crhodes", "Life.PacificLife.Net");
        }

        private void OnAD2(object sender, RoutedEventArgs e)
        {
            var gps = ADHelper.ADHelper.GetGroupsMembership("crhodes", "Life.PacificLife.Net");

            Groups.ItemsSource = gps;

            //foreach (Principal p in groups)
            //{
            //    System.Diagnostics.Debug.WriteLine(p.Name);
            //}
        }

        private void OnAD3(object sender, RoutedEventArgs e)
        {
            var agps = ADHelper.ADHelper.GetAuthorizationGroupsMembership("crhodes", "Life.PacificLife.Net");

            AuthorizationGroups.ItemsSource = agps;

            //foreach (Principal p in groups)
            //{
            //    System.Diagnostics.Debug.WriteLine(p.Name);
            //}

        }
    }

}
