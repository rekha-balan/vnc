using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
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

using SMO = Microsoft.SqlServer.Management.Smo;
using SMOW = Microsoft.SqlServer.Management.Smo.Wmi;

using EyeOnLife;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucAdmin_Databases.xaml
    /// </summary>
    public partial class wucAdminDX_DBStoredProcedures : ucBase
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE_X;
        private const string PLLOG_APPNAME = "EyeOnLife";

        #region Initialization

        public wucAdminDX_DBStoredProcedures()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();

        }

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                System.Windows.Data.CollectionViewSource myCollectionViewSource1 = (System.Windows.Data.CollectionViewSource)this.Resources["dBStoredProceduresViewSource"];
                myCollectionViewSource1.Source = EyeOnLife.Common.ApplicationDataSet.DBStoredProcedures;
            }
        }

        #endregion

        #region Event Handlers

        #region DataGrid Handlers

        
        #endregion

        private void btnLoadExtendedProperties_Click(object sender, RoutedEventArgs e)
        {
            //SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow =
            //    (SQLInformation.Data.ApplicationDataSet.DatabasesRow)
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            //LoadExtendedProperties(databaseRow);
        }

        private void btnSaveExtendedProperties_Click(object sender, RoutedEventArgs e)
        {
            //SQLInformation.Data.ApplicationDataSet.DatabasesRow database =
            //    (SQLInformation.Data.ApplicationDataSet.DatabasesRow)
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            //SaveExtendedProperties(database);
        }

        private void canAddCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserAddRows = true;
            }
        }

        private void canAddCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserAddRows = false;
            }
        }

        private void canDeleteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserDeleteRows = true;
            }
        }

        private void canDeleteCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserDeleteRows = false;
            }
        }

        private void readOnlyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.IsReadOnly = true;
            }
        }

        private void readOnlyCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.IsReadOnly = false;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.DBStoredProceduresTA.Update(Common.ApplicationDataSet.DBStoredProcedures);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.DBStoredProcedures.RejectChanges();
        }

        #endregion

    }
}
