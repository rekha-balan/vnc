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
    public static class ServerDdlTrigger
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_LINKEDSERVER;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Server instance, Guid instanceID, string instanceName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            try
            {
                foreach (MSMO.ServerDdlTrigger serverDdlTrigger in instance.Triggers)
                {
                    GetInfoFromSMO(instanceID, serverDdlTrigger, instanceName);
                }
            }
            catch (Exception)
            {
                VNC.AppLog.Warning("Cannot enumerate instance.Triggers.  Consider disabling ServerDdlTrigger expansion on instance", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
            }
#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.ServerDdlTriggersRow GetInfoFromSMO(Guid instanceID, MSMO.ServerDdlTrigger serverDdlTrigger, string instanceName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif
            SQLInformation.Data.ApplicationDataSet.ServerDdlTriggersRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.ServerDdlTriggers
                          where tb.Instance_ID == instanceID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_ServerDdlTrigger == serverDdlTrigger.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(serverDdlTrigger, dataRow);
                }
                else
                {
                    dataRow = Add(instanceID, serverDdlTrigger, instanceName);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
            }
#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.ServerDdlTriggersRow Add(Guid instanceID, MSMO.ServerDdlTrigger serverDdlTrigger, string instanceName)
        {
            SQLInformation.Data.ApplicationDataSet.ServerDdlTriggersRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.ServerDdlTriggers.NewServerDdlTriggersRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.Name_ServerDdlTrigger = serverDdlTrigger.Name;
                dataRow.Instance_ID = instanceID;
                dataRow.Name_Instance = instanceName;
                
                dataRow.CreateDate = serverDdlTrigger.CreateDate;
                dataRow.DateLastModified = serverDdlTrigger.DateLastModified;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.ServerDdlTriggers.AddServerDdlTriggersRow(dataRow);
                Common.ApplicationDataSet.ServerDdlTriggersTA.Update(Common.ApplicationDataSet.ServerDdlTriggers);
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

        private static void Update(MSMO.ServerDdlTrigger serverDdlTrigger, SQLInformation.Data.ApplicationDataSet.ServerDdlTriggersRow dataRow)
        {
            try
            {
                serverDdlTrigger.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.ServerDdlTriggersRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.ServerDdlTriggersTA.Update(Common.ApplicationDataSet.ServerDdlTriggers);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("ServerDdlTriggersRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.ServerDdlTrigger serverDdlTrigger, SQLInformation.Data.ApplicationDataSet.ServerDdlTriggersRow dataRow)
        {

            try
            {
                //dataRow.DateLastModified = serverDdlTrigger.DateLastModified;
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
