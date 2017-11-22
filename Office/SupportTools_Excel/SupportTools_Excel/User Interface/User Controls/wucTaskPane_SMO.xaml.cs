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
    /// Interaction logic for wucTaskPane_SMO.xaml
    /// </summary>
    public partial class wucTaskPane_SMO : UserControl
    {
        #region Fields and Properties

        SMO.Server _SMOServer;      // This is the real one
        SMOH.Server _SMOHServer;    // This is the one that Hides the access restrictions
                                    // by catching not found exceptions

        char[] splitSemicolonChar = { ';' };
        char[] splitPipeChar = { '|' };
        char[] splitSpaceChar = { ' ' };

        #endregion

        #region Constructors and Load

        public wucTaskPane_SMO()
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
            wucSQLInstance_Picker1.ControlChanged += wucSQLInstance_Picker1_ControlChanged;
        }

        private void wucSQLInstance_Picker1_ControlChanged()
        {
            VNC.AddinHelper.Common.WriteToDebugWindow("wucSQLInstance_Picker1.ControlChanged");
            // TODO(crhodes)
            // Need to rebind the combobox
        }

        #endregion

        #region Event Handlers

        private void btnCreateDatabaseInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XlHlp.ScreenUpdatesOff();

                foreach (string databaseName in cbeDatabases.Text.Split(splitSemicolonChar, StringSplitOptions.None))
                {
                    SMOH.Database dataBase = _SMOHServer.Databases[databaseName];
                    CreateWorksheet_DatabaseInfo(dataBase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

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

        private void btnCreateInstanceInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XlHlp.ScreenUpdatesOff();
                CreateWorksheet_InstanceInfo(_SMOHServer, (bool)ceListInstanceDetails.IsChecked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private void btnCreateStoredProcedureInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XlHlp.ScreenUpdatesOff();
                //SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
                //SMOH.StoredProcedure storedProcedure = dataBase.StoredProcedures[cbeStoredProcedures.Text];
                //CreateWorksheet_StoredProcedureInfo(storedProcedure);

                foreach (string databaseName in cbeDatabases.Text.Split(splitSemicolonChar, StringSplitOptions.None))
                {
                    SMOH.Database database = _SMOHServer.Databases[databaseName];
                    SMOH.StoredProcedure storedProcedure = database.StoredProcedures[cbeStoredProcedures.Text];
                    CreateWorksheet_StoredProcedureInfo(storedProcedure);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        //private void btnCreateStoredProcedureInfoWorkSheets_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();
        //        //SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
        //        //CreateAllWorksheeetsOf_StoredProcedureInfo(dataBase);

        //        foreach (string databaseName in cbeDatabases.Text.Split(splitSemicolonChar, StringSplitOptions.None))
        //        {
        //            SMOH.Database database = _SMOHServer.Databases[databaseName];
        //            CreateAllWorksheeetsOf_StoredProcedureInfo(database);
        //        }
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
        private void btnCreateTableInfoMasterWorkSheet_Click(object sender, RoutedEventArgs e)
        {
            if(! ValidUISelections()) { return; }

            bool orientVertical = GetDisplayOrientation();

            try
            {
                XlHlp.ScreenUpdatesOff();
                
                // This method knows how to handle multiple names, e.g. AML;EASE;FOO;BAR
                CreateWorksheet_TableInfoMaster(cbeDatabases.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        private void btnCreateTableInfoWorkSheet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XlHlp.ScreenUpdatesOff();

                //SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
                //SMOH.Table table = dataBase.Tables[cbeTables.Text];
                //CreateWorksheet_TableInfo(table);

                foreach (string databaseName in cbeDatabases.Text.Split(splitSemicolonChar, StringSplitOptions.None))
                {
                    SMOH.Database database = _SMOHServer.Databases[databaseName];

                    foreach (string tableName in cbeTables.Text.Split(splitSemicolonChar, StringSplitOptions.None))
                    {
                        string[] values = tableName.Split(splitSpaceChar, StringSplitOptions.None);

                        if (databaseName == values[1])
                        {
                            SMOH.Table table = database.Tables[values[0]];
                            CreateWorksheet_TableInfo(table, database.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                XlHlp.ScreenUpdatesOn(true);
            }
        }

        //private void btnCreateTableInfoWorkSheets_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        XlHlp.ScreenUpdatesOff();

        //        //SMOH.Database dataBase = _SMOHServer.Databases[cbeDatabases.Text];
        //        //CreateAllWorkSheetsOf_TableInfo(dataBase);

        //        foreach (string databaseName in cbeDatabases.Text.Split(splitSemicolonChar, StringSplitOptions.None))
        //        {
        //            SMOH.Database database = _SMOHServer.Databases[databaseName];

        //            CreateAllWorkSheetsOf_TableInfo(database);
        //        }
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

        private void btnLoadDatabaseContentComboBoxes_Click(object sender, RoutedEventArgs e)
        {
            foreach (string databaseName in cbeDatabases.Text.Split(splitSemicolonChar, StringSplitOptions.None))
            {
                if (databaseName == "")
                {
                    return;
                }

                SMOH.Database dataBase = _SMOHServer.Databases[databaseName];

                if ((bool)ceIncludeDBTables.IsChecked)
                {
                    UpdateDBTablesComboBox(databaseName);
                }

                if ((bool)ceIncludeDBViews.IsChecked)
                {
                    UpdateDBViewsComboBox(databaseName);
                }

                if ((bool)ceIncludeDBStoredProcedures.IsChecked)
                {
                    UpdateDBStoredProceduresComboBox(databaseName);
                }
            }

            // We may have added more than one database into the combobox
            // Sort so the Table Names are next to each other.

            List<string> items = new List<string>();

            foreach (var item in cbeTables.Items)
            {
                items.Add(item.ToString());
            }

            items.Sort();

            cbeTables.ItemsSource = items;
        }

        private void btnLogoff_Click(object sender, RoutedEventArgs e)
        {
            Logoff();
            //btnLogon.Enabled = true;
            //btnLogon.BackColor = SystemColors.Control;
            //btnLogoff.Enabled = false;
            //btnLogoff.BackColor = SystemColors.Control;
            //lblInstancName.Text = "";
            //gbInstanceOperations.Visible = false;
        }

        private void btnLogon_Click(object sender, RoutedEventArgs e)
        {
            if (Logon() == true)
            {
                //btnLogoff.Enabled = true;
                //btnLogoff.BackColor = Color.Green;
                //btnLogon.Enabled = false;
                //btnLogon.BackColor = Color.Green;
                //lblInstancName.Text = ucDBInstanceList.InstanceName;
                //gbInstanceOperations.Visible = true;
            }
        }


        private void cbeDatabases_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            //foreach (string databaseName in cbeDatabases.Text.Split(splitSemicolonChar, StringSplitOptions.None))
            //{
            //    if (databaseName == "")
            //    {
            //        return;
            //    }

            //    SMOH.Database dataBase = _SMOHServer.Databases[databaseName];

            //    if ((bool)ceIncludeDBTables.IsChecked)
            //    {
            //        UpdateDBTablesComboBox(databaseName);
            //    }

            //    if ((bool)ceIncludeDBViews.IsChecked)
            //    {
            //        UpdateDBViewsComboBox(databaseName);
            //    }

            //    if ((bool)ceIncludeDBStoredProcedures.IsChecked)
            //    {
            //        UpdateDBStoredProceduresComboBox(databaseName);
            //    }
            //}

            //List<string> items = new List<string>();

            //foreach (var item in cbeTables.Items)
            //{
            //    items.Add(item.ToString());
            //}

            //items.Sort();

            //cbeTables.ItemsSource = items;
        
        }

        //private static List<RoleNameID> Sort(List<RoleNameID> list)
        //{
        //    return list.OrderBy(role => role.Name).ToList();
        //    DevExpress.Xpf.Editors.ListItemCollection items = cbeTables.Items;
        //}

        private void UpdateDBStoredProceduresComboBox(string databaseName)
        {
            XlHlp.DisplayInWatchWindow(string.Format("  {0}", "Adding StoredProcedures to combobox ..."));

            // NB.  We need to use the StoredProcedure.Values here as we want to be able to filter out
            // System Stored Procedures

            foreach (SMOH.StoredProcedure sp in _SMOHServer.Databases[databaseName].StoredProcedures.Values)
            {
                if (sp.IsSystemObject == "1" && !(bool)ceIncludeSystemStoredProcedures.IsChecked)
                {
                    continue;
                }

                cbeStoredProcedures.Items.Add(string.Format("{0} {1}", sp.Name, databaseName));
                XlHlp.DisplayInWatchWindow(string.Format("  - {0}", sp.Name));
            }
        }

        private void UpdateDBTablesComboBox(string databaseName)
        {
            XlHlp.DisplayInWatchWindow(string.Format("  {0}", "Adding Tables to combobox ..."));

            foreach (string tableName in _SMOHServer.Databases[databaseName].Tables.Keys)
            {
                cbeTables.Items.Add(string.Format("{0} {1}", tableName, databaseName));
                XlHlp.DisplayInWatchWindow(string.Format("  - {0}", tableName));
            }
        }

        private void UpdateDBViewsComboBox(string databaseName)
        {
            XlHlp.DisplayInWatchWindow(string.Format("  {0}", "Adding Views to combobox ..."));

            // NB.  We need to use the Views.Values here as we want to be able to filter out
            // System Views

            foreach (SMOH.View view in _SMOHServer.Databases[databaseName].Views.Values)
            {
                if (view.IsSystemObject && !(bool)ceIncludeSystemStoredProcedures.IsChecked)
                {
                    continue;
                }

                cbeViews.Items.Add(string.Format("{0} {1}", view.Name, databaseName));
                XlHlp.DisplayInWatchWindow(string.Format("  - {0}", view.Name));
            }
        }

        #endregion

        #region Main Function Routines

        #region CreateWorksheet_*

        private void CreateAllWorksheetsOf_DatabaseInfo(SMOH.Server serverInstance)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            foreach (SMOH.Database database in serverInstance.Databases.Values)
            {
                try
                {
                    CreateWorksheet_DatabaseInfo(database);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void CreateAllWorksheeetsOf_StoredProcedureInfo(SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            foreach (SMOH.StoredProcedure storedProcedure in dataBase.StoredProcedures.Values)
            {
                try
                {
                    CreateWorksheet_StoredProcedureInfo(storedProcedure);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void CreateAllWorkSheetsOf_TableInfo(SMOH.Database database)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            foreach (SMOH.Table table in database.Tables.Values)
            {
                try
                {
                    CreateWorksheet_TableInfo(table, database.Name);
                    XlHlp.DisplayInWatchWindow(string.Format("  - Table >{0}<", table));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void CreateAllWorksheetsOf_ViewInfo(SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            foreach (SMOH.View view in dataBase.Views.Values)
            {
                try
                {
                    CreateWorksheet_ViewInfo(view);
                    XlHlp.DisplayInWatchWindow(string.Format("  - View >{0}<", view));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void CreateWorksheet_DatabaseInfo(SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            long startTicks = Common.WriteToDebugWindow("CreateDatabaseInfoWorkSheet(Start)");

            string sheetName = XlHlp.SafeSheetName("D>" + dataBase.Name);
            Worksheet ws = XlHlp.NewWorksheet(sheetName, afterSheetName: "LAST");

            // Output starts here.  Each Display method returns the output end point.

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: true);

            insertAt = AddSection_DatabaseInfo(insertAt, dataBase);

            insertAt = AddSection_ExtendedPropertyInfo(insertAt, dataBase.ExtendedProperties);

            insertAt = AddSection_FileGroupInfo(insertAt, dataBase);

            if ((bool)ceIncludeDBTables.IsChecked)
            {
                insertAt.IncrementRows(2);
                insertAt = AddSection_TableInfo(insertAt, dataBase);
            }

            if ((bool)ceIncludeDBViews.IsChecked)
            {
                insertAt.IncrementRows(2);
                insertAt = AddSection_ViewInfo(insertAt, dataBase);
            }

            if ((bool)ceIncludeDBStoredProcedures.IsChecked)
            {
                insertAt.IncrementRows(2);
                insertAt = AddSection_StoredProcedure(insertAt, dataBase);
            }

            Common.WriteToDebugWindow("CreateDatabaseInfoWorkSheet(Start)", startTicks);
        }

        private void CreateWorksheet_InstanceInfo(SMOH.Server serverInstance, bool showDetails)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            long startTicks = Common.WriteToDebugWindow("CreateInstanceInfoWorksheet(Start)");

            string sheetName = XlHlp.SafeSheetName("I>" + serverInstance.Name);
            Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

            // Output starts here.  Each Display method returns the output end point.

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: true);

            insertAt = AddSection_InstanceInfo(insertAt, serverInstance, showDetails);

            insertAt.IncrementRows(2);

            insertAt = AddSection_Databases(insertAt, serverInstance);

            insertAt.IncrementRows(2);

            insertAt = AddSection_Logins(insertAt, serverInstance);

            insertAt.IncrementRows(2);

            insertAt = AddSection_ServerRoles(insertAt, serverInstance);

            insertAt.IncrementRows(2);

            insertAt = AddSection_LinkedServers(insertAt, serverInstance);

            insertAt.IncrementRows(2);

            insertAt = AddSection_EndPoints(insertAt, serverInstance);

            insertAt.IncrementRows(2);

            Common.WriteToDebugWindow("CreateInstanceInfoWorksheet(End)", startTicks);
        }

        private void CreateWorksheet_StoredProcedureInfo(SMOH.StoredProcedure storedProcedure)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            long startTicks = Common.WriteToDebugWindow("CreateStoredProcedureInfoWorksheet(Start)");

            string sheetName = XlHlp.SafeSheetName("S>" + storedProcedure.Name);
            Worksheet ws = XlHlp.NewWorksheet(sheetName, afterSheetName: "LAST");
            int fontSize = 8;

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: true);

            XlHlp.AddTitledInfo(insertAt.AddRow(), "As of:", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            XlHlp.AddTitledInfo(insertAt.AddRow(), "DB Name:", storedProcedure.Name);

            XlHlp.AddContentToCell(insertAt.AddRow(), "Parameters", 14, XlHlp.MakeBold.Yes);

            insertAt.ClearOffsets();

            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 25, "Name", 12);
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "DataType", 12);
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Maximum\nLength", 12);
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Numeric\nPrecision", 12);
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Numeric\nScale", 12);
            XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Default\nValue", 12);

            insertAt = DisplayListOf_StoredProcedureParameters(insertAt, storedProcedure);

            insertAt.IncrementRows();

            insertAt.ClearOffsets();

            XlHlp.AddTitledInfo(insertAt.AddOffsetColumn(), "Header:", "");
            XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), storedProcedure.TextHeader, fontSize);

            insertAt.ClearOffsets();

            XlHlp.AddTitledInfo(insertAt.AddOffsetColumn(), "Body:", "");
            XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), storedProcedure.TextBody, fontSize);

            Common.WriteToDebugWindow("CreateStoredProcedureInfoWorksheet(End)", startTicks);
        }

        private void CreateWorksheet_TableInfo(SMOH.Table table, string databaseName)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            long startTicks = Common.WriteToDebugWindow("CreateTableInfoWorkSheet(Start)");

            // This exceeds the Max Worksheet name when we prepend databaseName :(

            //string sheetName = XlHlp.SafeSheetName(string.Format("{0} T>{1}", databaseName, table.Name));
            string sheetName = XlHlp.SafeSheetName("T>" + table.Name);
            Worksheet ws = XlHlp.NewWorksheet(sheetName, afterSheetName: "LAST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: true);

            XlHlp.AddTitledInfo(insertAt.AddRow(), "As of:", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            XlHlp.AddTitledInfo(insertAt.AddRow(), "Name:", table.Name);

            insertAt = AddSection_ExtendedPropertyInfo(insertAt, table.ExtendedProperties);

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            AddSection_ColumnInfo(insertAt, table);

            //insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tbl_{0}", table.Name));

            insertAt.Group(insertAt.OrientVertical, hide: true);

            insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);

            Common.WriteToDebugWindow("CreateTableInfoWorkSheet(End)", startTicks);
        }

        private void CreateWorksheet_TableInfoMaster(string databaseNames)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0} ({1})",
                System.Reflection.MethodInfo.GetCurrentMethod().Name, databaseNames));

            try
            {


                string sheetName = XlHlp.SafeSheetName(string.Format("{0}{1}", "MS>", "NeedGoodName"));
                Worksheet ws = XlHlp.NewWorksheet(sheetName, beforeSheetName: "FIRST");

                XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: true);

                XlHlp.AddTitledInfo(insertAt.AddRow(), "Table Column Information for ", databaseNames);

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "Database");
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "Table");
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "Column");
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "DataType");
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "MaximumLength");
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "NumericPrecision");
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "NumericScale");
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "IsPrimaryKey");
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "IsForeignKey");
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), "Nullable");

                insertAt.IncrementRows();

                if (0 == cbeTables.Text.Length)
                {
                    insertAt.ClearOffsets();
                    XlHlp.DisplayInWatchWindow(string.Format("No tables selected, aborting."));
                    XlHlp.AddContentToCell(insertAt.GetCurrentRange(), "No table selected, aborting.  Check cbeTables");

                    return;
                }

                foreach (string databaseName in cbeDatabases.Text.Split(splitSemicolonChar, StringSplitOptions.None))
                {
                    SMOH.Database database = _SMOHServer.Databases[databaseName];

                    foreach (string tableName in cbeTables.Text.Split(splitSemicolonChar, StringSplitOptions.None))
                    {
                        // TODO(crhodes)
                        // Need to make this smart enough to split out db;tablename

                        string[] values = tableName.Split(splitSpaceChar, StringSplitOptions.None);

                        if (databaseName == values[1])
                        {
                            insertAt = AddSection_Table_Column_Info(insertAt, database, values[0]);
                        }
                    }
                }

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblMasterInfo_{0}", databaseNames.Replace(";", "")));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.EndSectionAndSetNextLocation(insertAt.OrientVertical);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CreateWorksheet_ViewInfo(SMOH.View view)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            long startTicks = Common.WriteToDebugWindow("CreateViewInfoWorkSheet(Start)");

            string sheetName = XlHlp.SafeSheetName("V>" + view.Name);
            Worksheet ws = XlHlp.NewWorksheet(sheetName, afterSheetName: "LAST");

            XlHlp.XlLocation insertAt = new XlHlp.XlLocation(ws, row: 5, column: 1, orientVertical: true);

            insertAt.MarkStart(XlHlp.MarkType.GroupTable);

            XlHlp.AddTitledInfo(insertAt.AddRow(), "As of:", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            XlHlp.AddTitledInfo(insertAt.AddRow(), "Name:", view.Name);

            insertAt = AddSection_ExtendedPropertyInfo(insertAt, view.ExtendedProperties);

            AddSection_ColumnInfo(insertAt, view);

            Common.WriteToDebugWindow("CreateViewInfoWorkSheet(End)", startTicks);
        }
   
        #endregion

        #region AddSection_*
        
        private XlHlp.XlLocation AddSection_ColumnInfo(XlHlp.XlLocation insertAt, SMOH.Table table)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Columns", 14);

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20f, "Name", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 7.8f, "DataType", 10);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 7.8f, "Maximum\nLength", 10);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 7f, "Numeric\nPrecision", 10);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 7f, "Numeric\nScale", 10);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 7f, "Default", 10);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 4f, "ID", 10);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 8f, "Identity", 10);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 11f, "Is\nPrimaryKey", 10);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 11f, "Is\nForeignKey", 10);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 7f, "Nullable", 10);

                insertAt.IncrementRows();

                insertAt = DisplayListOf_Columns(insertAt, table);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblTableColumns_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_ColumnInfo(XlHlp.XlLocation insertAt, SMOH.View view)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Columns", 14);

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 35, "Name", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "DataType", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Maximum\nLength", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Numeric\nPrecision", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Numeric\nScale", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Default", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 5, "ID", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 12, "Identity", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 12, "Is\nPrimaryKey", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 12, "Is\nForeignKey", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 12, "Nullable", 12);

                insertAt.IncrementRows();

                insertAt = DisplayListOf_Columns(insertAt, view);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblViewColummns_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_DatabaseInfo(XlHlp.XlLocation insertAt, SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "As of:", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DB Name:", dataBase.Name);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Instance:", dataBase.Parent);

                XlHlp.AddTitledInfo(insertAt.AddRow(), "ActiveConnections:", dataBase.ActiveConnections);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AnsiNullDefault:", dataBase.AnsiNullDefault);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AnsiNullsEnabled:", dataBase.AnsiNullsEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AnsiPaddingEnabled:", dataBase.AnsiPaddingEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AnsiWarningsEnabled:", dataBase.AnsiWarningsEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AutoClose:", dataBase.AutoClose);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AutoCreateStatisticsEnabled:", dataBase.AutoCreateStatisticsEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AutoShrink:", dataBase.AutoShrink);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AutoUpdateStatisticsAsync:", dataBase.AutoUpdateStatisticsAsync);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "AutoUpdateStatisticsEnabled:", dataBase.AutoUpdateStatisticsEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "BrokerEnabled:", dataBase.BrokerEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "CaseSensitive:", dataBase.CaseSensitive);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ChangeTrackingAutoCleanUp:", dataBase.ChangeTrackingAutoCleanUp);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ChangeTrackingEnabled:", dataBase.ChangeTrackingEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ChangeTrackingRetentionPeriod:", dataBase.ChangeTrackingRetentionPeriod);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ChangeTrackingRetentionPeriodUnits:", dataBase.ChangeTrackingRetentionPeriodUnits);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "CloseCursorsOnCommitEnabled:", dataBase.CloseCursorsOnCommitEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Collation:", dataBase.Collation);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "CompatibilityLevel:", dataBase.CompatibilityLevel);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ConcatenateNullYieldsNull:", dataBase.ConcatenateNullYieldsNull);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "CreateDate:", dataBase.CreateDate);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DatabaseGuid:", dataBase.DatabaseGuid);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DatabaseSnapshotBaseName:", dataBase.DatabaseSnapshotBaseName);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DataSpaceUsage:", dataBase.DataSpaceUsage);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DateCorrelationOptimization:", dataBase.DateCorrelationOptimization);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DboLogin:", dataBase.DboLogin);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DefaultFileGroup:", dataBase.DefaultFileGroup);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DefaultFileStreamFileGroup:", dataBase.DefaultFileStreamFileGroup);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DefaultFullTextCatalog:", dataBase.DefaultFullTextCatalog);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "DefaultSchema:", dataBase.DefaultSchema);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "HonorBrokerPriority:", dataBase.HonorBrokerPriority);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ID:", dataBase.ID);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IndexSpaceUsage:", dataBase.IndexSpaceUsage);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsAccessible:", dataBase.IsAccessible);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDatabaseSnapshot:", dataBase.IsDatabaseSnapshot);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDatabaseSnapshotBase:", dataBase.IsDatabaseSnapshotBase);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDbAccessAdmin:", dataBase.IsDbAccessAdmin);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDbBackupOperator:", dataBase.IsDbBackupOperator);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDbDatareader:", dataBase.IsDbDatareader);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDbDatawriter:", dataBase.IsDbDatawriter);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDbDdlAdmin:", dataBase.IsDbDdlAdmin);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDbDenyDatareader:", dataBase.IsDbDenyDatareader);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDbDenyDatawriter:", dataBase.IsDbDenyDatawriter);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDbOwner:", dataBase.IsDbOwner);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsDbSecurityAdmin:", dataBase.IsDbSecurityAdmin);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsFullTextEnabled:", dataBase.IsFullTextEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsMailHost:", dataBase.IsMailHost);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsManagementDataWarehouse:", dataBase.IsManagementDataWarehouse);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsMirroringEnabled:", dataBase.IsMirroringEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsParameterizationForced:", dataBase.IsParameterizationForced);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsReadCommittedSnapshotOn:", dataBase.IsReadCommittedSnapshotOn);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsSystemObject:", dataBase.IsSystemObject);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsUpdateable:", dataBase.IsUpdateable);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "IsVarDecimalStorageFormatEnabled:", dataBase.IsVarDecimalStorageFormatEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "LastBackupDate:", dataBase.LastBackupDate);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "LastDifferentialBackupDate:", dataBase.LastDifferentialBackupDate);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "LastLogBackupDate:", dataBase.LastLogBackupDate);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "LocalCursorsDefault:", dataBase.LocalCursorsDefault);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "LogReuseWaitStatus:", dataBase.LogReuseWaitStatus);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringFailoverLogSequenceNumber:", dataBase.MirroringFailoverLogSequenceNumber);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringID:", dataBase.MirroringID);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringPartner:", dataBase.IsParameterizationForced);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringRedoQueueMaxSize:", dataBase.MirroringRedoQueueMaxSize);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringRoleSequence:", dataBase.MirroringRoleSequence);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringSafetyLevel:", dataBase.MirroringSafetyLevel);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringSafetySequence:", dataBase.MirroringSafetySequence);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringStatus:", dataBase.MirroringStatus);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringTimeout:", dataBase.MirroringTimeout);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "MirroringWitness:", dataBase.MirroringWitness);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "NumericRoundAbortEnabled:", dataBase.NumericRoundAbortEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Owner:", dataBase.Owner);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "PageVerify:", dataBase.PageVerify);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "PrimaryFilePath:", dataBase.PrimaryFilePath);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "QuotedIdentifiersEnabled:", dataBase.QuotedIdentifiersEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ReadOnly:", dataBase.ReadOnly);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "RecoveryForkGuid:", dataBase.RecoveryForkGuid);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "RecoveryModel:", dataBase.RecoveryModel);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "RecursiveTriggersEnabled:", dataBase.RecursiveTriggersEnabled);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Size:", dataBase.Size);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "SnapshotIsolationState:", dataBase.SnapshotIsolationState);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "SpaceAvailable:", dataBase.SpaceAvailable);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "State:", dataBase.State);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Status:", dataBase.Status);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "UserName:", dataBase.UserName);
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Version:", dataBase.Version);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_Databases(XlHlp.XlLocation insertAt, SMOH.Server serverInstance)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Databases", 14, XlHlp.MakeBold.Yes);

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 25, "Name", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 20, "Owner", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "Create\nDate", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "Size", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "DataSpace\nUsage", 12);

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "Tables", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "Views", 12);
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "StoredProcedures", 12);
                //XlHlp.AddColumnToSheet(ref ws, col++, 5,  row, "ID");
                //XlHlp.AddColumnToSheet(ref ws, col++, 35, row, "DatabaseGuid");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_DataBases(insertAt, serverInstance);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblDatabases_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }


        private XlHlp.XlLocation AddSection_DataFileInfo(XlHlp.XlLocation insertAt, SMOH.FileGroup fileGroup)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "DataFiles", 14, XlHlp.MakeBold.Yes);
                insertAt.IncrementRows();

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "FileName");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "AvailableSpace");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "Growth");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "GrowthType");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "MaxSize");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 13, "NumberOf\nDiskReads");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 13, "NumberOf\nDiskWrites");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Size");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "State");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 11, "UsedSpace");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Volume\nFreeSpace");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_DataFiles(insertAt, fileGroup);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblDataFile_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_EndPoints(XlHlp.XlLocation insertAt, SMOH.Server server)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Endpoints", 14, XlHlp.MakeBold.Yes);
                insertAt.IncrementRows();

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "EndpointState");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "EndpointType");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "ID");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "IsAdminEndpoint");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "IsSystemObject");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 13, "Owner");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 13, "Protocol");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "ProtocolType");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Urn");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_Endpoints(insertAt, server);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblEndPoints_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }
        private XlHlp.XlLocation AddSection_ExtendedPropertyInfo(XlHlp.XlLocation insertAt, SMO.ExtendedPropertyCollection extendedProperties)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "ExtendedProperties:", extendedProperties.Count.ToString());

                insertAt.IncrementRows();

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Value");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_ExtendedProperties(insertAt, extendedProperties);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblExtendedPropertyInfo_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_FileGroupInfo(XlHlp.XlLocation insertAt, SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "FileGroups:", dataBase.FileGroups.Count.ToString());

                //XlHlp.AddContentToCell(ws.Cells[currentRow++, 1], "FileGroups", 14, XlHlp.MakeBold.Yes);
                insertAt.IncrementRows();

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "IsDefault");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_FileGroups(insertAt, dataBase);

                //insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblFileGroupInfo_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_InstanceInfo(XlHlp.XlLocation insertAt, SMOH.Server serverInstance, bool showDetail)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "As of:", DateTime.Now.ToString());
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Instance Name:", serverInstance.Name);

                if (showDetail)
                {
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Backup\nDirectory:", serverInstance.BackupDirectory);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Browser\nService Account:", serverInstance.BrowserServiceAccount);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Browser\nStartMode:", serverInstance.BrowserStartMode);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Build\nClrVersion:", serverInstance.BuildClrVersionString);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "BuildNumber:", serverInstance.BuildNumber);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Collation:", serverInstance.Collation);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "ComparisonStyle:", serverInstance.ComparisonStyle);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "ComputerNamePhysicalNetBIOS:", serverInstance.ComputerNamePhysicalNetBIOS);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "DefaultFile:", serverInstance.DefaultFile);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "DefaultLog:", serverInstance.DefaultLog);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Edition:", serverInstance.Edition);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "ErrorLogPath:", serverInstance.ErrorLogPath);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "FilestreamLevel:", serverInstance.FilestreamLevel);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "FilestreamShareName:", serverInstance.FilestreamShareName);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "InstallDataDirectory:", serverInstance.InstallDataDirectory);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "InstallSharedDirectory:", serverInstance.InstallSharedDirectory);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "InstanceName:", serverInstance.InstanceName);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "IsCaseSensitive:", serverInstance.IsCaseSensitive);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "IsClustered:", serverInstance.IsClustered);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "IsFullTextInstalled:", serverInstance.IsFullTextInstalled);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "IsSingleUser:", serverInstance.IsSingleUser);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "LoginMode:", serverInstance.LoginMode);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "MasterDBPath:", serverInstance.MasterDBPath);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "MasterDBLogPath:", serverInstance.MasterDBLogPath);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "MaxPrecision:", serverInstance.MaxPrecision);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "NamedPipesEnabled:", serverInstance.NamedPipesEnabled);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "NetName:", serverInstance.NetName);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "NumberOfLogFiles:", serverInstance.NumberOfLogFiles);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "OSVersion:", serverInstance.OSVersion);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "PerfMonMode:", serverInstance.PerfMonMode);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "PhysicalMemory:", serverInstance.PhysicalMemory);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "PhysicalMemoryUsageInKB:", serverInstance.PhysicalMemoryUsageInKB);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Platform:", serverInstance.Platform);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Processors:", serverInstance.Processors);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "ProcessorUsage:", serverInstance.ProcessorUsage);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Product:", serverInstance.Product);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "ProductLevel:", serverInstance.ProductLevel);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "ResourceVersion:", serverInstance.ResourceVersionString);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Root\nDirectory:", serverInstance.RootDirectory);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "ServerType:", serverInstance.ServerType);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Service\nAccount:", serverInstance.ServiceAccount);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "ServiceInstanceId:", serverInstance.ServiceInstanceId);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "ServiceName:", serverInstance.ServiceName);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Service\nStartMode:", serverInstance.ServiceStartMode);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "SqlCharSet\nName:", serverInstance.SqlCharSetName);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "SqlDomain\nGroup:", serverInstance.SqlDomainGroup);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "SqlSortOrder\nName:", serverInstance.SqlSortOrderName);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Status:", serverInstance.Status);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "TcpEnabled:", serverInstance.TcpEnabled);
                    XlHlp.AddTitledInfo(insertAt.AddRow(), "Version:", serverInstance.VersionString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_LinkedServers(XlHlp.XlLocation insertAt, SMOH.Server serverInstance)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "LinkedServers", 14, XlHlp.MakeBold.Yes);

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 25, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 25, "Catalog");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_LinkedServers(insertAt, serverInstance);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblLinkedServers_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_Logins(XlHlp.XlLocation insertAt, SMOH.Server serverInstance)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
               System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "Logins", 14, XlHlp.MakeBold.Yes);

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 25, "Name");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_Logins(insertAt, serverInstance);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblLogins_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_ServerRoles(XlHlp.XlLocation insertAt, SMOH.Server serverInstance)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddContentToCell(insertAt.AddRow(), "ServerRoles", 14, XlHlp.MakeBold.Yes);

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 25, "Name");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_ServerRoles(insertAt, serverInstance);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblServerRoles_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_StoredProcedure(XlHlp.XlLocation insertAt, SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "StoredProcedures:", dataBase.StoredProcedures.Count.ToString());

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Owner");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "Create\nDate");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 13, "Date\nLastModified");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 11, "ID");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 12, "MethodName");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_StoredProcedures(insertAt, dataBase);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblStoredProcedure_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_Table_Column_Info(XlHlp.XlLocation insertAt, VNC.SMOHelper.Database database, string tableName)
        {
            XlHlp.DisplayInWatchWindow(string.Format("Database >{0}< Adding table >{1}< info ...", database.Name, tableName));

            try
            {
                SMOH.Table table = database.Tables[tableName];

                foreach (SMOH.Column column in table.Columns.Values)
                {
                    insertAt.ClearOffsets();

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), database.Name);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), table.Name);

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Name);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.DataType);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.MaximumLength);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.NumericPrecision);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.NumericScale);
                    //XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], column.Default);
                    //XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], column.ID);
                    //XlHlp.AddContentToCell(rng.Offset[rowsAdded, col++], column.Identity);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.InPrimaryKey);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.IsForeignKey);
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Nullable);

                    insertAt.IncrementRows();
                }

                //insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_TableInfo(XlHlp.XlLocation insertAt, SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Tables:", dataBase.Tables.Count.ToString());

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Owner");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "CreateDate");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 13, "Date\nLastModified");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 11, "DataSpace\nUsed"); ;
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 11, "ID");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 11, "RowCount");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_Tables(insertAt, dataBase);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblTableInfo_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }

        private XlHlp.XlLocation AddSection_ViewInfo(XlHlp.XlLocation insertAt, SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            try
            {
                XlHlp.AddTitledInfo(insertAt.AddRow(), "Views:", dataBase.Views.Count.ToString());

                insertAt.MarkStart(XlHlp.MarkType.GroupTable);

                insertAt.ClearOffsets();

                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 45, "Name");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "Owner");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 15, "CreateDate");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 13, "Date\nLastModified");
                XlHlp.AddColumnHeaderToSheet(insertAt.AddOffsetColumn(), 10, "ID");

                insertAt.IncrementRows();

                insertAt = DisplayListOf_Views(insertAt, dataBase);

                insertAt.MarkEnd(XlHlp.MarkType.GroupTable, string.Format("tblViewInfo_{0}", insertAt.workSheet.Name));

                insertAt.Group(insertAt.OrientVertical, hide: true);

                insertAt.IncrementRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return insertAt;
        }
        #endregion

        #region Display_*

        private XlHlp.XlLocation DisplayListOf_Columns(XlHlp.XlLocation insertAt, SMOH.Table table)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            foreach (SMOH.Column column in table.Columns.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.DataType);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.MaximumLength);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.NumericPrecision);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.NumericScale);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Default);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.ID);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Identity);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.InPrimaryKey);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.IsForeignKey);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Nullable);

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_Columns(XlHlp.XlLocation insertAt, SMOH.View view)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            foreach (SMOH.Column column in view.Columns.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.DataType);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.MaximumLength);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.NumericPrecision);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.NumericScale);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Default);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.ID);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Identity);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.InPrimaryKey);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.IsForeignKey);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), column.Nullable);

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_DataBases(XlHlp.XlLocation insertAt, SMOH.Server serverInstance)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateInstanceInfoWorksheet()

            foreach (SMOH.Database dataBase in serverInstance.Databases.Values)
            {
                XlHlp.DisplayInWatchWindow(string.Format("Adding Database Info for ({0})", dataBase.Name));
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataBase.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataBase.Owner);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataBase.CreateDate);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataBase.Size);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataBase.DataSpaceUsage);

                // These can throw exceptions if not access

                try
                {
                    // This is quick but returns 0 for counts

                    SMO.Database db = new SMO.Database(_SMOServer, dataBase.Name);
                    db.Refresh();

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), db.Tables.Count.ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), db.Views.Count.ToString());
                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), db.StoredProcedures.Count.ToString());

                    Int64 count = 0;

                    foreach (var item in db.Tables)
                    {
                        count++;
                    }

                    XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), count.ToString());

                    //XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataBase.Tables.Count().ToString());
                    //XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataBase.Views.Count().ToString());
                    //XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataBase.StoredProcedures.Count().ToString());
                }
                catch (Exception ex)
                {
                    // Quietly ignore
                }

                //XlHlp.AddContentToCell(rng.Offset[row, col++], dataBase.ID.ToString());
                //XlHlp.AddContentToCell(rng.Offset[row, col++], dataBase.DatabaseGuid.ToString());

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_DataFiles(XlHlp.XlLocation insertAt, SMOH.FileGroup fileGroup)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateInstanceInfoWorksheet()

            foreach (SMOH.DataFile dataFile in fileGroup.DataFiles.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.FileName);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.AvailableSpace);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.Growth);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.GrowthType);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.MaxSize);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.NumberOfDiskReads);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.NumberOfDiskWrites);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.Size);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.State);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.UsedSpace);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), dataFile.VolumeFreeSpace);

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_Endpoints(XlHlp.XlLocation insertAt, SMOH.Server server)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateInstanceInfoWorksheet()

            foreach (SMOH.Endpoint endPoint in server.Endpoints.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.EndpointState);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.EndpointType);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.ID);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.IsAdminEndpoint);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.IsSystemObject);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.Owner);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.Protocol);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.ProtocolType);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), endPoint.Urn);

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_ExtendedProperties(XlHlp.XlLocation insertAt, SMO.ExtendedPropertyCollection extendedProperties)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateInstanceInfoWorksheet()

            foreach (SMO.ExtendedProperty extendedProperty in extendedProperties)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), extendedProperty.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), extendedProperty.Value.ToString());

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_FileGroups(XlHlp.XlLocation insertAt, SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateInstanceInfoWorksheet()

            foreach (SMOH.FileGroup fileGroup in dataBase.FileGroups.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), fileGroup.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), fileGroup.IsDefault);

                insertAt.IncrementRows();

                insertAt = AddSection_DataFileInfo(insertAt, fileGroup);
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_LinkedServers(XlHlp.XlLocation insertAt, SMOH.Server serverInstance)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateInstanceInfoWorksheet()

            foreach (SMOH.LinkedServer linkedServer in serverInstance.LinkedServers.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), linkedServer.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), linkedServer.Catalog);

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_Logins(XlHlp.XlLocation insertAt, SMOH.Server serverInstance)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateInstanceInfoWorksheet()

            foreach (SMOH.Login login in serverInstance.Logins.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), login.Name);

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_ServerRoles(XlHlp.XlLocation insertAt, SMOH.Server serverInstance)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateInstanceInfoWorksheet()

            foreach (SMOH.ServerRole serverRole in serverInstance.ServerRoles.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), serverRole.Name);

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_StoredProcedureParameters(XlHlp.XlLocation insertAt, SMOH.StoredProcedure storedProcedure)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            foreach (SMOH.StoredProcedureParameter parameter in storedProcedure.Parameters.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), parameter.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), parameter.DataType);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), parameter.MaximumLength);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), parameter.NumericPrecision);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), parameter.NumericScale);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), parameter.DefaultValue);

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_StoredProcedures(XlHlp.XlLocation insertAt, SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            foreach (SMOH.StoredProcedure storedProcedure in dataBase.StoredProcedures.Values)
            {
                if (storedProcedure.IsSystemObject == "1" && ! (bool) ceIncludeSystemStoredProcedures.IsChecked)
                {
                    continue;
                }

                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), storedProcedure.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), storedProcedure.Owner);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), storedProcedure.CreateDate);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), storedProcedure.DateLastModified);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), storedProcedure.ID);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), storedProcedure.MethodName);

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_Tables(XlHlp.XlLocation insertAt, SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateDatabaseInfoWorkSheet()

            foreach (SMOH.Table table in dataBase.Tables.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), table.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), table.Owner);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), table.CreateDate);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), table.DateLastModified);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), table.DataSpaceUsed);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), table.ID);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), table.RowCount.ToString());

                insertAt.IncrementRows();
            }

            return insertAt;
        }

        private XlHlp.XlLocation DisplayListOf_Views(XlHlp.XlLocation insertAt, SMOH.Database dataBase)
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));

            // The columns in this method need to be kept in sync with CreateDatabaseInfoWorkSheet()

            foreach (SMOH.View view in dataBase.Views.Values)
            {
                insertAt.ClearOffsets();

                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), view.Name);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), view.Owner);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), view.CreateDate);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), view.DateLastModified);
                XlHlp.AddContentToCell(insertAt.AddOffsetColumn(), view.ID.ToString());

                insertAt.IncrementRows();
            }

            return insertAt;
        }
        
        #endregion

        private void Logoff()
        {
            if (_SMOServer != null)
            {
                _SMOServer.ConnectionContext.Disconnect();

                // May want to keep these around
                _SMOHServer = null;
            }

            cbeDatabases.Items.Clear();
            cbeTables.Items.Clear();
            cbeViews.Items.Clear();
            cbeStoredProcedures.Items.Clear();
        }

        private bool Logon()
        {
            XlHlp.DisplayInWatchWindow(string.Format("{0}",
                System.Reflection.MethodInfo.GetCurrentMethod().Name));
            bool result = false;

            _SMOServer = new SMO.Server(wucSQLInstance_Picker1.FullName);

            if (true == ceIntegratedSecurity.IsChecked)
            {
                _SMOServer.ConnectionContext.LoginSecure = true;
            }
            else
            {
                _SMOServer.ConnectionContext.LoginSecure = false;
                //_SMOServer.ConnectionContext.Login = txtUserName.Text;
                _SMOServer.ConnectionContext.Login = teUserName.Text;
                _SMOServer.ConnectionContext.Password = tePassword.Text;
            }

            try
            {
                // Don't have to explicitly connect.  Connection is established when needed and pooling is used.
                // If connect, no pooling.

                //_SMOServer.ConnectionContext.Connect();

                // Load the SMOHelper Server object with information.
                // This allows us to not worry about access privileges in this code.
                // Any values will be available or marked as "<No Access>"

                XlHlp.DisplayInWatchWindow(string.Format("  {0}", "Initializing new SMOHServer ..."));

                _SMOHServer = new SMOH.Server(_SMOServer);

                int count;

                cbeDatabases.Items.Clear();

                if (wucSQLInstance_Picker1.Databases != null)
                {
                    // Limit the databases to the ones in the XML.
                    // NB. We have to populate the Databases Property on SMOH.Server ourselves.

                    //count = wucSQLInstance_Picker1.Databases.Count;


                    //XlHlp.DisplayInWatchWindow(string.Format("  {0} ({1}) {2}", "Adding", count, "Databases to combobox ..."));

                    //_SMOHServer.Databases = new Dictionary<string, SMOH.Database>();

                    //count = _SMOHServer.Databases.Keys.Count;

                    foreach (var name in wucSQLInstance_Picker1.Databases)
                    {
                        XlHlp.DisplayInWatchWindow(string.Format("  - {0}", name));

                        //SMO.Database realDatabase = new SMO.Database(_SMOServer, name);
                        //SMOH.Database database = new SMOH.Database(realDatabase);
                        //_SMOHServer.Databases.Add(name, database);

                        cbeDatabases.Items.Add(name);
                    }
                }
                else
                {
                    // Get the list of databases from the Instance that was selected.
                    count = _SMOHServer.Databases.Keys.Count;

                    XlHlp.DisplayInWatchWindow(string.Format("  {0} ({1}) {2}", "Adding", count, "Databases to combobox ..."));

                    foreach (string name in _SMOHServer.Databases.Keys)
                    {
                        cbeDatabases.Items.Add(name);
                        XlHlp.DisplayInWatchWindow(string.Format("  - {0}", name));
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return result;
        }

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
