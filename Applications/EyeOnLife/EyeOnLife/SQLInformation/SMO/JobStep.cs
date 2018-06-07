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
    public static class JobStep
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.SQLINFORMATION_SMO_JOBSTEP;
        private const string LOG_APPNAME = "SQLInformation";

        #region Public Methods

        public static void LoadFromSMO(MSMOA.Job job, Guid jobID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMOA.JobStep jobStep in job.JobSteps)
            {
                GetInfoFromSMO(jobStep, jobID);
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        #endregion

        #region Private Methods

        private static SQLInformation.Data.ApplicationDataSet.JSJobStepsRow GetInfoFromSMO(MSMOA.JobStep jobStep, Guid jobID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter ({0})", jobStep.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif

            SQLInformation.Data.ApplicationDataSet.JSJobStepsRow dataRow = null;

            try
            {
                var dbs = from db in Common.ApplicationDataSet.JSJobSteps
                          where db.JSJob_ID == jobID
                          select db;

                var dbs2 = from db2 in dbs
                           where db2.Name_JSJobStep == jobStep.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(jobStep, dataRow);
                }
                else
                {
                    dataRow = Add(jobStep, jobID);
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
  
        private static SQLInformation.Data.ApplicationDataSet.JSJobStepsRow Add(MSMOA.JobStep jobStep, Guid jobID)
        {
            SQLInformation.Data.ApplicationDataSet.JSJobStepsRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.JSJobSteps.NewJSJobStepsRow();
                dataRow.ID = Guid.NewGuid();

                dataRow.Name_JSJobStep = jobStep.Name;
                
                dataRow.JSJob_ID = jobID;

                dataRow.DatabaseName = jobStep.DatabaseName;
                dataRow.DatabaseUserName = jobStep.DatabaseUserName;
                dataRow.LastRunDate = jobStep.LastRunDate;
                dataRow.LastRunDuration = jobStep.LastRunDuration;
                dataRow.LastRunOutcome = jobStep.LastRunOutcome.ToString();
                dataRow.LastRunRetries = jobStep.LastRunRetries;
                dataRow.ProxyName = jobStep.ProxyName;
                dataRow.Server = jobStep.Server;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JSJobSteps.AddJSJobStepsRow(dataRow);
                Common.ApplicationDataSet.JSJobStepsTA.Update(Common.ApplicationDataSet.JSJobSteps);
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

        private static void Update(MSMOA.JobStep jobStep, SQLInformation.Data.ApplicationDataSet.JSJobStepsRow dataRow)
        {
            try
            {
                jobStep.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.JSJobStepsRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.JSJobStepsTA.Update(Common.ApplicationDataSet.JSJobSteps);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("JSJobStepsRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMOA.JobStep jobStep, Data.ApplicationDataSet.JSJobStepsRow dataRow)
        {
            try
            {
                dataRow.DatabaseName = jobStep.DatabaseName;
                dataRow.DatabaseUserName = jobStep.DatabaseUserName;
                dataRow.LastRunDate = jobStep.LastRunDate;
                dataRow.LastRunDuration = jobStep.LastRunDuration;
                dataRow.LastRunOutcome = jobStep.LastRunOutcome.ToString();
                dataRow.LastRunRetries = jobStep.LastRunRetries;
                dataRow.ProxyName = jobStep.ProxyName;
                dataRow.Server = jobStep.Server;
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

        #endregion

    }
}
