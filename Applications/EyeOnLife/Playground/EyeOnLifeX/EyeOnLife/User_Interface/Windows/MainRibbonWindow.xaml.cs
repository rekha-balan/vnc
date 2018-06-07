using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Timers;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

//using IdentityMine.Avalon.Controls;
//using IdentityMine.Avalon.PatientSimulator;
//using PatientHelper;

using Microsoft.Windows.Controls.Ribbon;
using DevExpress.Xpf.Core;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for MainRibbonSkeleton.xaml
    /// </summary>
    public partial class MainRibbonWindow
    {
        const string TYPE_NAME = "MainRibbonWindow";

        MasterListManager MasterListManager;
        //LiveChartsManager liveChartsManager;
        //ChartRotatorManager chartRotatorManager;
        Detail3DManager Detail3DManager;
        //PowerChecker powerChecker;
        private ScrollViewer hiddenScrollViewer = null;
        private ScrollViewer visibleScrollViewer = null;

        public MainRibbonWindow()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            //this.Resources.MergedDictionaries.Add(Popular)
            InitializeComponent();
        }

        private void ShowInstances()
        {
            MessageBox.Show("ShowInstances");
        }

        private void ShowServers()
        {
            MessageBox.Show("ShowServers");
        }

        private void ShowServers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ShowServers");
        }

        private void ShowInstances(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ShowInstances");
        }

        private void ShowAbout(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ShowAbout");
        }

        private void AdminServers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AdminServers");
        }

        private void AdminInstances(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AdminInstances");
        }

        private void OnFileNewMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnFileOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnFileSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnAdminMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnFileExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClickOnCylon(object sender, RoutedEventArgs e)
        {
            Nullable<bool> dlgResult;
            AboutDialogWindow dlgWindow = new AboutDialogWindow();
            dlgWindow.Height = 315;
            dlgWindow.Width = 500;

            dlgWindow.InitializeComponent();

            dlgWindow.Owner = this;
            dlgWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            dlgResult = dlgWindow.ShowDialog();
        }

        //private void OnServerDetailButton(object sender, RoutedEventArgs e)
        //{
        //    Detail3DManager.ShowDetailBack();
        //    List3DOverlay.Visibility = Visibility.Visible;
        //}

        //private void Overview3D_FlipCompleted(object sender, EventArgs e)
        //{
        //    double angleTo = (double)Rotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateToProperty);
        //    double angleFrom = (double)Rotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty);
        //    Overview3D.Visibility = Visibility.Collapsed;
        //}

        //private void OnBackToServerOverview(object sender, RoutedEventArgs e)
        //{
        //    Detail3DManager.ShowDetailFront();
        //    List3DOverlay.Visibility = Visibility.Collapsed;
        //}

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                // Pretend we are in the EAC
                using(StreamReader reader = new StreamReader(@"Test Data\TestWindowConsoleParameter.xml"))
                {
                    ConfigData.RawXML = reader.ReadToEnd();
                }

                //Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
                //myCollectionViewSource.Source = Common.ApplicationDataSet.Servers;

                SQLInformation.Data.ApplicationDataSetTableAdapters.ServersTableAdapter serversTableAdapter;
                SQLInformation.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter instancesTableAdapter;
                SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter databasesTableAdapter;

                SQLInformation.Data.ApplicationDataSetTableAdapters.TableAdapterManager tableAdapterManager;

                serversTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
                instancesTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.InstancesTableAdapter();
                databasesTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.DatabasesTableAdapter();

                tableAdapterManager = new SQLInformation.Data.ApplicationDataSetTableAdapters.TableAdapterManager();

                // Must have to do more work before can do this.  Connection is null
                //tableAdapterManager.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;\

                // DO this instead for now.
                serversTableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
                instancesTableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;
                databasesTableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

                try
                {
                    serversTableAdapter.Fill(Common.ApplicationDataSet.Servers);
                    instancesTableAdapter.Fill(Common.ApplicationDataSet.Instances);
                    databasesTableAdapter.Fill(Common.ApplicationDataSet.Databases);
                    //serversListView.ItemsSource = Common.ApplicationDataSet.Servers;
                    //serversBindingSource.DataSource = Common.ApplicationDS.Servers;
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(text: "Initial Load Error: " + ex.ToString(), caption: "Error", buttons: System.Windows.Forms.MessageBoxButtons.OK);
                }
            }
        }

        private void OnWindowIntialized(object sender, EventArgs e)
        {
            Detail3DManager = new Detail3DManager(this);
            //powerChecker = new PowerChecker();
        }

        private void OnDisplayDatabases_Click(object sender, RoutedEventArgs e)
        {
            wucServers_Main.Visibility = System.Windows.Visibility.Collapsed;
            wucInstances_Main.Visibility = System.Windows.Visibility.Collapsed;
            //wucDatabases_Main.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnAdministration_Click(object sender, RoutedEventArgs e)
        {
            EyeOnLife.User_Interface.Windows.SQLMonitorDBAdminMain win1 = new SQLMonitorDBAdminMain();
            win1.Show();
        }

        private void btnServersGroup1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnServersGroup1_Click");
        }

        private void btnServersGroup2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnServersGroup2_Click");
        }

        private void btnServersGroup3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnServersGroup3_Click");
        }

        private void btnInstancesGroup1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnInstancesGroup1_Click");
        }

        private void btnInstancesGroup2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnInstancesGroup2_Click");
        }

        private void btnInstancesGroup3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("btnInstancesGroup3_Click");
        }

        private void OnDisplayServers_Click(object sender, RoutedEventArgs e)
        {
            wucServers_Main.Visibility = System.Windows.Visibility.Visible;
            wucInstances_Main.Visibility = System.Windows.Visibility.Collapsed;
            //wucDatabases_Main.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OnDisplayInstances_Click(object sender, RoutedEventArgs e)
        {
            wucServers_Main.Visibility = System.Windows.Visibility.Collapsed;
            wucInstances_Main.Visibility = System.Windows.Visibility.Visible;
            //wucDatabases_Main.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
