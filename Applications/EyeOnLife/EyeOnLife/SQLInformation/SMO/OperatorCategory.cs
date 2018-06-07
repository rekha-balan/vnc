﻿using System;
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
    public static class OperatorCategory
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_OPERATORCATEGORY;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMOA.JobServer jobServer, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMOA.OperatorCategory operatorCategory in jobServer.OperatorCategories)
            {
                GetInfoFromSMO(operatorCategory, jobServerRow);
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }


        private static SQLInformation.Data.ApplicationDataSet.JSOperatorCategoriesRow GetInfoFromSMO(MSMOA.OperatorCategory operatorCategory, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter ({0})", operatorCategory.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif

            SQLInformation.Data.ApplicationDataSet.JSOperatorCategoriesRow dataRow = null;

            try
            {
                var dbs = from db in Common.ApplicationDataSet.JSOperatorCategories
                          where db.JobServer_ID == jobServerRow.ID
                          select db;

                var dbs2 = from db2 in dbs
                           where db2.Name_JSOperatorCategory == operatorCategory.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(operatorCategory, dataRow);
                }
                else
                {
                    dataRow = Add(operatorCategory, jobServerRow);
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

        private static SQLInformation.Data.ApplicationDataSet.JSOperatorCategoriesRow Add(MSMOA.OperatorCategory operatorCategory, SQLInformation.Data.ApplicationDataSet.JobServersRow jobServerRow)
        {
            SQLInformation.Data.ApplicationDataSet.JSOperatorCategoriesRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.JSOperatorCategories.NewJSOperatorCategoriesRow();
                dataRow.ID = Guid.NewGuid();

                dataRow.Name_JSOperatorCategory = operatorCategory.Name;
                dataRow.JobServer_ID = jobServerRow.ID;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.JSOperatorCategories.AddJSOperatorCategoriesRow(dataRow);
                Common.ApplicationDataSet.JSOperatorCategoriesTA.Update(Common.ApplicationDataSet.JSOperatorCategories);
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

        private static void Update(MSMOA.OperatorCategory operatorCategory, SQLInformation.Data.ApplicationDataSet.JSOperatorCategoriesRow dataRow)
        {
            try
            {
                operatorCategory.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.JSOperatorCategoriesRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.JSOperatorCategoriesTA.Update(Common.ApplicationDataSet.JSOperatorCategories);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("JSOperatorCategoriesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMOA.OperatorCategory operatorCategory, Data.ApplicationDataSet.JSOperatorCategoriesRow dataRow)
        {
            try
            {
                //dataRow.X = operatorCategory.X;
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
