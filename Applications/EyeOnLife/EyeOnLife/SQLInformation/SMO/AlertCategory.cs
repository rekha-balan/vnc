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
    public static class AlertCategory
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_ALERTCATEGORY;
        private const string LOG_APPNAME = "SQLInformation";

        #region Public Methods

        public static void LoadFromSMO(MSMOA.JobServer jobServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            foreach (MSMOA.AlertCategory alertCategory in jobServer.AlertCategories)
            {
                GetInfoFromSMO(alertCategory, jobServerRow);
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        #endregion

        #region Private Methods

        private static SQLInformation.Data.ApplicationDataSet.JSAlertCategoriesRow GetInfoFromSMO(MSMOA.AlertCategory alertCategory, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter ({0})", alertCategory.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif

            SQLInformation.Data.ApplicationDataSet.JSAlertCategoriesRow dataRow = null;

            try
            {
                var dbs = from db in Common.ApplicationDataSet.JSAlertCategories
                          where db.JobServer_ID == jobServerRow.ID
                          select db;

                var dbs2 = from db2 in dbs
                           where db2.Name_JSAlertCategory == alertCategory.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(alertCategory, dataRow);
                }
                else
                {
                    dataRow = Add(alertCategory, jobServerRow);
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

        private static SQLInformation.Data.ApplicationDataSet.JSAlertCategoriesRow Add(MSMOA.AlertCategory alertCategory, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
            SQLInformation.Data.ApplicationDataSet.JSAlertCategoriesRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.JSAlertCategories.NewJSAlertCategoriesRow();
                dataRow.ID = Guid.NewGuid();

                dataRow.Name_JSAlertCategory = alertCategory.Name;
                dataRow.JobServer_ID = jobServerRow.ID;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JSAlertCategories.AddJSAlertCategoriesRow(dataRow);
                Common.ApplicationDataSet.JSAlertCategoriesTA.Update(Common.ApplicationDataSet.JSAlertCategories);
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

        private static void Update(MSMOA.AlertCategory alertCategory, SQLInformation.Data.ApplicationDataSet.JSAlertCategoriesRow dataRow)
        {
            try
            {
                alertCategory.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.JSAlertCategoriesRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.JSAlertCategoriesTA.Update(Common.ApplicationDataSet.JSAlertCategories);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("JSAlertCategoriesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        private static void UpdateDataSet(this MSMOA.AlertCategory alertCategory, Data.ApplicationDataSet.JSAlertCategoriesRow dataRow)
        {
            try
            {
                //dataRow.X = alertCategory.X;
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
