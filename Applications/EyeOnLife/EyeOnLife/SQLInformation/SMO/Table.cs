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
    public static class Table
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_TABLE;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Database database, Guid databaseID, ExpandMask.TableExpandSetting tableExpandSetting)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            SQLInformation.Data.ApplicationDataSet.DBTablesRow dataRow = null;

            MarkExistingItemsAsNotFound(databaseID);    // This enables cleanup of items that once existed but were deleted.

            foreach (MSMO.Table table in database.Tables)
            {
                if (table.IsSystemObject)
                {
                    continue;   // Skip System Tables
                }

                dataRow = GetInfoFromSMO(table, databaseID, tableExpandSetting);
                dataRow.NotFound = false;

                if (dataRow.ExpandColumns)
                {
                    try
                    {
                        TableColumn.LoadFromSMO(table, dataRow.ID);
                    }
                    catch (Exception ex)
                    {
                        ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 1);
                        //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }
                }

                try
                {
                    TableTrigger.LoadFromSMO(table, dataRow.ID);
                }
                catch (Exception ex)
                {
                    ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 2);
                    //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }

            }

            Common.ApplicationDataSet.DBTablesTA.Update(Common.ApplicationDataSet.DBTables);
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        private static void MarkExistingItemsAsNotFound(Guid databaseID)
        {
            var previouslyFound = Common.ApplicationDataSet.DBTables.Where(i => i.Database_ID == databaseID);

            foreach (var pf in previouslyFound)
            {
                pf.NotFound = true;
            }
        }

        private static SQLInformation.Data.ApplicationDataSet.DBTablesRow GetInfoFromSMO(MSMO.Table table, Guid databaseID, ExpandMask.TableExpandSetting tableExpandSetting)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter {0}", table.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
#endif
            SQLInformation.Data.ApplicationDataSet.DBTablesRow dataRow = null;

            try
            {
                var dbs = from tb in Common.ApplicationDataSet.DBTables
                          where tb.Database_ID == databaseID
                          select tb;

                var dbs2 = from db2 in dbs
                           where db2.Name_Table == table.Name
                           select db2;

                if (dbs2.Count() > 0)
                {
                    dataRow = dbs2.First();
                    Update(table, dataRow);
                }
                else
                {
                    dataRow = Add(databaseID, table, tableExpandSetting);
                }
            }
            catch (Exception ex)
            {
                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 5);
                //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
            }

#if TRACE
            VNC.AppLog.Trace4(string.Format("Exit {0}", table.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.DBTablesRow Add(Guid databaseID, MSMO.Table table, ExpandMask.TableExpandSetting tableExpandSetting)
        {
            SQLInformation.Data.ApplicationDataSet.DBTablesRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBTables.NewDBTablesRow();
                dataRow.ID = Guid.NewGuid();
                dataRow.Database_ID = databaseID;  // From above

                dataRow.Name_Table = table.Name;
                dataRow.CreateDate = table.CreateDate;

                try
                {
                    dataRow.DataSpaceUsed = table.DataSpaceUsed;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
#endif
                    dataRow.DataSpaceUsed = -1;
                }

                try
                {
                    dataRow.DateLastModified = table.DateLastModified;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
#endif
                }

                dataRow.FileGroup = table.FileGroup;
                dataRow.HasIndex = table.HasIndex;
                dataRow.Owner = table.Owner;
                dataRow.RowCount = table.RowCount;   // TODO(crhodes): Fix DB Schema to match
                dataRow.Table_ID = table.ID.ToString();

                dataRow.ExpandColumns = tableExpandSetting.ExpandColumns;

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBTables_Add(dataRow);

                //Common.ApplicationDataSet.DBTables.AddDBTablesRow(dataRow);

                //Common.ApplicationDataSet.DBTablesTA.Update(Common.ApplicationDataSet.DBTables);

                // TODO(crhodes) extract Extended Properties
            }
            catch (Exception ex)
            {
                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 8);
                //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

                //UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }

            return dataRow;
        }

        private static void Update(MSMO.Table table, SQLInformation.Data.ApplicationDataSet.DBTablesRow dataRow)
        {
            try
            {
                try
                {
                    try
                    {
                        dataRow.DataSpaceUsed = table.DataSpaceUsed;
                    }
                    catch (Exception ex)
                    {
#if TRACE
                        VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                        dataRow.DataSpaceUsed = -1;
                    }

                    try
                    {
                        dataRow.DateLastModified = table.DateLastModified;
                    }
                    catch (Exception ex)
                    {
#if TRACE
                        VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);
#endif
                    }

                    dataRow.FileGroup = table.FileGroup;
                    dataRow.Owner = table.Owner;
                    dataRow.RowCount = table.RowCount;
                }
                catch (Exception ex)
                {
                    ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 12);
                    //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12);
                    // TODO(crhodes):  
                    // Wrap anything above that throws an exception that we want to ignore, 
                    // e.g. property not available because of SQL Edition.

                }

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBTables_Update();
                //UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                ReportException(ex, dataRow, CLASS_BASE_ERRORNUMBER + 13);
                //VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);

                //UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        //private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBTablesRow dataRow, string snapShotError)
        //{
        //    try
        //    {
        //        dataRow.SnapShotDate = DateTime.Now;
        //        dataRow.SnapShotError = snapShotError;
        //        Common.ApplicationDataSet.DBTablesTA.Update(Common.ApplicationDataSet.DBTables);
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = string.Format("DBTablesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
        //        VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
        //    }
        //}

//        public static void UpdateDataSet(this MSMO.Table table, Data.ApplicationDataSet.DBTablesRow dataRow)
//        {

//            try
//            {
//                try
//                {
//                    dataRow.DataSpaceUsed = table.DataSpaceUsed;
//                }
//                catch (Exception ex)
//                {
//#if TRACE
//                    dataRow.DataSpaceUsed = -1;
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
//#endif  
//                }

//                try
//                {
//                    dataRow.DateLastModified = table.DateLastModified;
//                }
//                catch (Exception ex)
//                {
//#if TRACE
//                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);
//#endif
//                }

//                dataRow.FileGroup = table.FileGroup;
//                dataRow.Owner = table.Owner;
//                dataRow.RowCount = table.RowCount;
//            }
//            catch (Exception ex)
//            {
//                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 12);
//                // TODO(crhodes):  
//                // Wrap anything above that throws an exception that we want to ignore, 
//                // e.g. property not available because of SQL Edition.

//            }
//        }

        private static void ReportException(Exception ex, Data.ApplicationDataSet.DBTablesRow dataRow, int eventID)
        {
            string errorMessage = "";

            if (dataRow != null)
            {
                errorMessage = string.Format("DBTablesRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                dataRow.SnapShotError = errorMessage;
                Common.ApplicationDataSet.DBTables_Update();
            }
            else
            {
                errorMessage = string.Format("DBTablesRow(null) - ex:{0} ex.Inner:{1}", ex, ex.InnerException);
            }

            VNC.AppLog.Error(errorMessage, LOG_APPNAME, eventID);
        }

    }
}
