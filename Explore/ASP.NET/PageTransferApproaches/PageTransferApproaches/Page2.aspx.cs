using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VNC;

namespace PageTransferApproaches
{
    public partial class Page2 : System.Web.UI.Page
    {
        const Int32 BASE_ERRORNUMBER = 0;
        const String LOG_APPNAME = "WEBAPP";

        protected void Page_Load(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            txtErrorMessage.Text = (string)HttpContext.Current.Session["ErrorMessage"];

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("FilePath ({0})", Request.FilePath);
            sb.AppendFormat("\nCurrentExecutionFilePath ({0})", Request.CurrentExecutionFilePath);
            txtInfo.Text = sb.ToString();

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }
    }
}