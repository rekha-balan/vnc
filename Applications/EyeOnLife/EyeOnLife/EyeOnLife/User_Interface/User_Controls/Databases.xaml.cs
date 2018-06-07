using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using EyeOnLife.User_Interface.Windows;

using VNC;
using SQLInformation;

using DevExpress.Data.Filtering;

using MSMO = Microsoft.SqlServer.Management.Smo;
using MSMOA = Microsoft.SqlServer.Management.Smo.Agent;

namespace EyeOnLife.User_Interface.User_Controls
{
    public partial class Databases : wucDX_Base
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        #region Constructors

        public Databases()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
        }

        #endregion

        private System.Windows.Controls.ContentControl _DisplayOptions;

        public System.Windows.Controls.ContentControl DisplayOptions
        {
            get
            {
                if (_DisplayOptions == null)
                {
                    // cc1 is just admin stuff
                    // cc2 is Database stuff.
                    _DisplayOptions = new EyeOnLife.User_Interface.Content_Controls.cc1();
                }

                return _DisplayOptions;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _DisplayOptions = value;
                cc_DisplayOptions.Content = _DisplayOptions;
            }
        }

        #region Initialization

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Start", LOG_APPNAME);
#endif
            EyeOnLife.User_Interface.Helper.ValidateDataFullyLoaded();

            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    dataGrid.ItemsSource = EyeOnLife.Common.ApplicationDataSet.Databases;

                    StaticResourceExtension sr = new StaticResourceExtension();
                    sr.ResourceKey = "DisplayOptionsControls";
                    //System.Windows.Controls.ControlTemplate ct = new System.Windows.Controls.ControlTemplate();
                    //ct.Template = sr.ProvideValue();

                    //System.Windows.Controls.ContentControl cc = new EyeOnLife.User_Interface.Content_Controls.cc1();

                    //cc_DisplayOptions.Template = (System.Windows.Controls.ControlTemplate)sr.ProvideValue("DisplayOptionsControls");
                    cc_DisplayOptions.Content = DisplayOptions;
                    //cc_DisplayOptions.Content = cc;

                }

                ApplyDataFilters();

                ViewMode.ApplyAuthorization(this);

                // This list needs to match the CheckBox names in Display_StylesAndTemplates.xml
                string[] ckDisplayColumns = null;
                //string[] ckDisplayColumns = { "ckDisplayEnvironmentColumns", "ckDisplayNotesColumns", "ckDisplaySizeColumns" };

                // This list needs to match the CheckBox names in Display_StylesAndTemplates.xml
                ViewMode.UpdateDisplayColumnsCheckBoxes(cc_DisplayOptions, ckDisplayColumns);
                LogUsage(this.GetType());



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
#if TRACE
            VNC.AppLog.Trace5("End", LOG_APPNAME, startTicks);
#endif
        }
        #endregion

        #region Event Handlers

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

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            ahg_Top.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnFocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            //var row = e.NewRow;
            //var row2 = (SQLInformation.Data.ApplicationDataSet.DatabasesRow)((DataRowView)row).Row;
            //var sse = row2.SnapShotError;

            //if (sse != "")
            //{
            //    ; // Turn transparent
            //}
            //else
            //{
            //    ; // Turn Red
            //}
        }

        private void OnListDBLogins(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            // Get the list of logins for the Database

            var row = dataGrid.View.FocusedRowData.Row;

            SQLInformation.Data.ApplicationDataSet.DatabasesRow database =
                (SQLInformation.Data.ApplicationDataSet.DatabasesRow)((DataRowView)row).Row;

            Guid instanceID = database.Instance_ID;
            Guid dbID = database.ID;

            var logins = Common.ApplicationDataSet.Logins.Where(l => l.Instance_ID == instanceID).OrderBy(n => n.Name_Login);
            var dbUsers = Common.ApplicationDataSet.DBUsers.Where(u => u.Database_ID == dbID).OrderBy(n => n.Name_User);

            var win = new EyeOnLife.User_Interface.Windows.InstanceDatabaseInfo();

            win.tb_Instance.Text = database.Name_Instance;
            win.tb_Database.Text = database.Name_Database;

            win.lv_Logins.ItemsSource = logins;
            win.lv_Users.ItemsSource = dbUsers;

            win.Show();

            //foreach (var login in logins)
            //{
            //    System.Diagnostics.Debug.WriteLine("{0} - {1}", login.Name_Instance, login.Name_Login);
            //}

            //foreach (var user in dbUsers)
            //{
            //    System.Diagnostics.Debug.WriteLine("{0} - {1}", user.Login, user.Name_User); 
            //}
        }

        private void OnUpdateExtendedProperties(object sender, RoutedEventArgs e)
        {
            var row = dataGrid.View.FocusedRowData.Row;

            SQLInformation.Data.ApplicationDataSet.DatabasesRow database =
                (SQLInformation.Data.ApplicationDataSet.DatabasesRow)((DataRowView)row).Row;

            SaveExtendedProperties(database);

            // NB. The UI textBoxes are Bound to the dataGrid.  No need to updated.
            // However, push to SQLMonitor DB so still around if user relauches app before crawl updates.

            Common.ApplicationDataSet.Databases_Update();
            //Common.ApplicationDataSet.DatabasesTA.Update(Common.ApplicationDataSet.Databases);

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Databases_Update();
            //Common.ApplicationDataSet.DatabasesTA.Update(Common.ApplicationDataSet.Databases);

            ahg_Top.Visibility = System.Windows.Visibility.Hidden;
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Databases.RejectChanges();

            ahg_Top.Visibility = System.Windows.Visibility.Hidden;
        }

        #endregion

        #region Main Function Routines

        /// <summary>
        /// Filter the data that is being displayed.
        /// </summary>
        private void ApplyDataFilters()
        {

            CriteriaOperator filter = CriteriaOperator.Parse("[NotFound] = true");
            dataGrid.AddMRUFilter(filter);

            CriteriaOperator filter1 = CriteriaOperator.Parse("[NotFound] = false And [IsMonitored_Instance] = true");
            dataGrid.FilterCriteria = filter1;
            //dataGrid.ActiveFilterEnabled = true;

            //dataGrid.FilterString = "[Crawled] = 'True'";
        }

        private static void LoadExtendedProperties(SQLInformation.Data.ApplicationDataSet.DatabasesRow database)
        {
            //    var instanceName = from item in Common.ApplicationDataSet.Instances
            //                       where item.ID == database.Instance_ID
            //                       select item.Name_Instance;

            //    SMO.Server server = new SMO.Server((string)instanceName.First());
            //    server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            //    server.ConnectionContext.Login = "SMonitor";
            //    server.ConnectionContext.Password = "Pa$$word1";
            //    server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            //    SMO.ExtendedPropertyCollection extendedProps = server.Databases[database.Name_Database].ExtendedProperties;

            //    foreach (SMO.ExtendedProperty prop in extendedProps)
            //    {
            //        Console.WriteLine(string.Format("EP Name:{0}  Value:{1}", prop.Name, prop.Value));
            //    }

            //    try { database.EP_Area = (string)extendedProps["EP_Area"].Value; }
            //    catch (Exception) { database.EP_Area = "[Not Set]"; }

            //    try { database.EP_DBApprover = (string)extendedProps["EP_DBApprover"].Value; }
            //    catch (Exception) { database.EP_DBApprover = "[Not Set]"; }

            //    try { database.EP_DRTier = (string)extendedProps["EP_DRTier"].Value; }
            //    catch (Exception) { database.EP_DRTier = "[Not Set]"; }

            //    try { database.EP_PrimaryDBContact = (string)extendedProps["EP_PrimaryDBContact"].Value; }
            //    catch (Exception) { database.EP_PrimaryDBContact = "[Not Set]"; }

            //    try { database.EP_Team = (string)extendedProps["EP_Team"].Value; }
            //    catch (Exception) { database.EP_Team = "[Not Set]"; }
        }

        private void SaveExtendedProperties(SQLInformation.Data.ApplicationDataSet.DatabasesRow database)
        {
            var instances = from item in Common.ApplicationDataSet.Instances
                            where item.ID == database.Instance_ID
                            select new { Name = item.Name_Instance, Port = item.Port };
            string instanceName = (string)instances.First().Name;
            int port = (int)instances.First().Port;

            //MSMO.Server server = new MSMO.Server((string)instances.First());
            Microsoft.SqlServer.Management.Common.ServerConnection connection = new Microsoft.SqlServer.Management.Common.ServerConnection();
            connection.ServerInstance = string.Format("{0},{1}", instanceName, port);
            connection.NetworkProtocol = Microsoft.SqlServer.Management.Common.NetworkProtocol.TcpIp;

            MSMO.Server server = new MSMO.Server(connection);
            

            server.ConnectionContext.LoginSecure = true;   // SQL Authentication
            //server.ConnectionContext.Login = "SMonitor";
            //server.ConnectionContext.Password = "Pa$$word1";
            server.ConnectionContext.ConnectTimeout = 10;    // Seconds

            MSMO.ExtendedPropertyCollection extendedProps = server.Databases[database.Name_Database].ExtendedProperties;

            MSMO.Database smoDatabase = server.Databases[database.Name_Database];

            try
            {
                //extendedProps["EP_Area"].Value = database.EP_Area;
                extendedProps["EP_Area"].Value = te_EP_Area.Text;
                extendedProps["EP_Area"].Alter();
            }
            catch (Exception)
            {
                MSMO.ExtendedProperty ep = new MSMO.ExtendedProperty(smoDatabase, "EP_Area", te_EP_Area.Text);
                //MSMO.ExtendedProperty ep = new MSMO.ExtendedProperty(smoDatabase, "EP_Area", database.EP_Area);
                ep.Create();
            }

            try
            {
                extendedProps["EP_DBApprover"].Value = te_EP_DBApprover.Text;
                extendedProps["EP_DBApprover"].Alter();
            }
            catch (Exception)
            {
                MSMO.ExtendedProperty ep = new MSMO.ExtendedProperty(smoDatabase, "EP_DBApprover", te_EP_DBApprover.Text);
                ep.Create();
            }

            try
            {
                extendedProps["EP_DRTier"].Value  = te_EP_DRTier.Text;
                extendedProps["EP_DRTier"].Alter();
            }
            catch (Exception)
            {
                MSMO.ExtendedProperty ep = new MSMO.ExtendedProperty(smoDatabase, "EP_DRTier", te_EP_DRTier.Text);
                ep.Create();
            }

            try
            {
                extendedProps["EP_PrimaryDBContact"].Value = te_EP_PrimaryDBContact.Text;
                extendedProps["EP_PrimaryDBContact"].Alter();
            }
            catch (Exception)
            {
                MSMO.ExtendedProperty ep = new MSMO.ExtendedProperty(smoDatabase, "EP_PrimaryDBContact", te_EP_PrimaryDBContact.Text);
                ep.Create();
            }

            try
            {
                extendedProps["EP_Team"].Value = te_EP_Team.Text;
                extendedProps["EP_Team"].Alter();
            }
            catch (Exception)
            {
                MSMO.ExtendedProperty ep = new MSMO.ExtendedProperty(smoDatabase, "EP_Team", te_EP_Team.Text);
                ep.Create();
            }

        }

        #endregion

    }
}
