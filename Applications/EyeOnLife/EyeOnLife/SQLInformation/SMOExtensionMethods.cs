using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO=Microsoft.SqlServer.Management.Smo;
using MSMOA=Microsoft.SqlServer.Management.Smo.Agent;

using PacificLife.Life;

namespace SQLInformation
{
    public static class SMOExtensionMethods
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.SQLINFORMATION_SMOEXTM;
        private const string PLLOG_APPNAME = "SQLInformation";

        private static DateTime SQLMinDateTime = new DateTime(1753, 1, 1);

//        //public static void UpdateDataSet(this MSMOA.Alert alert, Data.ApplicationDataSet.JSAlertsRow jsAlertRow)
//        //{

//        //    try
//        //    {
//        //        //jobServerRow.X = jobServer.X;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
//        //        // that we want to ignore, e.g. property not available because of
//        //        // SQL Edition.
//        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
//        //        throw ex;
//        //    }
//        //}

//        //public static void UpdateDataSet(this MSMOA.AlertCategory alertCategory, Data.ApplicationDataSet.JSAlertCategoriesRow jsAlertCategoryRow)
//        //{

//        //    try
//        //    {
//        //        //jsAlertCategoryRow.X = alertCategory.X;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
//        //        // that we want to ignore, e.g. property not available because of
//        //        // SQL Edition.
//        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
//        //        throw ex;
//        //    }
//        //}

////        public static void UpdateDataSet(this MSMO.Database database, Data.ApplicationDataSet.DatabasesRow databaseRow)
////        {
////            try
////            {                
////                databaseRow.CreateDate = database.CreateDate;

////                try
////                {
////                    databaseRow.DataBaseGuid = database.DatabaseGuid.ToString();
////                }
////                catch (Exception )
////                {
////#if TRACE
////#endif
////                    databaseRow.DataBaseGuid = "<Not Available>";
////                }

////                try
////                {
////                    databaseRow.DataSpaceUsage = database.DataSpaceUsage;
////                }
////                catch (Exception ex)
////                {
////#if TRACE
////                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 26);
////#endif
////                }

////                try
////                {
////                    if (database.ExtendedProperties.Count > 0)
////                    {
////                        try { databaseRow.EP_Area = (string)database.ExtendedProperties["EP_Area"].Value; }
////                        catch (Exception) { databaseRow.EP_Area = "[Not Set]"; }

////                        try { databaseRow.EP_DBApprover = (string)database.ExtendedProperties["EP_DBApprover"].Value; }
////                        catch (Exception) { databaseRow.EP_DBApprover = "[Not Set]"; }

////                        try { databaseRow.EP_DRTier = (string)database.ExtendedProperties["EP_DRTier"].Value; }
////                        catch (Exception) { databaseRow.EP_DRTier = "[Not Set]"; }

////                        try { databaseRow.EP_PrimaryDBContact = (string)database.ExtendedProperties["EP_PrimaryDBContact"].Value; }
////                        catch (Exception) { databaseRow.EP_PrimaryDBContact = "[Not Set]"; }

////                        try { databaseRow.EP_Team = (string)database.ExtendedProperties["EP_Team"].Value; }
////                        catch (Exception) { databaseRow.EP_Team = "[Not Set]"; }
////                    }
////                }
////                catch (Exception ex)
////                {
////#if TRACE
////                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
////#endif
////                    // ExtendedProperties.Count throws exceptions on SQL2000??
////                }


////                try
////                {
////                    databaseRow.IndexSpaceUsage = database.IndexSpaceUsage;
////                }
////                catch (Exception ex)
////                {
////#if TRACE
////                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
////#endif
////                }

////                try
////                {
////                    databaseRow.LastBackupDate = (database.LastBackupDate > SQLMinDateTime ? database.LastBackupDate : SQLMinDateTime);
////                }
////                catch (Exception ex)
////                {
////#if TRACE
////                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
////#endif
////                }
////                try
////                {
////                    databaseRow.LastDifferentialBackupDate = (database.LastDifferentialBackupDate > SQLMinDateTime ? database.LastDifferentialBackupDate : SQLMinDateTime);
////                }
////                catch (Exception ex)
////                {
////#if TRACE
////                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
////#endif
////                }
////                try
////                {
////                    databaseRow.LastLogBackupDate = (database.LastLogBackupDate > SQLMinDateTime ? database.LastLogBackupDate : SQLMinDateTime);
////                }
////                catch (Exception ex)
////                {
////#if TRACE
////                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
////#endif
////                }

////                databaseRow.Owner = database.Owner;

////                databaseRow.RecoveryModel = database.RecoveryModel.ToString();

////                try
////                {
////                    databaseRow.Size = database.Size;
////                }
////                catch (Exception ex)
////                {
////#if TRACE
////                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
////#endif
////                }

////                try
////                {
////                    databaseRow.SpaceAvailable = database.SpaceAvailable;
////                }
////                catch (Exception ex)
////                {
////#if TRACE
////                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
////#endif
////                }

////            }
////            catch (Exception ex)
////            {
////                // TODO(crhodes):  Need to wrap anything above that throws an exception
////                // that we want to ignore, e.g. property not available because of
////                // SQL Edition.
////                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
////                throw ex;
////            }
////        }

//        //public static void UpdateDataSet(this MSMO.Database database, Data.ApplicationDataSet.DatabaseInfoRow databaseInfoRow)
//        //{
//        //    try
//        //    {
//        //        databaseInfoRow.IndexSpaceUsage = database.IndexSpaceUsage;
//        //        databaseInfoRow.DataSpaceUsage = database.DataSpaceUsage;
//        //        databaseInfoRow.Size = database.Size;
//        //        databaseInfoRow.SpaceAvailable = database.SpaceAvailable;

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
//        //        // that we want to ignore, e.g. property not available because of
//        //        // SQL Edition.
//        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);
//        //        throw ex;
//        //    }
//        //}

//        public static void UpdateDataSet(this MSMO.DatabaseDdlTrigger databaseDdlTrigger, Data.ApplicationDataSet.DBDdlTriggersRow triggerRow)
//        {

//            try
//            {
//                //instanceRow.ServiceName = server.ServiceName;
//            }
//            catch (Exception ex)
//            {
//                // TODO(crhodes):  Need to wrap anything above that throws an exception
//                // that we want to ignore, e.g. property not available because of
//                // SQL Edition.
//                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12);
//                throw ex;
//            }
//        }

        //public static void UpdateDataSet(this MSMO.DatabaseRole databaseRole, Data.ApplicationDataSet.DBRolesRow roleRow)
        //{

        //    try
        //    {
        //        roleRow.CreateDate = databaseRole.CreateDate;
        //        roleRow.DateLastModified = databaseRole.DateLastModified;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 13);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMO.DataFile dataFile, Data.ApplicationDataSet.DBDataFilesRow dataFileRow)
        //{

        //    try
        //    {
        //        //instanceRow.ServiceName = server.ServiceName;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 14);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMO.FileGroup fileGroup, Data.ApplicationDataSet.DBFileGroupsRow fileGroupRow)
        //{

        //    try
        //    {
        //        //instanceRow.ServiceName = server.ServiceName;
        //    }
        //    catch(Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.JobServer jobServer, Data.ApplicationDataSet.JobServersRow jobServerRow)
        //{

        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.JobCategory jobCategory, Data.ApplicationDataSet.JSJobCategoriesRow jsJobCategoryRow)
        //{

        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.Job job, Data.ApplicationDataSet.JSJobsRow jsJobRow)
        //{

        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.JobSchedule jobSchedule, Data.ApplicationDataSet.JSJobSchedulesRow jsJobScheduleRow)
        //{

        //    try
        //    {
        //        jsJobScheduleRow.ActiveEndDate = jobSchedule.ActiveEndDate;
        //        jsJobScheduleRow.ActiveEndTimeOfDay = jobSchedule.ActiveEndTimeOfDay;
        //        jsJobScheduleRow.ActiveStartDate = jobSchedule.ActiveStartDate;
        //        jsJobScheduleRow.ActiveStartTimeOfDay = jobSchedule.ActiveStartTimeOfDay;
        //        jsJobScheduleRow.DateCreated = jobSchedule.DateCreated;
        //        jsJobScheduleRow.FrequencyInterval = jobSchedule.FrequencyInterval.ToString();
        //        jsJobScheduleRow.FrequencyRecurrenceFactor = jobSchedule.FrequencyRecurrenceFactor.ToString();
        //        jsJobScheduleRow.FrequencyRelativeIntervals = jobSchedule.FrequencyRelativeIntervals.ToString();
        //        jsJobScheduleRow.FrequencySubDayInterval = jobSchedule.FrequencySubDayInterval.ToString();
        //        jsJobScheduleRow.FrequencySubDayTypes = jobSchedule.FrequencySubDayTypes.ToString();
        //        jsJobScheduleRow.FrequencyTypes = jobSchedule.FrequencyTypes.ToString();
        //        jsJobScheduleRow.ID_JobSchedule = jobSchedule.ID;
        //        jsJobScheduleRow.IsEnabled = jobSchedule.IsEnabled;
        //        jsJobScheduleRow.JobCount = jobSchedule.JobCount;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.JobStep jobStep, Data.ApplicationDataSet.JSJobStepsRow jsJobStepRow)
        //{

        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.OperatorCategory operatorCategory, Data.ApplicationDataSet.JSOperatorCategoriesRow jsOperatorCategoryRow)
        //{

        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.Operator operatorX, Data.ApplicationDataSet.JSOperatorsRow jsOperatorRow)
        //{
        //    // NB. operator is a keyword so use operatorX
        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.ProxyAccount proxyAccount, Data.ApplicationDataSet.JSProxyAccountsRow jsProxyAccountRow)
        //{

        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        // TODO(crhodes): Not sure about SharedSchedules yet

        //public static void UpdateDataSet(this MSMOA.Sc jobServer, Data.ApplicationDataSet.JobServersRow jobServerRow)
        //{

        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.TargetServerGroup targetServerGroup, Data.ApplicationDataSet.JSTargetServerGroupsRow jsTargetServerGroupRow)
        //{

        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMOA.TargetServer targetServer, Data.ApplicationDataSet.JSTargetServersRow jsTargetServerRow)
        //{

        //    try
        //    {
        //        //jobServerRow.X = jobServer.X;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMO.LinkedServer linkedServer, Data.ApplicationDataSet.LinkedServersRow linkedServerRow)
        //{

        //    try
        //    {
        //        //linkedServerRow.DateLastModified = linkedServer.DateLastModified;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMO.LogFile logFile, Data.ApplicationDataSet.DBLogFilesRow logFileRow)
        //{

        //    try
        //    {
        //        //instanceRow.ServiceName = server.ServiceName;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 16);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMO.Login login, Data.ApplicationDataSet.LoginsRow loginRow)
        //{
        //    try
        //    {
        //        // Determine what might change and update it
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 17);
        //        throw ex;
        //    }
        //}

//        public static void UpdateDataSet(this MSMO.Server server, Data.ApplicationDataSet.InstancesRow instanceRow)
//        {
//            try
//            {
//                try
//                {
//                    instanceRow.BrowserStartMode = server.BrowserStartMode.ToString();
//                }
//                catch (Exception ex)
//                {
//#if TRACE
//                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 18);
//#endif
//                    instanceRow.BrowserStartMode = "<Exception>";
//                }


//                instanceRow.Collation = server.Collation;
//                instanceRow.Edition = server.Edition;
//                instanceRow.EngineEdition = server.EngineEdition.ToString();
//                instanceRow.IsClustered = server.IsClustered;
//                instanceRow.NetName = server.NetName;
//                instanceRow.OSVersion = server.OSVersion;
//                instanceRow.PerfMonMode = server.PerfMonMode.ToString();
//                instanceRow.PhysicalMemory = server.PhysicalMemory;
//                instanceRow.Platform = server.Platform;
//                instanceRow.Processors = server.Processors;
//                instanceRow.Product = server.Product;
//                instanceRow.ProductLevel = server.ProductLevel;

//                try
//                {
//                    instanceRow.ServiceAccount = server.ServiceAccount;
//                }
//                catch (Exception ex)
//                {
//#if TRACE
//                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 19);
//#endif
//                    instanceRow.ServiceAccount = "<Exception>";
//                }

//                try
//                {
//                    instanceRow.ServiceInstanceId = server.ServiceInstanceId;
//                }
//                catch (Exception ex)
//                {
//#if TRACE
//                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 20);
//#endif
//                    instanceRow.ServiceInstanceId = "<Exception>";
//                }

//                instanceRow.ServiceName = server.ServiceName;
//                instanceRow.Status = server.Status.ToString();
//                instanceRow.Version = server.VersionString;
//            }
//            catch (Exception ex)
//            {
//                // TODO(crhodes):  Need to wrap anything above that throws an exception
//                // that we want to ignore, e.g. property not available because of
//                // SQL Edition.
//                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 21);
//                throw ex;
//            }
//        }

        //public static void UpdateDataSet(this MSMO.Server server, Data.ApplicationDataSet.InstanceInfoRow instanceRow)
        //{
        //    try
        //    {
        //        instanceRow.PhysicalMemory = server.PhysicalMemory;
        //        //instanceRow.Platform = server.Platform;
        //        instanceRow.Processors = server.Processors;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 22);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMO.ServerRole serverRole, Data.ApplicationDataSet.ServerRolesRow serverRoleRow)
        //{
        //    try
        //    {
        //        // Determine what might change and update it
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 23);
        //        throw ex;
        //    }
        //}

//        public static void UpdateDataSet(this MSMO.StoredProcedure storedProcedure, Data.ApplicationDataSet.DBStoredProceduresRow storedProcedureRow)
//        {
//            try
//            {
//                storedProcedureRow.Owner = storedProcedure.Owner;
//                storedProcedureRow.CreateDate = storedProcedure.CreateDate;

//                if (Data.Config.ADSLoad_DBStoredProcedureContent)
//                {
//                    try
//                    {
//                        storedProcedureRow.MethodName = storedProcedure.MethodName;
//                    }
//                    catch (Exception ex)
//                    {
//#if TRACE
//                        PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 24);
//#endif
//                        storedProcedureRow.MethodName = "<Exception>";
//                    }
//                    try
//                    {
//                        storedProcedureRow.TextHeader = storedProcedure.TextHeader;
//                    }
//                    catch (Exception ex)
//                    {
//#if TRACE
//                        PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 25);
//#endif
//                        storedProcedureRow.TextHeader = "<Exception>";
//                    }

//                    try
//                    {
//                        storedProcedureRow.TextBody = storedProcedure.TextBody;
//                    }
//                    catch (Exception ex)
//                    {
//#if TRACE
//                        PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 26);
//#endif
//                        storedProcedureRow.TextBody = "<Exception>";
//                    }
//                }

//                try
//                {
//                    storedProcedureRow.DateLastModified = storedProcedure.DateLastModified;
//                }
//                catch (Exception ex)
//                {
//#if TRACE
//                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 26);
//#endif
//                }

//            }
//            catch (Exception ex)
//            {
//                // TODO(crhodes):  Need to wrap anything above that throws an exception
//                // that we want to ignore, e.g. property not available because of
//                // SQL Edition.
//                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 28);
//                throw ex;
//            }
//        }

//        public static void UpdateDataSet(this MSMO.Table table, Data.ApplicationDataSet.DBTablesRow tableRow)
//        {

//            try
//            {
//                tableRow.DataSpaceUsed = table.DataSpaceUsed;

//                try
//                {
//                    tableRow.DateLastModified = table.DateLastModified;
//                }
//                catch (Exception ex)
//                {
//#if TRACE
//                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 29);
//#endif
//                }

//                tableRow.Owner = table.Owner;
//                tableRow.RowCount = table.RowCount;
//            }
//            catch (Exception ex)
//            {
//                // TODO(crhodes):  Need to wrap anything above that throws an exception
//                // that we want to ignore, e.g. property not available because of
//                // SQL Edition.
//                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 30);
//                throw ex;
//            }
//        }

        //public static void UpdateDataSet(this MSMO.Trigger trigger, Data.ApplicationDataSet.TBTriggersRow triggerRow)
        //{

        //    try
        //    {
        //        //instanceRow.ServiceName = server.ServiceName;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 31);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMO.Trigger trigger, Data.ApplicationDataSet.VWTriggersRow triggerRow)
        //{

        //    try
        //    {
        //        //instanceRow.ServiceName = server.ServiceName;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 32);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMO.User user, Data.ApplicationDataSet.DBUsersRow userRow)
        //{

        //    try
        //    {
        //        //instanceRow.ServiceName = server.ServiceName;
        //    }
        //    catch(Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 33);
        //        throw ex;
        //    }
        //}

        //public static void UpdateDataSet(this MSMO.UserDefinedFunction userDefinedFunction, Data.ApplicationDataSet.DBUserDefinedFunctionsRow userDefinedFunctionRow)
        //{

        //    try
        //    {
        //        //instanceRow.ServiceName = server.ServiceName;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO(crhodes):  Need to wrap anything above that throws an exception
        //        // that we want to ignore, e.g. property not available because of
        //        // SQL Edition.
        //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 34);
        //        throw ex;
        //    }
        //}

//        public static void UpdateDataSet(this MSMO.View view, Data.ApplicationDataSet.DBViewsRow viewRow)
//        {
//            try
//            {
//                viewRow.CreateDate = view.CreateDate;
//                //viewRow.DataSpaceUsed = view.DataSpaceUsed;

//                try
//                {
//                    viewRow.DateLastModified = view.DateLastModified;
//                }
//                catch (Exception ex)
//                {
//#if TRACE
//                    PLLog.Debug(ex.ToString(), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 35);
//#endif
//                }

//                viewRow.Owner = view.Owner;
//            }
//            catch (Exception ex)
//            {
//                // TODO(crhodes):  Need to wrap anything above that throws an exception
//                // that we want to ignore, e.g. property not available because of
//                // SQL Edition.
//                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 36);
//                throw ex;
//            }
//        }

    }
}
