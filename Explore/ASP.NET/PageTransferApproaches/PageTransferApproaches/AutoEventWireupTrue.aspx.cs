using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VNC;

namespace PageTransferApproaches
{
    public partial class AutoEventWireupTrue : System.Web.UI.Page
    {
        const Int32 BASE_ERRORNUMBER = 0;
        const String LOG_APPNAME = "WEBAPP";

        protected void Page_Load(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }
    }
}