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
    public static class Operator
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_OPERATOR;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMOA.JobServer jobServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMOA.Operator operatorX in jobServer.Operators)
            {
                GetInfoFromSMO(operatorX, jobServerRow);
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.JSOperatorsRow GetInfoFromSMO(MSMOA.Operator operatorX, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter ({0})", operatorX.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif

            SQLInformation.Data.ApplicationDataSet.JSOperatorsRow dataRow = null;

            try
            {
                var dbs = from db in Common.ApplicationDataSet.JSOperators
                          where db.JobServer_ID == jobServerRow.ID
                          select db;

                var dbs2 = from db2 in dbs
                           where db2.Name_JSOperator == operatorX.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(operatorX, dataRow);
                }
                else
                {
                    dataRow = Add(operatorX, jobServerRow);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
            }

#if TRACE
            VNC.AppLog.Trace4("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.JSOperatorsRow Add(MSMOA.Operator operatorX, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
            SQLInformation.Data.ApplicationDataSet.JSOperatorsRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.JSOperators.NewJSOperatorsRow();
                dataRow.ID = Guid.NewGuid();
                dataRow.JobServer_ID = jobServerRow.ID;
                dataRow.Name_JSOperator = operatorX.Name;
                dataRow.CategoryName = operatorX.CategoryName;
                dataRow.EmailAddress = operatorX.EmailAddress;
                dataRow.Enabled = operatorX.Enabled;
                dataRow.ID_Operator = operatorX.ID;
                dataRow.LastEmailDate = operatorX.LastEmailDate;
                dataRow.LastNetSendDate = operatorX.LastNetSendDate;
                dataRow.LastPagerDate = operatorX.LastPagerDate;
                dataRow.NetSendAddress = operatorX.NetSendAddress;
                dataRow.PagerAddress = operatorX.PagerAddress;
                dataRow.PagerDays = operatorX.PagerDays.ToString();
                // TODO(crhodes): Is there a better datatype/
                dataRow.SaturdayPagerEndTime = operatorX.SaturdayPagerEndTime.ToString();
                dataRow.SaturdayPagerStartTime = operatorX.SaturdayPagerStartTime.ToString();
                dataRow.SundayPagerEndTime = operatorX.SaturdayPagerEndTime.ToString();
                dataRow.SundayPagerStartTime = operatorX.SaturdayPagerStartTime.ToString();
                dataRow.WeekdayPagerEndTime = operatorX.WeekdayPagerEndTime.ToString();
                dataRow.WeekdayPagerStartTime = operatorX.WeekdayPagerStartTime.ToString();

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JSOperators.AddJSOperatorsRow(dataRow);
                Common.ApplicationDataSet.JSOperatorsTA.Update(Common.ApplicationDataSet.JSOperators);
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

        private static void Update(MSMOA.Operator operatorX, SQLInformation.Data.ApplicationDataSet.JSOperatorsRow dataRow)
        {
            try
            {
                operatorX.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.JSOperatorsRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.JSOperatorsTA.Update(Common.ApplicationDataSet.JSOperators);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("InstancesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMOA.Operator operatorX, Data.ApplicationDataSet.JSOperatorsRow dataRow)
        {
            // NB. operator is a keyword so use operatorX
            try
            {
                //dataRow.X = operatorX.X;
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
