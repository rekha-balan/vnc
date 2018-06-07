using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO = Microsoft.SqlServer.Management.Smo;
using MSMOA = Microsoft.SqlServer.Management.Smo.Agent;

using SQLInformation;
using VNC;

namespace SQLInformation.SMO
{
    public static class Instance
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_INSTANCE;
        private const string LOG_APPNAME = "SQLInformation";

        #region Public Methods

        public static void LoadFromSMO(Data.ApplicationDataSet.InstancesDataTable instances,
            ExpandMask.InstanceExpandSetting instanceExpandSetting,
            ExpandMask.JobServerExpandSetting jobServerExpandSetting,
            ExpandMask.DatabaseExpandSetting databaseExpandSetting)
        {
#if TRACE
            long startTicks = VNC.AppLog.Info("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (Data.ApplicationDataSet.InstancesRow instanceRow in instances)
            {
                if (instanceRow.IsMonitored && instanceExpandSetting.IsMonitored)
                {
                    var updatedInstanceRow = GetInfoFromSMO(instanceRow, instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);

                    // Some information  is not provided by SMO.  Add it in.

                    updatedInstanceRow.ADDomain = instanceRow.ADDomain;
                    updatedInstanceRow.Environment = instanceRow.Environment;
                    updatedInstanceRow.SecurityZone = instanceRow.SecurityZone;

                    Helper.TakeInstanceSnapShot(updatedInstanceRow);
                }
            }

#if TRACE
                VNC.AppLog.Info("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif     
        }


        public static SQLInformation.Data.ApplicationDataSet.InstancesRow GetInfoFromSMO(Data.ApplicationDataSet.InstancesRow dataRow,
            ExpandMask.InstanceExpandSetting instanceExpandSetting,
            ExpandMask.JobServerExpandSetting jobServerExpandSetting,
            ExpandMask.DatabaseExpandSetting databaseExpandSetting)
        {
#if TRACE
            long startTicks = VNC.AppLog.Info(
                string.Format("Enter ({0}) IES:{1}  JES:{2}  DES:{3}", 
                    dataRow.Name_Instance,
                    instanceExpandSetting.ExpandMask, jobServerExpandSetting.ExpandMask, databaseExpandSetting.ExpandMask),
                LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            long stopwatchFrequency = Stopwatch.Frequency;

            MSMO.Server serverInstance = null;

            // First try to connect to the instance and retrieve a simple property.  
            // If cannot, don't bother doing more.
            try
            {
                serverInstance = Server.GetFromSMO(dataRow.Name_Instance);

                string s = serverInstance.NetName;

                // If we got to the server make everything upper case.

                dataRow.NetName = s.ToUpper();
                dataRow.Name_Instance = dataRow.Name_Instance.ToUpper();
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException )
            {
                dataRow.SnapShotDuration = (Stopwatch.GetTimestamp() - startTicks) / stopwatchFrequency;

                // Make names lower case to indicate error.

                dataRow.NetName = dataRow.NetName.ToLower();
                dataRow.Name_Instance = dataRow.Name_Instance.ToLower();
                dataRow.SnapShotError = "Connection Failure Exception";

                Common.ApplicationDataSet.Instances_Update();

                //UpdateDatabaseWithSnapShot(dataRow, "Connection Failure Exception");

                VNC.AppLog.Warning(string.Format("Connection Failure Exception: {0}", dataRow.Name_Instance), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
                return dataRow;
            }
            catch (Exception ex)
            {
                //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);

                dataRow.SnapShotDuration = (Stopwatch.GetTimestamp() - startTicks) / stopwatchFrequency;
                dataRow.NetName = dataRow.NetName.ToLower();
                dataRow.Name_Instance = dataRow.Name_Instance.ToLower();

                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 4);

                //UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
                return dataRow;
            }

            try
            {
                Update(serverInstance, dataRow);

                //UpdateDatabaseWithSnapShot(dataRow, "");

                if (dataRow.ExpandJobServer && instanceExpandSetting.ExpandJobServer)
                {
                    JobServer.LoadFromSMO(serverInstance.JobServer, dataRow.ID, dataRow.Name_Instance, jobServerExpandSetting);
                }

                if (dataRow.ExpandLinkedServers && instanceExpandSetting.ExpandLinkedServers)
                {
                    LinkedServer.LoadFromSMO(serverInstance, dataRow.ID, dataRow.Name_Instance);
                }

                if (dataRow.ExpandLogins && instanceExpandSetting.ExpandLogins)
                {
                    Login.LoadFromSMO(serverInstance, dataRow.ID, dataRow.Name_Instance);
                }

                if (dataRow.ExpandServerRoles && instanceExpandSetting.ExpandServerRoles)
                {
                    ServerRole.LoadFromSMO(serverInstance, dataRow.ID, dataRow.Name_Instance);
                }

                if (dataRow.ExpandTriggers && instanceExpandSetting.ExpandTriggers)
                {
                    ServerDdlTrigger.LoadFromSMO(serverInstance, dataRow.ID, dataRow.Name_Instance);
                }

                if ((dataRow.ExpandContent && instanceExpandSetting.ExpandContent)
                    || (dataRow.ExpandStorage && instanceExpandSetting.ExpandStorage))
                {
                    Database.LoadFromSMO(serverInstance, dataRow.ID, databaseExpandSetting);
                }

                // We now have all the information for the instance.  
                // This will get pushed to the snapshot by our caller.

                UpdateTotalStorageUsed(dataRow);

                dataRow.SnapShotDuration = (Stopwatch.GetTimestamp() - startTicks) / stopwatchFrequency;

                Common.ApplicationDataSet.Instances_Update();
                //Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
            }
            catch (Exception ex)
            {
                //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);

                dataRow.SnapShotDuration = (Stopwatch.GetTimestamp() - startTicks) / stopwatchFrequency;
                dataRow.NetName = dataRow.NetName.ToLower();
                dataRow.Name_Instance = dataRow.Name_Instance.ToLower();

                dataRow.SnapShotDate = DateTime.Now;
                //dataRow.SnapShotError = "";

                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 5);
                //Common.ApplicationDataSet.Instances_Update();

                //UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
#if TRACE
            VNC.AppLog.Info(string.Format("Exit ({0})", dataRow.Name_Instance), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6, startTicks);
#endif
            return dataRow;
        }

        // There is no Add Method as the Instances are added manually.  In the future if a reliable method to extract instances
        // from servers is established the crawler could start with servers.

        #endregion

        #region Private Methods


        //private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.InstancesRow dataRow, string snapShotError)
        //{
        //    try
        //    {
        //        dataRow.SnapShotDate = DateTime.Now;
        //        dataRow.SnapShotError = snapShotError;
        //        Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = string.Format("InstancesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
        //        VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
        //    }
        //}

        private static void Update(MSMO.Server server, Data.ApplicationDataSet.InstancesRow dataRow)
        {
            try
            {
                try
                {
                    dataRow.BrowserStartMode = server.BrowserStartMode.ToString();
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
#endif
                    dataRow.BrowserStartMode = "<Exception>";
                }

                dataRow.Collation = server.Collation;
                dataRow.Edition = server.Edition;
                dataRow.EngineEdition = server.EngineEdition.ToString();
                dataRow.IsClustered = server.IsClustered;
                dataRow.NetName = server.NetName.ToUpper();
                dataRow.OSVersion = server.OSVersion;
                dataRow.PerfMonMode = server.PerfMonMode.ToString();
                dataRow.PhysicalMemory = server.PhysicalMemory;
                dataRow.Platform = server.Platform;
                dataRow.Processors = server.Processors;
                dataRow.Product = server.Product;
                dataRow.ProductLevel = server.ProductLevel;

                // Calculate the storage utilization of the Instance


                try
                {
                    dataRow.ServiceAccount = server.ServiceAccount;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
#endif
                    dataRow.ServiceAccount = "<Exception>";
                }

                try
                {
                    dataRow.ServiceInstanceId = server.ServiceInstanceId;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
#endif
                    dataRow.ServiceInstanceId = "<Exception>";
                }

                dataRow.ServiceName = server.ServiceName;
                dataRow.Status = server.Status.ToString();
                dataRow.Version = server.VersionString;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.Instances_Update();
            }
            catch (Exception ex)
            {
                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 10);
                //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

        private static void UpdateTotalStorageUsed(Data.ApplicationDataSet.InstancesRow dataRow)
        {
            try
            {
                var totalDataSpace = Common.ApplicationDataSet.Databases
                                    .Where(db => db.Instance_ID == dataRow.ID)
                                    .Where(db => db.NotFound != true)
                                    .Select(db => db.DataSpaceUsage)
                                    .Sum();

                dataRow.Total_DataSpaceUsage = totalDataSpace;

                var totalIndexSpace = Common.ApplicationDataSet.Databases
                                    .Where(db => db.Instance_ID == dataRow.ID)
                                    .Where(db => db.NotFound != true)
                                    .Select(db => db.IndexSpaceUsage)
                                    .Sum();

                dataRow.Total_IndexSpaceUsage = totalIndexSpace;

                var totalSize = Common.ApplicationDataSet.Databases
                                    .Where(db => db.Instance_ID == dataRow.ID)
                                    .Where(db => db.NotFound != true)
                                    .Select(db => db.Size)
                                    .Sum();

                dataRow.Total_Size = totalSize;

                var totalSpaceAvailable = Common.ApplicationDataSet.Databases
                                    .Where(db => db.Instance_ID == dataRow.ID)
                                    .Where(db => db.NotFound != true)
                                    .Select(db => db.SpaceAvailable)
                                    .Sum();

                dataRow.Total_SpaceAvailable = totalSpaceAvailable;
            }
            catch (Exception ex)
            {
                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 11);
                //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);                
            }
        }

        private static void ReportException(Exception ex, Data.ApplicationDataSet.InstancesRow dataRow, int eventID)
        {
            string errorMessage = "";

            if (dataRow != null)
            {
                errorMessage = string.Format("InstancesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                dataRow.SnapShotError = errorMessage;
                Common.ApplicationDataSet.Instances_Update();
            }
            else
            {
                errorMessage = string.Format("InstancesRow(null) - ex:{0} ex.Inner:{1}", ex, ex.InnerException);
            }

            VNC.AppLog.Error(errorMessage, LOG_APPNAME, eventID);
        }

        #endregion

    }
}
