using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VNC;

namespace PageTransferApproaches
{
    public partial class MainPage : System.Web.UI.Page
    {
        const Int32 BASE_ERRORNUMBER = 0;
        const String LOG_APPNAME = "WEBAPP";

        #region Protected Methods

        protected void btn1_Click(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            try
            {
                txtErrorMessage.Text = "";
                Response.Redirect(txtPage1URL.Text);
                HttpContext.Current.Session["ErrorMessage"] = "After Redirect(url)";
            }
            catch (Exception ex)
            {
                AppLog.Error(ex, LOG_APPNAME);
                HttpContext.Current.Session["ErrorMessage"] = ex.ToString();
            }

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            try
            {
                txtErrorMessage.Text = "";
                Response.Redirect(txtPage1URL.Text, false);
                HttpContext.Current.Session["ErrorMessage"] = "After Redirect(url, false)";
            }
            catch (Exception ex)
            {
                AppLog.Error(ex, LOG_APPNAME);
                HttpContext.Current.Session["ErrorMessage"] = ex.ToString();
            }

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btn3_Click(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            try
            {
                txtErrorMessage.Text = "";
                Response.Redirect(txtPage1URL.Text, true);
                HttpContext.Current.Session["ErrorMessage"] = "After Redirect(url, true)";
            }
            catch (Exception ex)
            {
                AppLog.Error(ex, LOG_APPNAME);
                HttpContext.Current.Session["ErrorMessage"] = ex.ToString();
            }

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btn4_Click(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            try
            {
                txtErrorMessage.Text = "";
                Server.Transfer(txtPage2URL.Text);
                HttpContext.Current.Session["ErrorMessage"] = "After Transfer(url)";
            }
            catch (Exception ex)
            {
                AppLog.Error(ex, LOG_APPNAME);
                HttpContext.Current.Session["ErrorMessage"] = ex.ToString();
            }

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btn5_Click(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            try
            {
                txtErrorMessage.Text = "";
                Server.Transfer(txtPage2URL.Text, false);
                HttpContext.Current.Session["ErrorMessage"] = "After Transfer(url, false)";
            }
            catch (Exception ex)
            {
                AppLog.Error(ex, LOG_APPNAME);
                HttpContext.Current.Session["ErrorMessage"] = ex.ToString();
            }

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btn6_Click(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            try
            {
                txtErrorMessage.Text = "";
                Server.Transfer(txtPage2URL.Text, true);
                HttpContext.Current.Session["ErrorMessage"] = "After Transfer(url, true)";
            }
            catch (Exception ex)
            {
                AppLog.Error(ex, LOG_APPNAME);
                HttpContext.Current.Session["ErrorMessage"] = ex.ToString();
            }

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

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

        #endregion

        protected void btnPageLifeCycle_Click(object sender, EventArgs e)
        {
            Response.Redirect("PageLifeCycle.aspx");
        }
    }
}