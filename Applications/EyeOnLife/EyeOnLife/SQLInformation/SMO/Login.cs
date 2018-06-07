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
    public static class Login
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_LOGIN;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Server instance, Guid instanceID, string instanceName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.Login login in instance.Logins)
            {
                if (login.IsSystemObject)
                {
                    continue;   // Skip System Tables
                }

                GetInfoFromSMO(instanceID, login, instanceName);
            }
#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.LoginsRow GetInfoFromSMO(Guid instanceID, MSMO.Login login, string instanceName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.LoginsRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.Logins
                          where tb.Instance_ID == instanceID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_Login == login.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();

                    Update(login, dataRow);
                }
                else
                {
                    dataRow = Add(instanceID, login, instanceName);
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

        private static SQLInformation.Data.ApplicationDataSet.LoginsRow Add(Guid instanceID, MSMO.Login login, string instanceName)
        {
            SQLInformation.Data.ApplicationDataSet.LoginsRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.Logins.NewLoginsRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.Name_Login = login.Name;
                dataRow.Instance_ID = instanceID;
                dataRow.Name_Instance = instanceName;
                dataRow.CreateDate = login.CreateDate;
                dataRow.DateLastModified = login.DateLastModified;
                dataRow.DefaultDatabase = login.DefaultDatabase;
                dataRow.LoginType = login.LoginType.ToString();

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.Logins.AddLoginsRow(dataRow);
                Common.ApplicationDataSet.LoginsTA.Update(Common.ApplicationDataSet.Logins);
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

        private static void Update(MSMO.Login login, SQLInformation.Data.ApplicationDataSet.LoginsRow dataRow)
        {
            try
            {
                login.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.LoginsRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.LoginsTA.Update(Common.ApplicationDataSet.Logins);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("LoginsRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.Login login, Data.ApplicationDataSet.LoginsRow dataRow)
        {
            try
            {
                //dataRow.X = login.X;
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
