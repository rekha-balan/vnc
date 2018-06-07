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
    public static class TableColumn
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_TABLECOLUMN;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Table table, Guid tableID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            foreach (MSMO.Column column in table.Columns)
            {
                GetInfoFromSMO(column, tableID);
            }
#if TRACE
            VNC.AppLog.Trace4("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1, startTicks);
#endif
        }

        private static SQLInformation.Data.ApplicationDataSet.TBColumnsRow GetInfoFromSMO(MSMO.Column column, Guid tableID)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
#endif
            SQLInformation.Data.ApplicationDataSet.TBColumnsRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.TBColumns
                          where tb.DBTable_ID == tableID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_Column == column.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(column, dataRow);
                }
                else
                {
                    dataRow = Add(tableID, column);
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

        private static SQLInformation.Data.ApplicationDataSet.TBColumnsRow Add(Guid tableID, MSMO.Column column)
        {
            SQLInformation.Data.ApplicationDataSet.TBColumnsRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.TBColumns.NewTBColumnsRow();
                dataRow.ID = Guid.NewGuid();
                dataRow.Name_Column = column.Name;

                dataRow.DBTable_ID = tableID;
                dataRow.DataType = column.DataType.ToString();
                dataRow.Identity = column.Identity;
                dataRow.IsForeignKey = column.IsForeignKey;
                dataRow.Nullable = column.Nullable;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.TBColumns.AddTBColumnsRow(dataRow);
                Common.ApplicationDataSet.TBColumnsTA.Update(Common.ApplicationDataSet.TBColumns);
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

        private static void Update(MSMO.Column column, SQLInformation.Data.ApplicationDataSet.TBColumnsRow dataRow)
        {
            try
            {
                column.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.TBColumnsRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.TBColumnsTA.Update(Common.ApplicationDataSet.TBColumns);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("TBColumnsRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.Column column, Data.ApplicationDataSet.TBColumnsRow dataRow)
        {
            try
            {
                //dataRow.X = column.X;
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
