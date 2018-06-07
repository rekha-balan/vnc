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

using PacificLife.Life;

using SMO = Microsoft.SqlServer.Management.Smo;
using SMOW = Microsoft.SqlServer.Management.Smo.Wmi;

//using SMOH = SMOHelper;

using EyeOnLife;

namespace EyeOnLife.User_Interface.User_Controls
{

    /// <summary>
    /// Interaction logic for wucAdmin_DBDataFiles.xaml
    /// </summary>
    public partial class wucAdmin_DBDataFiles : ucBase
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE_X;
        private const string PLLOG_APPNAME = "SQLINFOAGENT";
        const string TYPE_NAME = "wucAdmin_DBDataFiles";

        #region Initialization

        private void ucBase_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["dBDataFilesViewSource"];
                myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.DBDataFiles;
            }
        }

        public wucAdmin_DBDataFiles()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();

        }

        #endregion

        #region Event Handlers

        private void btnDoX_Click(object sender, RoutedEventArgs e)
        {

        }

        private void canAddCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.CanUserAddRows = true;
            //}
        }

        private void canAddCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.CanUserAddRows = false;
            //}
        }

        private void canDeleteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.CanUserDeleteRows = true;
            //}
        }

        private void canDeleteCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //    if (dataGrid != null)
            //    {
            //        dataGrid.CanUserDeleteRows = false;
            //    }
        }

        private void readOnlyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.IsReadOnly = true;
            //}
        }

        private void readOnlyCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    dataGrid.IsReadOnly = false;
            //}
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SQLInformation.Data.ApplicationDataSetTableAdapters.DBDataFilesTableAdapter ta = new SQLInformation.Data.ApplicationDataSetTableAdapters.DBDataFilesTableAdapter();

            ta.Connection.ConnectionString = SQLInformation.Data.Config.SQLMonitorDBConnection;

            ta.Update(Common.ApplicationDataSet.DBDataFiles);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            //HIS.Library.Common.HISSchema.CancelEdit();
        }

        #region DataGrid Handlers

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _editMode = true;
        }

        private void dataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Delete &&
            //  _editMode == false &&
            //  dataGrid.CanUserDeleteRows == true)
            //{
            //    if (MessageBox.Show("Do you want to delete this Attribute?", "Attributes", MessageBoxButton.YesNo) ==
            //      MessageBoxResult.Yes)
            //    {
            //        Attributes.Remove((HIS.Library.AttributeEC)dataGrid.SelectedItem);
            //    }
            //    else
            //    {
            //        e.Handled = true;
            //    }
            //}
        }
        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var snd = sender;

            DataGridRow gridRow = e.Row;
            _editMode = false;
        }

        #endregion

        #endregion

        #region Main Function Routines


        #endregion

    }
}
