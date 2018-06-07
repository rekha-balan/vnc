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
    public static class TargetServer
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_TARGETSERVER;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMOA.JobServer jobServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMOA.TargetServer targetServer in jobServer.TargetServers)
            {
                GetInfoFromSMO(targetServer, jobServerRow);
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.JSTargetServersRow GetInfoFromSMO(MSMOA.TargetServer targetServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter ({0})", targetServer.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif

            SQLInformation.Data.ApplicationDataSet.JSTargetServersRow dataRow = null;

            try
            {
                var dbs = from db in Common.ApplicationDataSet.JSTargetServers
                          where db.JobServer_ID == jobServerRow.ID
                          select db;

                var dbs2 = from db2 in dbs
                           where db2.Name_JSTargetServer == targetServer.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(targetServer, dataRow);
                }
                else
                {
                    dataRow = Add(targetServer, jobServerRow);
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

        private static SQLInformation.Data.ApplicationDataSet.JSTargetServersRow Add(MSMOA.TargetServer targetServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
            SQLInformation.Data.ApplicationDataSet.JSTargetServersRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.JSTargetServers.NewJSTargetServersRow();
                dataRow.ID = Guid.NewGuid();
                dataRow.JobServer_ID = jobServerRow.ID;
                dataRow.Name_JSTargetServer = targetServer.Name;
                dataRow.EnlistDate = targetServer.EnlistDate;
                dataRow.LastPollDate = targetServer.LastPollDate;
                dataRow.LocalTime = targetServer.LocalTime;
                dataRow.Location = targetServer.Location;
                dataRow.PendingInstructions = targetServer.PendingInstructions;
                dataRow.PollingInterval = targetServer.PollingInterval;
                dataRow.State = targetServer.State.ToString();
                dataRow.Status = targetServer.Status.ToString();

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JSTargetServers.AddJSTargetServersRow(dataRow);
                Common.ApplicationDataSet.JSTargetServersTA.Update(Common.ApplicationDataSet.JSTargetServers);
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

        private static void Update(MSMOA.TargetServer targetServer, SQLInformation.Data.ApplicationDataSet.JSTargetServersRow dataRow)
        {
            try
            {
                targetServer.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.JSTargetServersRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.JSTargetServersTA.Update(Common.ApplicationDataSet.JSTargetServers);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("JSTargetServersRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMOA.TargetServer targetServer, Data.ApplicationDataSet.JSTargetServersRow dataRow)
        {
            try
            {
                //dataRow.X = targetServer.X;
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
