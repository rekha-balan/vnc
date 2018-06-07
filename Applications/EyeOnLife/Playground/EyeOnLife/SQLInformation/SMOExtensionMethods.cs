using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO=Microsoft.SqlServer.Management.Smo;
using PacificLife.Life;

namespace SQLInformation
{
    public static class SMOExtensionMethods
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "SQLINFOAGENT";

        private static DateTime SQLMinDateTime = new DateTime(1753, 1, 1);

        public static void UpdateDataSet(this MSMO.Database database, Data.ApplicationDataSet.DatabasesRow databaseRow)
        {
            try
            {                
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

                try
                {
                    databaseRow.DataSpaceUsage = database.DataSpaceUsage;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

                try
                {
                    if (database.ExtendedProperties.Count > 0)
                    {
                        try { databaseRow.EP_Area = (string)database.ExtendedProperties["EP_Area"].Value; }
                        catch (Exception) { databaseRow.EP_Area = "[Not Set]"; }

                        try { databaseRow.EP_DBApprover = (string)database.ExtendedProperties["EP_DBApprover"].Value; }
                        catch (Exception) { databaseRow.EP_DBApprover = "[Not Set]"; }

                        try { databaseRow.EP_DRTier = (string)database.ExtendedProperties["EP_DRTier"].Value; }
                        catch (Exception) { databaseRow.EP_DRTier = "[Not Set]"; }

                        try { databaseRow.EP_PrimaryDBContact = (string)database.ExtendedProperties["EP_PrimaryDBContact"].Value; }
                        catch (Exception) { databaseRow.EP_PrimaryDBContact = "[Not Set]"; }

                        try { databaseRow.EP_Team = (string)database.ExtendedProperties["EP_Team"].Value; }
                        catch (Exception) { databaseRow.EP_Team = "[Not Set]"; }
                    }
                }
                catch (Exception ex)
                {
                    // ExtendedProperties.Count throws exceptions on SQL2000??
                }


                try
                {
                    databaseRow.IndexSpaceUsage = database.IndexSpaceUsage;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

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

                databaseRow.Owner = database.Owner;

                databaseRow.RecoveryModel = database.RecoveryModel.ToString();

                try
                {
                    databaseRow.Size = database.Size;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

                try
                {
                    databaseRow.SpaceAvailable = database.SpaceAvailable;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

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

        public static void UpdateDataSet(this MSMO.Database database, Data.ApplicationDataSet.DatabaseInfoRow databaseRow)
        {
            try
            {
                databaseRow.IndexSpaceUsage = database.IndexSpaceUsage;
                databaseRow.DataSpaceUsage = database.DataSpaceUsage;
                databaseRow.Size = database.Size;
                databaseRow.SpaceAvailable = database.SpaceAvailable;

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

        public static void UpdateDataSet(this MSMO.DatabaseDdlTrigger databaseDdlTrigger, Data.ApplicationDataSet.DBDdlTriggersRow triggerRow)
        {

            try
            {
                //instanceRow.ServiceName = server.ServiceName;
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

        public static void UpdateDataSet(this MSMO.DatabaseRole databaseRole, Data.ApplicationDataSet.DBRolesRow roleRow)
        {

            try
            {
                roleRow.CreateDate = databaseRole.CreateDate;
                roleRow.DateLastModified = databaseRole.DateLastModified;
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

        public static void UpdateDataSet(this MSMO.DataFile dataFile, Data.ApplicationDataSet.DBDataFilesRow dataFileRow)
        {

            try
            {
                //instanceRow.ServiceName = server.ServiceName;
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

        public static void UpdateDataSet(this MSMO.FileGroup fileGroup, Data.ApplicationDataSet.DBFileGroupsRow fileGroupRow)
        {

            try
            {
                //instanceRow.ServiceName = server.ServiceName;
            }
            catch(Exception ex)
            {
                // TODO(crhodes):  Need to wrap anything above that throws an exception
                // that we want to ignore, e.g. property not available because of
                // SQL Edition.
                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                throw ex;
            }
        }

        public static void UpdateDataSet(this MSMO.LogFile logFile, Data.ApplicationDataSet.DBLogFilesRow logFileRow)
        {

            try
            {
                //instanceRow.ServiceName = server.ServiceName;
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

        public static void UpdateDataSet(this MSMO.Login login, Data.ApplicationDataSet.LoginsRow loginRow)
        {
            try
            {
                // Determine what might change and update it
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

        public static void UpdateDataSet(this MSMO.Server server, Data.ApplicationDataSet.InstancesRow instanceRow)
        {

            try
            {
                try
                {
                    instanceRow.BrowserStartMode = server.BrowserStartMode.ToString();
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    instanceRow.BrowserStartMode = "<Exception>";
                }
                instanceRow.ServiceName = server.ServiceName;
                instanceRow.Collation = server.Collation;
                instanceRow.Edition = server.Edition;
                instanceRow.IsClustered = server.IsClustered;
                instanceRow.NetName = server.NetName;
                instanceRow.OSVersion = server.OSVersion;
                instanceRow.PhysicalMemory = server.PhysicalMemory;
                instanceRow.Platform = server.Platform;
                instanceRow.Processors = server.Processors;

                try
                {
                    instanceRow.ServiceAccount = server.ServiceAccount;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    instanceRow.ServiceAccount = "<Exception>";
                }

                try
                {
                    instanceRow.ServiceInstanceId = server.ServiceInstanceId;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    instanceRow.ServiceInstanceId = "<Exception>";
                }

                instanceRow.EngineEdition = server.EngineEdition.ToString();
                instanceRow.Product = server.Product;
                instanceRow.ProductLevel = server.ProductLevel;
                instanceRow.Version = server.VersionString;
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

        public static void UpdateDataSet(this MSMO.Server server, Data.ApplicationDataSet.InstanceInfoRow instanceRow)
        {
            try
            {
                instanceRow.PhysicalMemory = server.PhysicalMemory;
                //instanceRow.Platform = server.Platform;
                instanceRow.Processors = server.Processors;
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

        public static void UpdateDataSet(this MSMO.ServerRole serverRole, Data.ApplicationDataSet.ServerRolesRow serverRoleRow)
        {
            try
            {
                // Determine what might change and update it
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

        public static void UpdateDataSet(this MSMO.StoredProcedure storedProcedure, Data.ApplicationDataSet.DBStoredProceduresRow storedProcedureRow)
        {
            try
            {
                storedProcedureRow.Owner = storedProcedure.Owner;
                storedProcedureRow.CreateDate = storedProcedure.CreateDate;

                if (Data.Config.ADSLoad_DBStoredProcedureContent)
                {
                    try
                    {
                        storedProcedureRow.MethodName = storedProcedure.MethodName;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                        storedProcedureRow.MethodName = "<Exception>";
                    }
                    try
                    {
                        storedProcedureRow.TextHeader = storedProcedure.TextHeader;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                        storedProcedureRow.TextHeader = "<Exception>";
                    }

                    try
                    {
                        storedProcedureRow.TextBody = storedProcedure.TextBody;
                    }
                    catch (Exception ex)
                    {
                        PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                        storedProcedureRow.TextBody = "<Exception>";
                    }
                }

                try
                {
                    storedProcedureRow.DateLastModified = storedProcedure.DateLastModified;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

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

        public static void UpdateDataSet(this MSMO.Table table, Data.ApplicationDataSet.DBTablesRow tableRow)
        {

            try
            {
                tableRow.DataSpaceUsed = table.DataSpaceUsed;

                try
                {
                    tableRow.DateLastModified = table.DateLastModified;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

                tableRow.Owner = table.Owner;
                tableRow.RowCount = table.RowCount;
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

        public static void UpdateDataSet(this MSMO.Trigger trigger, Data.ApplicationDataSet.TBTriggersRow triggerRow)
        {

            try
            {
                //instanceRow.ServiceName = server.ServiceName;
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

        public static void UpdateDataSet(this MSMO.Trigger trigger, Data.ApplicationDataSet.VWTriggersRow triggerRow)
        {

            try
            {
                //instanceRow.ServiceName = server.ServiceName;
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

        public static void UpdateDataSet(this MSMO.User user, Data.ApplicationDataSet.DBUsersRow userRow)
        {

            try
            {
                //instanceRow.ServiceName = server.ServiceName;
            }
            catch(Exception ex)
            {
                // TODO(crhodes):  Need to wrap anything above that throws an exception
                // that we want to ignore, e.g. property not available because of
                // SQL Edition.
                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                throw ex;
            }
        }

        public static void UpdateDataSet(this MSMO.UserDefinedFunction userDefinedFunction, Data.ApplicationDataSet.DBUserDefinedFunctionsRow userDefinedFunctionRow)
        {

            try
            {
                //instanceRow.ServiceName = server.ServiceName;
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

        public static void UpdateDataSet(this MSMO.View view, Data.ApplicationDataSet.DBViewsRow viewRow)
        {
            try
            {
                viewRow.CreateDate = view.CreateDate;
                //viewRow.DataSpaceUsed = view.DataSpaceUsed;

                try
                {
                    viewRow.DateLastModified = view.DateLastModified;
                }
                catch (Exception ex)
                {
                    PLLog.Warning(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                }

                viewRow.Owner = view.Owner;
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

    }
}
