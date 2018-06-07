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
    public static class JobSchedule
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_JOBSCHEDULE;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMOA.JobServer jobServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMOA.JobSchedule jobSchedule in jobServer.SharedSchedules)
            {
                GetInfoFromSMO(jobSchedule, jobServerRow);

                // Perhaps this is where to get Steps
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.JSJobSchedulesRow GetInfoFromSMO(MSMOA.JobSchedule jobSchedule, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter ({0})", jobSchedule.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.JSJobSchedulesRow dataRow = null;

            try
            {
                // TODO(crhodes) Needs work.  See TBTrigger for hints
                // JSJob_ID may be null.
                var jbs = from js in Common.ApplicationDataSet.JSJobSchedules
                          where js.JSJob_ID == jobServerRow.ID
                          select js;

                var dbs2 = from db2 in jbs
                           where db2.Name_JSJobSchedule == jobSchedule.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(jobSchedule, dataRow);
                }
                else
                {
                    dataRow = Add(jobSchedule);
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

        private static SQLInformation.Data.ApplicationDataSet.JSJobSchedulesRow Add(MSMOA.JobSchedule jobSchedule)
        {
            SQLInformation.Data.ApplicationDataSet.JSJobSchedulesRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.JSJobSchedules.NewJSJobSchedulesRow();
                dataRow.ID = Guid.NewGuid();

                dataRow.Name_JSJobSchedule = jobSchedule.Name;
                //jobScheduleRow.JSJob_ID = jobID;

                dataRow.ActiveEndDate = jobSchedule.ActiveEndDate;
                dataRow.ActiveEndTimeOfDay = jobSchedule.ActiveEndTimeOfDay;
                dataRow.ActiveStartDate = jobSchedule.ActiveStartDate;
                dataRow.ActiveStartTimeOfDay = jobSchedule.ActiveStartTimeOfDay;
                dataRow.DateCreated = jobSchedule.DateCreated;
                dataRow.FrequencyInterval = jobSchedule.FrequencyInterval.ToString();
                dataRow.FrequencyRecurrenceFactor = jobSchedule.FrequencyRecurrenceFactor.ToString();
                dataRow.FrequencyRelativeIntervals = jobSchedule.FrequencyRelativeIntervals.ToString();
                dataRow.FrequencySubDayInterval = jobSchedule.FrequencySubDayInterval.ToString();
                dataRow.FrequencySubDayTypes = jobSchedule.FrequencySubDayTypes.ToString();
                dataRow.FrequencyTypes = jobSchedule.FrequencyTypes.ToString();
                dataRow.ID_JobSchedule = jobSchedule.ID;
                dataRow.IsEnabled = jobSchedule.IsEnabled;
                dataRow.JobCount = jobSchedule.JobCount;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JSJobSchedules.AddJSJobSchedulesRow(dataRow);
                Common.ApplicationDataSet.JSJobSchedulesTA.Update(Common.ApplicationDataSet.JSJobSchedules);
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

        private static void Update(MSMOA.JobSchedule jobSchedule, SQLInformation.Data.ApplicationDataSet.JSJobSchedulesRow dataRow)
        {
            try
            {
                jobSchedule.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.JSJobSchedulesRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.JSJobSchedulesTA.Update(Common.ApplicationDataSet.JSJobSchedules);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("JSJobSchedulesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMOA.JobSchedule jobSchedule, Data.ApplicationDataSet.JSJobSchedulesRow dataRow)
        {
            try
            {
                dataRow.ActiveEndDate = jobSchedule.ActiveEndDate;
                dataRow.ActiveEndTimeOfDay = jobSchedule.ActiveEndTimeOfDay;
                dataRow.ActiveStartDate = jobSchedule.ActiveStartDate;
                dataRow.ActiveStartTimeOfDay = jobSchedule.ActiveStartTimeOfDay;
                dataRow.DateCreated = jobSchedule.DateCreated;
                dataRow.FrequencyInterval = jobSchedule.FrequencyInterval.ToString();
                dataRow.FrequencyRecurrenceFactor = jobSchedule.FrequencyRecurrenceFactor.ToString();
                dataRow.FrequencyRelativeIntervals = jobSchedule.FrequencyRelativeIntervals.ToString();
                dataRow.FrequencySubDayInterval = jobSchedule.FrequencySubDayInterval.ToString();
                dataRow.FrequencySubDayTypes = jobSchedule.FrequencySubDayTypes.ToString();
                dataRow.FrequencyTypes = jobSchedule.FrequencyTypes.ToString();
                dataRow.ID_JobSchedule = jobSchedule.ID;
                dataRow.IsEnabled = jobSchedule.IsEnabled;
                dataRow.JobCount = jobSchedule.JobCount;
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
