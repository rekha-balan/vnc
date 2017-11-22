﻿using System;
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

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using Microsoft.Office.Interop.Excel;

using Microsoft.SqlServer.Management.Common;
using SMO = Microsoft.SqlServer.Management.Smo;
using SMOWMI = Microsoft.SqlServer.Management.Smo.Wmi;

using SMOH = VNC.SMOHelper;
using XlHlp = VNC.AddinHelper.Excel;

using System.Text.RegularExpressions;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucExcel_FolderMap.xaml
    /// </summary>
    public partial class wucExcel_FolderMap : UserControl
    {
        #region Fields and Properties

        SMO.Server _SMOServer;      // This is the real one
        SMOH.Server _SMOHServer;    // This is the one that Hides the access restrictions
                                    // by catching not found exceptions

        #endregion

        #region Constructors and Load

        public wucExcel_FolderMap()
        {
            InitializeComponent();
            LoadControlContents();
        }

        private void LoadControlContents()
        {
            try
            {
                wucSQLInstance_Picker1.PopulateControlFromFile(Common.cCONFIG_FILE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            wucSQLInstance_Picker1.ControlChanged += WucSQLInstance_Picker1_ControlChanged;
            //wucTFSProvider_Picker.ControlChanged += tfsProvider_Picker1_ControlChanged;
        }

        private void WucSQLInstance_Picker1_ControlChanged()
        {
            VNC.AddinHelper.Common.WriteToDebugWindow("wucSQLInstance_Picker1.ControlChanged");
        }

        #endregion

        #region Event Handlers

        //private void btnCreateDatabaseInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();
        //        SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
        //        CreateWorksheet_DatabaseInfo(dataBase);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        XlHlp.ScreenUpdatesOn(true);
        //    }
        //}

        //private void btnCreateDatabaseInfoWorkSheets_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();
        //        CreateAllWorksheetsOf_DatabaseInfo(_SMOHServer);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        XlHlp.ScreenUpdatesOn(true);
        //    }
        //}

        //private void btnCreateInstanceInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();
        //        CreateWorksheet_InstanceInfo(_SMOHServer, (bool)ceListInstanceDetails.IsChecked);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        XlHlp.ScreenUpdatesOn(true);
        //    }
        //}

        //private void btnCreateStoredProcedureInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();
        //        SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
        //        SMOH.StoredProcedure storedProcedure = dataBase.StoredProcedures[cbeStoredProcedures.Text];
        //        CreateWorksheet_StoredProcedureInfo(storedProcedure);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        XlHlp.ScreenUpdatesOn(true);
        //    }
        //}

        //private void btnCreateStoredProcedureInfoWorkSheets_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();
        //        SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
        //        CreateAllWorksheeetsOf_StoredProcedureInfo(dataBase);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        XlHlp.ScreenUpdatesOn(true);
        //    }
        //}
        //private void btnCreateTableInfoMasterWorkSheet_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!ValidUISelections()) { return; }

        //    bool orientVertical = GetDisplayOrientation();

        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();
        //        SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
        //        CreateWorksheet_TableInfoMaster(orientVertical, dataBase);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        XlHlp.ScreenUpdatesOn(true);
        //    }
        //}

        //private void btnCreateTableInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();
        //        SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
        //        SMOH.Table table = dataBase.Tables[cbeTables.Text];
        //        CreateWorksheet_TableInfo(table);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        XlHlp.ScreenUpdatesOn(true);
        //    }
        //}

        //private void btnCreateTableInfoWorkSheets_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();
        //        SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
        //        CreateAllWorkSheetsOf_TableInfo(dataBase);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        XlHlp.ScreenUpdatesOn(true);
        //    }
        //}

        private void btnCreateViewInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Main Function Routines

        #region CreateWorksheet_*


        #endregion

        #region AddSection_*

        
        #endregion

        #region Display_*
        
        #endregion


        #endregion

        #region Utility Routines



        #endregion

        #region Private Methods

        private bool GetDisplayOrientation()
        {
            //return (bool)ceOrientOutputVertically.IsChecked;
            return true;
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateFolderMap_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
