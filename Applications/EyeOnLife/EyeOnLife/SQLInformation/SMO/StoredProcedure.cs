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
    public static class StoredProcedure
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_STOREDPROCEDURE;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            MarkExistingItemsAsNotFound(databaseID);    // This enables cleanup of items that once existed but were deleted.

            foreach (MSMO.StoredProcedure storedProcedure in database.StoredProcedures)
            {
                if (storedProcedure.IsSystemObject)
                {
                    continue;   // Skip System StoredProcedures
                }

                SQLInformation.Data.ApplicationDataSet.DBStoredProceduresRow spRow = GetInfoFromSMO(storedProcedure, databaseID);
                spRow.NotFound = false;
            }

            Common.ApplicationDataSet.DBStoredProceduresTA.Update(Common.ApplicationDataSet.DBStoredProcedures);
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static void MarkExistingItemsAsNotFound(Guid databaseID)
        {
            var previouslyFound = Common.ApplicationDataSet.DBStoredProcedures.Where(i => i.Database_ID == databaseID);

            foreach (var pf in previouslyFound)
            {
                pf.NotFound = true;
            }
        }

        private static SQLInformation.Data.ApplicationDataSet.DBStoredProceduresRow GetInfoFromSMO(MSMO.StoredProcedure storedProcedure, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.DBStoredProceduresRow dataRow = null;

            try
            {
                var dbs = from sp in Common.ApplicationDataSet.DBStoredProcedures
                          where sp.Database_ID == databaseID
                          select sp;

                var dbs2 = from db2 in dbs
                           where db2.Name_StoredProcedure == storedProcedure.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(storedProcedure, dataRow);
                }
                else
                {
                    dataRow = Add(databaseID, storedProcedure);
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

        private static SQLInformation.Data.ApplicationDataSet.DBStoredProceduresRow Add(Guid databaseID, MSMO.StoredProcedure storedProcedure)
        {
            SQLInformation.Data.ApplicationDataSet.DBStoredProceduresRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBStoredProcedures.NewDBStoredProceduresRow();

                dataRow.ID = Guid.NewGuid();   // See if this is available from SP.
                dataRow.Name_StoredProcedure = storedProcedure.Name;
                dataRow.StoredProcedure_ID = storedProcedure.ID.ToString(); ;
                dataRow.Database_ID = databaseID;  // From above
                dataRow.Owner = storedProcedure.Owner;
                dataRow.CreateDate = storedProcedure.CreateDate;
                dataRow.IsSystemObject = storedProcedure.IsSystemObject;

                // Loading the body of the Stored Procedure is expensive.  Optional.

                if (Data.Config.ADSLoad_DBStoredProcedureContent)
                {
                    try
                    {
                        dataRow.MethodName = storedProcedure.MethodName;
                    }
                    catch (Exception ex)
                    {
#if TRACE
                        VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
#endif
                        dataRow.MethodName = "<Not Available>";
                    }

                    try
                    {
                        dataRow.TextHeader = storedProcedure.TextHeader;
                    }
                    catch (Exception ex)
                    {
#if TRACE
                        VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
#endif
                        dataRow.TextHeader = "<No Access>";
                    }

                    try
                    {
                        dataRow.TextBody = storedProcedure.TextBody;
                    }
                    catch (Exception ex)
                    {
#if TRACE
                        VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
#endif
                        dataRow.TextBody = "<No Access>";
                    }
                }

                try
                {
                    dataRow.DateLastModified = storedProcedure.DateLastModified;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
#endif
                }

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBStoredProcedures.AddDBStoredProceduresRow(dataRow);
                Common.ApplicationDataSet.DBStoredProceduresTA.Update(Common.ApplicationDataSet.DBStoredProcedures);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }

            return dataRow;
        }

        private static void Update(MSMO.StoredProcedure storedProcedure, SQLInformation.Data.ApplicationDataSet.DBStoredProceduresRow dataRow)
        {
            try
            {
                storedProcedure.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBStoredProceduresRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DBStoredProceduresTA.Update(Common.ApplicationDataSet.DBStoredProcedures);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DBStoredProceduresRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.StoredProcedure storedProcedure, Data.ApplicationDataSet.DBStoredProceduresRow dataRow)
        {
            try
            {
                dataRow.Owner = storedProcedure.Owner;
                dataRow.CreateDate = storedProcedure.CreateDate;

                if (Data.Config.ADSLoad_DBStoredProcedureContent)
                {
                    try
                    {
                        dataRow.MethodName = storedProcedure.MethodName;
                    }
                    catch (Exception ex)
                    {
#if TRACE
                        VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);
#endif
                        dataRow.MethodName = "<Exception>";
                    }
                    try
                    {
                        dataRow.TextHeader = storedProcedure.TextHeader;
                    }
                    catch (Exception ex)
                    {
#if TRACE
                        VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12);
#endif
                        dataRow.TextHeader = "<Exception>";
                    }

                    try
                    {
                        dataRow.TextBody = storedProcedure.TextBody;
                    }
                    catch (Exception ex)
                    {
#if TRACE
                        VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 13);
#endif
                        dataRow.TextBody = "<Exception>";
                    }
                }

                try
                {
                    dataRow.DateLastModified = storedProcedure.DateLastModified;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 14);
#endif
                }

            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 15);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

    }
}
