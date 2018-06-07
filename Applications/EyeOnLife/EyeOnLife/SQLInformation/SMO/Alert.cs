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
    public static class Alert
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_ALERT;
        private const string LOG_APPNAME = "SQLInformation";

        #region Public Methods

        public static void LoadFromSMO(MSMOA.JobServer jobServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMOA.Alert alert in jobServer.Alerts)
            {
                GetInfoFromSMO(alert, jobServerRow);
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        #endregion

        #region Private Methods

        private static SQLInformation.Data.ApplicationDataSet.JSAlertsRow GetInfoFromSMO(MSMOA.Alert alert, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter ({0})", alert.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif

            SQLInformation.Data.ApplicationDataSet.JSAlertsRow dataRow = null;

            try
            {
                var dbs = from db in Common.ApplicationDataSet.JSAlerts
                          where db.JobServer_ID == jobServerRow.ID
                          select db;

                var dbs2 = from db2 in dbs
                           where db2.Name_JSAlert == alert.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(alert, dataRow);
                }
                else
                {
                    dataRow = Add(alert, jobServerRow);
                }
            }
            catch (Exception ex)
            {
                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 3);
            }

#if TRACE
            VNC.AppLog.Trace4("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.JSAlertsRow Add(MSMOA.Alert alert, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
            SQLInformation.Data.ApplicationDataSet.JSAlertsRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.JSAlerts.NewJSAlertsRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.JobServer_ID = jobServerRow.ID;
                dataRow.Name_JSAlert = alert.Name;
                dataRow.AlertType = alert.AlertType.ToString();
                dataRow.CategoryName = alert.CategoryName;
                dataRow.CountResetDate = alert.CountResetDate;
                dataRow.DatabaseName = alert.DatabaseName;
                dataRow.IsEnabled = alert.IsEnabled;
                dataRow.JobID = alert.JobID;
                dataRow.JobName = alert.JobName;
                dataRow.LastOccurrenceDate = alert.LastOccurrenceDate;
                dataRow.LastResponseDate = alert.LastResponseDate;
                dataRow.NotificationMessage = alert.NotificationMessage;
                dataRow.OccurrenceCount = alert.OccurrenceCount;
                dataRow.Severity = alert.Severity;

                try
                {
                    dataRow.WmiEventNamespace = alert.WmiEventNamespace;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
#endif
                    dataRow.WmiEventNamespace = "<Exception>";
                }

                try
                {
                    dataRow.WmiEventQuery = alert.WmiEventQuery;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
#endif
                    dataRow.WmiEventNamespace = "<Exception>";
                }

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JSAlerts_Add(dataRow);
            }
            catch (Exception ex)
            {
                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 7);
            }

            return dataRow;
        }

        private static void Update(MSMOA.Alert alert, SQLInformation.Data.ApplicationDataSet.JSAlertsRow dataRow)
        {
            try
            {
                //dataRow.X = alert.X;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JSAlerts_Update();
            }
            catch (Exception ex)
            {
                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 8);
            }
        }

        private static void ReportException(Exception ex, Data.ApplicationDataSet.JSAlertsRow dataRow, int eventID)
        {
            string errorMessage = "";

            if (dataRow != null)
            {
                errorMessage = string.Format("JSAlertsRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                dataRow.SnapShotError = errorMessage;
                Common.ApplicationDataSet.JSAlerts_Update();
            }
            else
            {
                errorMessage = string.Format("JSAlertsRow(null) - ex:{0} ex.Inner:{1}", ex, ex.InnerException);
            }

            VNC.AppLog.Error(errorMessage, LOG_APPNAME, eventID);
        }

        #endregion

    }
}
