using System;
using System.Data;
using System.IO;
using System.Threading;

using ADSTA = SQLInformation.Data.ApplicationDataSetTableAdapters;
using VNC;

namespace SQLInformation.Data
{

    public partial class ApplicationDataSet
    {
        partial class InstancesDataTable
        {
        }
    
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.SQLINFORMATION_DATA_ADS;
        private const string LOG_APPNAME = "SQLInformation";

        // This is used to single thread access to the dataset
        static readonly object _concurrencyLock = new object();

        public ADSTA.ServersTableAdapter ServersTA { get; set; }
        public ADSTA.ServerInfoTableAdapter ServerInfoTA { get; set; }

        public ADSTA.InstancesTableAdapter InstancesTA { get; set; }
        public ADSTA.InstanceInfoTableAdapter InstanceInfoTA { get; set; }
        
        public ADSTA.JobServersTableAdapter JobServersTA { get; set; }

        public ADSTA.JSAlertCategoriesTableAdapter JSAlertCategoriesTA { get; set; }
        public ADSTA.JSAlertsTableAdapter JSAlertsTA { get; set; }
        public ADSTA.JSJobCategoriesTableAdapter JSJobCategoriesTA { get; set; }
        public ADSTA.JSJobsTableAdapter JSJobsTA { get; set; }
        public ADSTA.JSJobSchedulesTableAdapter JSJobSchedulesTA { get; set; }
        public ADSTA.JSJobStepsTableAdapter JSJobStepsTA { get; set; }
        public ADSTA.JSOperatorCategoriesTableAdapter JSOperatorCategoriesTA { get; set; }
        public ADSTA.JSOperatorsTableAdapter JSOperatorsTA { get; set; }
        public ADSTA.JSProxyAccountsTableAdapter JSProxyAccountsTA { get; set; }
        public ADSTA.JSSharedSchedulesTableAdapter JSSharedSchedulesTA { get; set; }
        public ADSTA.JSTargetServerGroupsTableAdapter JSTargetServerGroupsTA { get; set; }
        public ADSTA.JSTargetServersTableAdapter JSTargetServersTA { get; set; }

        public ADSTA.LinkedServersTableAdapter LinkedServersTA { get; set; }
        public ADSTA.LoginsTableAdapter LoginsTA { get; set; }
        public ADSTA.ServerDdlTriggersTableAdapter ServerDdlTriggersTA { get; set; }
        public ADSTA.ServerRolesTableAdapter ServerRolesTA { get; set; }

        public ADSTA.DatabasesTableAdapter DatabasesTA { get; set; }
        public ADSTA.DatabaseInfoTableAdapter DatabaseInfoTA { get; set; }

        public ADSTA.DBDataFilesTableAdapter DBDataFilesTA { get; set; }
        public ADSTA.DBDataFileInfoTableAdapter DBDataFileInfoTA { get; set; }

        public ADSTA.DBDdlTriggersTableAdapter DBDdlTriggersTA { get; set; }

        public ADSTA.DBFileGroupsTableAdapter DBFileGroupsTA { get; set; }
        public ADSTA.DBFileGroupInfoTableAdapter DBFileGroupInfoTA { get; set; }

        public ADSTA.DBLogFilesTableAdapter DBLogFilesTA { get; set; }
        public ADSTA.DBLogFileInfoTableAdapter DBLogFileInfoTA { get; set; }

        public ADSTA.DBRolesTableAdapter DBRolesTA { get; set; }
        public ADSTA.DBStoredProceduresTableAdapter DBStoredProceduresTA { get; set; }
        public ADSTA.DBTablesTableAdapter DBTablesTA { get; set; }
        public ADSTA.DBUserDefinedFunctionsTableAdapter DBUserDefinedFunctionsTA { get; set; }
        public ADSTA.DBUsersTableAdapter DBUsersTA { get; set; }
        public ADSTA.DBViewsTableAdapter DBViewsTA { get; set; }

        public ADSTA.TBColumnsTableAdapter TBColumnsTA { get; set; }
        public ADSTA.TBTriggersTableAdapter TBTriggersTA { get; set; }

        public ADSTA.VWColumnsTableAdapter VWColumnsTA { get; set; }
        public ADSTA.VWTriggersTableAdapter VWTriggersTA { get; set; }

        public ADSTA.LKUP_ADDomainsTableAdapter LU_ADDomainsTA { get; set; }
        public ADSTA.LKUP_EnvironmentsTableAdapter LU_EnvironmentsTA { get; set; }
        public ADSTA.LKUP_SecurityZonesTableAdapter LU_SecurityZonesTA { get; set; }
        public ADSTA.LKUP_SQLServerVersionsTableAdapter LU_SQLServerVersionsTA { get; set; }

        public ADSTA.ApplicationUsageTableAdapter ApplicationUsageTA { get; set; }
        public ADSTA.CrawlerExpandSettingsTableAdapter CrawlerExpandSettingsTA { get; set; }

        SQLInformation.Data.ApplicationDataSetTableAdapters.TableAdapterManager _taManager = null;

        public SQLInformation.Data.ApplicationDataSetTableAdapters.TableAdapterManager TaManager
        {
            get
            {
                if (_taManager == null)
                {
                    try
                    {
                        // Create the TableAdapterManager.  
                        // This enables the cascading updates/deletes of the dataset to work.
                        _taManager = new ADSTA.TableAdapterManager();

                        #region Create the TableAdapters for all the tables

                        // These are in by hierarchy (servers, instance, database) then by name order

                        ServersTA = new ADSTA.ServersTableAdapter();
                        ServerInfoTA = new ADSTA.ServerInfoTableAdapter();

                        InstancesTA = new ADSTA.InstancesTableAdapter();
                        InstanceInfoTA = new ADSTA.InstanceInfoTableAdapter();

                        JobServersTA = new ADSTA.JobServersTableAdapter();
                        JSAlertCategoriesTA = new ADSTA.JSAlertCategoriesTableAdapter();
                        JSAlertsTA = new ADSTA.JSAlertsTableAdapter();
                        JSJobCategoriesTA = new ADSTA.JSJobCategoriesTableAdapter();
                        JSJobsTA = new ADSTA.JSJobsTableAdapter();
                        JSJobSchedulesTA = new ADSTA.JSJobSchedulesTableAdapter();
                        JSJobStepsTA = new ADSTA.JSJobStepsTableAdapter();
                        JSOperatorCategoriesTA = new ADSTA.JSOperatorCategoriesTableAdapter();
                        JSOperatorsTA = new ADSTA.JSOperatorsTableAdapter();
                        JSProxyAccountsTA = new ADSTA.JSProxyAccountsTableAdapter();
                        JSSharedSchedulesTA = new ADSTA.JSSharedSchedulesTableAdapter();
                        JSTargetServerGroupsTA = new ADSTA.JSTargetServerGroupsTableAdapter();
                        JSTargetServersTA = new ADSTA.JSTargetServersTableAdapter();

                        LinkedServersTA = new ADSTA.LinkedServersTableAdapter();
                        LoginsTA = new ADSTA.LoginsTableAdapter();
                        ServerDdlTriggersTA = new ADSTA.ServerDdlTriggersTableAdapter();
                        ServerRolesTA = new ADSTA.ServerRolesTableAdapter();

                        DatabasesTA = new ADSTA.DatabasesTableAdapter();
                        DatabaseInfoTA = new ADSTA.DatabaseInfoTableAdapter();

                        DBDataFilesTA = new ADSTA.DBDataFilesTableAdapter();
                        DBDataFileInfoTA = new ADSTA.DBDataFileInfoTableAdapter();

                        DBDdlTriggersTA = new ADSTA.DBDdlTriggersTableAdapter();

                        DBFileGroupsTA = new ADSTA.DBFileGroupsTableAdapter();
                        DBFileGroupInfoTA = new ADSTA.DBFileGroupInfoTableAdapter();

                        DBLogFilesTA = new ADSTA.DBLogFilesTableAdapter();
                        DBLogFileInfoTA = new ADSTA.DBLogFileInfoTableAdapter();

                        DBRolesTA = new ADSTA.DBRolesTableAdapter();
                        DBStoredProceduresTA = new ADSTA.DBStoredProceduresTableAdapter();
                        DBTablesTA = new ADSTA.DBTablesTableAdapter();
                        DBUserDefinedFunctionsTA = new ADSTA.DBUserDefinedFunctionsTableAdapter();
                        DBUsersTA = new ADSTA.DBUsersTableAdapter();
                        DBViewsTA = new ADSTA.DBViewsTableAdapter();

                        TBColumnsTA = new ADSTA.TBColumnsTableAdapter();
                        TBTriggersTA = new ADSTA.TBTriggersTableAdapter();
                        VWColumnsTA = new ADSTA.VWColumnsTableAdapter();
                        VWTriggersTA = new ADSTA.VWTriggersTableAdapter();

                        LU_ADDomainsTA = new ADSTA.LKUP_ADDomainsTableAdapter();
                        LU_EnvironmentsTA = new ADSTA.LKUP_EnvironmentsTableAdapter();
                        LU_SecurityZonesTA = new ADSTA.LKUP_SecurityZonesTableAdapter();
                        LU_SQLServerVersionsTA = new ADSTA.LKUP_SQLServerVersionsTableAdapter();

                        ApplicationUsageTA = new ADSTA.ApplicationUsageTableAdapter();
                        CrawlerExpandSettingsTA = new ADSTA.CrawlerExpandSettingsTableAdapter();

                        #endregion

                        #region Hook the table adapters to the table manager

                        _taManager.ServersTableAdapter = ServersTA;
                        _taManager.ServerInfoTableAdapter = ServerInfoTA;

                        _taManager.InstancesTableAdapter = InstancesTA;
                        _taManager.InstanceInfoTableAdapter = InstanceInfoTA;

                        _taManager.JobServersTableAdapter = JobServersTA;
                        _taManager.JSAlertCategoriesTableAdapter = JSAlertCategoriesTA;
                        _taManager.JSAlertsTableAdapter = JSAlertsTA;
                        _taManager.JSJobCategoriesTableAdapter = JSJobCategoriesTA;
                        _taManager.JSJobsTableAdapter = JSJobsTA;
                        _taManager.JSJobSchedulesTableAdapter = JSJobSchedulesTA;
                        _taManager.JSJobStepsTableAdapter = JSJobStepsTA;
                        _taManager.JSOperatorCategoriesTableAdapter = JSOperatorCategoriesTA;
                        _taManager.JSOperatorsTableAdapter = JSOperatorsTA;
                        _taManager.JSProxyAccountsTableAdapter = JSProxyAccountsTA;
                        _taManager.JSSharedSchedulesTableAdapter = JSSharedSchedulesTA;
                        _taManager.JSTargetServerGroupsTableAdapter = JSTargetServerGroupsTA;
                        _taManager.JSTargetServersTableAdapter = JSTargetServersTA;

                        _taManager.LinkedServersTableAdapter = LinkedServersTA;
                        _taManager.LoginsTableAdapter = LoginsTA;
                        _taManager.ServerDdlTriggersTableAdapter = ServerDdlTriggersTA;
                        _taManager.ServerRolesTableAdapter = ServerRolesTA;

                        _taManager.DatabasesTableAdapter = DatabasesTA;
                        _taManager.DatabaseInfoTableAdapter = DatabaseInfoTA;

                        _taManager.DBDataFilesTableAdapter = DBDataFilesTA;
                        _taManager.DBDataFileInfoTableAdapter = DBDataFileInfoTA;

                        _taManager.DBDdlTriggersTableAdapter = DBDdlTriggersTA;

                        _taManager.DBFileGroupsTableAdapter = DBFileGroupsTA;
                        _taManager.DBFileGroupInfoTableAdapter = DBFileGroupInfoTA;

                        _taManager.DBLogFilesTableAdapter = DBLogFilesTA;
                        _taManager.DBLogFileInfoTableAdapter = DBLogFileInfoTA;

                        _taManager.DBRolesTableAdapter = DBRolesTA;
                        _taManager.DBStoredProceduresTableAdapter = DBStoredProceduresTA;
                        _taManager.DBTablesTableAdapter = DBTablesTA;
                        _taManager.DBUserDefinedFunctionsTableAdapter = DBUserDefinedFunctionsTA;
                        _taManager.DBUsersTableAdapter = DBUsersTA;
                        _taManager.DBViewsTableAdapter = DBViewsTA;

                        _taManager.TBColumnsTableAdapter = TBColumnsTA;
                        _taManager.TBTriggersTableAdapter = TBTriggersTA;

                        _taManager.VWColumnsTableAdapter = VWColumnsTA;
                        _taManager.VWTriggersTableAdapter = VWTriggersTA;

                        _taManager.LKUP_ADDomainsTableAdapter = LU_ADDomainsTA;
                        _taManager.LKUP_EnvironmentsTableAdapter = LU_EnvironmentsTA;
                        _taManager.LKUP_SecurityZonesTableAdapter = LU_SecurityZonesTA;
                        _taManager.LKUP_SQLServerVersionsTableAdapter = LU_SQLServerVersionsTA;

                        _taManager.ApplicationUsageTableAdapter = ApplicationUsageTA;
                        _taManager.CrawlerExpandSettingsTableAdapter = CrawlerExpandSettingsTA;

                        #endregion

                        #region And update all the connection strings.

                        _taManager.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        ServersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        ServerInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        InstancesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        InstanceInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        JobServersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        JSAlertCategoriesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSAlertsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSJobCategoriesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSJobsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSJobSchedulesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSJobStepsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSOperatorCategoriesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSOperatorsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSProxyAccountsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSSharedSchedulesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSTargetServerGroupsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        JSTargetServersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        LinkedServersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        LoginsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        ServerDdlTriggersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        ServerRolesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DatabasesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DatabaseInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DBDataFilesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBDataFileInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DBDdlTriggersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DBFileGroupsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBFileGroupInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DBLogFilesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBLogFileInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DBRolesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBStoredProceduresTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBTablesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBUserDefinedFunctionsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBUsersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBViewsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        TBColumnsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        TBTriggersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        VWColumnsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        VWTriggersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        LU_ADDomainsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        LU_EnvironmentsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        LU_SecurityZonesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        LU_SQLServerVersionsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        ApplicationUsageTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        CrawlerExpandSettingsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        #endregion

                    }
                    catch (Exception ex)
                    {
                        VNC.AppLog.Error(string.Format("ConnectionString:>{0}<", Config.SQLMonitorDBConnection), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                        VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    }
                }

                return _taManager;
            }
            set
            {
                _taManager = value;
            }
        }

        /// <summary>
        /// Load the tables that are most often used.
        /// </summary>
        /// <param name="applicationDS"></param>
        private void LoadMainTables(ApplicationDataSet applicationDS)
        {
#if TRACE
            long startTicksTotal = VNC.AppLog.Trace("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif
            long startTicks = 0;

            if (Config.ADSLoad_Servers)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill Servers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
#endif
                // Intialize the TAManager and all the other TableAdapters by using the embedded ServersTableAdapter
                // See the Get method on TaManager.
                lock (_concurrencyLock)
                {
                    TaManager.ServersTableAdapter.Fill(applicationDS.Servers);
                }

#if TRACE
                VNC.AppLog.Trace2("End Fill Servers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5, startTicks);
#endif
            }

            if (Config.ADSLoad_Instances)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill Instances", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
#endif
                lock (_concurrencyLock)
                {
                    InstancesTA.Fill(applicationDS.Instances);
                }

#if TRACE
                VNC.AppLog.Trace2("End Fill Instances", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9, startTicks);
#endif
            }

            if (Config.ADSLoad_Logins)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill Logins", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 14);
#endif
                lock (_concurrencyLock)
                {
                    LoginsTA.Fill(applicationDS.Logins);
                }

#if TRACE
                VNC.AppLog.Trace2("End Fill Logins", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15, startTicks);
#endif
            }

            if (Config.ADSLoad_Databases)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill Databases", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 18);
#endif
                lock (_concurrencyLock)
                {
                    DatabasesTA.Fill(applicationDS.Databases);
                }

#if TRACE
                VNC.AppLog.Trace2("End Fill Databases", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 19, startTicks);
#endif
            }

#if TRACE
            VNC.AppLog.Trace("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 62, startTicksTotal);
#endif
        }

        /// <summary>
        /// Load the tables that contain details inside a database.  These are needed for the hierarchy explorers.
        /// </summary>
        /// <param name="applicationDS"></param>
        private void LoadDBContentTables(ApplicationDataSet applicationDS)
        {
#if TRACE
            long startTicksTotal = VNC.AppLog.Trace("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif
            long startTicks = 0;

            if (Config.ADSLoad_DBDdlTriggers)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBDdlTriggers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 26);
#endif
                lock (_concurrencyLock)
                {
                    DBDdlTriggersTA.Fill(applicationDS.DBDdlTriggers);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBDdlTriggers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 27, startTicks);
#endif
            }


            if (Config.ADSLoad_DBRoles)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBRoles", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 34);
#endif
                lock (_concurrencyLock)
                {
                    DBRolesTA.Fill(applicationDS.DBRoles);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBRoles", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 35, startTicks);
#endif
            }


            if (Config.ADSLoad_DBStoredProcedures)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBStoredProcedures", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 36);
#endif
                lock (_concurrencyLock)
                {
                    DBStoredProceduresTA.Fill(applicationDS.DBStoredProcedures);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBStoredProcedures", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 37, startTicks);
#endif
            }

            if (Config.ADSLoad_DBTables)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBTables", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 38);
#endif
                lock (_concurrencyLock)
                {
                    DBTablesTA.Fill(applicationDS.DBTables);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBTables", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 39, startTicks);
#endif
            }

            if (Config.ADSLoad_DBUserDefinedFunctions)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBUserDefinedFunctions", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 40);
#endif
                lock (_concurrencyLock)
                {
                    DBUserDefinedFunctionsTA.Fill(applicationDS.DBUserDefinedFunctions);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBUserDefinedFunctions", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 41, startTicks);
#endif
            }

            if (Config.ADSLoad_DBUsers)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBUsers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 42);
#endif
                lock (_concurrencyLock)
                {
                    DBUsersTA.Fill(applicationDS.DBUsers);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBUsers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 43, startTicks);
#endif
            }

            if (Config.ADSLoad_DBViews)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBViews", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 44);
#endif
                lock (_concurrencyLock)
                {
                    DBViewsTA.Fill(applicationDS.DBViews);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBViews", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 45, startTicks);
#endif
            }

            if (Config.ADSLoad_TBColumns)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill TBColumns", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 46);
#endif
                lock (_concurrencyLock)
                {
                    TBColumnsTA.Fill(applicationDS.TBColumns);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill TBColumns", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 47, startTicks);
#endif
            }

            if (Config.ADSLoad_TBTriggers)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill TBTriggers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 46);
#endif
                lock (_concurrencyLock)
                {
                    TBTriggersTA.Fill(applicationDS.TBTriggers);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill TBTriggers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 47, startTicks);
#endif
            }

            if (Config.ADSLoad_VWColumns)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill VWColumns", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 46);
#endif
                lock (_concurrencyLock)
                {
                    VWColumnsTA.Fill(applicationDS.VWColumns);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill VWColumns", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 47, startTicks);
#endif
            }

            if (Config.ADSLoad_VWTriggers)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill VWTriggers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 48);
#endif
                lock (_concurrencyLock)
                {
                    VWTriggersTA.Fill(applicationDS.VWTriggers);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill VWTriggers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 49, startTicks);
#endif
            }
#if TRACE
            VNC.AppLog.Trace("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 62, startTicksTotal);
#endif
        }

        /// <summary>
        /// Load the tables that contain database storage information.
        /// </summary>
        /// <param name="applicationDS"></param>
        private void LoadSnapShotTables(ApplicationDataSet applicationDS)
        {
#if TRACE
            long startTicksTotal = VNC.AppLog.Trace("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif
            long startTicks = 0;

            if (Config.ADSLoad_ServerInfo)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill ServerInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
#endif
                lock (_concurrencyLock)
                {
                    ServerInfoTA.Fill(applicationDS.ServerInfo);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill ServerInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7, startTicks);
#endif
            }

            if (Config.ADSLoad_InstanceInfo)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill InstanceInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    InstanceInfoTA.Fill(applicationDS.InstanceInfo);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill InstanceInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_DatabaseInfo)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DatabaseInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 20);
#endif
                lock (_concurrencyLock)
                {
                    DatabaseInfoTA.Fill(applicationDS.DatabaseInfo);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DatabaseInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 21, startTicks);
#endif
            }

            if (Config.ADSLoad_DBDataFiles)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBDataFiles", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 22);
#endif
                lock (_concurrencyLock)
                {
                    DBDataFilesTA.Fill(applicationDS.DBDataFiles);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBDataFiles", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 23, startTicks);
#endif
            }
            if (Config.ADSLoad_DBDataFileInfo)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBDataFileInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 24);
#endif
                lock (_concurrencyLock)
                {
                    DBDataFileInfoTA.Fill(applicationDS.DBDataFileInfo);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBDataFileInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 25, startTicks);
#endif
            }

            if (Config.ADSLoad_DBFileGroups)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBFileGroups", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 28);
#endif
                lock (_concurrencyLock)
                {
                    DBFileGroupsTA.Fill(applicationDS.DBFileGroups);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBFileGroups", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 29, startTicks);
#endif
            }

            if (Config.ADSLoad_DBFileGroupInfo)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBFileGroupInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 28);
#endif
                lock (_concurrencyLock)
                {
                    DBFileGroupInfoTA.Fill(applicationDS.DBFileGroupInfo);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBFileGroupInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 29, startTicks);
#endif
            }

            if (Config.ADSLoad_DBLogFiles)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBLogFiles", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 30);
#endif
                lock (_concurrencyLock)
                {
                    DBLogFilesTA.Fill(applicationDS.DBLogFiles);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBLogFiles", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 31, startTicks);
#endif
            }

            if (Config.ADSLoad_DBLogFileInfo)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill DBLogFileInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 32);
#endif
                lock (_concurrencyLock)
                {
                    DBLogFileInfoTA.Fill(applicationDS.DBLogFileInfo);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill DBLogFileInfo", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 33, startTicks);
#endif
            }

#if TRACE
            VNC.AppLog.Trace("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 62, startTicksTotal);
#endif
        }

        /// <summary>
        ///  Load the tables that contain secondary information about instances.
        /// </summary>
        /// <param name="applicationDS"></param>
        private void LoadInstanceContentTables(ApplicationDataSet applicationDS)
        {
#if TRACE
            long startTicksTotal = VNC.AppLog.Trace("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif
            long startTicks = 0;

            if (Config.ADSLoad_LinkedServers)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill LinkedServers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 14);
#endif
                lock (_concurrencyLock)
                {
                    LinkedServersTA.Fill(applicationDS.LinkedServers);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill LinkedServers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15, startTicks);
#endif
            }

            if (Config.ADSLoad_ServerDdlTriggers)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill ServerDdlTriggers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 16);
#endif
                lock (_concurrencyLock)
                {
                    ServerDdlTriggersTA.Fill(applicationDS.ServerDdlTriggers);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill ServerDdlTriggers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 17, startTicks);
#endif
            }

            if (Config.ADSLoad_ServerRoles)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill ServerRoles", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 16);
#endif
                lock (_concurrencyLock)
                {
                    ServerRolesTA.Fill(applicationDS.ServerRoles);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill ServerRoles", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 17, startTicks);
#endif
            }
#if TRACE
            VNC.AppLog.Trace("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 62, startTicksTotal);
#endif
        }

        /// <summary>
        /// Load the tables that contain jobserver information
        /// </summary>
        /// <param name="applicationDS"></param>
        private void LoadJobServerTables(ApplicationDataSet applicationDS)
        {
#if TRACE
            long startTicksTotal = VNC.AppLog.Trace("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif
            long startTicks = 0;


            if (Config.ADSLoad_JobServers)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JobServers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JobServersTA.Fill(applicationDS.JobServers);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JobServers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSAlertCategories)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSAlertCategories", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSAlertCategoriesTA.Fill(applicationDS.JSAlertCategories);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSAlertCategories", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSAlerts)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSAlerts", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSAlertsTA.Fill(applicationDS.JSAlerts);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSAlerts", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSJobCategories)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSJobCategories", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSJobCategoriesTA.Fill(applicationDS.JSJobCategories);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSJobCategories", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSJobs)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSJobs", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSJobsTA.Fill(applicationDS.JSJobs);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSJobs", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSJobSchedules)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSJobSchedules", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSJobSchedulesTA.Fill(applicationDS.JSJobSchedules);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSJobSchedules", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSJobSteps)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSJobSteps", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSJobStepsTA.Fill(applicationDS.JSJobSteps);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSJobSteps", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSOperatorCategories)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSOperatorCategories", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSOperatorCategoriesTA.Fill(applicationDS.JSOperatorCategories);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSOperatorCategories", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSOperators)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSOperators", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSOperatorsTA.Fill(applicationDS.JSOperators);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSOperators", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSSharedSchedules)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSSharedSchedules", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSSharedSchedulesTA.Fill(applicationDS.JSSharedSchedules);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSSharedSchedules", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSTargetServerGroups)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSTargetServerGroups", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSTargetServerGroupsTA.Fill(applicationDS.JSTargetServerGroups);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSTargetServerGroups", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

            if (Config.ADSLoad_JSTargetServers)
            {
#if TRACE
                startTicks = VNC.AppLog.Trace2("Fill JSTargetServers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                lock (_concurrencyLock)
                {
                    JSTargetServersTA.Fill(applicationDS.JSTargetServers);
                }
#if TRACE
                VNC.AppLog.Trace2("End Fill JSTargetServers", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11, startTicks);
#endif
            }

#if TRACE
            VNC.AppLog.Trace("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 62, startTicksTotal);
#endif
        }

        /// <summary>
        /// Load the tables that contain jobserver information
        /// </summary>
        /// <param name="applicationDS"></param>
        private void LoadLKUPandSupportTables(ApplicationDataSet applicationDS)
        {
#if TRACE
            long startTicksTotal = VNC.AppLog.Trace("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif
            long startTicks = 0;

#if TRACE
            startTicks = VNC.AppLog.Trace2("Fill LKUP_ADDomains", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 50);
#endif
            lock (_concurrencyLock)
            {
                LU_ADDomainsTA.Fill(applicationDS.LKUP_ADDomains);
            }
#if TRACE
            VNC.AppLog.Trace2("End Fill LKUP_ADDomains", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 51, startTicks);
#endif

#if TRACE
            startTicks = VNC.AppLog.Trace2("Fill LKUP_Environments", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 52);
#endif
            lock (_concurrencyLock)
            {
                LU_EnvironmentsTA.Fill(applicationDS.LKUP_Environments);
            }

#if TRACE
            VNC.AppLog.Trace2("End Fill LKUP_Environments", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 53, startTicks);
#endif

#if TRACE
            startTicks = VNC.AppLog.Trace2("Fill LKUP_SecurityZones", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 54);
#endif
            lock (_concurrencyLock)
            {
                LU_SecurityZonesTA.Fill(applicationDS.LKUP_SecurityZones);
            }

#if TRACE
            VNC.AppLog.Trace2("End Fill LKUP_SecurityZones", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 55, startTicks);
#endif

#if TRACE
            startTicks = VNC.AppLog.Trace2("Fill LKUP_SQLServerVersions", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 56);
#endif
            lock (_concurrencyLock)
            {
                LU_SQLServerVersionsTA.Fill(applicationDS.LKUP_SQLServerVersions);
            }
            
#if TRACE
            VNC.AppLog.Trace2("End Fill LKUP_SQLServerVersions", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 57, startTicks);
#endif

#if TRACE
            startTicks = VNC.AppLog.Trace2("Fill ApplicationUsage", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 58);
#endif
            lock (_concurrencyLock)
            {
                ApplicationUsageTA.Fill(applicationDS.ApplicationUsage);
            }

#if TRACE
            VNC.AppLog.Trace2("End Fill ApplicationUsage", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 59, startTicks);
#endif

#if TRACE
            startTicks = VNC.AppLog.Trace2("Fill CrawlerExpandSettings", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 58);
#endif
            lock (_concurrencyLock)
            {
                CrawlerExpandSettingsTA.Fill(applicationDS.CrawlerExpandSettings);
            }

#if TRACE
            VNC.AppLog.Trace2("End Fill CrawlerExpandSettings", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 59, startTicks);
#endif
#if TRACE
            VNC.AppLog.Trace("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 62, startTicksTotal);
#endif
        }

        private void LoadTablesInBackGround(Data.ApplicationDataSet applicationDS)
        {
            // Might be able to do this in parallel after we figure out locking.
            // For now just do serially.

            //LoadInstanceContentTables(applicationDS);
            //LoadSnapShotTables(applicationDS);
            //LoadDBContentTables(applicationDS);
            //LoadJobServerTables(applicationDS);

            Thread t1 = new Thread(() => LoadInstanceContentTables(applicationDS));
            t1.Start();

            Thread t2 = new Thread(() => LoadSnapShotTables(applicationDS));
            t2.Start();

            Thread t3 = new Thread(() => LoadDBContentTables(applicationDS));
            t3.Start();

            Thread t4 = new Thread(() => LoadJobServerTables(applicationDS));
            t4.Start();

            Common.DataFullyLoaded = true;
        }

        public void LoadApplicationDataSetFromDB(Data.ApplicationDataSet applicationDS)
        {
#if TRACE
            long startTicksTotal = VNC.AppLog.Trace("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif
            try
            {
                long startTicks = 0;

                VNC.AppLog.Info("Clearing ApplicationDataSet...", LOG_APPNAME);
                applicationDS.Clear();
                Common.DataFullyLoaded = false;

                LoadMainTables(applicationDS);

                LoadLKUPandSupportTables(applicationDS);

                // Load the rest of the tables

                Thread t = new Thread(() => LoadTablesInBackGround(applicationDS));
                t.Start();

            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(string.Format("ConnectionString:>{0}<", Config.SQLMonitorDBConnection), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 60);
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 61);
            }

#if TRACE
            VNC.AppLog.Trace("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 62, startTicksTotal);
#endif
        }

        public InstancesRow GetNewInstancesRow(string instanceFullName)
        {
            InstancesRow newRow = Common.ApplicationDataSet.Instances.NewInstancesRow();

            newRow.ID = Guid.NewGuid();
            newRow.Name_Instance = instanceFullName;

            // New rows are rarely added to the instance table.  Start with Monitoring turned off.

            newRow.IsMonitored = false;

            // But get everything ready to go.

            newRow.ExpandContent = true;
            newRow.ExpandStorage = true;
            newRow.ExpandJobServer = true;
            newRow.ExpandLogins = true;
            newRow.ExpandServerRoles = true;
            newRow.ExpandTriggers = true;

            return newRow;
        }

        public void Databases_Add(DatabasesRow dataRow)
        {
            lock (_concurrencyLock)
            {
                this.Databases.AddDatabasesRow(dataRow);
                this.DatabasesTA.Update(this.Databases);
            }
        }

        public void Databases_Update()
        {
            lock (_concurrencyLock)
            {
                this.DatabasesTA.Update(this.Databases);                
            }
        }

        public void DBViews_Add(DBViewsRow dataRow)
        {
            lock (_concurrencyLock)
            {
                this.DBViews.AddDBViewsRow(dataRow);
                this.DBViewsTA.Update(this.DBViews);
            }
        }

        public void DBViews_Update()
        {
            lock (_concurrencyLock)
            {
                this.DBViewsTA.Update(this.DBViews);
            }
        }

        public void Instances_Update()
        {
            lock (_concurrencyLock)
            {
                this.InstancesTA.Update(this.Instances);
            }
        }

        public void Servers_Update()
        {
            lock (_concurrencyLock)
            {
                this.ServersTA.Update(this.Servers);
            }
        }

        public void DBTables_Add(DBTablesRow dataRow)
        {
            lock (_concurrencyLock)
            {
                this.DBTables.AddDBTablesRow(dataRow);
                this.DBTablesTA.Update(this.DBTables);
            }
        }

        public void DBTables_Update()
        {
            lock (_concurrencyLock)
            {
                this.DBTablesTA.Update(this.DBTables);
            }
        }

        public void JSAlerts_Add(JSAlertsRow dataRow)
        {
            lock (_concurrencyLock)
            {
                this.JSAlerts.AddJSAlertsRow(dataRow);
                this.JSAlertsTA.Update(this.JSAlerts);
            }
        }

        public void JSAlerts_Update()
        {
            lock (_concurrencyLock)
            {
                this.JSAlertsTA.Update(this.JSAlerts);
            }
        }
    }

}



