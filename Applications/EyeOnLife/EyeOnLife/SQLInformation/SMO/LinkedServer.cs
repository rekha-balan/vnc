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
    public static class LinkedServer
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_LINKEDSERVER;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Server instance, Guid instanceID, string instanceName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.LinkedServer linkedServer in instance.LinkedServers)
            {
                GetInfoFromSMO(instanceID, linkedServer, instanceName);
            }
#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.LinkedServersRow GetInfoFromSMO(Guid instanceID, MSMO.LinkedServer linkedServer, string instanceName)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.LinkedServersRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.LinkedServers
                          where tb.Instance_ID == instanceID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_LinkedServer == linkedServer.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(linkedServer, dataRow);
                }
                else
                {
                    dataRow = Add(instanceID, linkedServer, instanceName);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
            }
#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.LinkedServersRow Add(Guid instanceID, MSMO.LinkedServer linkedServer, string instanceName)
        {
            SQLInformation.Data.ApplicationDataSet.LinkedServersRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.LinkedServers.NewLinkedServersRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.Name_LinkedServer = linkedServer.Name;
                dataRow.Instance_ID = instanceID;
                dataRow.Name_Instance = instanceName;
                dataRow.Catalog = linkedServer.Catalog;
                dataRow.DataAccess = linkedServer.DataAccess;
                dataRow.DataSource = linkedServer.DataSource;
                dataRow.DateLastModified = linkedServer.DateLastModified;
                dataRow.ID_LinkedServer = linkedServer.ID;
                dataRow.IsPromotionofDistributedTransactionsForRPCEnabled = linkedServer.IsPromotionofDistributedTransactionsForRPCEnabled;
                dataRow.Location = linkedServer.Location;
                dataRow.ProductName = linkedServer.ProductName;
                dataRow.ProviderName = linkedServer.ProviderName;
                dataRow.ProviderString = linkedServer.ProviderString;
                dataRow.Publisher = linkedServer.Publisher;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.LinkedServers.AddLinkedServersRow(dataRow);
                Common.ApplicationDataSet.LinkedServersTA.Update(Common.ApplicationDataSet.LinkedServers);
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

        private static void Update(MSMO.LinkedServer linkedServer, SQLInformation.Data.ApplicationDataSet.LinkedServersRow dataRow)
        {
            try
            {
                linkedServer.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.LinkedServersRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.LinkedServersTA.Update(Common.ApplicationDataSet.LinkedServers);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("InstancesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.LinkedServer linkedServer, Data.ApplicationDataSet.LinkedServersRow dataRow)
        {
            try
            {
                //dataRow.DateLastModified = linkedServer.DateLastModified;
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
