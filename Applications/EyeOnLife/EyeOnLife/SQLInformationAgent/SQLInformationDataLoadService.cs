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

using SQLInformation;
using SQLInformation.Data;
using VNC;

namespace SQLInformationAgent
{
    public partial class SQLInformationDataLoadService : ServiceBase
    {

        public static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE + 0;
        private const string LOG_APPNAME = "SQLINFOAGENT";

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
            long startTicks = VNC.AppLog.Info("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);

            try
            {
                // Load the database.  Only need this for the Crawler Config to start.

                SQLInformation.Common.ApplicationDataSet.LoadApplicationDataSetFromDB(SQLInformation.Common.ApplicationDataSet);

                //Initialize the timer that handles period wake-ups
                // Set the time to begin execution and how often it repeats
                // Timer.Change() takes two arguments 1. When to start, 2. Period to continue.

                if (Config.LoadCycle_Weekly_Run)
                {
                	_RequestTimer_Weekly = new Timer(Helper.TakeSnapShot_Weekly, null, Timeout.Infinite, Timeout.Infinite);

                    VNC.AppLog.Info(String.Format("Setting Weekly Request Timer StartTime:{0}: in:{1} Restart in:{2}",
                        Config.LoadCycle_Weekly_StartTime,
                        Helper.GetTimeout(Config.LoadCycle_Weekly_StartTime), Helper.GetTimeout(Config.LoadCycle_Weekly_ReStartTime)),
                        LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);

                    _RequestTimer_Weekly.Change(Helper.GetTimeout(Config.LoadCycle_Weekly_StartTime), Helper.GetTimeout(Config.LoadCycle_Weekly_ReStartTime));
                }

                if (Config.LoadCycle_Daily_Run)
                {
                    _RequestTimer_Daily = new Timer(Helper.TakeSnapShot_Daily, null, Timeout.Infinite, Timeout.Infinite);

                    VNC.AppLog.Info(String.Format("Setting Daily Request Timer StartTime:{0}: in:{1} Restart in:{2}",
                        Config.LoadCycle_Daily_StartTime,
                        Helper.GetTimeout(Config.LoadCycle_Daily_StartTime), Helper.GetTimeout(Config.LoadCycle_Daily_ReStartTime)),
                        LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);

                    _RequestTimer_Daily.Change(Helper.GetTimeout(Config.LoadCycle_Daily_StartTime), Helper.GetTimeout(Config.LoadCycle_Daily_ReStartTime));
                }

                if (Config.LoadCycle_IntraDay_Run)
                {
                    _RequestTimer_IntraDay = new Timer(Helper.TakeSnapShot_IntraDay, null, Timeout.Infinite, Timeout.Infinite);

                    VNC.AppLog.Info(String.Format("Setting IntraDay Request Timer StartTime:{0}: in:{1} Restart in:{2}",
                        Config.LoadCycle_IntraDay_StartTime,
                        Helper.GetTimeout(Config.LoadCycle_IntraDay_StartTime), Helper.GetTimeout(Config.LoadCycle_IntraDay_ReStartTime)),
                        LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);

                    _RequestTimer_IntraDay.Change(Helper.GetTimeout(Config.LoadCycle_IntraDay_StartTime), Helper.GetTimeout(Config.LoadCycle_IntraDay_ReStartTime));
                }
            }
            catch(Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
            }

            VNC.AppLog.Info("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6, startTicks);
        }

        protected override void OnContinue()
        {
            long startTicks = VNC.AppLog.Info("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);

            //OnStart(null);

            VNC.AppLog.Info("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8, startTicks);
        }

        protected override void OnStop()
        {
            long startTicks = VNC.AppLog.Info("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);

            //_RequestTimer_Daily.Dispose();
            //_RequestTimer_IntraDay.Dispose();

            VNC.AppLog.Info("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10, startTicks);
        }

        protected override void OnPause()
        {
            long startTicks = VNC.AppLog.Info("Start", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);

            //OnStop();

            VNC.AppLog.Info("End", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12, startTicks);
        }

    }
}
