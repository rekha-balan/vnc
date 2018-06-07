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
    class Exceptions
    {
        //private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_EXCEPTIONS;
        private const string LOG_APPNAME = "SQLInformation";

        // Creates some methods that will take input from the catch blocks and write out exceptions.
        // Need to think through how to make this generic as will have different types of exceptions (not a problem)
        // and different TableAdapters and DataTables to update.
        // Seems like reflection to get the ".Update() method and some way of casting the datatable.
        //
        // Want to be able to do something like this
        //
        // LogSnapShotError/LogSnapShotWarning(...)
        //  UpdateDatatable with SnapShot Error
        //  VNC.AppLog info
        //
        // LogSnapShotError(Message,TableAdaper,DataTable, EventID)
        // LogSnapShotWarning(Message,TableAdapter,DataTable,EventID)

        public static void ReportException<T>(Exception ex, T dataRow, int eventID)
        {
            string errorMessage = "";

            Type typ = dataRow.GetType();

            //var foo = ((typ)dataRow).ID;
            //Type gtyp = typ.GetGenericTypeDefinition();

            //if (dataRow != null)
            //{
            //    errorMessage = string.Format("JSAlertsRow.ID:{0} - ex:{1} ex.Inner:{2}", dataRow.ID, ex, ex.InnerException);
            //}
            //else
            //{
            //    errorMessage = string.Format("JSAlertsRow(null) - ex:{0} ex.Inner:{1}", ex, ex.InnerException);
            //}

            VNC.AppLog.Error(errorMessage, LOG_APPNAME, eventID);
        }

    }
}
