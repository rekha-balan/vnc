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
    public static class SharedSchedule
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_SHAREDSCHEDULE;
        private const string LOG_APPNAME = "SQLInformation";

        private static void Load_JSSharedSchedules_FromSMO(MSMOA.JobServer jobServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMOA.JobSchedule jobSchedule in jobServer.SharedSchedules)
            {
                GetInfoFromSMO(jobSchedule, jobServerRow);
            }
#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.JSJobSchedulesRow GetInfoFromSMO(MSMOA.JobSchedule jobSchedule, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter ({0})", jobSchedule.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif

            SQLInformation.Data.ApplicationDataSet.JSJobSchedulesRow dataRow = null;

            //// TODO(crhodes) Needs work.  See TBTrigger for hints
            //var dbs = from db in Common.ApplicationDataSet.JSJobSchedules
            //          where db. == jobServerRow.ID
            //          select db;

            //var dbs2 = from db2 in dbs
            //           where db2.Name_JSJobSchedule == jobSchedule.Name
            //           select db2;

            //if (dbs2.Count() > 0)
            //{
            //    // We have found a JobServer with a matching name.
            //    // TODO(crhodes): There should only be one.

            //    try
            //    {
            //        foreach (var item in dbs2)
            //        {
            //            jobScheduleRow = item;

            //            //if (targetServerRow.IsMonitored)
            //            //{
            //            //    // Update the main entry

            //            jobSchedule.UpdateDataSet(item);
            //            jobScheduleRow.SnapShotDate = DateTime.Now;
            //            jobScheduleRow.SnapShotError = "";
            //            Common.ApplicationDataSet.JSJobSchedulesTA.Update(Common.ApplicationDataSet.JSJobSchedules);

            //            //    // Add add the Snapshot

            //            //    //TakeJobServerSnapShot(jobServerRow);
            //            //}
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        jobScheduleRow.SnapShotDate = DateTime.Now;
            //        jobScheduleRow.SnapShotError = ex.ToString().Substring(0, 256);
            //        Common.ApplicationDataSet.JSJobSchedulesTA.Update(Common.ApplicationDataSet.JSJobSchedules);
            //        VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 41);
            //    }
            //}
            //else
            //{
            //    // Add new one
            //    try
            //    {
            //        jobScheduleRow = Common.ApplicationDataSet.JSJobSchedules.NewJSJobSchedulesRow();
            //        jobScheduleRow.ID = Guid.NewGuid();

            //        jobScheduleRow.Name_JSJobSchedule = jobSchedule.Name;
            //        jobScheduleRow.JSJob_ID = jobID;

            //        jobScheduleRow.ActiveEndDate = jobSchedule.ActiveEndDate;
            //        jobScheduleRow.ActiveEndTimeOfDay = jobSchedule.ActiveEndTimeOfDay;
            //        jobScheduleRow.ActiveStartDate = jobSchedule.ActiveStartDate;
            //        jobScheduleRow.ActiveStartTimeOfDay = jobSchedule.ActiveStartTimeOfDay;
            //        jobScheduleRow.DateCreated = jobSchedule.DateCreated;
            //        jobScheduleRow.FrequencyInterval = jobSchedule.FrequencyInterval.ToString();
            //        jobScheduleRow.FrequencyRecurrenceFactor = jobSchedule.FrequencyRecurrenceFactor.ToString();
            //        jobScheduleRow.FrequencyRelativeIntervals = jobSchedule.FrequencyRelativeIntervals.ToString();
            //        jobScheduleRow.FrequencySubDayInterval = jobSchedule.FrequencySubDayInterval.ToString();
            //        jobScheduleRow.FrequencySubDayTypes = jobSchedule.FrequencySubDayTypes.ToString();
            //        jobScheduleRow.FrequencyTypes = jobSchedule.FrequencyTypes.ToString();
            //        jobScheduleRow.ID_JobSchedule = jobSchedule.ID;
            //        jobScheduleRow.IsEnabled = jobSchedule.IsEnabled;
            //        jobScheduleRow.JobCount = jobSchedule.JobCount;

            //        jobScheduleRow.SnapShotDate = DateTime.Now;
            //dataRow.SnapShotError = "";


            //        Common.ApplicationDataSet.JSJobSchedules.AddJSJobSchedulesRow(jobScheduleRow);
            //        Common.ApplicationDataSet.JSJobSchedulesTA.Update(Common.ApplicationDataSet.JSJobSchedules);
            //    }
            //    catch (Exception ex)
            //    {
            //        jobScheduleRow.SnapShotDate = DateTime.Now;
            //        jobScheduleRow.SnapShotError = ex.ToString().Substring(0, 256);
            //        Common.ApplicationDataSet.JSJobSchedulesTA.Update(Common.ApplicationDataSet.JSJobSchedules);
            //        VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 47);
            //    }
            //}

#if TRACE
            VNC.AppLog.Trace4("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
#endif
            return dataRow;
        }

        private static void Update(MSMOA.JobSchedule jobSchedule, SQLInformation.Data.ApplicationDataSet.JSJobSchedulesRow dataRow)
        {
            //try
            //{
            //    if (dataRow.IsMonitored)
            //    {
            //        jobSchedule.UpdateDataSet(dataRow);

            //        UpdateDatabaseWithSnapShot(dataRow, "");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));

            //    VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
            //}
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

        private static void UpdateDataSet(this MSMOA.JobSchedule jobSchedule, Data.ApplicationDataSet.JSJobSchedulesRow dataRow)
        {
            try
            {
                //dataRow.X = jobSchedule.X;
            }
            catch (Exception ex)
            {
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
            }
        }
    }
}
