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
    public static class TargetServerGroup
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_TARGETSERVERGROUP;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMOA.JobServer jobServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMOA.TargetServerGroup targetServerGroup in jobServer.TargetServerGroups)
            {
                GetInfoFromSMO(targetServerGroup, jobServerRow);
            }
#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }
        private static SQLInformation.Data.ApplicationDataSet.JSTargetServerGroupsRow GetInfoFromSMO(MSMOA.TargetServerGroup targetServerGroup, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace1(string.Format("Enter ({0})", targetServerGroup.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif

            SQLInformation.Data.ApplicationDataSet.JSTargetServerGroupsRow dataRow = null;

            try
            {
                var dbs = from db in Common.ApplicationDataSet.JSTargetServerGroups
                          where db.JobServer_ID == jobServerRow.ID
                          select db;

                var dbs2 = from db2 in dbs
                           where db2.Name_JSTargetServerGroups == targetServerGroup.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(targetServerGroup, dataRow);
                }
                else
                {
                    dataRow = Add(targetServerGroup, jobServerRow);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
            }

#if TRACE
            VNC.AppLog.Trace1("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.JSTargetServerGroupsRow Add(MSMOA.TargetServerGroup targetServerGroup, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
            SQLInformation.Data.ApplicationDataSet.JSTargetServerGroupsRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.JSTargetServerGroups.NewJSTargetServerGroupsRow();
                dataRow.ID = Guid.NewGuid();

                dataRow.Name_JSTargetServerGroups = targetServerGroup.Name;
                dataRow.JobServer_ID = jobServerRow.ID;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JSTargetServerGroups.AddJSTargetServerGroupsRow(dataRow);
                Common.ApplicationDataSet.JSTargetServerGroupsTA.Update(Common.ApplicationDataSet.JSTargetServerGroups);
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

        private static void Update(MSMOA.TargetServerGroup targetServerGroup, SQLInformation.Data.ApplicationDataSet.JSTargetServerGroupsRow dataRow)
        {
            try
            {

                targetServerGroup.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.JSTargetServerGroupsRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.JSTargetServerGroupsTA.Update(Common.ApplicationDataSet.JSTargetServerGroups);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("JSTargetServerGroupsRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMOA.TargetServerGroup targetServerGroup, Data.ApplicationDataSet.JSTargetServerGroupsRow dataRow)
        {

            try
            {
                //dataRow.X = targetServerGroup.X;
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
