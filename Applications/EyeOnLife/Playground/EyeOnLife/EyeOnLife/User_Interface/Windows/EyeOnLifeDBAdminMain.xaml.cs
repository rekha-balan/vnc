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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for SQLMonitorDBAdminMain.xaml
    /// </summary>
    public partial class SQLMonitorDBAdminMain : DXWindow
    {
        public SQLMonitorDBAdminMain()
        {
            InitializeComponent();
            this.Width = SQLInformation.Data.Config.ScreenWidth_Admin;
            this.Height = SQLInformation.Data.Config.ScreenHeight_Admin;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
                //myCollectionViewSource.Source = Common.ApplicationDataSet.Servers;

                //Data.ApplicationDataSetTableAdapters.ServersTableAdapter serversTableAdapter;
                //Data.ApplicationDataSetTableAdapters.TableAdapterManager tableAdapterManager;
                //serversTableAdapter = new EyeOnLife.Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
                //tableAdapterManager = new EyeOnLife.Data.ApplicationDataSetTableAdapters.TableAdapterManager();

                //serversTableAdapter.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

                //try
                //{
                //    serversTableAdapter.Fill(Common.ApplicationDataSet.Servers);
                //    //serversBindingSource.DataSource = Common.ApplicationDS.Servers;
                //}
                //catch(Exception ex)
                //{
                //    System.Windows.Forms.MessageBox.Show(text: "Initial Load Error: " + ex.ToString(), caption: "Error", buttons: System.Windows.Forms.MessageBoxButtons.OK);
                //}
            }
        }
    }
}
