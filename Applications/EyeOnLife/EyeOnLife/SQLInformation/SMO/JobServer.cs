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
    public static class  JobServer
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_JOBSERVER;
        private const string LOG_APPNAME = "SQLInformation";


        public static void LoadFromSMO(MSMOA.JobServer jobServer, Guid instanceID, string instanceName, ExpandMask.JobServerExpandSetting jobServerExpandSetting)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace2("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow = JobServer.GetInfoFromSMO(instanceID, instanceName, jobServer);

            try
            {
                if (jobServerRow.IsMonitored && jobServerExpandSetting.IsMonitored)
                {
                    if (jobServerRow.ExpandAlertCategories && jobServerExpandSetting.ExpandAlertCategories)
                    {
                        AlertCategory.LoadFromSMO(jobServer, jobServerRow);
                    }

                    if (jobServerRow.ExpandAlerts && jobServerExpandSetting.ExpandAlerts)
                    {
                        Alert.LoadFromSMO(jobServer, jobServerRow);
                    }

                    if (jobServerRow.ExpandJobCategories && jobServerExpandSetting.ExpandJobCategories)
                    {
                        JobCategory.LoadFromSMO(jobServer, jobServerRow);
                    }

                    if (jobServerRow.ExpandJobs && jobServerExpandSetting.ExpandJobs)
                    {
                        Job.LoadFromSMO(jobServer, jobServerRow);
                    }

                    //if (jobServerRow.ExpandJobSteps && jobServerExpandSetting.ExpandJobSteps)
                    //{
                    //    Load_JSJobSteps_FromSMO(jobServer, jobServerRow);
                    //}

                    if (jobServerRow.ExpandOperatorCategories && jobServerExpandSetting.ExpandOperatorCategories)
                    {
                        OperatorCategory.LoadFromSMO(jobServer, jobServerRow);
                    }

                    if (jobServerRow.ExpandOperators && jobServerExpandSetting.ExpandOperators)
                    {
                        Operator.LoadFromSMO(jobServer, jobServerRow);
                    }

                    if (jobServerRow.ExpandProxyAccounts && jobServerExpandSetting.ExpandProxyAccounts)
                    {
                        ProxyAccount.LoadFromSMO(jobServer, jobServerRow);
                    }

                    if (jobServerRow.ExpandSharedSchedules && jobServerExpandSetting.ExpandSharedSchedules)
                    {
                        JobSchedule.LoadFromSMO(jobServer, jobServerRow);
                    }

                    if (jobServerRow.ExpandTargetServerGroups && jobServerExpandSetting.ExpandTargetServerGroups)
                    {
                        TargetServerGroup.LoadFromSMO(jobServer, jobServerRow);
                    }

                    if (jobServerRow.ExpandTargetServers && jobServerExpandSetting.ExpandTargetServers)
                    {
                        TargetServer.LoadFromSMO(jobServer, jobServerRow);
                    }

                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
            }

#if TRACE
            VNC.AppLog.Trace2("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }

        public static SQLInformation.Data.ApplicationDataSet.JobServersRow GetInfoFromSMO(Guid instanceID, string instanceName, MSMOA.JobServer jobServer)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter ({0})", jobServer.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
#endif

            SQLInformation.Data.ApplicationDataSet.JobServersRow dataRow = null;

            try
            {
                var dbs = from db in Common.ApplicationDataSet.JobServers
                          where db.Instance_ID == instanceID
                          select db;

                var dbs2 = from db2 in dbs
                           where db2.Name_JobServer == jobServer.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(jobServer, dataRow);
                }
                else
                {
                    dataRow = Add(instanceID, instanceName, jobServer);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
            }

#if TRACE
            VNC.AppLog.Trace4("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.JobServersRow Add(Guid instanceID, string instanceName, MSMOA.JobServer jobServer)
        {
            SQLInformation.Data.ApplicationDataSet.JobServersRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.JobServers.NewJobServersRow();
                dataRow.ID = Guid.NewGuid();

                dataRow.Name_JobServer = jobServer.Name;
                dataRow.Instance_ID = instanceID;
                dataRow.Name_Instance = instanceName;

                dataRow.ErrorLogFile = jobServer.ErrorLogFile;
                dataRow.HostLoginName = jobServer.HostLoginName;

                // TODO(crhodes): Change datatype in table to int.
                dataRow.MaximumHistoryRows = jobServer.MaximumHistoryRows.ToString(); ;
                dataRow.MaximumJobHistoryRows = jobServer.MaximumJobHistoryRows.ToString();

                try
                {
                    dataRow.MsxAccountCredentialName = jobServer.MsxAccountCredentialName;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
#endif                    
                }

                dataRow.MsxAccountName = jobServer.MsxAccountName;
                dataRow.MsxServerName = jobServer.MsxServerName;

                // TODO(crhodes): Think through how to set this.

                ExpandMask.JobServerExpandSetting expandSetting = new ExpandMask.JobServerExpandSetting(instanceID);

                dataRow.IsMonitored = expandSetting.IsMonitored;

                dataRow.ExpandAlertCategories = expandSetting.ExpandAlertCategories;
                dataRow.ExpandAlerts = expandSetting.ExpandAlerts;
                dataRow.ExpandJobCategories = expandSetting.ExpandJobCategories;
                dataRow.ExpandJobs = expandSetting.ExpandJobs;
                dataRow.ExpandOperatorCategories = expandSetting.ExpandOperatorCategories;
                dataRow.ExpandOperators = expandSetting.ExpandOperators;
                dataRow.ExpandProxyAccounts = expandSetting.ExpandProxyAccounts;
                dataRow.ExpandSharedSchedules = expandSetting.ExpandSharedSchedules;
                dataRow.ExpandTargetServerGroups = expandSetting.ExpandTargetServerGroups;
                dataRow.ExpandTargetServers = expandSetting.ExpandTargetServers;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JobServers.AddJobServersRow(dataRow);
                Common.ApplicationDataSet.JobServersTA.Update(Common.ApplicationDataSet.JobServers);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }

            return dataRow;
        }

        private static void Update(MSMOA.JobServer jobServer, SQLInformation.Data.ApplicationDataSet.JobServersRow dataRow)
        {
            try
            {
                if (dataRow.IsMonitored)
                {
                    jobServer.UpdateDataSet(dataRow);

                    UpdateDatabaseWithSnapShot(dataRow, "");
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.JobServersRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.JobServersTA.Update(Common.ApplicationDataSet.JobServers);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("InstancesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        private static void UpdateDataSet(this MSMOA.JobServer jobServer, Data.ApplicationDataSet.JobServersRow dataRow)
        {
            try
            {
                //dataRow.X = jobServer.X;
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }
    }
}
