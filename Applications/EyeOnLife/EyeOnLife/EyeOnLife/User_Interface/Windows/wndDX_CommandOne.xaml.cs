using System;
using System.Collections.Generic;
using System.Data;
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

using DevExpress.Xpf.Core;
using System.Diagnostics;
using DevExpress.Xpf.Core.Commands;
//using DevExpress.Xpf.DemoBase;
using DevExpress.Utils;
using System.Collections;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for wndDX_Instance.xaml
    /// </summary>
    public partial class wndDX_CommandOne : DXWindow
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        DevExpress.Xpf.Grid.SelectedRowsCollection targets = null;
        private IList selectedRows;

        public wndDX_CommandOne()
        {
            InitializeComponent();
        }

        public wndDX_CommandOne(DevExpress.Xpf.Grid.SelectedRowsCollection selectedRows)
        {
            InitializeComponent();
            targets = selectedRows;
        }

        public wndDX_CommandOne(IList selectedRows)
        {
            this.selectedRows = selectedRows;
        }

        private void DXWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void OnCreateSMOLogin(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView s in targets)
            {
                string message = "";

                try
                {
                    SQLInformation.Data.ApplicationDataSet.InstancesRow instance =
                        (SQLInformation.Data.ApplicationDataSet.InstancesRow)s.Row;

                    ProgressResults.Items.Add(string.Format("Attempting Logon Creation on:{0}", instance.Name_Instance));
                   
                    SQLInformation.SMO.Actions.Instance.CreateSQLMonitorLogin(instance.Name_Instance, out message);

                    ProgressResults.Items.Add(string.Format("   Result:{0}", message));
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

    }
}
