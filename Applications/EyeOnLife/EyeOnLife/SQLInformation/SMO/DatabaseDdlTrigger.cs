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
    public static class DatabaseDdlTrigger
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_DATABASEDDLTRIGGER;
        private const string LOG_APPNAME = "SQLInformation";

        #region Public Methods

        public static void LoadFromSMO(MSMO.Database database, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            try
            {
                MarkExistingItemsAsNotFound(databaseID);    // This enables cleanup of items that once existed but were deleted.

                foreach (MSMO.DatabaseDdlTrigger ddlTrigger in database.Triggers)
                {
                    try
                    {
                        if (ddlTrigger.IsSystemObject)
                        {
                            continue;   // Skip System Tables
                        }
                    }
                    catch (Exception ex)
                    {
#if TRACE
                        VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
#endif
                    }

                    SQLInformation.Data.ApplicationDataSet.DBDdlTriggersRow ddlRow = GetInfoFromSMO(ddlTrigger, databaseID);
                    ddlRow.NotFound = false;

                }

                Common.ApplicationDataSet.DBDdlTriggersTA.Update(Common.ApplicationDataSet.DBDdlTriggers);
            }
            catch (Exception ex)
            {
#if TRACE
                VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
                // Triggers not available in SQL 2000
            }
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        #endregion

        #region Private Methods

        private static void MarkExistingItemsAsNotFound(Guid databaseID)
        {
            var previouslyFound = Common.ApplicationDataSet.DBDdlTriggers.Where(i => i.Database_ID == databaseID);

            foreach (var pf in previouslyFound)
            {
                pf.NotFound = true;
            }
        }

        private static SQLInformation.Data.ApplicationDataSet.DBDdlTriggersRow GetInfoFromSMO(MSMO.DatabaseDdlTrigger ddlTrigger, Guid databaseID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
#endif
            SQLInformation.Data.ApplicationDataSet.DBDdlTriggersRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.DBDdlTriggers
                          where tb.Database_ID == databaseID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_Trigger == ddlTrigger.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(ddlTrigger, dataRow);
                }
                else
                {
                    dataRow = Add(databaseID, ddlTrigger);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
            }
#if TRACE
            VNC.AppLog.Trace4("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.DBDdlTriggersRow Add(Guid databaseID, MSMO.DatabaseDdlTrigger ddlTrigger)
        {
            SQLInformation.Data.ApplicationDataSet.DBDdlTriggersRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBDdlTriggers.NewDBDdlTriggersRow();

                dataRow.ID = Guid.NewGuid();
                dataRow.Name_Trigger = ddlTrigger.Name;
                //newTrigger.Table_ID = trigger.ID.ToString();
                dataRow.Database_ID = databaseID;  // From above
                dataRow.CreateDate = ddlTrigger.CreateDate;

                try
                {
                    dataRow.DateLastModified = ddlTrigger.DateLastModified;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
#endif
                }

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBDdlTriggers.AddDBDdlTriggersRow(dataRow);
                Common.ApplicationDataSet.DBDdlTriggersTA.Update(Common.ApplicationDataSet.DBDdlTriggers);

            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }

            return dataRow;
        }

        private static void Update(MSMO.DatabaseDdlTrigger ddlTrigger, SQLInformation.Data.ApplicationDataSet.DBDdlTriggersRow dataRow)
        {
            try
            {
                ddlTrigger.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);   

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBDdlTriggersRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DBDdlTriggersTA.Update(Common.ApplicationDataSet.DBDdlTriggers);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DBDdlTriggersRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
            }
        }

        private static void UpdateDataSet(this MSMO.DatabaseDdlTrigger databaseDdlTrigger, Data.ApplicationDataSet.DBDdlTriggersRow dataRow)
        {
            try
            {
                //dataRow.X = databaseDdlTrigger.X;
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.
            }
        }

        #endregion

    }
}
