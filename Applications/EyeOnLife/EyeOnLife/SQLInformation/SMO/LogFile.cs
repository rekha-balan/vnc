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
    public static class LogFile
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_LOGFILE;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.LogFile logFile in database.LogFiles)
            {
                Get_DBLogFile_FromSMO(logFile, databaseID);
            }

#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.DBLogFilesRow Get_DBLogFile_FromSMO(MSMO.LogFile logFile, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif

            SQLInformation.Data.ApplicationDataSet.DBLogFilesRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.DBLogFiles
                          where tb.Database_ID == databaseID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_LogFile == logFile.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(logFile, dataRow);
                }
                else
                {
                    dataRow = Add(logFile, databaseID);
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

        private static SQLInformation.Data.ApplicationDataSet.DBLogFilesRow Add( MSMO.LogFile logFile, Guid databaseID)
        {
            SQLInformation.Data.ApplicationDataSet.DBLogFilesRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBLogFiles.NewDBLogFilesRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.Database_ID = databaseID;
                dataRow.Name_LogFile = logFile.Name;

//                try
//                {
//                    dataRow.BytesReadFromDisk = (int)logFile.BytesReadFromDisk; // TODO(crhodes)
//                }
//                catch (Exception ex)
//                {
//                    dataRow.BytesReadFromDisk = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
//#endif
//                }
//                try
//                {
//                    dataRow.BytesWrittenToDisk = (int)logFile.BytesWrittenToDisk; // TODO(crhodes)
//                }
//                catch (Exception ex)
//                {
//                    dataRow.BytesWrittenToDisk = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
//#endif
//                }

                dataRow.FileName = logFile.FileName;
                dataRow.Growth = (int)logFile.Growth;  // TODO(crhodes)
                dataRow.GrowthType = logFile.GrowthType.ToString();
                //newLogFile.ID_LogFile = logFile.ID;
                //newLogFile.IsPrimaryFile = logFile.IsPrimaryFile;
                dataRow.MaxSize = (int)logFile.MaxSize;

//                try
//                {
//                    dataRow.NumberOfDiskReads = (int)logFile.NumberOfDiskReads; // TODO(crhodes)
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
//                    dataRow.NumberOfDiskWrites = (int)logFile.NumberOfDiskWrites; // TODO(crhodes)
//                }
//                catch (Exception ex)
//                {
//                    dataRow.NumberOfDiskWrites = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
//#endif
//                }

                dataRow.Size = (int)logFile.Size;
                dataRow.UsedSpace = (int)logFile.UsedSpace;

                try
                {
                    dataRow.VolumeFreeSpace = logFile.VolumeFreeSpace;
                }
                catch (Exception ex)
                {
                    dataRow.VolumeFreeSpace = logFile.VolumeFreeSpace;
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
#endif
                }

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBLogFiles.AddDBLogFilesRow(dataRow);
                Common.ApplicationDataSet.DBLogFilesTA.Update(Common.ApplicationDataSet.DBLogFiles);
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

        private static void Update(MSMO.LogFile logFile, SQLInformation.Data.ApplicationDataSet.DBLogFilesRow dataRow)
        {
            try
            {
                logFile.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");

                // Add the Snapshot

                SMO.Helper.TakeDBLogFileSnapShot(dataRow);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBLogFilesRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DBLogFilesTA.Update(Common.ApplicationDataSet.DBLogFiles);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DBLogFilesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.LogFile logFile, Data.ApplicationDataSet.DBLogFilesRow dataRow)
        {
            try
            {
//                try
//                {
//                    dataRow.BytesReadFromDisk = logFile.BytesReadFromDisk;
//                }
//                catch (Exception ex)
//                {
//                    dataRow.BytesReadFromDisk = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 14);
//#endif
//                }

//                try
//                {
//                    dataRow.BytesWrittenToDisk = logFile.BytesWrittenToDisk;
//                }
//                catch (Exception ex)
//                {
//                    dataRow.BytesWrittenToDisk = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
//#endif
//                }

                dataRow.MaxSize = logFile.MaxSize;

//                try
//                {
//                    dataRow.NumberOfDiskReads = logFile.NumberOfDiskReads;
//                }
//                catch (Exception ex)
//                {
//                    dataRow.NumberOfDiskReads = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 16);
//#endif
//                }

//                try
//                {
//                    dataRow.NumberOfDiskWrites = logFile.NumberOfDiskWrites;
//                }
//                catch (Exception ex)
//                {
//                    dataRow.NumberOfDiskWrites = -1;
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 17);
//#endif
//                }

                dataRow.Size = logFile.Size;
                dataRow.UsedSpace = logFile.UsedSpace;

                try
                {
                    dataRow.VolumeFreeSpace = logFile.VolumeFreeSpace;
                }
                catch (Exception ex)
                {
                    dataRow.VolumeFreeSpace = -1;
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 18);
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

    }
}
