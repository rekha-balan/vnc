using System;
using System.Threading;
using ADSTA = VNCWPFUserControls.Data.ApplicationDataSetTableAdapters;

namespace VNCWPFUserControls.Data
{

    partial class ApplicationDataSet
    {
        private static int BASE_ERRORNUMBER = ErrorNumbers.APPERROR;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        // This is used to single thread access to the dataset
        static readonly object _concurrencyLock = new object();

        public ADSTA.ApplicationUsageTA ApplicationUsageTA { get; set; }

        Data.ApplicationDataSetTableAdapters.TableAdapterManager _taManager = null;

        public Data.ApplicationDataSetTableAdapters.TableAdapterManager TaManager
        {
            get
            {
                if (_taManager == null)
                {
                    try
                    {
                        // Create the TableAdapterManager.  
                        // This enables the cascading updates/deletes of the dataset to work.
                        _taManager = new ADSTA.TableAdapterManager();

                        #region Create the TableAdapters for all the tables

                        ApplicationUsageTA = new ADSTA.ApplicationUsageTA();

                        #endregion

                        #region Hook the table adapters to the table manager

                        _taManager.ApplicationUsageTA = ApplicationUsageTA;

                        #endregion

                        #region And update all the connection strings.

                        _taManager.Connection.ConnectionString = Config.ConnectionString;

                        ApplicationUsageTA.Connection.ConnectionString = Config.ConnectionString;

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        //VNC.AppLog.Error(string.Format("ConnectionString:>{0}<", Config.SQLMonitorDBConnection), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                        //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                    }
                }

                return _taManager;
            }
            set
            {
                _taManager = value;
            }
        }

        public void LoadApplicationDataSetFromDB(Data.ApplicationDataSet applicationDS)
        {
#if TRACE
            long startTicksTotal = VNC.AppLog.Trace("Start", LOG_APPNAME, BASE_ERRORNUMBER + 3);
#endif
            try
            {
                long startTicks = 0;

                VNC.AppLog.Info("Clearing ApplicationDataSet...", LOG_APPNAME);
                applicationDS.Clear();
                Common.DataFullyLoaded = false;

                LoadMainTables(applicationDS);

                // Load the rest of the tables

                Thread t = new Thread(() => LoadTablesInBackGround(applicationDS));
                t.Start();

            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(string.Format("ConnectionString:>{0}<", Config.ConnectionString), LOG_APPNAME, BASE_ERRORNUMBER + 60);
                VNC.AppLog.Error(ex, LOG_APPNAME, BASE_ERRORNUMBER + 61);
            }

#if TRACE
            VNC.AppLog.Trace("End", LOG_APPNAME, BASE_ERRORNUMBER + 62, startTicksTotal);
#endif
        }
        /// <summary>
        /// Load the tables that are most often used.
        /// </summary>
        /// <param name="applicationDS"></param>
        private void LoadMainTables(ApplicationDataSet applicationDS)
        {
            lock (_concurrencyLock)
            {
                TaManager.ApplicationUsageTA.Fill(applicationDS.ApplicationUsage);
            }
        }

        private void LoadTablesInBackGround(Data.ApplicationDataSet applicationDS)
        {
            //// Might be able to do this in parallel after we figure out locking.
            //// For now just do serially.

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

        partial class ApplicationUsageDataTable
        {
        }
    }
}
