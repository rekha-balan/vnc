using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PacificLife.Life;

namespace SQLInformation
{
    public class Helper
    {
        public static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE + 0;
        private const string PLLOG_APPNAME = "SQLINFOAGENT";

        /// <summary>
        /// Does X
        /// </summary> 
        public static TimeSpan GetTimeout(string sSetting)
        {
            // TODO(crhodes):  Look at getting this Configuration stuff out of the code here.
            // Would need to pass in or create select case ...

            string s = ConfigurationManager.AppSettings[sSetting];

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

        public static void TakeSnapShot(Object state, ExpandMask.InstanceExpandSetting instanceExpandSetting, ExpandMask.DatabaseExpandSetting databaseExpandSetting)
        {
#if TRACE
            long startTicks = PLLog.Trace("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
#endif
            System.Diagnostics.Debug.WriteLine("TakeSnapShot");

            SMO.Helper.Load_Instances_FromSMO(Common.ApplicationDataSet.Instances, instanceExpandSetting, databaseExpandSetting);

            //// Only do work if a business day, otherwise wait till tomorrow.

            //Predictor predictor = new Predictor(Config.MCRRequestsDBConnection, true, false, true);

            ////Don't run on non-business days

            //if(predictor.IsBusinessDay(Config.CurrentDate))
            //{
            //    PLLog.Info("Business Day", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);

            //    try
            //    {
            //        Data.DB.UpdateOffices();
            //        GetUnprocessedRequestsFromDB();
            //    }
            //    catch(Exception ex)
            //    {
            //        // TODO(crhodes): Determine what should happen here.  Seems like we should not continue.
            //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
            //    }
            //}
            //else
            //{
            //    PLLog.Info("Not a Business Day", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
            //}
#if TRACE
            PLLog.Trace("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
#endif
        }
        
        /// <summary>
        /// Process work each time the timer "Awakens" the SQLInformation DataLoadService
        /// </summary>
        public static void TakeSnapShot_Daily(Object state)
        {
#if TRACE
            long startTicks = PLLog.Trace("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
#endif
            System.Diagnostics.Debug.WriteLine("TakeSnapShot_Daily");

            // TODO(crhodes): Figure out what we want to do here.   For now just grab something out of Config.

            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(Data.Config.ExpandSetting_Daily_Database);
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(Data.Config.ExpandSetting_Daily_Instance);

            PLLog.Debug(
                string.Format("Start instanceExpandSetting:{0} databaseExpandSetting:{1}", instanceExpandSetting.ExpandMask, databaseExpandSetting.ExpandMask),
                PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

            SMO.Helper.Load_Instances_FromSMO(Common.ApplicationDataSet.Instances, instanceExpandSetting, databaseExpandSetting); 

            //// Only do work if a business day, otherwise wait till tomorrow.

            //Predictor predictor = new Predictor(Config.MCRRequestsDBConnection, true, false, true);

            ////Don't run on non-business days

            //if(predictor.IsBusinessDay(Config.CurrentDate))
            //{
            //    PLLog.Info("Business Day", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);

            //    try
            //    {
            //        Data.DB.UpdateOffices();
            //        GetUnprocessedRequestsFromDB();
            //    }
            //    catch(Exception ex)
            //    {
            //        // TODO(crhodes): Determine what should happen here.  Seems like we should not continue.
            //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
            //    }
            //}
            //else
            //{
            //    PLLog.Info("Not a Business Day", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
            //}
#if TRACE
            PLLog.Trace("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
#endif
        }

        /// <summary>
        /// Process work each time the timer "Awakens" the SQLInformation DataLoadService
        /// </summary>
        public static void TakeSnapShot_IntraDay(Object state)
        {
#if TRACE
            long startTicks = PLLog.Trace("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
#endif
            System.Diagnostics.Debug.WriteLine("TakeSnapShot_IntraDay");

            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(Data.Config.ExpandSetting_IntraDay_Database);
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(Data.Config.ExpandSetting_IntraDay_Instance);

            PLLog.Debug(
                string.Format("Start instanceExpandSetting:{0} databaseExpandSetting:{1}", instanceExpandSetting.ExpandMask, databaseExpandSetting.ExpandMask),
                PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

            SMO.Helper.Load_Instances_FromSMO(Common.ApplicationDataSet.Instances, instanceExpandSetting, databaseExpandSetting); 

            //// Only do work if a business day, otherwise wait till tomorrow.

            //Predictor predictor = new Predictor(Config.MCRRequestsDBConnection, true, false, true);

            ////Don't run on non-business days

            //if(predictor.IsBusinessDay(Config.CurrentDate))
            //{
            //    PLLog.Info("Business Day", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);

            //    try
            //    {
            //        Data.DB.UpdateOffices();
            //        GetUnprocessedRequestsFromDB();
            //    }
            //    catch(Exception ex)
            //    {
            //        // TODO(crhodes): Determine what should happen here.  Seems like we should not continue.
            //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
            //    }
            //}
            //else
            //{
            //    PLLog.Info("Not a Business Day", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
            //}

            PLLog.Trace("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
        }

        /// <summary>
        /// Process work each time the timer "Awakens" the SQLInformation DataLoadService
        /// </summary>
        public static void TakeSnapShot_Weekly(Object state)
        {
#if TRACE
            long startTicks = PLLog.Trace("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
#endif
            System.Diagnostics.Debug.WriteLine("TakeSnapShot_Weekly");

            // TODO(crhodes): Figure out what we want to do here.   For now just grab something out of Config.

            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(Data.Config.ExpandSetting_Weekly_Database);
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(Data.Config.ExpandSetting_Weekly_Instance);

            PLLog.Debug(
                string.Format("Start instanceExpandSetting:{0} databaseExpandSetting:{1}", instanceExpandSetting.ExpandMask, databaseExpandSetting.ExpandMask),
                PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

            SMO.Helper.Load_Instances_FromSMO(Common.ApplicationDataSet.Instances, instanceExpandSetting, databaseExpandSetting);

            //// Only do work if a business day, otherwise wait till tomorrow.

            //Predictor predictor = new Predictor(Config.MCRRequestsDBConnection, true, false, true);

            ////Don't run on non-business days

            //if(predictor.IsBusinessDay(Config.CurrentDate))
            //{
            //    PLLog.Info("Business Day", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);

            //    try
            //    {
            //        Data.DB.UpdateOffices();
            //        GetUnprocessedRequestsFromDB();
            //    }
            //    catch(Exception ex)
            //    {
            //        // TODO(crhodes): Determine what should happen here.  Seems like we should not continue.
            //        PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
            //    }
            //}
            //else
            //{
            //    PLLog.Info("Not a Business Day", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
            //}
#if TRACE
            PLLog.Trace("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
#endif
        }

    }
}
