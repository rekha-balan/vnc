using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO = Microsoft.SqlServer.Management.Smo;
using MSMOA = Microsoft.SqlServer.Management.Smo.Agent;

using SQLInformation;
using VNC;

namespace SQLInformation.SMO
{
    public static class DataFile
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_DATAFILE;
        private const string LOG_APPNAME = "SQLInformation";

        #region Public Methods

        public static void LoadFromSMO(MSMO.FileGroup fileGroup, Guid fileGroupID, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            foreach (MSMO.DataFile dataFile in fileGroup.Files)
            {
                GetInfoFromSMO(fileGroupID, fileGroup.Name, dataFile, databaseID);
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        #endregion

        #region Private Methods

        private static SQLInformation.Data.ApplicationDataSet.DBDataFilesRow GetInfoFromSMO(Guid fileGroupID, string fileGroupName, MSMO.DataFile dataFile, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.DBDataFilesRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.DBDataFiles
                          where tb.FileGroup_ID == fileGroupID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_DataFile == dataFile.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(dataFile, dataRow);
                }
                else
                {
                    dataRow = Add(fileGroupID, dataFile, databaseID);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
            }
#if TRACE
            VNC.AppLog.Trace4("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.DBDataFilesRow Add(Guid fileGroupID, MSMO.DataFile dataFile, Guid databaseID)
        {
            SQLInformation.Data.ApplicationDataSet.DBDataFilesRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBDataFiles.NewDBDataFilesRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.FileGroup_ID = fileGroupID;
                dataRow.Database_ID = databaseID;
                dataRow.ID_DataFile = dataFile.ID;
                dataRow.Name_DataFile = dataFile.Name;
                dataRow.AvailableSpace = dataFile.AvailableSpace;

//                try
//                {
//                    dataRow.BytesReadFromDisk = (int)dataFile.BytesReadFromDisk; // TODO(crhodes)
//                }
//                catch (Exception ex)
//                {
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
//#endif
//                    dataRow.BytesReadFromDisk = -1;
//                }

//                try
//                {
//                    dataRow.BytesWrittenToDisk = (int)dataFile.BytesWrittenToDisk; // TODO(crhodes)
//                }
//                catch (Exception ex)
//                {
//                    dataRow.BytesWrittenToDisk = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
//#endif
//                }

                dataRow.FileName = dataFile.FileName;
                dataRow.Growth = (int)dataFile.Growth;  // TODO(crhodes)
                dataRow.GrowthType = dataFile.GrowthType.ToString();
                dataRow.ID_DataFile = dataFile.ID;
                dataRow.IsPrimaryFile = dataFile.IsPrimaryFile;
                dataRow.MaxSize = dataFile.MaxSize;

//                try
//                {
//                    dataRow.NumberOfDiskReads = (int)dataFile.NumberOfDiskReads; // TODO(crhodes)
//                }
//                catch (Exception ex)
//                {
//                    dataRow.NumberOfDiskReads = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
//#endif
//                }

//                try
//                {
//                    dataRow.NumberOfDiskWrites = (int)dataFile.NumberOfDiskWrites; // TODO(crhodes)
//                }
//                catch (Exception ex)
//                {
//                    dataRow.NumberOfDiskWrites = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
//#endif
//                }

                dataRow.Size = dataFile.Size;
                dataRow.UsedSpace = dataFile.UsedSpace;

                try
                {
                    dataRow.VolumeFreeSpace = dataFile.VolumeFreeSpace;
                }
                catch (Exception ex)
                {
                    dataRow.VolumeFreeSpace = dataFile.VolumeFreeSpace;
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
#endif
                }

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBDataFiles.AddDBDataFilesRow(dataRow);
                Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }

            return dataRow;
        }

        private static void Update(MSMO.DataFile dataFile, SQLInformation.Data.ApplicationDataSet.DBDataFilesRow dataRow)
        {
            try
            {
                dataFile.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");

                // Add add the Snapshot

                SMO.Helper.TakeDBDataFileSnapShot(dataRow);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBDataFilesRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DBDataFilesTA.Update(Common.ApplicationDataSet.DBDataFiles);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DBDataFilesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.DataFile dataFile, Data.ApplicationDataSet.DBDataFilesRow dataRow)
        {
            try
            {

                try
                {
                    dataRow.AvailableSpace = dataFile.AvailableSpace;
                }
                catch (Exception ex)
                {
                    dataRow.AvailableSpace = -1;
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12);
#endif
                }

//                try
//                {
//                    dataRow.BytesReadFromDisk = dataFile.BytesReadFromDisk;
//                }
//                catch (Exception ex)
//                {
//                    dataRow.BytesReadFromDisk = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 13);
//#endif
//                }

//                try
//                {
//                    dataRow.BytesWrittenToDisk = dataFile.BytesWrittenToDisk;
//                }
//                catch (Exception ex)
//                {
//                    dataRow.BytesWrittenToDisk = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 14);
//#endif
//                }

                dataRow.MaxSize = dataFile.MaxSize;

//                try
//                {
//                    dataRow.NumberOfDiskReads = dataFile.NumberOfDiskReads;
//                }
//                catch (Exception ex)
//                {
//                    dataRow.NumberOfDiskReads = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
//#endif
//                }

//                try
//                {
//                    dataRow.NumberOfDiskWrites = dataFile.NumberOfDiskWrites;
//                }
//                catch (Exception ex)
//                {
//                    dataRow.NumberOfDiskWrites = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 16);
//#endif
//                }

                dataRow.Size = dataFile.Size;
                dataRow.UsedSpace = dataFile.UsedSpace;

                try
                {
                    dataRow.VolumeFreeSpace = dataFile.VolumeFreeSpace;
                }
                catch (Exception ex)
                {
                    dataRow.VolumeFreeSpace = -1;
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 17);
#endif
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

        #endregion

    }
}
