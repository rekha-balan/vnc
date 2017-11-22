﻿using System;
using System.Threading;

namespace SupportTools_Visio.Data
{


    public partial class ApplicationDS
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SupportTools_Visio;

        public void LoadApplicationDataSetFromDB(Data.ApplicationDS applicationDS)
        {
#if TRACE
            long startTicksTotal = VNC.AppLog.Trace("Start", Common.LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif
            try
            {
                long startTicks = 0;

                VNC.AppLog.Info("Clearing ApplicationDataSet...", Common.LOG_APPNAME);
                applicationDS.Clear();
                Common.DataFullyLoaded = false;

                //LoadMainTables(applicationDS);

                //LoadLKUPandSupportTables(applicationDS);

                // Load the rest of the tables

                Thread t = new Thread(() => LoadTablesInBackGround(applicationDS));
                t.Start();

            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(string.Format("ConnectionString:>{0}<", Config.SmartsDBConnection), Common.LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 60);
                VNC.AppLog.Error(ex, Common.LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 61);
            }

#if TRACE
            VNC.AppLog.Trace("End", Common.LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 62, startTicksTotal);
#endif
        }

        private void LoadTablesInBackGround(Data.ApplicationDS applicationDS)
        {
            // Might be able to do this in parallel after we figure out locking.
            // For now just do serially.

            //LoadInstanceContentTables(applicationDS);
            //LoadSnapShotTables(applicationDS);
            //LoadDBContentTables(applicationDS);
            //LoadJobServerTables(applicationDS);

            //Thread t1 = new Thread(() => LoadInstanceContentTables(applicationDS));
            //t1.Start();

            //Thread t2 = new Thread(() => LoadSnapShotTables(applicationDS));
            //t2.Start();

            //Thread t3 = new Thread(() => LoadDBContentTables(applicationDS));
            //t3.Start();

            //Thread t4 = new Thread(() => LoadJobServerTables(applicationDS));
            //t4.Start();

            Common.DataFullyLoaded = true;
        }
    }
}
