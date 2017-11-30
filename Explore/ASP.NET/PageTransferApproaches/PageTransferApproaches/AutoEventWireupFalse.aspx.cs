﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VNC;

namespace PageTransferApproaches
{
    public partial class AutoEventWireupFalse : System.Web.UI.Page
    {
        const Int32 BASE_ERRORNUMBER = 0;
        const String LOG_APPNAME = "WEBAPP";

        // Following are three alternative ways of binding an event
        // handler to an event when AutoEventWireup is false.  For
        // any given event do this binding only once or the handler
        // will be called multiple times.

        // You can wire up events in the page's constructor.
        public AutoEventWireupFalse()
        {
            Load += new EventHandler(Page_Load);
        }

        // You can override the OnInit event and wire up events there.

        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    Load += new EventHandler(Page_Load);
        //}

        // Or you can override the event's OnEventname method and
        // call your handler from there.  You can also put the code
        // execute when the event fires within the override method itself.

        //protected override void OnLoad(EventArgs e)
        //{
        //    //Page_Load(null, null);
        //    //base.OnLoad(e);
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }
    }
}