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
    public static class User
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_USER;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Database database, Guid databaseID, string databaseName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.User user in database.Users)
            {
                if (user.IsSystemObject)
                {
                    continue;   // Skip System Users
                }

                GetInfoFromSMO(user, databaseID, databaseName);
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.DBUsersRow GetInfoFromSMO(MSMO.User user, Guid databaseID, string databaseName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.DBUsersRow dataRow = null;
      
            try
            {
                var dbs = from tb in Common.ApplicationDataSet.DBUsers
                            where tb.Database_ID == databaseID
                            select tb;

                var dbs2 = from db2 in dbs
                            where db2.Name_User == user.Name
                            select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(user, dataRow);
                }
                else
                {
                    dataRow = Add(user, databaseID, databaseName);
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

        private static SQLInformation.Data.ApplicationDataSet.DBUsersRow Add(MSMO.User user, Guid databaseID, string databaseName)
        {
            SQLInformation.Data.ApplicationDataSet.DBUsersRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBUsers.NewDBUsersRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.Database_ID = databaseID;
                dataRow.Name_Database = databaseName;
                dataRow.IsSystemObject = user.IsSystemObject;
                dataRow.Name_User = user.Name;
                dataRow.CreateDate = user.CreateDate;
                dataRow.DateLastModified = user.DateLastModified;
                dataRow.Login = user.Login;
                dataRow.LoginType = user.LoginType.ToString();

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBUsers.AddDBUsersRow(dataRow);
                Common.ApplicationDataSet.DBUsersTA.Update(Common.ApplicationDataSet.DBUsers);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
                // TODO(crhodes):  Need to wrap anything above that throws an exception
                // that we want to ignore, e.g. property not available because of
                // SQL Edition.

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }

            return dataRow;
        }

        private static void Update(MSMO.User user, SQLInformation.Data.ApplicationDataSet.DBUsersRow dataRow)
        {
            try
            {
                user.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBUsersRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DBUsersTA.Update(Common.ApplicationDataSet.DBUsers);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DBUsersRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.User user, Data.ApplicationDataSet.DBUsersRow dataRow)
        {

            try
            {
                //dataRow.X = user.X;
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

    }
}
