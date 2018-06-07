using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SQLInformation.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSQLMonitorAdminMain.xaml
    /// </summary>
    public partial class wucSQLMonitorAdminMain : UserControl
    {
        const string TYPE_NAME = "wucSQLMonitorAdminMain";

        public wucSQLMonitorAdminMain()
        {
            InitializeComponent();

        }

        private ucBase _currentControl;

        #region Event Handlers


        private void btnDatabases_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.User_Interface.User_Controls.wucAdmin_Databases uc = new User_Controls.wucAdmin_Databases();

            //uc.databasesDataGrid.ItemsSource = Common.ApplicationDataSet.Databases;
            ShowUserControl(uc);
            uc.UpdateLayout();
        }

        private void btnServers_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.User_Interface.User_Controls.wucAdmin_Servers uc = new User_Controls.wucAdmin_Servers();

            //uc.serversDataGrid.ItemsSource = Common.ApplicationDataSet.Servers;
            ShowUserControl(uc);
            uc.UpdateLayout();
        }

        private void UnhookTitleEvent(ucBase control)
        {
            if (control != null)
            {
                control.TitleChanged -= SetTitle;
            }
        }

        private void ShowUserControl(ucBase control)
        {
            //UnhookTitleEvent(_currentControl);
            dpUserControlContainer.Children.Clear();

            if (control != null)
            {
            	dpUserControlContainer.Children.Add(control);
                _currentControl = control;
            }

            //HookTitleEvent(_currentControl);
        }

        private void HookTitleEvent(ucBase control)
        {
            SetTitle(control, EventArgs.Empty);

            if (control != null)
            {
                control.TitleChanged += SetTitle;
            }

        }

        private void SetTitle(object sender, EventArgs e)
        {
            //ucBase uc = sender as ucBase;

            //if (uc != null && ! string.IsNullOrEmpty(uc.Title))
            //{
            //    this.Title = string.Format("{0}", uc.Title);
            //}
            //else
            //{
            //    this.Title = "main Form";
            //}
        }

        private void btnTables_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInstances_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.User_Interface.User_Controls.wucAdmin_Instances uc = new User_Controls.wucAdmin_Instances();

            //uc.instancesDataGrid.ItemsSource = Common.ApplicationDataSet.Instances;
            ShowUserControl(uc);
        }

        private void btnTypes_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            //if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            //{


            //    //Load your data here and assign the result to the CollectionViewSource.
            //    System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
            //    myCollectionViewSource.Source = Common.ApplicationDataSet.Servers;

            //    Data.ApplicationDataSetTableAdapters.ServersTableAdapter serversTableAdapter;
            //    Data.ApplicationDataSetTableAdapters.TableAdapterManager tableAdapterManager;
            //    serversTableAdapter = new SQLInformation.Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
            //    tableAdapterManager = new SQLInformation.Data.ApplicationDataSetTableAdapters.TableAdapterManager();

            //    serversTableAdapter.Connection.ConnectionString = ConfigData.SQLMonitorDBConnection;

            //    try
            //    {
            //        serversTableAdapter.Fill(Common.ApplicationDataSet.Servers);
            //        //serversBindingSource.DataSource = Common.ApplicationDS.Servers;
            //    }
            //    catch(Exception ex)
            //    {
            //        System.Windows.Forms.MessageBox.Show(text: "Initial Load Error: " + ex.ToString(), caption: "Error", buttons: System.Windows.Forms.MessageBoxButtons.OK);
            //    }
            //}
        }
    }
}
