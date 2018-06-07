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
    public static class FileGroup
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_FILEGROUP;
        private const string LOG_APPNAME = "SQLInformation";

        #region Public Methods

        public static void LoadFromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            foreach (MSMO.FileGroup fileGroup in database.FileGroups)
            {
                GetInfoFromSMO(fileGroup, databaseID);
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        #endregion

        #region Private Methods

        private static SQLInformation.Data.ApplicationDataSet.DBFileGroupsRow GetInfoFromSMO(MSMO.FileGroup fileGroup, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.DBFileGroupsRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.DBFileGroups
                          where tb.Database_ID == databaseID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_FileGroup == fileGroup.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(fileGroup, dataRow);
                }
                else
                {
                    dataRow = Add(fileGroup, databaseID);
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

        private static SQLInformation.Data.ApplicationDataSet.DBFileGroupsRow Add(MSMO.FileGroup fileGroup, Guid databaseID)
        {
            SQLInformation.Data.ApplicationDataSet.DBFileGroupsRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBFileGroups.NewDBFileGroupsRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.Database_ID = databaseID;
                dataRow.Name_FileGroup = fileGroup.Name;
                dataRow.Size = fileGroup.Size;

                Common.ApplicationDataSet.DBFileGroups.AddDBFileGroupsRow(dataRow);

                //fileGroupID = newFileGroup.ID;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                // Need to update before calling LoadDBDataFiles_FromSMO
                Common.ApplicationDataSet.DBFileGroupsTA.Update(Common.ApplicationDataSet.DBFileGroups);

                DataFile.LoadFromSMO(fileGroup, dataRow.ID, databaseID);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }

            return dataRow;
        }

        private static void Update(MSMO.FileGroup fileGroup, SQLInformation.Data.ApplicationDataSet.DBFileGroupsRow dataRow)
        {
            try
            {
                fileGroup.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");

                // Add the snapshot

                SMO.Helper.TakeDBFileGroupSnapShot(dataRow);

                // Add info about the DataFiles belonging to this filegroup.

                DataFile.LoadFromSMO(fileGroup, dataRow.ID, dataRow.Database_ID);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBFileGroupsRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DBFileGroupsTA.Update(Common.ApplicationDataSet.DBFileGroups);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DBFileGroupsRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        private static void UpdateDataSet(this MSMO.FileGroup fileGroup, Data.ApplicationDataSet.DBFileGroupsRow dataRow)
        {
            try
            {
                dataRow.Size = fileGroup.Size;
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

        #endregion

    }
}
