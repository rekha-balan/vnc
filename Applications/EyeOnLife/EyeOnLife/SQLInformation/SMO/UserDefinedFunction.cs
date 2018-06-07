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
    public static class UserDefinedFunction
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_USERDEFINEDFUNCTION;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            MarkExistingItemsAsNotFound(databaseID);    // This enables cleanup of items that once existed but were deleted.

            foreach (MSMO.UserDefinedFunction userDefinedFunction in database.UserDefinedFunctions)
            {
                if (userDefinedFunction.IsSystemObject)
                {
                    continue;   // Skip System UDF
                }

                SQLInformation.Data.ApplicationDataSet.DBUserDefinedFunctionsRow udfRow = GetInfoFromSMO(userDefinedFunction, databaseID);
                udfRow.NotFound = false;

            }

            Common.ApplicationDataSet.DBUserDefinedFunctionsTA.Update(Common.ApplicationDataSet.DBUserDefinedFunctions);
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static void MarkExistingItemsAsNotFound(Guid databaseID)
        {
            var previouslyFound = Common.ApplicationDataSet.DBUserDefinedFunctions.Where(i => i.Database_ID == databaseID);

            foreach (var pf in previouslyFound)
            {
                pf.NotFound = true;
            }
        }

        private static SQLInformation.Data.ApplicationDataSet.DBUserDefinedFunctionsRow GetInfoFromSMO(MSMO.UserDefinedFunction userDefinedFunction, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.DBUserDefinedFunctionsRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.DBUserDefinedFunctions
                          where tb.Database_ID == databaseID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_UserDefinedFunction == userDefinedFunction.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(userDefinedFunction, dataRow);
                }
                else
                {
                    dataRow = Add(databaseID, userDefinedFunction);
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

        private static SQLInformation.Data.ApplicationDataSet.DBUserDefinedFunctionsRow Add(Guid databaseID, MSMO.UserDefinedFunction userDefinedFunction)
        {
            SQLInformation.Data.ApplicationDataSet.DBUserDefinedFunctionsRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBUserDefinedFunctions.NewDBUserDefinedFunctionsRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.Database_ID = databaseID;
                dataRow.IsSystemObject = userDefinedFunction.IsSystemObject;
                dataRow.Name_UserDefinedFunction = userDefinedFunction.Name;
                dataRow.CreateDate = userDefinedFunction.CreateDate;

                try
                {
                    dataRow.DateLastModified = userDefinedFunction.DateLastModified;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
#endif
                }

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBUserDefinedFunctions.AddDBUserDefinedFunctionsRow(dataRow);
                Common.ApplicationDataSet.DBUserDefinedFunctionsTA.Update(Common.ApplicationDataSet.DBUserDefinedFunctions);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }

            return dataRow;
        }

        private static void Update(MSMO.UserDefinedFunction userDefinedFunction, SQLInformation.Data.ApplicationDataSet.DBUserDefinedFunctionsRow dataRow)
        {
            try
            {
                userDefinedFunction.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBUserDefinedFunctionsRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DBUserDefinedFunctionsTA.Update(Common.ApplicationDataSet.DBUserDefinedFunctions);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DBUserDefinedFunctionsRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.UserDefinedFunction userDefinedFunction, Data.ApplicationDataSet.DBUserDefinedFunctionsRow dataRow)
        {

            try
            {
                //dataRow.X = userDefinedFunction.X;
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

    }
}
