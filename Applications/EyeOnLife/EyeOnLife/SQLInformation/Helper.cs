using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using SQLInformation;
using VNC;


namespace SQLInformation
{
    public class Helper
    {
        public static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE + 0;
        private const string LOG_APPNAME = "SQLINFOAGENT";

        #region Main Function Routines

        /// <summary>
        /// Does X
        /// </summary> 
        public static TimeSpan GetTimeout(string sSetting)
        {
            // TODO(crhodes):  Look at getting this Configuration stuff out of the code here.
            // Would need to pass in or create select case ...

            //string s = ConfigurationManager.AppSettings[sSetting];
            string s = sSetting;

            if(s.IndexOf(":") > -1)
            {
                try
                {
                    TimeSpan span = DateTime.ParseExact(s, "hh:mm tt", null).TimeOfDay.Subtract(DateTime.Now.TimeOfDay);
                    if(span < new TimeSpan(0L))
                    {
                        return span.Add(new TimeSpan(0, 24, 0, 0, 0));
                    }
                    return span;
                }
                catch
                {
                    throw;
                }
            }
            string[] strArray = s.Split(new char[] { ' ' });
            if(strArray.Length != 2)
            {
                throw new FormatException("Unsupported format in configuration timeout setting: " + sSetting);
            }
            switch(strArray[1].ToLower())
            {
                case "day":
                case "days":
                    return new TimeSpan(Convert.ToInt32(strArray[0]), 0, 0, 0, 0);

                case "hour":
                case "hours":
                    return new TimeSpan(0, Convert.ToInt32(strArray[0]), 0, 0, 0);

                case "minute":
                case "minutes":
                    return new TimeSpan(0, 0, Convert.ToInt32(strArray[0]), 0, 0);

                case "second":
                case "seconds":
                    return new TimeSpan(0, 0, 0, Convert.ToInt32(strArray[0]), 0);

                case "millisecond":
                case "milliseconds":
                    return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(strArray[0]));
            }

            throw new FormatException("Invalid unit specified for time in configuration timeout setting: " + sSetting);
        }
        
        public static void IndicateApplicationUsage(string application, DateTime eventDate, string user, string message)
        {
            var dataRow = Common.ApplicationDataSet.ApplicationUsage.NewApplicationUsageRow();

            dataRow.Application = application;
            dataRow.EventDate = eventDate;
            dataRow.User = user;
            dataRow.EventMessage = message;

            Common.ApplicationDataSet.ApplicationUsage.AddApplicationUsageRow(dataRow);
            // HACK(crhodes)
            // Skip writng to database for now
            //Common.ApplicationDataSet.ApplicationUsageTA.Update(Common.ApplicationDataSet.ApplicationUsage);
        }

        public static void TakeSnapShot(
            ExpandMask.InstanceExpandSetting instanceExpandSetting, 
            ExpandMask.JobServerExpandSetting jobServerExpandSetting, 
            ExpandMask.DatabaseExpandSetting databaseExpandSetting)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
#endif
            System.Diagnostics.Debug.WriteLine("TakeSnapShot");

            VNC.AppLog.Debug(
                string.Format("Start instanceExpandSetting:{0} jobServerExpandSetting:{2} databaseExpandSetting:{2}", 
                    instanceExpandSetting.ExpandMask, jobServerExpandSetting, databaseExpandSetting.ExpandMask),
                LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

            // Need to load the data here as it may change from snapshot to snapshot.

            //var dataRow = Common.ApplicationDataSet.ApplicationUsage.NewApplicationUsageRow();
            var currentTime = DateTime.Now;
            IPrincipal currentUser = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            var eventMessage = string.Format("Start TakeSnapShot(IES:{0}  JES:{1}  DES:{2})",
                instanceExpandSetting.ExpandMask, jobServerExpandSetting.ExpandMask, databaseExpandSetting.ExpandMask);

            IndicateApplicationUsage(LOG_APPNAME, currentTime, currentUser.Identity.Name, eventMessage);

            SQLInformation.Common.ApplicationDataSet.LoadApplicationDataSetFromDB(SQLInformation.Common.ApplicationDataSet);

            SMO.Instance.LoadFromSMO(Common.ApplicationDataSet.Instances, instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);

            long duration = (Stopwatch.GetTimestamp() - startTicks) / Stopwatch.Frequency;

            eventMessage = string.Format("End TakeSnapShot(IES:{0}  JES:{1}  DES:{2}) - {3}",
                instanceExpandSetting.ExpandMask, jobServerExpandSetting.ExpandMask, databaseExpandSetting.ExpandMask, duration);

            IndicateApplicationUsage(LOG_APPNAME, currentTime, currentUser.Identity.Name, eventMessage);

            //SMO.Helper.Load_Instances_FromSMO(Common.ApplicationDataSet.Instances, instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);

            //// Only do work if a business day, otherwise wait till tomorrow.

            //Predictor predictor = new Predictor(Config.MCRRequestsDBConnection, true, false, true);

            ////Don't run on non-business days

            //if(predictor.IsBusinessDay(Config.CurrentDate))
            //{
            //    VNC.AppLog.Info("Business Day", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);

            //    try
            //    {
            //        Data.DB.UpdateOffices();
            //        GetUnprocessedRequestsFromDB();
            //    }
            //    catch(Exception ex)
            //    {
            //        // TODO(crhodes): Determine what should happen here.  Seems like we should not continue.
            //        VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
            //    }
            //}
            //else
            //{
            //    VNC.AppLog.Info("Not a Business Day", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
            //}
#if TRACE
            VNC.AppLog.Trace("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
#endif
        }
        
        public static void TakeSnapShot_Daily(Object state)
        {
            long startTicks = VNC.AppLog.Info("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(Data.Config.ExpandSetting_Daily_Database);
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(Data.Config.ExpandSetting_Daily_Instance);
            ExpandMask.JobServerExpandSetting jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(Data.Config.ExpandSetting_Daily_JobServer);

            TakeSnapShot(instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);

            VNC.AppLog.Info("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
        }

        public static void TakeSnapShot_IntraDay(Object state)
        {
            long startTicks = VNC.AppLog.Info("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(Data.Config.ExpandSetting_IntraDay_Database);
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(Data.Config.ExpandSetting_IntraDay_Instance);
            ExpandMask.JobServerExpandSetting jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(Data.Config.ExpandSetting_IntraDay_JobServer);

            // TODO(crhodes): Get concurrency or day of week scheduling to work right before allowing this.
            //TakeSnapShot(instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);

            VNC.AppLog.Info("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
        }

        public static void TakeSnapShot_Weekly(Object state)
        {
            long startTicks = VNC.AppLog.Info("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(Data.Config.ExpandSetting_Weekly_Database);
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(Data.Config.ExpandSetting_Weekly_Instance);
            ExpandMask.JobServerExpandSetting jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(Data.Config.ExpandSetting_Weekly_JobServer);

            // TODO(crhodes): Get concurrency or day of week scheduling to work right before allowing this.
            //TakeSnapShot(instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);

            VNC.AppLog.Info("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
        }

        #endregion

    }
}
