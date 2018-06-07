using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO=Microsoft.SqlServer.Management.Smo;
using MSMOA = Microsoft.SqlServer.Management.Smo.Agent;

using SQLInformation;
using VNC;

namespace SQLInformation.SMO
{
    public class Helper
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.SQLINFORMATION_SMO_HELPER;
        private const string LOG_APPNAME = "SQLInformation";

        #region Public Methods

        #region Take Shapshots

        public static void TakeInstanceSnapShot(SQLInformation.Data.ApplicationDataSet.InstancesRow instanceRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            Data.ApplicationDataSet.InstanceInfoRow instanceInfoRow = null;

            try
            {
                instanceInfoRow = Common.ApplicationDataSet.InstanceInfo.NewInstanceInfoRow();

                instanceInfoRow.SnapShotDate = instanceRow.SnapShotDate;
                instanceInfoRow.SnapShotDuration = instanceRow.SnapShotDuration;
                instanceInfoRow.SnapShotError = instanceRow.SnapShotError;

                instanceInfoRow.Instance_ID = instanceRow.ID;
                instanceInfoRow.Name_Instance = instanceRow.Name_Instance;

                instanceInfoRow.ADDomain = instanceRow.ADDomain;
                instanceInfoRow.Environment = instanceRow.Environment;
                instanceInfoRow.SecurityZone = instanceRow.SecurityZone;

                instanceInfoRow.PhysicalMemory = instanceRow.PhysicalMemory;
                instanceInfoRow.Processors = instanceRow.Processors;

                instanceInfoRow.Total_DataSpaceUsage = instanceRow.Total_DataSpaceUsage;
                instanceInfoRow.Total_IndexSpaceUsage = instanceRow.Total_IndexSpaceUsage;
                instanceInfoRow.Total_Size = instanceRow.Total_Size;
                instanceInfoRow.Total_SpaceAvailable = instanceRow.Total_SpaceAvailable;

                Common.ApplicationDataSet.InstanceInfo.AddInstanceInfoRow(instanceInfoRow);
                Common.ApplicationDataSet.InstanceInfoTA.Update(Common.ApplicationDataSet.InstanceInfo);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                instanceInfoRow.SnapShotDate = DateTime.Now;
                instanceInfoRow.SnapShotError = ex.ToString().Substring(0, 256);
                Common.ApplicationDataSet.InstanceInfoTA.Update(Common.ApplicationDataSet.InstanceInfo);
            }
#if TRACE
            VNC.AppLog.Trace4("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void TakeDatabaseSnapShot(SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            Data.ApplicationDataSet.DatabaseInfoRow databaseInfoRow = null;

            try
            {
                databaseInfoRow = Common.ApplicationDataSet.DatabaseInfo.NewDatabaseInfoRow();

                databaseInfoRow.SnapShotDate = databaseRow.SnapShotDate;
                databaseInfoRow.SnapShotError = databaseRow.SnapShotError;

                databaseInfoRow.Database_ID = databaseRow.ID;
                databaseInfoRow.Name_Database = databaseRow.Name_Database;
                databaseInfoRow.Instance_ID = databaseRow.Instance_ID;
                databaseInfoRow.Name_Instance = databaseRow.Name_Instance;

                databaseInfoRow.IndexSpaceUsage = databaseRow.IndexSpaceUsage;
                databaseInfoRow.DataSpaceUsage = databaseRow.DataSpaceUsage;
                databaseInfoRow.Size = databaseRow.Size;
                databaseInfoRow.SpaceAvailable = databaseRow.SpaceAvailable;

                Common.ApplicationDataSet.DatabaseInfo.AddDatabaseInfoRow(databaseInfoRow);
                Common.ApplicationDataSet.DatabaseInfoTA.Update(Common.ApplicationDataSet.DatabaseInfo);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);

                databaseInfoRow.SnapShotDate = DateTime.Now;
                databaseInfoRow.SnapShotError = ex.ToString().Substring(0, 256);
                Common.ApplicationDataSet.DatabaseInfoTA.Update(Common.ApplicationDataSet.DatabaseInfo);
            }
#if TRACE
            VNC.AppLog.Trace4("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        public static void TakeDBDataFileSnapShot(SQLInformation.Data.ApplicationDataSet.DBDataFilesRow dbDataFileRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
#endif
            Data.ApplicationDataSet.DBDataFileInfoRow infoRow = null;

            try
            {
                infoRow = Common.ApplicationDataSet.DBDataFileInfo.NewDBDataFileInfoRow();

                infoRow.SnapShotDate = dbDataFileRow.SnapShotDate;
                infoRow.SnapShotError = dbDataFileRow.SnapShotError;

                infoRow.DBDataFile_ID = dbDataFileRow.ID;
                infoRow.Database_ID = dbDataFileRow.Database_ID;
                infoRow.AvailableSpace = dbDataFileRow.AvailableSpace;
                //infoRow.BytesReadFromDisk = dbDataFileRow.BytesReadFromDisk;
                //infoRow.BytesWrittenToDisk = dbDataFileRow.BytesWrittenToDisk;
                infoRow.MaxSize = dbDataFileRow.MaxSize;
                //infoRow.NumberOfDiskReads = dbDataFileRow.NumberOfDiskReads;
                //infoRow.NumberOfDiskWrites = dbDataFileRow.NumberOfDiskWrites;
                infoRow.Size = dbDataFileRow.Size;
                infoRow.UsedSpace = dbDataFileRow.UsedSpace;
                infoRow.VolumeFreeSpace = dbDataFileRow.VolumeFreeSpace;

                Common.ApplicationDataSet.DBDataFileInfo.AddDBDataFileInfoRow(infoRow);
                Common.ApplicationDataSet.DBDataFileInfoTA.Update(Common.ApplicationDataSet.DBDataFileInfo);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);

                infoRow.SnapShotDate = DateTime.Now;
                infoRow.SnapShotError = ex.ToString().Substring(0, 256);
                Common.ApplicationDataSet.DBDataFileInfoTA.Update(Common.ApplicationDataSet.DBDataFileInfo);
            }
#if TRACE
            VNC.AppLog.Trace4("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12, startTicks);
#endif
        }

        public static void TakeDBFileGroupSnapShot(SQLInformation.Data.ApplicationDataSet.DBFileGroupsRow dbFileGroupRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
#endif
            Data.ApplicationDataSet.DBFileGroupInfoRow infoRow = null;

            try
            {
                infoRow = Common.ApplicationDataSet.DBFileGroupInfo.NewDBFileGroupInfoRow();

                infoRow.SnapShotDate = dbFileGroupRow.SnapShotDate;
                infoRow.Size = dbFileGroupRow.Size;
                infoRow.DBFileGroup_ID = dbFileGroupRow.ID;
                infoRow.Database_ID = dbFileGroupRow.Database_ID;

                Common.ApplicationDataSet.DBFileGroupInfo.AddDBFileGroupInfoRow(infoRow);
                Common.ApplicationDataSet.DBFileGroupInfoTA.Update(Common.ApplicationDataSet.DBFileGroupInfo);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);

                infoRow.SnapShotDate = DateTime.Now;
                infoRow.SnapShotError = ex.ToString().Substring(0, 256);
                Common.ApplicationDataSet.DBFileGroupInfoTA.Update(Common.ApplicationDataSet.DBFileGroupInfo);
            }
#if TRACE
            VNC.AppLog.Trace4("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12, startTicks);
#endif
        }

        public static void TakeDBLogFileSnapShot(SQLInformation.Data.ApplicationDataSet.DBLogFilesRow dbLogFileRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 13);
#endif
            Data.ApplicationDataSet.DBLogFileInfoRow infoRow = null;

            try
            {
                infoRow = Common.ApplicationDataSet.DBLogFileInfo.NewDBLogFileInfoRow();

                infoRow.SnapShotDate = dbLogFileRow.SnapShotDate;
                infoRow.SnapShotError = dbLogFileRow.SnapShotError;

                infoRow.DBLogFile_ID = dbLogFileRow.ID;
                infoRow.Database_ID = dbLogFileRow.Database_ID;
                //infoRow.BytesReadFromDisk = dbLogFileRow.BytesReadFromDisk;
                //infoRow.BytesWrittenToDisk = dbLogFileRow.BytesWrittenToDisk;
                infoRow.MaxSize = dbLogFileRow.MaxSize;
                //infoRow.NumberOfDiskReads = dbLogFileRow.NumberOfDiskReads;
                //infoRow.NumberOfDiskWrites = dbLogFileRow.NumberOfDiskWrites;
                infoRow.Size = dbLogFileRow.Size;
                infoRow.UsedSpace = dbLogFileRow.UsedSpace;
                infoRow.VolumeFreeSpace = dbLogFileRow.VolumeFreeSpace;

                Common.ApplicationDataSet.DBLogFileInfo.AddDBLogFileInfoRow(infoRow);
                Common.ApplicationDataSet.DBLogFileInfoTA.Update(Common.ApplicationDataSet.DBLogFileInfo);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 20);

                infoRow.SnapShotDate = DateTime.Now;
                infoRow.SnapShotError = ex.ToString().Substring(0, 256);
                Common.ApplicationDataSet.DBLogFileInfoTA.Update(Common.ApplicationDataSet.DBLogFileInfo);
            }
#if TRACE
            VNC.AppLog.Trace4("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 21, startTicks);
#endif
        }
        
    #endregion

        #endregion
        #region Private Methods

        private static string GetInstanceName(Guid instanceID)
        {
            var ins = from instance in Common.ApplicationDataSet.Instances
                      where instance.ID == instanceID
                      select instance;

            return ins.First().Name_Instance;
        }

        #endregion

    }

}
