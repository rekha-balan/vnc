using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO = Microsoft.SqlServer.Management.Smo;
using MSMOA = Microsoft.SqlServer.Management.Smo.Agent;

using SQLInformation;
using VNC;

namespace SQLInformation.SMO
{
    public static class Database
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_DATABASE;
        private const string LOG_APPNAME = "SQLInformation";

        private static DateTime SQLMinDateTime = new DateTime(1753, 1, 1);

        #region Public Methods

        public static void LoadFromSMO(MSMO.Server server, Guid instanceID, ExpandMask.DatabaseExpandSetting databaseExpandSettings)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            long stopwatchFrequency = Stopwatch.Frequency;

            MarkExistingItemsAsNotFound(instanceID);    // This enables cleanup of items that once existed but were deleted.

            foreach (MSMO.Database database in server.Databases)
            {
                long loopStartTicks = Stopwatch.GetTimestamp();

                if (!database.IsAccessible)
                {
                    VNC.AppLog.Warning(string.Format("Database: {0} is not accessible, skipping", database.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    continue;
                }

                SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow = GetInfoFromSMO(instanceID, server.Name, database);
                databaseRow.NotFound = false;

                try
                {
                    if (databaseRow.IsMonitored && databaseExpandSettings.IsMonitored)
                    {
                        if (databaseRow.ExpandStoredProcedures && databaseExpandSettings.ExpandStoredProcedures)
                        {
                            StoredProcedure.LoadFromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandTables && databaseExpandSettings.ExpandTables)
                        {
                            ExpandMask.TableExpandSetting tableExpandSetting = new ExpandMask.TableExpandSetting(databaseRow.DefaultTableExpandMask);
                            Table.LoadFromSMO(database, databaseRow.ID, tableExpandSetting);
                        }

                        if (databaseRow.ExpandViews && databaseExpandSettings.ExpandViews)
                        {
                            ExpandMask.ViewExpandSetting viewExpandSetting = new ExpandMask.ViewExpandSetting(databaseRow.DefaultViewExpandMask);
                            View.LoadFromSMO(database, databaseRow.ID, viewExpandSetting);
                        }

                        if (databaseRow.ExpandFileGroups && databaseExpandSettings.ExpandFileGroups)
                        {
                            FileGroup.LoadFromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandLogFiles && databaseExpandSettings.ExpandLogFiles)
                        {
                            LogFile.LoadFromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandRoles && databaseExpandSettings.ExpandRoles)
                        {
                            DatabaseRole.LoadFromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandTriggers && databaseExpandSettings.ExpandTriggers)
                        {
                            DatabaseDdlTrigger.LoadFromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandUserDefinedFunctions && databaseExpandSettings.ExpandUserDefinedFunctions)
                        {
                            UserDefinedFunction.LoadFromSMO(database, databaseRow.ID);
                        }

                        if (databaseRow.ExpandUsers && databaseExpandSettings.ExpandUsers)
                        {
                            User.LoadFromSMO(database, databaseRow.ID, databaseRow.Name_Database);
                        }
                    }
                }
                catch (Exception ex)
                {
                    VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }

                databaseRow.SnapShotDuration = (Stopwatch.GetTimestamp() - loopStartTicks) / stopwatchFrequency;
                Common.ApplicationDataSet.DatabasesTA.Update(Common.ApplicationDataSet.Databases);
            }

#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        #endregion

        #region Private Methods

        private static void MarkExistingItemsAsNotFound(Guid instanceID)
        {
            var previouslyFound = Common.ApplicationDataSet.Databases.Where(i => i.Instance_ID == instanceID);

            //((MSMO.SmoCollectionBase)server.Databases).Where(xx => xx)
            foreach (var pf in previouslyFound)
            {
                pf.NotFound = true;
            }
        }

        private static SQLInformation.Data.ApplicationDataSet.DatabasesRow GetInfoFromSMO(Guid instanceID, string instanceName, MSMO.Database database)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3(string.Format("Enter ({0})", database.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
#endif
            Debug.WriteLine("   DB:{0}", database.Name);
            SQLInformation.Data.ApplicationDataSet.DatabasesRow dataRow = null;

            try
            {
                var dbs = from db in Common.ApplicationDataSet.Databases
                          where db.Instance_ID == instanceID
                          select db;

                var dbs2 = from db2 in dbs
                           where db2.Name_Database == database.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(database, dataRow);
                }
                else
                {
                    dataRow = Add(instanceID, instanceName, database);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
            }

#if TRACE
            VNC.AppLog.Trace3(string.Format("Exit ({0})", database.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.DatabasesRow Add(Guid instanceID, string instanceName, MSMO.Database database)
        {
            SQLInformation.Data.ApplicationDataSet.DatabasesRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.Databases.NewDatabasesRow();
                dataRow.ID = Guid.NewGuid();

                dataRow.Name_Database = database.Name;

                dataRow.CreateDate = database.CreateDate;

                try
                {
                    dataRow.DataBaseGuid = database.DatabaseGuid.ToString();
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
#endif
                    dataRow.DataBaseGuid = "<Not Available>";
                }

                dataRow.ID_DB = database.ID;
                dataRow.Instance_ID = instanceID;
                dataRow.Name_Instance = instanceName;

                try
                {
                    dataRow.DefaultFileGroup = database.DefaultFileGroup;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
#endif
                }

                try
                {
                    dataRow.DataSpaceUsage = database.DataSpaceUsage;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
#endif
                }

                try
                {
                    dataRow.IndexSpaceUsage = database.IndexSpaceUsage;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                }

                try
                {
                    dataRow.Size = database.Size;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);
#endif
                }

                try
                {
                    dataRow.SpaceAvailable = database.SpaceAvailable;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12);
#endif
                }

                try
                {
                    dataRow.LastBackupDate = (database.LastBackupDate > SQLMinDateTime ? database.LastBackupDate : SQLMinDateTime);
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 13);
#endif
                }

                try
                {
                    dataRow.LastDifferentialBackupDate = (database.LastDifferentialBackupDate > SQLMinDateTime ? database.LastDifferentialBackupDate : SQLMinDateTime);
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 14);
#endif
                }

                try
                {
                    dataRow.LastLogBackupDate = (database.LastLogBackupDate > SQLMinDateTime ? database.LastLogBackupDate : SQLMinDateTime);
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
#endif
                }

                try
                {
                    dataRow.Owner = database.Owner;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 16);
#endif
                }

                dataRow.RecoveryModel = database.RecoveryModel.ToString();

                // TODO(crhodes): Think through how to set this.

                ExpandMask.DatabaseExpandSetting expandSetting = new ExpandMask.DatabaseExpandSetting(instanceID);

                dataRow.IsMonitored = expandSetting.IsMonitored;

                dataRow.ExpandDataFiles = expandSetting.ExpandDataFiles;
                dataRow.ExpandFileGroups = expandSetting.ExpandFileGroups;
                dataRow.ExpandLogFiles = expandSetting.ExpandLogFiles;
                dataRow.ExpandRoles = expandSetting.ExpandRoles;
                dataRow.ExpandStoredProcedures = expandSetting.ExpandStoredProcedures;
                dataRow.ExpandTables = expandSetting.ExpandTables;
                dataRow.ExpandTriggers = expandSetting.ExpandTriggers;
                dataRow.ExpandUserDefinedFunctions = expandSetting.ExpandUserDefinedFunctions;
                dataRow.ExpandUsers = expandSetting.ExpandUsers;
                dataRow.ExpandViews = expandSetting.ExpandViews;

                // TODO(crhodes): Need to get this passed in.

                dataRow.DefaultTableExpandMask = 0;
                dataRow.DefaultViewExpandMask = 0;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.Databases.AddDatabasesRow(dataRow);
                Common.ApplicationDataSet.DatabasesTA.Update(Common.ApplicationDataSet.Databases);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 17);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }

            return dataRow;
        }

        private static void Update(MSMO.Database database, SQLInformation.Data.ApplicationDataSet.DatabasesRow dataRow)
        {
            try
            {
                if (dataRow.IsMonitored)
                {
                    database.UpdateDataSet(dataRow);

                    UpdateDatabaseWithSnapShot(dataRow, "");

                    // Add the Snapshot

                    SMO.Helper.TakeDatabaseSnapShot(dataRow);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 18);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DatabasesRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DatabasesTA.Update(Common.ApplicationDataSet.Databases);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DatabasesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 19);
            }
        }

        public static void UpdateDataSet(this MSMO.Database database, Data.ApplicationDataSet.DatabasesRow dataRow)
        {
            try
            {
                // If we got here, update the Instance Name in case it was not accessible for some reason.

                dataRow.Name_Instance = dataRow.Name_Instance.ToUpper();

                dataRow.CreateDate = database.CreateDate;

                try
                {
                    dataRow.DataBaseGuid = database.DatabaseGuid.ToString();
                }
                catch (Exception )
                {
#if TRACE

#endif
                    dataRow.DataBaseGuid = "<Not Available>";
                }

                try
                {
                    dataRow.DefaultFileGroup = database.DefaultFileGroup;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 20);
#endif
                }

                try
                {
                    dataRow.DataSpaceUsage = database.DataSpaceUsage;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 21);
#endif
                }

                try
                {
                    if (database.ExtendedProperties.Count > 0)
                    {
                        try { dataRow.EP_Area = (string)database.ExtendedProperties["EP_Area"].Value; }
                        catch (Exception) { dataRow.EP_Area = "[Not Set]"; }

                        try { dataRow.EP_DBApprover = (string)database.ExtendedProperties["EP_DBApprover"].Value; }
                        catch (Exception) { dataRow.EP_DBApprover = "[Not Set]"; }

                        try { dataRow.EP_DRTier = (string)database.ExtendedProperties["EP_DRTier"].Value; }
                        catch (Exception) { dataRow.EP_DRTier = "[Not Set]"; }

                        try { dataRow.EP_PrimaryDBContact = (string)database.ExtendedProperties["EP_PrimaryDBContact"].Value; }
                        catch (Exception) { dataRow.EP_PrimaryDBContact = "[Not Set]"; }

                        try { dataRow.EP_Team = (string)database.ExtendedProperties["EP_Team"].Value; }
                        catch (Exception) { dataRow.EP_Team = "[Not Set]"; }
                    }
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 22);
#endif
                    // ExtendedProperties.Count throws exceptions on SQL2000??
                }


                try
                {
                    dataRow.IndexSpaceUsage = database.IndexSpaceUsage;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 23);
#endif
                }

                try
                {
                    dataRow.LastBackupDate = (database.LastBackupDate > SQLMinDateTime ? database.LastBackupDate : SQLMinDateTime);
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 24);
#endif
                }

                try
                {
                    dataRow.LastDifferentialBackupDate = (database.LastDifferentialBackupDate > SQLMinDateTime ? database.LastDifferentialBackupDate : SQLMinDateTime);
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 25);
#endif
                }

                try
                {
                    dataRow.LastLogBackupDate = (database.LastLogBackupDate > SQLMinDateTime ? database.LastLogBackupDate : SQLMinDateTime);
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 26);
#endif
                }

                try
                {
                    dataRow.Owner = database.Owner;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 27);
#endif
                }

                dataRow.RecoveryModel = database.RecoveryModel.ToString();

                try
                {
                    dataRow.Size = database.Size;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 28);
#endif
                }

                try
                {
                    dataRow.SpaceAvailable = database.SpaceAvailable;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 29);
#endif
                }

            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 30);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

        #endregion

    }
}
