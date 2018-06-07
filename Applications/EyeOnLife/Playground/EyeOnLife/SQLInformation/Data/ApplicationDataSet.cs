using System;
using System.Data;
using System.IO;

using ADSTA = SQLInformation.Data.ApplicationDataSetTableAdapters;
using PacificLife.Life;

namespace SQLInformation.Data
{

    public partial class ApplicationDataSet
    {
        public ADSTA.ServersTableAdapter ServersTA { get; set; }
        public ADSTA.ServerInfoTableAdapter ServerInfoTA { get; set; }

        public ADSTA.InstancesTableAdapter InstancesTA { get; set; }
        public ADSTA.InstanceInfoTableAdapter InstanceInfoTA { get; set; }

        public ADSTA.JobsTableAdapter JobsTA { get; set; }
        public ADSTA.LoginsTableAdapter LoginsTA { get; set; }
        public ADSTA.ServerRolesTableAdapter ServerRolesTA { get; set; }

        public ADSTA.DatabasesTableAdapter DatabasesTA { get; set; }
        public ADSTA.DatabaseInfoTableAdapter DatabaseInfoTA { get; set; }

        public ADSTA.DBDataFilesTableAdapter DBDataFilesTA { get; set; }
        public ADSTA.DBDataFileInfoTableAdapter DBDataFileInfoTA { get; set; }

        public ADSTA.DBDdlTriggersTableAdapter DBDdlTriggersTA { get; set; }
        public ADSTA.DBFileGroupsTableAdapter DBFileGroupsTA { get; set; }

        public ADSTA.DBLogFilesTableAdapter DBLogFilesTA { get; set; }
        public ADSTA.DBLogFileInfoTableAdapter DBLogFileInfoTA { get; set; }

        public ADSTA.DBRolesTableAdapter DBRolesTA { get; set; }
        public ADSTA.DBStoredProceduresTableAdapter DBStoredProceduresTA { get; set; }
        public ADSTA.DBTablesTableAdapter DBTablesTA { get; set; }
        public ADSTA.DBUserDefinedFunctionsTableAdapter DBUserDefinedFunctionsTA { get; set; }
        public ADSTA.DBUsersTableAdapter DBUsersTA { get; set; }
        public ADSTA.DBViewsTableAdapter DBViewsTA { get; set; }
        public ADSTA.TBTriggersTableAdapter TBTriggersTA { get; set; }
        public ADSTA.VWTriggersTableAdapter VWTriggersTA { get; set; }

        public ADSTA.LKUP_ADDomainsTableAdapter LU_ADDomainsTA { get; set; }
        public ADSTA.LKUP_EnvironmentsTableAdapter LU_EnvironmentsTA { get; set; }
        public ADSTA.LKUP_SQLServerVersionsTableAdapter LU_SQLServerVersionsTA { get; set; }

        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "SQLInformation";

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

                        JobsTA = new ADSTA.JobsTableAdapter();
                        LoginsTA = new ADSTA.LoginsTableAdapter();
                        ServerRolesTA = new ADSTA.ServerRolesTableAdapter();

                        DatabasesTA = new ADSTA.DatabasesTableAdapter();
                        DatabaseInfoTA = new ADSTA.DatabaseInfoTableAdapter();

                        DBDataFilesTA = new ADSTA.DBDataFilesTableAdapter();
                        DBDataFileInfoTA = new ADSTA.DBDataFileInfoTableAdapter();

                        DBDdlTriggersTA = new ADSTA.DBDdlTriggersTableAdapter();
                        DBFileGroupsTA = new ADSTA.DBFileGroupsTableAdapter();

                        DBLogFilesTA = new ADSTA.DBLogFilesTableAdapter();
                        DBLogFileInfoTA = new ADSTA.DBLogFileInfoTableAdapter();

                        DBRolesTA = new ADSTA.DBRolesTableAdapter();
                        DBStoredProceduresTA = new ADSTA.DBStoredProceduresTableAdapter();
                        DBTablesTA = new ADSTA.DBTablesTableAdapter();
                        DBUserDefinedFunctionsTA = new ADSTA.DBUserDefinedFunctionsTableAdapter();
                        DBUsersTA = new ADSTA.DBUsersTableAdapter();
                        DBViewsTA = new ADSTA.DBViewsTableAdapter();
                        TBTriggersTA = new ADSTA.TBTriggersTableAdapter();
                        VWTriggersTA = new ADSTA.VWTriggersTableAdapter();

                        LU_ADDomainsTA = new ADSTA.LKUP_ADDomainsTableAdapter();
                        LU_EnvironmentsTA = new ADSTA.LKUP_EnvironmentsTableAdapter();
                        LU_SQLServerVersionsTA = new ADSTA.LKUP_SQLServerVersionsTableAdapter();
                        #endregion

                        #region Hook the table adapters to the table manager

                        _taManager.ServersTableAdapter = ServersTA;
                        _taManager.ServerInfoTableAdapter = ServerInfoTA;

                        _taManager.InstancesTableAdapter = InstancesTA;
                        _taManager.InstanceInfoTableAdapter = InstanceInfoTA;

                        _taManager.JobsTableAdapter = JobsTA;
                        _taManager.LoginsTableAdapter = LoginsTA;
                        _taManager.ServerRolesTableAdapter = ServerRolesTA;

                        _taManager.DatabasesTableAdapter = DatabasesTA;
                        _taManager.DatabaseInfoTableAdapter = DatabaseInfoTA;

                        _taManager.DBDataFilesTableAdapter = DBDataFilesTA;
                        _taManager.DBDataFileInfoTableAdapter = DBDataFileInfoTA;

                        _taManager.DBDdlTriggersTableAdapter = DBDdlTriggersTA;
                        _taManager.DBFileGroupsTableAdapter = DBFileGroupsTA;

                        _taManager.DBLogFilesTableAdapter = DBLogFilesTA;
                        _taManager.DBLogFileInfoTableAdapter = DBLogFileInfoTA;

                        _taManager.DBRolesTableAdapter = DBRolesTA;
                        _taManager.DBStoredProceduresTableAdapter = DBStoredProceduresTA;
                        _taManager.DBTablesTableAdapter = DBTablesTA;
                        _taManager.DBUserDefinedFunctionsTableAdapter = DBUserDefinedFunctionsTA;
                        _taManager.DBUsersTableAdapter = DBUsersTA;
                        _taManager.DBViewsTableAdapter = DBViewsTA;

                        _taManager.TBTriggersTableAdapter = TBTriggersTA;
                        _taManager.VWTriggersTableAdapter = VWTriggersTA;

                        _taManager.LKUP_ADDomainsTableAdapter = LU_ADDomainsTA;
                        _taManager.LKUP_EnvironmentsTableAdapter = LU_EnvironmentsTA;
                        _taManager.LKUP_SQLServerVersionsTableAdapter = LU_SQLServerVersionsTA;

                        #endregion

                        #region And update all the connection strings.
                        
                        _taManager.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        ServersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        ServerInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        InstancesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        InstanceInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        JobsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        LoginsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        ServerRolesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DatabasesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DatabaseInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DBDataFilesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBDataFileInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DBDdlTriggersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBFileGroupsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DBLogFilesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBLogFileInfoTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        DBRolesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBStoredProceduresTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBTablesTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBUserDefinedFunctionsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBUsersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        DBViewsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        TBTriggersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        VWTriggersTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                        LU_ADDomainsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        LU_EnvironmentsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;
                        LU_SQLServerVersionsTA.Connection.ConnectionString = Config.SQLMonitorDBConnection;

                #endregion

                    }
                    catch (Exception ex)
                    {
                        PLLog.Error(string.Format("ConnectionString:>{0}<", Config.SQLMonitorDBConnection), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }
                }

                return _taManager;
            }
            set
            {
                _taManager = value;
            }
        }

        public void LoadApplicationDataSetFromDB(Data.ApplicationDataSet applicationDS)
        {
#if TRACE
            long startTicksTotal = PLLog.Trace("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            try
            {

                long startTicks = 0;


                if (Config.ADSLoad_Servers)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill Servers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    // Intialize the TAManager and all the other TableAdapters by using the embedded ServersTableAdapter
                    // See the Get method on TaManager.
                    TaManager.ServersTableAdapter.Fill(applicationDS.Servers);
#if TRACE
                    PLLog.Trace2("End Fill Servers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_ServerInfo)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill ServerInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    ServerInfoTA.Fill(applicationDS.ServerInfo);
#if TRACE
                    PLLog.Trace2("End Fill ServerInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_Instances)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill Instances", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    InstancesTA.Fill(applicationDS.Instances);
#if TRACE
                    PLLog.Trace2("End Fill Instances", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_InstanceInfo)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill InstanceInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    InstanceInfoTA.Fill(applicationDS.InstanceInfo); 
#if TRACE
                    PLLog.Trace2("End Fill InstanceInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_Jobs)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill Jobs", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    JobsTA.Fill(applicationDS.Jobs);
#if TRACE
                    PLLog.Trace2("End Fill Jobs", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_Logins)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill Logins", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    LoginsTA.Fill(applicationDS.Logins);
#if TRACE
                    PLLog.Trace2("End Fill Logins", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_ServerRoles)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill ServerRoles", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    ServerRolesTA.Fill(applicationDS.ServerRoles);
#if TRACE
                    PLLog.Trace2("End Fill ServerRoles", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_Databases)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill Databases", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DatabasesTA.Fill(applicationDS.Databases);
#if TRACE
                    PLLog.Trace2("End Fill Databases", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DatabaseInfo)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DatabaseInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DatabaseInfoTA.Fill(applicationDS.DatabaseInfo);
#if TRACE
                    PLLog.Trace2("End Fill DatabaseInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBDataFiles)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBDataFiles", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBDataFilesTA.Fill(applicationDS.DBDataFiles); ;
#if TRACE
                    PLLog.Trace2("End Fill DBDataFiles", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }
                if (Config.ADSLoad_DBDataFileInfo)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBDataFileInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBDataFileInfoTA.Fill(applicationDS.DBDataFileInfo);
#if TRACE
                    PLLog.Trace2("End Fill DBDataFileInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBDdlTriggers)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBDdlTriggers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBDdlTriggersTA.Fill(applicationDS.DBDdlTriggers);
#if TRACE
                    PLLog.Trace2("End Fill DBDdlTriggers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBFileGroups)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBFileGroups", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBFileGroupsTA.Fill(applicationDS.DBFileGroups);
#if TRACE
                    PLLog.Trace2("End Fill DBFileGroups", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBLogFiles)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBLogFiles", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBLogFilesTA.Fill(applicationDS.DBLogFiles);
#if TRACE
                    PLLog.Trace2("End Fill DBLogFiles", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBLogFileInfo)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBLogFileInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBLogFileInfoTA.Fill(applicationDS.DBLogFileInfo);
#if TRACE
                    PLLog.Trace2("End Fill DBLogFileInfo", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBRoles)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBRoles", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBRolesTA.Fill(applicationDS.DBRoles);
                }
#if TRACE
                    PLLog.Trace2("End Fill DBRoles", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif

                if (Config.ADSLoad_DBStoredProcedures)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBStoredProcedures", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBStoredProceduresTA.Fill(applicationDS.DBStoredProcedures);

#if TRACE
                    PLLog.Trace2("End Fill DBStoredProcedures", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBTables)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBTables", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBTablesTA.Fill(applicationDS.DBTables);
#if TRACE
                    PLLog.Trace2("End Fill DBTables", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBUserDefinedFunctions)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBUserDefinedFunctions", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBUserDefinedFunctionsTA.Fill(applicationDS.DBUserDefinedFunctions);
#if TRACE
                    PLLog.Trace2("End Fill DBUserDefinedFunctions", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBUsers)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBUsers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBUsersTA.Fill(applicationDS.DBUsers); ;
#if TRACE
                    PLLog.Trace2("End Fill DBUsers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_DBViews)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill DBViews", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    DBViewsTA.Fill(applicationDS.DBViews);
#if TRACE
                    PLLog.Trace2("End Fill DBViews", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_TBTriggers)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill TBTriggers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    TBTriggersTA.Fill(applicationDS.TBTriggers); ;
#if TRACE
                    PLLog.Trace2("End Fill TBTriggers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

                if (Config.ADSLoad_VWTriggers)
                {
#if TRACE
                    startTicks = PLLog.Trace2("Fill VWTriggers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                    VWTriggersTA.Fill(applicationDS.VWTriggers);
#if TRACE
                    PLLog.Trace2("End Fill VWTriggers", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
                }

#if TRACE
                startTicks = PLLog.Trace2("Fill LKUP_ADDomains", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                LU_ADDomainsTA.Fill(applicationDS.LKUP_ADDomains);
#if TRACE
                PLLog.Trace2("End Fill LKUP_ADDomains", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif

#if TRACE
                startTicks = PLLog.Trace2("Fill LKUP_Environments", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                LU_EnvironmentsTA.Fill(applicationDS.LKUP_Environments);
#if TRACE
                PLLog.Trace2("End Fill LKUP_Environments", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif

#if TRACE
                startTicks = PLLog.Trace2("Fill LKUP_SQLServerVersions", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
                LU_SQLServerVersionsTA.Fill(applicationDS.LKUP_SQLServerVersions);
#if TRACE
                PLLog.Trace2("End Fill LKUP_SQLServerVersions", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif

            }
            catch (Exception ex)
            {
                PLLog.Error(string.Format("ConnectionString:>{0}<", Config.SQLMonitorDBConnection), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
            }

#if TRACE
            PLLog.Trace("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicksTotal);
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
            newRow.ExpandJobs = true;
            newRow.ExpandLogins = true;
            newRow.ExpandServerRoles = true;
            newRow.ExpandTriggers = true;

            return newRow;
        }

    }

}

namespace SQLInformation.Data.ApplicationDataSetTableAdapters {
    
    
    public partial class DBDataFilesTableAdapter {
    }
}
