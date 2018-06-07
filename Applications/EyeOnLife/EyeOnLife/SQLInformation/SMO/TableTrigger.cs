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
    public static class TableTrigger
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_TABLETRIGGER;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Table table, Guid tableID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.Trigger trigger in table.Triggers)
            {
                if (trigger.IsSystemObject)
                {
                    continue;   // Skip System Triggers
                }

                GetInfoFromSMO(trigger, tableID);
            }
#if TRACE
            VNC.AppLog.Trace4("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.TBTriggersRow GetInfoFromSMO(MSMO.Trigger trigger, Guid tableID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.TBTriggersRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.TBTriggers
                          where tb.DBTable_ID == tableID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_Trigger == trigger.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(trigger, dataRow);
                }
                else
                {
                    dataRow = Add(tableID, trigger);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3);
            }
#if TRACE
            VNC.AppLog.Trace5("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.TBTriggersRow Add(Guid tableID, MSMO.Trigger trigger)
        {
            SQLInformation.Data.ApplicationDataSet.TBTriggersRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.TBTriggers.NewTBTriggersRow();
                dataRow.ID = Guid.NewGuid();   // See if this is available from table.
                dataRow.Name_Trigger = trigger.Name;

                //newTrigger.Table_ID = trigger.ID.ToString();
                dataRow.DBTable_ID = tableID;  // From above
                dataRow.CreateDate = trigger.CreateDate;

                try
                {
                    dataRow.DateLastModified = trigger.DateLastModified;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
#endif
                }

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.TBTriggers.AddTBTriggersRow(dataRow);
                Common.ApplicationDataSet.TBTriggersTA.Update(Common.ApplicationDataSet.TBTriggers);
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

        private static void Update(MSMO.Trigger trigger, SQLInformation.Data.ApplicationDataSet.TBTriggersRow dataRow)
        {
            try
            {
                trigger.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.TBTriggersRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.TBTriggersTA.Update(Common.ApplicationDataSet.TBTriggers);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("TBTriggersRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.Trigger trigger, Data.ApplicationDataSet.TBTriggersRow dataRow)
        {

            try
            {
                //dataRow.X = trigger.X;
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
