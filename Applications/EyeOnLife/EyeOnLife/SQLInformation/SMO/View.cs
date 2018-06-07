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
    public static class View
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_VIEW;
        private const string LOG_APPNAME = "SQLInformation";

        public static void LoadFromSMO(MSMO.Database database, Guid databaseID, ExpandMask.ViewExpandSetting viewExpandSetting)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace3("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif

            MarkExistingItemsAsNotFound(databaseID);    // This enables cleanup of items that once existed but were deleted.

            foreach (MSMO.View view in database.Views)
            {
                if (view.IsSystemObject)
                {
                    continue;   // Skip System Views
                }

                SQLInformation.Data.ApplicationDataSet.DBViewsRow viewRow = GetInfoFromSMO(databaseID, view, viewExpandSetting);
                viewRow.NotFound = false;

                if (viewRow.ExpandColumns)
                {
                    try
                    {
                        ViewColumn.LoadFromSMO(view, viewRow.ID);
                    }
                    catch (Exception ex)
                    {
                        VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                    }
                }

                try
                {
                    ViewTrigger.LoadFromSMO(view, viewRow.ID);
                }
                catch (Exception ex)
                {
                    VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2);
                }
            }

            Common.ApplicationDataSet.DBViewsTA.Update(Common.ApplicationDataSet.DBViews);
#if TRACE
            VNC.AppLog.Trace3("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 3, startTicks);
#endif
        }

        private static void MarkExistingItemsAsNotFound(Guid databaseID)
        {
            var previouslyFound = Common.ApplicationDataSet.DBViews.Where(i => i.Database_ID == databaseID);

            foreach (var pf in previouslyFound)
            {
                pf.NotFound = true;
            }
        }

        private static SQLInformation.Data.ApplicationDataSet.DBViewsRow GetInfoFromSMO(Guid databaseID, MSMO.View view, ExpandMask.ViewExpandSetting viewExpandSetting)
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace4(string.Format("Enter {0}", view.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 4);
#endif
            Guid viewID = Guid.Empty;

            SQLInformation.Data.ApplicationDataSet.DBViewsRow dataRow = null;

            try
            {
                var vs = from v in Common.ApplicationDataSet.DBViews
                         where v.Database_ID == databaseID
                         select v;

                var vs2 = from v2 in vs
                          where v2.Name_View == view.Name
                          select v2;

                if (vs2.Count() > 0)
                {
                    dataRow = vs2.First();
                    Update(view, dataRow);
                }
                else
                {
                    dataRow = Add(databaseID, view, viewExpandSetting);
                }
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
            }

#if TRACE
            VNC.AppLog.Trace4(string.Format("Exit {0}", view.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6, startTicks);
#endif
            return dataRow;
        }

        private static SQLInformation.Data.ApplicationDataSet.DBViewsRow Add(Guid databaseID, MSMO.View view, ExpandMask.ViewExpandSetting viewExpandSetting)
        {
            SQLInformation.Data.ApplicationDataSet.DBViewsRow dataRow = null;

            try
            {
                dataRow = Common.ApplicationDataSet.DBViews.NewDBViewsRow();
                dataRow.ID = Guid.NewGuid();
                dataRow.Name_View = view.Name;
                dataRow.View_ID = view.ID;
                dataRow.Database_ID = databaseID;  // From above
                dataRow.Owner = view.Owner;
                dataRow.ExpandColumns = viewExpandSetting.ExpandColumns;
                dataRow.CreateDate = view.CreateDate;

                //newView.DataSpaceUsed = int.Parse(viewH.DataSpaceUsed);   // This is not supported yet.

                try
                {
                    dataRow.DateLastModified = view.DateLastModified;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
#endif
                }

                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = "";

                Common.ApplicationDataSet.DBViews.AddDBViewsRow(dataRow);
                Common.ApplicationDataSet.DBViewsTA.Update(Common.ApplicationDataSet.DBViews);

                //viewID = viewRow.ID;
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

        private static void Update(MSMO.View view, SQLInformation.Data.ApplicationDataSet.DBViewsRow dataRow)
        {
            try
            {
                view.UpdateDataSet(dataRow);

                UpdateDatabaseWithSnapShot(dataRow, "");
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 9);

                UpdateDatabaseWithSnapShot(dataRow, ex.ToString().Substring(0, 256));
            }
        }

        private static void UpdateDatabaseWithSnapShot(Data.ApplicationDataSet.DBViewsRow dataRow, string snapShotError)
        {
            try
            {
                dataRow.SnapShotDate = DateTime.Now;
                dataRow.SnapShotError = snapShotError;
                Common.ApplicationDataSet.DBViewsTA.Update(Common.ApplicationDataSet.DBViews);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("DBViewsRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
                VNC.AppLog.Error(errorMessage, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 7);
            }
        }

        public static void UpdateDataSet(this MSMO.View view, Data.ApplicationDataSet.DBViewsRow dataRow)
        {
            try
            {
                dataRow.CreateDate = view.CreateDate;

                try
                {
                    dataRow.DateLastModified = view.DateLastModified;
                }
                catch (Exception ex)
                {
#if TRACE
                    VNC.AppLog.Debug(ex.ToString(), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 10);
#endif
                }

                dataRow.Owner = view.Owner;
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 11);
                // TODO(crhodes):  
                // Wrap anything above that throws an exception that we want to ignore, 
                // e.g. property not available because of SQL Edition.

            }
        }

    }
}
