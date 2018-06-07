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
    public static class DatabaseInfo
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_DATABASEINFO;
        private const string LOG_APPNAME = "SQLInformation";

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DatabaseInfoRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DatabaseInfoTA.Update(Common.ApplicationDataSet.DatabaseInfo);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DatabaseInfoRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
            }
        }

        public static void UpdateDataSet(this MSMO.Database database, Data.ApplicationDataSet.DatabaseInfoRow dataRow)
        {
            try
            {
                dataRow.IndexSpaceUsage = database.IndexSpaceUsage;
                dataRow.DataSpaceUsage = database.DataSpaceUsage;
                dataRow.Size = database.Size;
                dataRow.SpaceAvailable = database.SpaceAvailable;
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }
    }
}
