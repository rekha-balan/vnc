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
    public static class DatabaseRole
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_DATABASEROLE;
        private const string LOG_APPNAME = "SQLInformation";

        #region Public Methods

        public static void LoadFromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            // This will be a problem if skip IsFixedRole, see below.

            //MarkExistingItemsAsNotFound(databaseID);    // This enables cleanup of items that once existed but were deleted.

            foreach (MSMO.DatabaseRole databaseRole in database.Roles)
            {
                if (databaseRole.IsFixedRole)
                {
                    continue;   // Skip Fixed Roles
                }

                SQLInformation.Data.ApplicationDataSet.DBRolesRow rolesRow = GetInfoFromSMO(databaseRole, databaseID);
                rolesRow.NotFound = false;
            }

            Common.ApplicationDataSet.DBRolesTA.Update(Common.ApplicationDataSet.DBRoles);
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        #endregion

        #region Private Methods

        private static void MarkExistingItemsAsNotFound(Guid databaseID)
        {
            var previouslyFound = Common.ApplicationDataSet.DBRoles.Where(i => i.Database_ID == databaseID);

            foreach (var pf in previouslyFound)
            {
                pf.NotFound = true;
            }
        }

        private static SQLInformation.Data.ApplicationDataSet.DBRolesRow GetInfoFromSMO(MSMO.DatabaseRole databaseRole, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.DBRolesRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.DBRoles
                          where tb.Database_ID == databaseID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_Role == databaseRole.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(databaseRole, dataRow);
                }
                else
                {
                    dataRow = Add(databaseRole, databaseID);
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

        private static SQLInformation.Data.ApplicationDataSet.DBRolesRow Add(MSMO.DatabaseRole databaseRole, Guid databaseID)
        {
            SQLInformation.Data.ApplicationDataSet.DBRolesRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBRoles.NewDBRolesRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.Database_ID = databaseID;
                dataRow.IsFixedRole = databaseRole.IsFixedRole;
                dataRow.Name_Role = databaseRole.Name;
                dataRow.CreateDate = databaseRole.CreateDate;
                dataRow.DateLastModified = databaseRole.DateLastModified;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBRoles.AddDBRolesRow(dataRow);
                Common.ApplicationDataSet.DBRolesTA.Update(Common.ApplicationDataSet.DBRoles);
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

        private static void Update(MSMO.DatabaseRole databaseRole, SQLInformation.Data.ApplicationDataSet.DBRolesRow dataRow)
        {
            try
            {
                databaseRole.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBRolesRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DBRolesTA.Update(Common.ApplicationDataSet.DBRoles);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DBRolesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        private static void UpdateDataSet(this MSMO.DatabaseRole databaseRole, Data.ApplicationDataSet.DBRolesRow dataRow)
        {
            try
            {
                dataRow.CreateDate = databaseRole.CreateDate;
                dataRow.DateLastModified = databaseRole.DateLastModified;
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

        #endregion

    }
}
