using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PacificLife.Life;
using SQLInformation;
using SQLInformation.Data;

namespace SQLInformationAgent
{
    public partial class SQLInformationDataLoadService : ServiceBase
    {

        public static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE + 0;
        private const string PLLOG_APPNAME = "SQLINFOAGENT";

        //This timer kicks off a thread to do all the work
        private static Timer _RequestTimer_Weekly;
        private static Timer _RequestTimer_Daily;
        private static Timer _RequestTimer_IntraDay;

        public SQLInformationDataLoadService()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            long startTicks = PLLog.Info("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);

            try
            {
                // Load the database.

                SQLInformation.Common.ApplicationDataSet.LoadApplicationDataSetFromDB(SQLInformation.Common.ApplicationDataSet);

                //Initialize the timer that handles period wake-ups

                _RequestTimer_Weekly = new Timer(Helper.TakeSnapShot_Weekly, null, Timeout.Infinite, Timeout.Infinite);

                _RequestTimer_Daily = new Timer(Helper.TakeSnapShot_Daily, null, Timeout.Infinite, Timeout.Infinite);

                _RequestTimer_IntraDay = new Timer(Helper.TakeSnapShot_IntraDay, null, Timeout.Infinite, Timeout.Infinite);


                // Set the time to begin execution and how often it repeats
                // Timer.Change() takes two arguments 1. When to start, 2. Period to continue.

                PLLog.Info(String.Format("Setting Weekly Request Timer StartTime:{0}: in:{1} Restart in:{2}",
                    Config.Weekly_StartTime,
                    Helper.GetTimeout("Weekly StartTime"), Helper.GetTimeout("Weekly RestartTime")),
                    PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);

                _RequestTimer_Weekly.Change(Helper.GetTimeout("Weekly StartTime"), Helper.GetTimeout("Weekly RestartTime"));

                PLLog.Info(String.Format("Setting Daily Request Timer StartTime:{0}: in:{1} Restart in:{2}",
                    Config.Daily_StartTime,
                    Helper.GetTimeout("Daily StartTime"), Helper.GetTimeout("Daily RestartTime")),
                    PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);

                _RequestTimer_Daily.Change(Helper.GetTimeout("Daily StartTime"), Helper.GetTimeout("Daily RestartTime"));

                PLLog.Info(String.Format("Setting IntraDay Request Timer StartTime:{0}: in:{1} Restart in:{2}",
                    Config.IntraDay_StartTime,
                    Helper.GetTimeout("IntraDay StartTime"), Helper.GetTimeout("IntraDay RestartTime")),
                    PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);

                _RequestTimer_IntraDay.Change(Helper.GetTimeout("IntraDay StartTime"), Helper.GetTimeout("IntraDay RestartTime"));
            }
            catch(Exception ex)
            {
                PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
            }

            PLLog.Info("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6, startTicks);
        }

        protected override void OnContinue()
        {
            long startTicks = PLLog.Info("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);

            //OnStart(null);

            PLLog.Info("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8, startTicks);
        }

        protected override void OnStop()
        {
            long startTicks = PLLog.Info("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);

            _RequestTimer_Daily.Dispose();
            _RequestTimer_IntraDay.Dispose();

            PLLog.Info("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
        }

        protected override void OnPause()
        {
            long startTicks = PLLog.Info("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);

            //OnStop();

            PLLog.Info("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12, startTicks);
        }

    }
}
