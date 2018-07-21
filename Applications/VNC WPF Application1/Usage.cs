using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

//using SQLInformation;
using VNC;


namespace VNC_WPF_Application1
{
    public class Usage
    {
        public static int CLASS_BASE_ERRORNUMBER = VNC_WPF_Application1.ErrorNumbers.VNC_WPF_Application1 + 0;
        private const string LOG_APPNAME = Common.LOG_APPNAME;

        #region Main Function Routines

        public static void IndicateApplicationUsage(string application, DateTime eventDate, string user, string message)
        {
            var dataRow = Common.ApplicationDS.ApplicationUsage.NewApplicationUsageRow();

            dataRow.Application = application;
            dataRow.EventDate = eventDate;
            dataRow.User = user;
            dataRow.EventMessage = message;

            Common.ApplicationDS.ApplicationUsage.AddApplicationUsageRow(dataRow);
            // TODO(crhodes):
            // Uncomment once we figure out Database
            //Common.ApplicationDS.ApplicationUsageTA.Update(Common.ApplicationDS.ApplicationUsage);
        }


        #endregion

    }
}
