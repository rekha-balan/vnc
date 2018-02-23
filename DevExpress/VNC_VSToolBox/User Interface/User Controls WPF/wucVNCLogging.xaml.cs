using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Text.RegularExpressions;

namespace VNC_VSToolBox.User_Interface.User_Controls_WPF
{
    /// <summary>
    /// Interaction logic for wucVNCLogging.xaml
    /// </summary>
    public partial class wucVNCLogging : UserControl
    {
        #region Fields and Properties


        #endregion

        #region Constructors and Load

        public wucVNCLogging()
        {
            InitializeComponent();
            LoadControlContents();
        }

        private void LoadControlContents()
        {
            try
            {
                //wucSQLInstance_Picker1.PopulateControlFromFile(Common.cCONFIG_FILE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //wucSQLInstance_Picker1.ControlChanged += WucSQLInstance_Picker1_ControlChanged;
            //wucTFSProvider_Picker.ControlChanged += tfsProvider_Picker1_ControlChanged;
        }

        private void WucSQLInstance_Picker1_ControlChanged()
        {
            //VNC.AddinHelper.Common.WriteToDebugWindow("wucSQLInstance_Picker1.ControlChanged");
        }

        #endregion

        #region Event Handlers

        private void btnCreateDatabaseInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    XlHlp.ScreenUpdatesOff();
            //    SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
            //    CreateWorksheet_DatabaseInfo(dataBase);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            ////finally
            ////{
            ////    XlHlp.ScreenUpdatesOn(true);
            ////}
        }

        private void btnCreateDatabaseInfoWorkSheets_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    XlHlp.ScreenUpdatesOff();
            //    CreateAllWorksheetsOf_DatabaseInfo(_SMOHServer);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    XlHlp.ScreenUpdatesOn(true);
            //}
        }

        private void btnCreateInstanceInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    XlHlp.ScreenUpdatesOff();
            //    CreateWorksheet_InstanceInfo(_SMOHServer, (bool)ceListInstanceDetails.IsChecked);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    XlHlp.ScreenUpdatesOn(true);
            //}
        }

        private void btnCreateStoredProcedureInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    XlHlp.ScreenUpdatesOff();
            //    SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
            //    SMOH.StoredProcedure storedProcedure = dataBase.StoredProcedures[cbeStoredProcedures.Text];
            //    CreateWorksheet_StoredProcedureInfo(storedProcedure);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    XlHlp.ScreenUpdatesOn(true);
            //}
        }

        


        private void cbeDatabases_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            //cbeTables.Items.Clear();

            //foreach (string name in _SMOHServer.Databases[cbeDatabases.Text].Tables.Keys)
            //{
            //    cbeTables.Items.Add(name);
            //}

            //cbeViews.Items.Clear();

            //foreach (SMOH.View view in _SMOHServer.Databases[cbeDatabases.Text].Views.Values)
            //{
            //    if (view.IsSystemObject && !(bool)ceIncludeSystemStoredProcedures.IsChecked)
            //    {
            //        continue;
            //    }

            //    cbeViews.Items.Add(view.Name);
            //}

            //cbeStoredProcedures.Items.Clear();

            //foreach (SMOH.StoredProcedure sp in _SMOHServer.Databases[cbeDatabases.Text].StoredProcedures.Values)
            //{
            //    if (sp.IsSystemObject == "1" && !(bool)ceIncludeSystemStoredProcedures.IsChecked)
            //    {
            //        continue;
            //    }

            //    cbeStoredProcedures.Items.Add(sp.Name);
            //}
        }

        #endregion

        #region Main Function Routines

       

        #endregion

        #region Utility Routines



        #endregion

        #region Private Methods

        private bool GetDisplayOrientation()
        {
            return (bool)ceOrientOutputVertically.IsChecked;
        }

        private bool ValidUISelections()
        {
            //if (cbeTeamProjectCollections.SelectedText.Length > 0)
            //{
            return true;
            //}
            //else
            //{
            //    MessageBox.Show("Must Select Team Project Collection first", "UI Selection Incomplete");
            //    return false;
            //}
        }

        #endregion
    }
}
