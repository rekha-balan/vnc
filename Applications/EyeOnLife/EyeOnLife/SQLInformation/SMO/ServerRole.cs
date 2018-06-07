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
    public static class ServerRole
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_SERVERROLE;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Server instance, Guid instanceID, string instanceName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.ServerRole serverRole in instance.Roles)
            {
                //try
                //{
                //    if (serverRole.IsFixedRole)
                //    {
                //        continue;   // Skip Fixed Roles
                //    }
                //}
                //catch (Exception ex)
                //{
                //    // Not available on SQL 2000 :(
                //}

                GetInfoFromSMO(serverRole, instanceID, instanceName);
            }
#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }


        private static SQLInformation.Data.ApplicationDataSet.ServerRolesRow GetInfoFromSMO(MSMO.ServerRole serverRole, Guid instanceID, string instanceName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.ServerRolesRow dataRow = null;

            try
            {
                var dbs = from sr in Common.ApplicationDataSet.ServerRoles
                          where sr.Instance_ID == instanceID
                          select sr;

                var dbs2 = from db2 in dbs
                           where db2.Name_ServerRole == serverRole.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(serverRole, dataRow);
                }
                else
                {
                    dataRow = Add(serverRole, instanceID, instanceName);
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

        private static SQLInformation.Data.ApplicationDataSet.ServerRolesRow Add(MSMO.ServerRole serverRole, Guid instanceID, string instanceName)
        {
            SQLInformation.Data.ApplicationDataSet.ServerRolesRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.ServerRoles.NewServerRolesRow();

                dataRow.ID = Guid.NewGuid();

                dataRow.Name_ServerRole = serverRole.Name;
                dataRow.Instance_ID = instanceID;
                dataRow.Name_Instance = instanceName;

                // Not until SQL 2012
                //try
                //{
                //    newServerRoleRow.CreateDate = serverRole.DateCreated;
                //}
                //catch (Exception ex)
                //{
                //    VNC.AppLog.Warning(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
                //}

                //try
                //{
                //    newServerRoleRow.DateLastModified = serverRole.DateModified;
                //}
                //catch (Exception ex)
                //{
                //    VNC.AppLog.Warning(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
                //}

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.ServerRoles.AddServerRolesRow(dataRow);
                Common.ApplicationDataSet.ServerRolesTA.Update(Common.ApplicationDataSet.ServerRoles);
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

        private static void Update(MSMO.ServerRole serverRole, SQLInformation.Data.ApplicationDataSet.ServerRolesRow dataRow)
        {
            try
            {
                serverRole.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.ServerRolesRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.ServerRolesTA.Update(Common.ApplicationDataSet.ServerRoles);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("ServerRolesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.ServerRole serverRole, Data.ApplicationDataSet.ServerRolesRow dataRow)
        {
            try
            {
                //dataRow.X = serverRole.X;
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
