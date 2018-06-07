using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO=Microsoft.SqlServer.Management.Smo;
using PacificLife.Life;
using SQLInformation;

namespace SQLInformation.SMO
{
    public class Helper
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "SQLINFOAGENT";

        private static DateTime SQLMinDateTime = new DateTime(1753, 1, 1);

        #region Public Methods

        public static MSMO.Server Get_Server(string instanceName)
        {
            MSMO.Server server = new MSMO.Server(instanceName);
            server.ConnectionContext.ConnectTimeout = Data.Config.SQLInformationAgent_ConnectionTimeout;

            if ( ! Data.Config.SQLInformationAgent_LoginSecure)
            {
                server.ConnectionContext.LoginSecure = false;   // SQL Authentication
                server.ConnectionContext.Login = Data.Config.SQLInformationAgent_UserName;
                server.ConnectionContext.Password = Data.Config.SQLInformationAgent_Password;
            }

            return server;
        }

  
        #region Load Collections

        public static void Load_Databases_FromSMO(MSMO.Server server, Guid instanceID, ExpandMask.DatabaseExpandSetting databaseExpandSettings)
        {
#if TRACE
            long startTicks = PLLog.Trace1("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.Database database in server.Databases)
            {
                System.Diagnostics.Debug.WriteLine("  Database:" + database.Name);

                SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow = Get_Database_FromSMO(instanceID, server.Name, database);

                try
                {
                    if (databaseRow.IsMonitored && databaseExpandSettings.IsMonitored)
                    {
                        if (databaseRow.ExpandStoredProcedures && databaseExpandSettings.ExpandStoredProcedures)
                        {
                        	Load_DBStoredProcedures_FromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandTables && databaseExpandSettings.ExpandTables)
                        {
                            Load_DBTables_FromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandViews && databaseExpandSettings.ExpandViews)
                        {
                        	Load_DBViews_FromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandFileGroups && databaseExpandSettings.ExpandFileGroups)
                        {
                            Load_DBDFileGroups_FromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandLogFiles && databaseExpandSettings.ExpandLogFiles)
                        {
                            Load_DBLogFiles_FromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandRoles && databaseExpandSettings.ExpandRoles)
                        {
                            Load_DBRoles_FromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandTriggers && databaseExpandSettings.ExpandTriggers)
                        {
                            Load_DBDdlTriggers_FromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandUserDefinedFunctions && databaseExpandSettings.ExpandUserDefinedFunctions)
                        {
                            Load_DBUserDefinedFunctions_FromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandUsers && databaseExpandSettings.ExpandUsers)
                        {
                            Load_DBUsers_FromSMO(database, databaseRow.ID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }

            }
#if TRACE
            PLLog.Trace1("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBDataFiles_FromSMO(MSMO.FileGroup fileGroup, Guid fileGroupID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBDataFilesRow newDataFileRow = null;

            foreach (MSMO.DataFile dataFile in fileGroup.Files)
            {
                System.Diagnostics.Debug.WriteLine("    DB DataFile:" + dataFile.Name);

                Get_DBDataFile_FromSMO(fileGroupID, fileGroup.Name, dataFile);
            }

#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBDdlTriggers_FromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.DatabaseDdlTrigger ddlTrigger in database.Triggers)
            {
                if (ddlTrigger.IsSystemObject)
                {
                    continue;   // Skip System Tables
                }
                
                System.Diagnostics.Debug.WriteLine("    DB DdlTrigger:" + ddlTrigger.Name);

                Get_DBDdlTrigger_FromSMO(ddlTrigger, databaseID);

            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBDFileGroups_FromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.FileGroup fileGroup in database.FileGroups)
            {
                System.Diagnostics.Debug.WriteLine("    DB FileGroup:" + fileGroup.Name);

                Get_DBFileGroup_FromSMO(fileGroup, databaseID);
            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBLogFiles_FromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.LogFile logFile in database.LogFiles)
            {
                System.Diagnostics.Debug.WriteLine("    DB LogFile:" + logFile.Name);

                Get_DBLogFile_FromSMO(logFile, databaseID);
            }

#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBRoles_FromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.DatabaseRole databaseRole in database.Roles)
            {
                if(databaseRole.IsFixedRole)
                {
                    continue;   // Skip Fixed Roles
                }

                System.Diagnostics.Debug.WriteLine("    DB Role:" + databaseRole.Name);
                
                Get_DBRole_FromSMO(databaseRole, databaseID);
            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBStoredProcedures_FromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.StoredProcedure storedProcedure in database.StoredProcedures)
            {
                if(storedProcedure.IsSystemObject)
                {
                    continue;   // Skip System StoredProcedures
                }

                System.Diagnostics.Debug.WriteLine("    DB SP:" + storedProcedure.Name);

                Get_DBStoredProcedure_FromSMO(storedProcedure, databaseID);
            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBTables_FromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBTablesRow newDBTableRow = null;

            foreach (MSMO.Table table in database.Tables)
            {
                if (table.IsSystemObject)
                {
                    continue;   // Skip System Tables
                }

                System.Diagnostics.Debug.WriteLine("    DB Table:" + table.Name);

                newDBTableRow = Get_DBTable_FromSMO(table, databaseID);

                try
                {
                    Load_TBTriggers_FromSMO(table, newDBTableRow.ID);
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }
            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBUserDefinedFunctions_FromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.UserDefinedFunction userDefinedFunction in database.UserDefinedFunctions)
            {
                System.Diagnostics.Debug.WriteLine("    DB UDF:" + userDefinedFunction.Name);

                if (userDefinedFunction.IsSystemObject)
                {
                    continue;   // Skip System UDF
                }

                Get_DBUserDefinedFunction_FromSMO(userDefinedFunction, databaseID);
            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBUsers_FromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach(MSMO.User user in database.Users)
            {
                System.Diagnostics.Debug.WriteLine("    DB User:" + user.Name);

                if(user.IsSystemObject)
                {
                    continue;   // Skip System Users
                }

                Get_DBUser_FromSMO(user, databaseID); 
            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_DBViews_FromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.View view in database.Views)
            {
                if (view.IsSystemObject)
                {
                    continue;   // Skip System Views
                }

                System.Diagnostics.Debug.WriteLine("    DB View:" + view.Name);

                SQLInformation.Data.ApplicationDataSet.DBViewsRow viewRow = Get_DBView_FromSMO(databaseID, view);

                try
                {
                    Load_VWTriggers_FromSMO(view, viewRow.ID);
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }
            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_Instances_FromSMO(Data.ApplicationDataSet.InstancesDataTable instances, ExpandMask.InstanceExpandSetting instanceExpandSetting, ExpandMask.DatabaseExpandSetting databaseExpandSetting)
        {
#if TRACE
            long startTicks = PLLog.Trace("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (Data.ApplicationDataSet.InstancesRow instanceRow in instances)
            {
                if (instanceRow.IsMonitored && instanceExpandSetting.IsMonitored)
                {
                    System.Diagnostics.Debug.WriteLine("Monitoring Instance:" + instanceRow.Name_Instance);

                    Get_Instance_FromSMO(instanceRow, instanceExpandSetting, databaseExpandSetting);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Not Monitored Instance:" + instanceRow.Name_Instance);
                    continue;
                }
            }

#if TRACE
            PLLog.Trace("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_Logins_FromSMO(MSMO.Server instance, Guid instanceID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach(MSMO.Login login in instance.Logins)
            {
                if (login.IsSystemObject)
                {
                    continue;   // Skip System Tables
                }

                System.Diagnostics.Debug.WriteLine(" I Login:" + login.Name);

                Get_Login_FromSMO(instanceID, login);
            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_ServerRoles_FromSMO(MSMO.Server instance, Guid instanceID)
        {
#if TRACE
            long startTicks = PLLog.Trace2("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach(MSMO.ServerRole serverRole in instance.Roles)
            {
                //try
                //{
                //    if (serverRole.IsFixedRole)
                //    {
                //        continue;   // Skip Fixed Roles
                //    }
                //}
                //catch (Exception ex)
                //{
                //    // Not available on SQL 2000 :(
                //}

                System.Diagnostics.Debug.WriteLine(" I ServerRole:" + serverRole.Name);

                Get_ServerRole_FromSMO(serverRole, instanceID);
            }
#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_TBTriggers_FromSMO(MSMO.Table table, Guid tableID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.Trigger trigger in table.Triggers)
            {
                if (trigger.IsSystemObject)
                {
                    continue;   // Skip System Triggers
                }

                System.Diagnostics.Debug.WriteLine("      TB Trigger:" + trigger.Name);

                Get_TBTrigger_FromSMO(trigger, tableID);
            }
#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void Load_VWTriggers_FromSMO(MSMO.View view, Guid viewID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.Trigger trigger in view.Triggers)
            {
                if (trigger.IsSystemObject)
                {
                    continue;   // Skip System Triggers
                }

                System.Diagnostics.Debug.WriteLine("       VW Trigger:" + trigger.Name);

                Get_VWTrigger_FromSMO(trigger, viewID);
            }
#if TRACE
            PLLog.Trace3("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        #endregion      

        #region Get Individual Item
        
        private static string GetInstanceName(Guid instanceID)
        {
            var ins = from instance in Common.ApplicationDataSet.Instances
                      where instance.ID == instanceID
                      select instance;

            return ins.First().Name_Instance;
        }

        public static SQLInformation.Data.ApplicationDataSet.DatabasesRow Get_Database_FromSMO(Guid instanceID, string instanceName, MSMO.Database database)
        {
#if TRACE
            long startTicks = PLLog.Trace2(string.Format("Enter ({0})", database.Name), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            Guid databaseID = default(Guid);

            SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow = null;

            var dbs = from db in Common.ApplicationDataSet.Databases
                      where db.Instance_ID == instanceID
                      select db;

            var dbs2 = from db2 in dbs
                       where db2.Name_Database == database.Name
                       select db2;

            if (dbs2.Count() > 0)
            {
                // We have found a database with a matching name.
                // TODO(crhodes): There should only be one.

                try
                {
                    foreach (var item in dbs2)
                    {
                        databaseRow = item;

                        if (databaseRow.IsMonitored)
                        {
                            // Update the main entry

                            database.UpdateDataSet(item);
                            databaseRow.SnapShotDate = DateTime.Now;
                            Common.ApplicationDataSet.DatabasesTA.Update(Common.ApplicationDataSet.Databases);

                            // Add add the Snapshot

                            TakeDatabaseSnapShot(databaseRow);
                        }
                    }
                }
                catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
                {
                    databaseRow.SnapShotDate = DateTime.Now;
                    databaseRow.SnapShotError = "ConnectionFailureException";
                    //ta.Update(Common.ApplicationDataSet.Instances);
                    Common.ApplicationDataSet.DatabasesTA.Update(Common.ApplicationDataSet.Databases);
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }
                catch (Exception ex)
                {
                    databaseRow.SnapShotDate = DateTime.Now;
                    databaseRow.SnapShotError = ex.ToString().Substring(1, 256);
                    //ta.Update(Common.ApplicationDataSet.Instances);
                    Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }
            }
            else
            {
                try
                {
                    // Must be a Database we haven't seen.
                    databaseRow = Common.ApplicationDataSet.Databases.NewDatabasesRow();
                    databaseRow.ID = Guid.NewGuid();

                    databaseRow.Name_Database = database.Name;

                    databaseRow.CreateDate = database.CreateDate;

                    try
                    {
                        databaseRow.DataBaseGuid = database.DatabaseGuid.ToString();
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                        databaseRow.DataBaseGuid = "<Not Available>";
                    }

                    databaseRow.ID_DB = database.ID;
                    databaseRow.Instance_ID = instanceID;
                    databaseRow.Name_Instance = GetInstanceName(instanceID);
                    databaseRow.Name_Instance = instanceName;

                    try
                    {
                        databaseRow.LastBackupDate = (database.LastBackupDate > SQLMinDateTime ? database.LastBackupDate : SQLMinDateTime);
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }
                    try
                    {
                        databaseRow.LastDifferentialBackupDate = (database.LastDifferentialBackupDate > SQLMinDateTime ? database.LastDifferentialBackupDate : SQLMinDateTime);
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }
                    try
                    {
                        databaseRow.LastLogBackupDate = (database.LastLogBackupDate > SQLMinDateTime ? database.LastLogBackupDate : SQLMinDateTime);
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    databaseRow.RecoveryModel = database.RecoveryModel.ToString();

                    // TODO(crhodes): Think through how to set this.

                    ExpandMask.DatabaseExpandSetting expandSetting = new ExpandMask.DatabaseExpandSetting(instanceID);

                    databaseRow.IsMonitored = expandSetting.IsMonitored;

                    databaseRow.ExpandDataFiles = expandSetting.ExpandDataFiles;
                    databaseRow.ExpandFileGroups = expandSetting.ExpandFileGroups;
                    databaseRow.ExpandLogFiles = expandSetting.ExpandLogFiles;
                    databaseRow.ExpandRoles = expandSetting.ExpandRoles;
                    databaseRow.ExpandStoredProcedures = expandSetting.ExpandStoredProcedures;
                    databaseRow.ExpandTables = expandSetting.ExpandTables;
                    databaseRow.ExpandTriggers = expandSetting.ExpandTriggers;
                    databaseRow.ExpandUserDefinedFunctions = expandSetting.ExpandUserDefinedFunctions;
                    databaseRow.ExpandUsers = expandSetting.ExpandUsers;
                    databaseRow.ExpandViews = expandSetting.ExpandViews;

                    databaseRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.Databases.AddDatabasesRow(databaseRow);
                    Common.ApplicationDataSet.DatabasesTA.Update(Common.ApplicationDataSet.Databases);
                }
                catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
                {
                    databaseRow.SnapShotDate = DateTime.Now;
                    databaseRow.SnapShotError = "ConnectionFailureException";
                    //ta.Update(Common.ApplicationDataSet.Instances);
                    Common.ApplicationDataSet.DatabasesTA.Update(Common.ApplicationDataSet.Databases);
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }
                catch (Exception ex)
                {
                    databaseRow.SnapShotDate = DateTime.Now;
                    databaseRow.SnapShotError = ex.ToString().Substring(1,256);
                    //ta.Update(Common.ApplicationDataSet.Instances);
                    Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }
            }

#if TRACE
            PLLog.Trace2("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
            return databaseRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.DBDataFilesRow Get_DBDataFile_FromSMO(Guid fileGroupID, string fileGroupName, MSMO.DataFile dataFile)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBDataFilesRow newDataFileRow = null;

            var tbs = from tb in Common.ApplicationDataSet.DBDataFiles
                        where tb.FileGroup_ID == fileGroupID
                        select tb;

            var tbs2 = from tb2 in tbs
                        where tb2.Name_DataFile == dataFile.Name
                        select tb2;

            if (tbs2.Count() > 0)
            {
                try
                {
                    foreach (var item in tbs2)
                    {
                        newDataFileRow = item;
                        dataFile.UpdateDataSet(newDataFileRow);
                        newDataFileRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);

                        // Add add the Snapshot

                        TakeDBDataFileSnapShot(newDataFileRow);
                    }
                }
                catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
                {
                    newDataFileRow.SnapShotDate = DateTime.Now;
                    newDataFileRow.SnapShotError = "ConnectionFailureException";
                    Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }
                catch (Exception ex)
                {
                    newDataFileRow.SnapShotDate = DateTime.Now;
                    newDataFileRow.SnapShotError = ex.ToString().Substring(1, 256);
                    Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }
            }
            else
            {
                // Must be a DataFile we haven't seen.
                try
                {
                    newDataFileRow = Common.ApplicationDataSet.DBDataFiles.NewDBDataFilesRow();

                    newDataFileRow.ID = Guid.NewGuid();
                    newDataFileRow.FileGroup_ID = fileGroupID;
                    newDataFileRow.ID_DataFile = dataFile.ID;
                    newDataFileRow.Name_DataFile = dataFile.Name;
                    newDataFileRow.AvailableSpace = dataFile.AvailableSpace;

                    try
                    {
                        newDataFileRow.BytesReadFromDisk = (int)dataFile.BytesReadFromDisk; // TODO(crhodes)
                    }
                    catch(Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    try
                    {
                        newDataFileRow.BytesWrittenToDisk = (int)dataFile.BytesWrittenToDisk; // TODO(crhodes)
                    }
                    catch(Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1); 
                    }

                    newDataFileRow.FileName = dataFile.FileName;
                    newDataFileRow.Growth = (int)dataFile.Growth;  // TODO(crhodes)
                    newDataFileRow.GrowthType = dataFile.GrowthType.ToString();
                    newDataFileRow.ID_DataFile = dataFile.ID;
                    newDataFileRow.IsPrimaryFile = dataFile.IsPrimaryFile;
                    newDataFileRow.MaxSize = dataFile.MaxSize;

                    try
                    {
                        newDataFileRow.NumberOfDiskReads = (int)dataFile.NumberOfDiskReads; // TODO(crhodes)
                    }
                    catch(Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    try
                    {
                        newDataFileRow.NumberOfDiskWrites = (int)dataFile.NumberOfDiskWrites; // TODO(crhodes)
                    }
                    catch(Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1); 
                    }

                    newDataFileRow.Size = dataFile.Size;
                    newDataFileRow.UsedSpace = dataFile.UsedSpace;

                    try
                    {
                        newDataFileRow.VolumeFreeSpace = dataFile.VolumeFreeSpace;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    newDataFileRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.DBDataFiles.AddDBDataFilesRow(newDataFileRow);
                    Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);
                    //ta.Update(Common.ApplicationDataSet.DBDataFiles);
                }
                catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
                {
                    newDataFileRow.SnapShotDate = DateTime.Now;
                    newDataFileRow.SnapShotError = "ConnectionFailureException";
                    Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }
                catch (Exception ex)
                {
                    newDataFileRow.SnapShotDate = DateTime.Now;
                    newDataFileRow.SnapShotError = ex.ToString().Substring(1, 256);
                    Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }
            }
#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
            return newDataFileRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.DBDdlTriggersRow Get_DBDdlTrigger_FromSMO(MSMO.DatabaseDdlTrigger ddlTrigger, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBDdlTriggersRow newDBDdlTriggerRow = null;

            var tbs = from tb in Common.ApplicationDataSet.DBDdlTriggers
                      where tb.Database_ID == databaseID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_Trigger == ddlTrigger.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                // Found a table with a matching name.  Update the things that might have changed.

                try
                {
                    foreach (var item in tbs2)
                    {
                        newDBDdlTriggerRow = item;

                        ddlTrigger.UpdateDataSet(newDBDdlTriggerRow);
                        newDBDdlTriggerRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.DBDdlTriggersTA.Update(Common.ApplicationDataSet.DBDdlTriggers);
                        //ta.Update(Common.ApplicationDataSet.DBDdlTriggers);
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a new Trigger we haven't seen.
                    newDBDdlTriggerRow = Common.ApplicationDataSet.DBDdlTriggers.NewDBDdlTriggersRow();

                    newDBDdlTriggerRow.ID = Guid.NewGuid();   // See if this is available from table.
                    newDBDdlTriggerRow.Name_Trigger = ddlTrigger.Name;
                    //newTrigger.Table_ID = trigger.ID.ToString();
                    newDBDdlTriggerRow.Database_ID = databaseID;  // From above
                    newDBDdlTriggerRow.CreateDate = ddlTrigger.CreateDate;

                    try
                    {
                        newDBDdlTriggerRow.DateLastModified = ddlTrigger.DateLastModified;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    newDBDdlTriggerRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.DBDdlTriggers.AddDBDdlTriggersRow(newDBDdlTriggerRow);
                    Common.ApplicationDataSet.DBDdlTriggersTA.Update(Common.ApplicationDataSet.DBDdlTriggers);

                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
            return newDBDdlTriggerRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.DBFileGroupsRow Get_DBFileGroup_FromSMO(MSMO.FileGroup fileGroup, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBFileGroupsRow newDBFileGroupRow = null;

            var tbs = from tb in Common.ApplicationDataSet.DBFileGroups
                      where tb.Database_ID == databaseID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_FileGroup == fileGroup.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                try
                {
                    newDBFileGroupRow = tbs2.First();

                    newDBFileGroupRow.SnapShotDate = DateTime.Now;
                    // Need to update before calling LoadDBDataFiles_FromSMO
                    Common.ApplicationDataSet.DBFileGroupsTA.Update(Common.ApplicationDataSet.DBFileGroups);

                    Load_DBDataFiles_FromSMO(fileGroup, newDBFileGroupRow.ID);

                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a FileGroup we haven't seen.
                    SQLInformation.Data.ApplicationDataSet.DBFileGroupsRow newFileGroup = Common.ApplicationDataSet.DBFileGroups.NewDBFileGroupsRow();

                    newFileGroup.ID = Guid.NewGuid();
                    newFileGroup.Database_ID = databaseID;
                    newFileGroup.Name_FileGroup = fileGroup.Name;
                    newFileGroup.Size = fileGroup.Size;

                    Common.ApplicationDataSet.DBFileGroups.AddDBFileGroupsRow(newFileGroup);

                    //fileGroupID = newFileGroup.ID;

                    newFileGroup.SnapShotDate = DateTime.Now;
                    // Need to update before calling LoadDBDataFiles_FromSMO
                    Common.ApplicationDataSet.DBFileGroupsTA.Update(Common.ApplicationDataSet.DBFileGroups);

                    Load_DBDataFiles_FromSMO(fileGroup, newFileGroup.ID);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
            return newDBFileGroupRow;
        }

        public static SQLInformation.Data.ApplicationDataSet.DBLogFilesRow Get_DBLogFile_FromSMO(MSMO.LogFile logFile, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            SQLInformation.Data.ApplicationDataSet.DBLogFilesRow newDBLogFileRow = null;

            var tbs = from tb in Common.ApplicationDataSet.DBLogFiles
                      where tb.Database_ID == databaseID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_LogFile == logFile.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                try
                {
                    foreach (var item in tbs2)
                    {
                        newDBLogFileRow = item;
                        logFile.UpdateDataSet(newDBLogFileRow);
                        newDBLogFileRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.DBLogFilesTA.Update(Common.ApplicationDataSet.DBLogFiles);

                        // Add the Snapshot

                        TakeDBLogFileSnapShot(newDBLogFileRow);
                    }
                }
                catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
                {
                    newDBLogFileRow.SnapShotDate = DateTime.Now;
                    newDBLogFileRow.SnapShotError = "ConnectionFailureException";
                    Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }
                catch (Exception ex)
                {
                    newDBLogFileRow.SnapShotDate = DateTime.Now;
                    newDBLogFileRow.SnapShotError = ex.ToString().Substring(1, 256);
                    Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }
            }
            else
            {
                // Must be a LogFile we haven't seen.
                try
                {
                    newDBLogFileRow = Common.ApplicationDataSet.DBLogFiles.NewDBLogFilesRow();

                    newDBLogFileRow.ID = Guid.NewGuid();
                    newDBLogFileRow.Database_ID = databaseID;
                    newDBLogFileRow.Name_LogFile = logFile.Name;

                    try
                    {
                        newDBLogFileRow.BytesReadFromDisk = (int)logFile.BytesReadFromDisk; // TODO(crhodes)
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }
                    try
                    {
                        newDBLogFileRow.BytesWrittenToDisk = (int)logFile.BytesWrittenToDisk; // TODO(crhodes)
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    newDBLogFileRow.FileName = logFile.FileName;
                    newDBLogFileRow.Growth = (int)logFile.Growth;  // TODO(crhodes)
                    newDBLogFileRow.GrowthType = logFile.GrowthType.ToString();
                    //newLogFile.ID_LogFile = logFile.ID;
                    //newLogFile.IsPrimaryFile = logFile.IsPrimaryFile;
                    newDBLogFileRow.MaxSize = (int)logFile.MaxSize;

                    try
                    {
                        newDBLogFileRow.NumberOfDiskReads = (int)logFile.NumberOfDiskReads; // TODO(crhodes)
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }
                    try
                    {
                        newDBLogFileRow.NumberOfDiskWrites = (int)logFile.NumberOfDiskWrites; // TODO(crhodes)
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    newDBLogFileRow.Size = (int)logFile.Size;
                    newDBLogFileRow.UsedSpace = (int)logFile.UsedSpace;

                    try
                    {
                        newDBLogFileRow.VolumeFreeSpace = logFile.VolumeFreeSpace;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    newDBLogFileRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.DBLogFiles.AddDBLogFilesRow(newDBLogFileRow);
                    Common.ApplicationDataSet.DBLogFilesTA.Update(Common.ApplicationDataSet.DBLogFiles);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }

#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return newDBLogFileRow;
        }

        public static SQLInformation.Data.ApplicationDataSet.DBRolesRow Get_DBRole_FromSMO(MSMO.DatabaseRole databaseRole, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBRolesRow newDBRoleRow = null;

            var tbs = from tb in Common.ApplicationDataSet.DBRoles
                      where tb.Database_ID == databaseID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_Role == databaseRole.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                try
                {
                    foreach (var item in tbs2)
                    {
                        newDBRoleRow = item;
                        databaseRole.UpdateDataSet(newDBRoleRow);
                        newDBRoleRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.DBRolesTA.Update(Common.ApplicationDataSet.DBRoles);
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a Role we haven't seen.
                    newDBRoleRow = Common.ApplicationDataSet.DBRoles.NewDBRolesRow();

                    newDBRoleRow.ID = Guid.NewGuid();
                    newDBRoleRow.Database_ID = databaseID;
                    newDBRoleRow.IsFixedRole = databaseRole.IsFixedRole;
                    newDBRoleRow.Name_Role = databaseRole.Name;
                    newDBRoleRow.CreateDate = databaseRole.CreateDate;
                    newDBRoleRow.DateLastModified = databaseRole.DateLastModified;

                    newDBRoleRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.DBRoles.AddDBRolesRow(newDBRoleRow);
                    Common.ApplicationDataSet.DBRolesTA.Update(Common.ApplicationDataSet.DBRoles);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }

#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return newDBRoleRow;
        }

        public static SQLInformation.Data.ApplicationDataSet.DBStoredProceduresRow Get_DBStoredProcedure_FromSMO(MSMO.StoredProcedure storedProcedure, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBStoredProceduresRow newDBStoredProcedureRow = null;

            var sps = from sp in Common.ApplicationDataSet.DBStoredProcedures
                      where sp.Database_ID == databaseID
                      select sp;

            var sps2 = from sp2 in sps
                       where sp2.Name_StoredProcedure == storedProcedure.Name
                       select sp2;

            if (sps2.Count() > 0)
            {
                // Found a table with a matching name.  Update the things that might have changed.

                try
                {
                    foreach (var item in sps2)
                    {
                        newDBStoredProcedureRow = item;

                        storedProcedure.UpdateDataSet(newDBStoredProcedureRow);
                        newDBStoredProcedureRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.DBStoredProceduresTA.Update(Common.ApplicationDataSet.DBStoredProcedures);
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a new StoredProcedure we haven't seen.
                    newDBStoredProcedureRow = Common.ApplicationDataSet.DBStoredProcedures.NewDBStoredProceduresRow();

                    newDBStoredProcedureRow.ID = Guid.NewGuid();   // See if this is available from SP.
                    newDBStoredProcedureRow.Name_StoredProcedure = storedProcedure.Name;
                    newDBStoredProcedureRow.StoredProcedure_ID = storedProcedure.ID.ToString(); ;
                    newDBStoredProcedureRow.Database_ID = databaseID;  // From above
                    newDBStoredProcedureRow.Owner = storedProcedure.Owner;
                    newDBStoredProcedureRow.CreateDate = storedProcedure.CreateDate;
                    newDBStoredProcedureRow.IsSystemObject = storedProcedure.IsSystemObject;

                    // Loading the body of the Stored Procedure is expensive.  Optional.

                    if (Data.Config.ADSLoad_DBStoredProcedureContent)
                    {
                        try
                        {
                            newDBStoredProcedureRow.MethodName = storedProcedure.MethodName;
                        }
                        catch (Exception ex)
                        {
                            newDBStoredProcedureRow.MethodName = "<Not Available>";
                        }
                        try
                        {
                            newDBStoredProcedureRow.TextHeader = storedProcedure.TextHeader;
                        }
                        catch (Exception ex)
                        {
                            newDBStoredProcedureRow.TextHeader = "<No Access>";
                        }

                        try
                        {
                            newDBStoredProcedureRow.TextBody = storedProcedure.TextBody;
                        }
                        catch (Exception ex)
                        {
                            newDBStoredProcedureRow.TextBody = "<No Access>";
                        }
                    }

                    try
                    {
                        newDBStoredProcedureRow.DateLastModified = storedProcedure.DateLastModified;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    newDBStoredProcedureRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.DBStoredProcedures.AddDBStoredProceduresRow(newDBStoredProcedureRow);
                    Common.ApplicationDataSet.DBStoredProceduresTA.Update(Common.ApplicationDataSet.DBStoredProcedures);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return newDBStoredProcedureRow;
        }

        public static SQLInformation.Data.ApplicationDataSet.DBTablesRow Get_DBTable_FromSMO(MSMO.Table table, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBTablesRow newDBTableRow = null;

            var tbs = from tb in Common.ApplicationDataSet.DBTables
                      where tb.Database_ID == databaseID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_Table == table.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                // Found a table with a matching name.  Update the things that might have changed.

                try
                {
                    foreach (var item in tbs2)
                    {
                        newDBTableRow = item;
                        table.UpdateDataSet(newDBTableRow);
                        newDBTableRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.DBTablesTA.Update(Common.ApplicationDataSet.DBTables);
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a new table we haven't seen.
                    newDBTableRow = Common.ApplicationDataSet.DBTables.NewDBTablesRow();
                    newDBTableRow.ID = Guid.NewGuid();
                    newDBTableRow.Database_ID = databaseID;  // From above

                    newDBTableRow.Name_Table = table.Name;
                    newDBTableRow.CreateDate = table.CreateDate;
                    newDBTableRow.DataSpaceUsed = table.DataSpaceUsed;

                    try
                    {
                        newDBTableRow.DateLastModified = table.DateLastModified;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    newDBTableRow.HasIndex = table.HasIndex;
                    newDBTableRow.Owner = table.Owner;
                    newDBTableRow.RowCount = table.RowCount;   // TODO(crhodes): Fix DB Schema to match
                    newDBTableRow.Table_ID = table.ID.ToString();

                    newDBTableRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.DBTables.AddDBTablesRow(newDBTableRow);
                    Common.ApplicationDataSet.DBTablesTA.Update(Common.ApplicationDataSet.DBTables);

                    // TODO(crhodes) extract Extended Properties
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }

#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return newDBTableRow;
        }

        public static SQLInformation.Data.ApplicationDataSet.DBUserDefinedFunctionsRow Get_DBUserDefinedFunction_FromSMO(MSMO.UserDefinedFunction userDefinedFunction, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBUserDefinedFunctionsRow newDBUserDefinedFunctionRow = null;

            var tbs = from tb in Common.ApplicationDataSet.DBUserDefinedFunctions
                      where tb.Database_ID == databaseID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_UserDefinedFunction == userDefinedFunction.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                try
                {
                    foreach (var item in tbs2)
                    {
                        newDBUserDefinedFunctionRow = item;
                        userDefinedFunction.UpdateDataSet(newDBUserDefinedFunctionRow);
                        newDBUserDefinedFunctionRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.DBUserDefinedFunctionsTA.Update(Common.ApplicationDataSet.DBUserDefinedFunctions);
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a Role we haven't seen.
                    newDBUserDefinedFunctionRow = Common.ApplicationDataSet.DBUserDefinedFunctions.NewDBUserDefinedFunctionsRow();

                    newDBUserDefinedFunctionRow.ID = Guid.NewGuid();
                    newDBUserDefinedFunctionRow.Database_ID = databaseID;
                    newDBUserDefinedFunctionRow.IsSystemObject = userDefinedFunction.IsSystemObject;
                    newDBUserDefinedFunctionRow.Name_UserDefinedFunction = userDefinedFunction.Name;
                    newDBUserDefinedFunctionRow.CreateDate = userDefinedFunction.CreateDate;
                    newDBUserDefinedFunctionRow.DateLastModified = userDefinedFunction.DateLastModified;

                    newDBUserDefinedFunctionRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.DBUserDefinedFunctions.AddDBUserDefinedFunctionsRow(newDBUserDefinedFunctionRow);
                    Common.ApplicationDataSet.DBUserDefinedFunctionsTA.Update(Common.ApplicationDataSet.DBUserDefinedFunctions);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }

#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return newDBUserDefinedFunctionRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.DBUsersRow Get_DBUser_FromSMO(MSMO.User user, Guid databaseID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBUsersRow newDBUserRow = null;

            var tbs = from tb in Common.ApplicationDataSet.DBUsers
                      where tb.Database_ID == databaseID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_User == user.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                try
                {
                    foreach (var item in tbs2)
                    {
                        newDBUserRow = item;
                        user.UpdateDataSet(newDBUserRow);
                        newDBUserRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.DBUsersTA.Update(Common.ApplicationDataSet.DBUsers);
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a Role we haven't seen.
                    newDBUserRow = Common.ApplicationDataSet.DBUsers.NewDBUsersRow();

                    newDBUserRow.ID = Guid.NewGuid();
                    newDBUserRow.Database_ID = databaseID;
                    newDBUserRow.IsSystemObject = user.IsSystemObject;
                    newDBUserRow.Name_User = user.Name;
                    newDBUserRow.CreateDate = user.CreateDate;
                    newDBUserRow.DateLastModified = user.DateLastModified;

                    newDBUserRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.DBUsers.AddDBUsersRow(newDBUserRow);
                    Common.ApplicationDataSet.DBUsersTA.Update(Common.ApplicationDataSet.DBUsers);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return newDBUserRow;
        }

        public static SQLInformation.Data.ApplicationDataSet.DBViewsRow Get_DBView_FromSMO(Guid databaseID, MSMO.View view)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            Guid viewID = Guid.Empty;

            SQLInformation.Data.ApplicationDataSet.DBViewsRow viewRow = null;

            var vs = from v in Common.ApplicationDataSet.DBViews
                     where v.Database_ID == databaseID
                     select v;

            var vs2 = from v2 in vs
                      where v2.Name_View == view.Name
                      select v2;

            if (vs2.Count() > 0)
            {
                // Found a view with a matching name.  Update the things that might have changed.

                try
                {
                    foreach (var item in vs2)
                    {
                        viewRow = item;

                        view.UpdateDataSet(viewRow);
                        viewRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.DBViewsTA.Update(Common.ApplicationDataSet.DBViews);
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a view we haven't seen.
                    viewRow = Common.ApplicationDataSet.DBViews.NewDBViewsRow();
                    viewRow.ID = Guid.NewGuid();
                    viewRow.Name_View = view.Name;
                    viewRow.View_ID = view.ID;
                    viewRow.Database_ID = databaseID;  // From above
                    viewRow.Owner = view.Owner;
                    viewRow.CreateDate = view.CreateDate;
                    viewRow.SnapShotDate = DateTime.Now;
                    //newView.DataSpaceUsed = int.Parse(viewH.DataSpaceUsed);   // This is not supported yet.

                    try
                    {
                        viewRow.DateLastModified = view.DateLastModified;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    Common.ApplicationDataSet.DBViews.AddDBViewsRow(viewRow);
                    Common.ApplicationDataSet.DBViewsTA.Update(Common.ApplicationDataSet.DBViews);

                    //viewID = viewRow.ID;
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }

#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
            return viewRow;
        }

        public static void Get_Instance_FromSMO(Data.ApplicationDataSet.InstancesRow instanceRow, ExpandMask.InstanceExpandSetting instanceExpandSetting, ExpandMask.DatabaseExpandSetting databaseExpandSetting)
        {
#if TRACE
            long startTicks = PLLog.Trace1(string.Format("Enter ({0})", instanceRow.Name_Instance), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            try
            {
                MSMO.Server server = SQLInformation.SMO.Helper.Get_Server(instanceRow.Name_Instance);
                server.UpdateDataSet(instanceRow);

                // Won't get here if instance not accessible.

                instanceRow.SnapShotDate = DateTime.Now;
                Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
                //ta.Update(Common.ApplicationDataSet.Instances);

                //if (instanceRow.ExpandLogins)
                if (instanceRow.ExpandLogins && instanceExpandSetting.ExpandLogins)
                {
                	Load_Logins_FromSMO(server, instanceRow.ID);
                }

                //if (instanceRow.ExpandServerRoles)
                if (instanceRow.ExpandServerRoles && instanceExpandSetting.ExpandServerRoles)
                {
                	Load_ServerRoles_FromSMO(server, instanceRow.ID);
                }

                if ((instanceRow.ExpandContent && instanceExpandSetting.ExpandContent) 
                    || (instanceRow.ExpandStorage && instanceExpandSetting.ExpandStorage))
                {
                    Load_Databases_FromSMO(server, instanceRow.ID, databaseExpandSetting);
                }
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
            {
                instanceRow.SnapShotDate = DateTime.Now;
                instanceRow.NetName = "ConnectionFailureException";
                //ta.Update(Common.ApplicationDataSet.Instances);
                Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
                PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
            }
            catch (Exception ex)
            {
                instanceRow.SnapShotDate = DateTime.Now;
                instanceRow.NetName = "Unknown Exception";
                //ta.Update(Common.ApplicationDataSet.Instances);
                Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
            }
#if TRACE
            PLLog.Trace1("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.LoginsRow Get_Login_FromSMO(Guid instanceID, MSMO.Login login)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.LoginsRow loginRow = null;

            var tbs = from tb in Common.ApplicationDataSet.Logins
                      where tb.Instance_ID == instanceID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_Login == login.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                try
                {
                    loginRow = tbs2.First();
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a Login we haven't seen.
                    loginRow = Common.ApplicationDataSet.Logins.NewLoginsRow();

                    loginRow.ID = Guid.NewGuid();
                    loginRow.Name_Login = login.Name;
                    loginRow.Instance_ID = instanceID;

                    loginRow.CreateDate = login.CreateDate;
                    loginRow.DateLastModified = login.DateLastModified;
                    loginRow.DefaultDatabase = login.DefaultDatabase;
                    loginRow.LoginType = login.LoginType.ToString();

                    loginRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.Logins.AddLoginsRow(loginRow);
                    Common.ApplicationDataSet.LoginsTA.Update(Common.ApplicationDataSet.Logins);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return loginRow;
        }

        public static SQLInformation.Data.ApplicationDataSet.ServerRolesRow Get_ServerRole_FromSMO(MSMO.ServerRole serverRole, Guid instanceID)
        {
#if TRACE
            long startTicks = PLLog.Trace3("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.ServerRolesRow newServerRoleRow = null;

            var tbs = from sr in Common.ApplicationDataSet.ServerRoles
                      where sr.Instance_ID == instanceID
                      select sr;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_ServerRole == serverRole.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                // We have found a ServerRole with a matching name.

                try
                {
                    foreach (var item in tbs2)
                    {
                        newServerRoleRow = item;
                        serverRole.UpdateDataSet(newServerRoleRow);
                        newServerRoleRow.SnapShotDate = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a ServerRole we haven't seen.
                    newServerRoleRow = Common.ApplicationDataSet.ServerRoles.NewServerRolesRow();

                    newServerRoleRow.ID = Guid.NewGuid();

                    newServerRoleRow.Name_ServerRole = serverRole.Name;
                    newServerRoleRow.Instance_ID = instanceID;

                    // Not until SQL 2012
                    //try
                    //{
                    //    newServerRoleRow.CreateDate = serverRole.DateCreated;
                    //}
                    //catch (Exception ex)
                    //{
                    //    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    //}

                    //try
                    //{
                    //    newServerRoleRow.DateLastModified = serverRole.DateModified;
                    //}
                    //catch (Exception ex)
                    //{
                    //    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    //}
        
                    newServerRoleRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.ServerRoles.AddServerRolesRow(newServerRoleRow);
                    Common.ApplicationDataSet.ServerRolesTA.Update(Common.ApplicationDataSet.ServerRoles);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
#if TRACE
            PLLog.Trace3("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return newServerRoleRow;
        }

        public static SQLInformation.Data.ApplicationDataSet.TBTriggersRow Get_TBTrigger_FromSMO(MSMO.Trigger trigger, Guid tableID)
        {
#if TRACE
            long startTicks = PLLog.Trace4("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.TBTriggersRow newTBTriggerRow = null;

            var tbs = from tb in Common.ApplicationDataSet.TBTriggers
                      where tb.DBTable_ID == tableID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_Trigger == trigger.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                // Found a table with a matching name.  Update the things that might have changed.

                try
                {
                    foreach (var item in tbs2)
                    {
                        newTBTriggerRow = item;
                        trigger.UpdateDataSet(newTBTriggerRow);
                        newTBTriggerRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.TBTriggersTA.Update(Common.ApplicationDataSet.TBTriggers);
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a new Trigger we haven't seen.
                    newTBTriggerRow = Common.ApplicationDataSet.TBTriggers.NewTBTriggersRow();
                    newTBTriggerRow.ID = Guid.NewGuid();   // See if this is available from table.
                    newTBTriggerRow.Name_Trigger = trigger.Name;

                    //newTrigger.Table_ID = trigger.ID.ToString();
                    newTBTriggerRow.DBTable_ID = tableID;  // From above
                    newTBTriggerRow.CreateDate = trigger.CreateDate;

                    try
                    {
                        newTBTriggerRow.DateLastModified = trigger.DateLastModified;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    newTBTriggerRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.TBTriggers.AddTBTriggersRow(newTBTriggerRow);
                    Common.ApplicationDataSet.TBTriggersTA.Update(Common.ApplicationDataSet.TBTriggers);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
#if TRACE
            PLLog.Trace4("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return newTBTriggerRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.VWTriggersRow Get_VWTrigger_FromSMO(MSMO.Trigger trigger, Guid viewID)
        {
#if TRACE
            long startTicks = PLLog.Trace4("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.VWTriggersRow newVWTriggerRow = null;

            var tbs = from tb in Common.ApplicationDataSet.VWTriggers
                      where tb.DBView_ID == viewID
                      select tb;

            var tbs2 = from tb2 in tbs
                       where tb2.Name_Trigger == trigger.Name
                       select tb2;

            if (tbs2.Count() > 0)
            {
                // Found a table with a matching name.

                try
                {
                    foreach (var item in tbs2)
                    {
                        newVWTriggerRow = item;
                        trigger.UpdateDataSet(newVWTriggerRow);
                        newVWTriggerRow.SnapShotDate = DateTime.Now;
                        Common.ApplicationDataSet.VWTriggersTA.Update(Common.ApplicationDataSet.VWTriggers);
                    }
                }
                catch (Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
            else
            {
                try
                {
                    // Must be a new Trigger we haven't seen.
                    SQLInformation.Data.ApplicationDataSet.VWTriggersRow newTriggerRow = Common.ApplicationDataSet.VWTriggers.NewVWTriggersRow();
                    newTriggerRow.ID = Guid.NewGuid();   // See if this is available from table.
                    newTriggerRow.Name_Trigger = trigger.Name;

                    //newTrigger.Table_ID = trigger.ID.ToString();
                    newTriggerRow.DBView_ID = viewID;  // From above
                    newTriggerRow.CreateDate = trigger.CreateDate;

                    try
                    {
                        newTriggerRow.DateLastModified = trigger.DateLastModified;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }

                    newTriggerRow.SnapShotDate = DateTime.Now;

                    Common.ApplicationDataSet.VWTriggers.AddVWTriggersRow(newTriggerRow);
                    Common.ApplicationDataSet.VWTriggersTA.Update(Common.ApplicationDataSet.VWTriggers);
                }
                catch (Exception ex)
                {
                    // TODO(crhodes):  Need to wrap anything above that throws an exception
                    // that we want to ignore, e.g. property not available because of
                    // SQL Edition.
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    throw ex;
                }
            }
#if TRACE
            PLLog.Trace4("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
            return newVWTriggerRow;
        }

        #endregion

        #region Take Shapshots
        
        private static void TakeDatabaseSnapShot(SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow)
        {
#if TRACE
            long startTicks = PLLog.Trace4("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            Data.ApplicationDataSet.DatabaseInfoRow databaseInfoRow = null;

            try
            {
                databaseInfoRow = Common.ApplicationDataSet.DatabaseInfo.NewDatabaseInfoRow();

                databaseInfoRow.SnapShotDate = databaseRow.SnapShotDate;
                databaseInfoRow.Database_ID = databaseRow.ID;
                databaseInfoRow.Instance_ID = databaseRow.Instance_ID;
                databaseInfoRow.IndexSpaceUsage = databaseRow.IndexSpaceUsage;
                databaseInfoRow.DataSpaceUsage = databaseRow.DataSpaceUsage;
                databaseInfoRow.Size = databaseRow.Size;
                databaseInfoRow.SpaceAvailable = databaseRow.SpaceAvailable;

                Common.ApplicationDataSet.DatabaseInfo.AddDatabaseInfoRow(databaseInfoRow);
                Common.ApplicationDataSet.DatabaseInfoTA.Update(Common.ApplicationDataSet.DatabaseInfo);
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
            {
                databaseInfoRow.SnapShotDate = DateTime.Now;
                databaseInfoRow.SnapShotError = "ConnectionFailureException";
                Common.ApplicationDataSet.DatabaseInfoTA.Update(Common.ApplicationDataSet.DatabaseInfo);
                PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
            }
            catch (Exception ex)
            {
                databaseInfoRow.SnapShotDate = DateTime.Now;
                databaseInfoRow.SnapShotError = ex.ToString().Substring(1, 256);
                Common.ApplicationDataSet.DatabaseInfoTA.Update(Common.ApplicationDataSet.DatabaseInfo);
                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
            }
#if TRACE
            PLLog.Trace4("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        private static void TakeDBDataFileSnapShot(SQLInformation.Data.ApplicationDataSet.DBDataFilesRow dbDataFileRow)
        {
#if TRACE
            long startTicks = PLLog.Trace4("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            Data.ApplicationDataSet.DBDataFileInfoRow infoRow = null;

            try
            {
                infoRow = Common.ApplicationDataSet.DBDataFileInfo.NewDBDataFileInfoRow();

                infoRow.SnapShotDate = dbDataFileRow.SnapShotDate;
                infoRow.DBDataFile_ID = dbDataFileRow.ID;
                infoRow.AvailableSpace = dbDataFileRow.AvailableSpace;
                try
                {
                    infoRow.BytesReadFromDisk = dbDataFileRow.BytesReadFromDisk;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }
                try
                {
                    infoRow.BytesWrittenToDisk = dbDataFileRow.BytesWrittenToDisk;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }
                infoRow.MaxSize = dbDataFileRow.MaxSize;
                infoRow.NumberOfDiskReads = dbDataFileRow.NumberOfDiskReads;
                infoRow.NumberOfDiskWrites = dbDataFileRow.NumberOfDiskWrites;
                infoRow.Size = dbDataFileRow.Size;
                infoRow.UsedSpace = dbDataFileRow.UsedSpace;
                try
                {
                    infoRow.VolumeFreeSpace = dbDataFileRow.VolumeFreeSpace;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

                Common.ApplicationDataSet.DBDataFileInfo.AddDBDataFileInfoRow(infoRow);
                Common.ApplicationDataSet.DBDataFileInfoTA.Update(Common.ApplicationDataSet.DBDataFileInfo);
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
            {
                infoRow.SnapShotDate = DateTime.Now;
                infoRow.SnapShotError = "ConnectionFailureException";
                Common.ApplicationDataSet.DBDataFileInfoTA.Update(Common.ApplicationDataSet.DBDataFileInfo);
                PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
            }
            catch (Exception ex)
            {
                infoRow.SnapShotDate = DateTime.Now;
                infoRow.SnapShotError = ex.ToString().Substring(1, 256);
                Common.ApplicationDataSet.DBDataFileInfoTA.Update(Common.ApplicationDataSet.DBDataFileInfo);
                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
            }
#if TRACE
            PLLog.Trace4("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        private static void TakeDBLogFileSnapShot(SQLInformation.Data.ApplicationDataSet.DBLogFilesRow dbLogFileRow)
        {
#if TRACE
            long startTicks = PLLog.Trace4("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            Data.ApplicationDataSet.DBLogFileInfoRow infoRow = null;

            try
            {
                infoRow = Common.ApplicationDataSet.DBLogFileInfo.NewDBLogFileInfoRow();

                infoRow.SnapShotDate = dbLogFileRow.SnapShotDate;
                infoRow.DBLogFile_ID = dbLogFileRow.ID;

                try
                {
                    infoRow.BytesReadFromDisk = dbLogFileRow.BytesReadFromDisk;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }
                try
                {
                    infoRow.BytesWrittenToDisk = dbLogFileRow.BytesWrittenToDisk;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

                infoRow.MaxSize = dbLogFileRow.MaxSize;
                infoRow.NumberOfDiskReads = dbLogFileRow.NumberOfDiskReads;
                infoRow.NumberOfDiskWrites = dbLogFileRow.NumberOfDiskWrites;
                infoRow.Size = dbLogFileRow.Size;
                infoRow.UsedSpace = dbLogFileRow.UsedSpace;

                try
                {
                    infoRow.VolumeFreeSpace = dbLogFileRow.VolumeFreeSpace;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

                Common.ApplicationDataSet.DBLogFileInfo.AddDBLogFileInfoRow(infoRow);
                Common.ApplicationDataSet.DBLogFileInfoTA.Update(Common.ApplicationDataSet.DBLogFileInfo);
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
            {
                infoRow.SnapShotDate = DateTime.Now;
                infoRow.SnapShotError = "ConnectionFailureException";
                Common.ApplicationDataSet.DBLogFileInfoTA.Update(Common.ApplicationDataSet.DBLogFileInfo);
                PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
            }
            catch (Exception ex)
            {
                infoRow.SnapShotDate = DateTime.Now;
                infoRow.SnapShotError = ex.ToString().Substring(1, 256);
                Common.ApplicationDataSet.DBLogFileInfoTA.Update(Common.ApplicationDataSet.DBLogFileInfo);
                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
            }
#if TRACE
            PLLog.Trace4("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }
        
    #endregion

        #endregion

    }

}
