using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VNC;

namespace PageTransferApproaches
{
    public partial class PageLifeCycle : System.Web.UI.Page
    {
        const Int32 BASE_ERRORNUMBER = 0;
        const String LOG_APPNAME = "WEBAPP";

        // Initialization
        //
        // During page initialization, controls on the page are available and each control's UniqueID property is set. 
        // A master page and themes are also applied to the page if applicable. If the current request is a postback, 
        // the postback data has not yet been loaded and control property values have not been restored to the values from view state.

        protected void Page_PreInit(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Raised after the start stage is complete and before the initialization stage begins.
            // 
            //  Use this event for the following:
            //      Check the IsPostBack property to determine whether this is the first time the page is being processed.
            //      The IsCallback and IsCrossPagePostBack properties have also been set at this time.
            //      Create or re-create dynamic controls.
            //      Set a master page dynamically.
            //      Set the Theme property dynamically.
            //      Read or set profile property values
            //
            // If the request is a postback, the values of the controls have not yet been restored from view state. 
            // If you set a control property at this stage, its value might be overwritten in the next event.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Raised after all controls have been initialized and any skin settings have been applied. 
            // The Init event of individual controls occurs before the Init event of the page.
            //
            // Use this event to read or initialize control properties.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void Page_InitComplete(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Raised at the end of the page's initialization stage. 
            // Only one operation takes place between the Init and InitComplete events: tracking of view state changes is turned on. 
            // View state tracking enables controls to persist any values that are programmatically added to the ViewState collection. 
            // Until view state tracking is turned on, any values added to view state are lost across postbacks. 
            // Controls typically turn on view state tracking immediately after they raise their Init event.
            //
            // Use this event to make changes to view state that you want to make sure are persisted after the next postback. 
        
            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        // Load
        //
        // During load, if the current request is a postback, control properties are loaded with information 
        // recovered from view state and control state

        protected override void OnPreLoad(EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Use this event if you need to perform processing on your page or control before the  Load event.
            // Before the Page instance raises this event, it loads view state for itself and all controls, and 
            // then processes any postback data included with the Request instance.
            //
            // Raised after the page loads view state for itself and all controls, 
            // and after it processes postback data that is included with the Request instance.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // The  Page calls the  OnLoad event method on the  Page, then recursively does the same for each child
            // control, which does the same for each of its child controls until the page and all controls are loaded.
            // Use the OnLoad event method to set properties in controls and establish database connections.

            // The Page object calls the OnLoad method on the Page object, and then recursively does the same 
            // for each child control until the page and all controls are loaded. The Load event of individual controls
            // occurs after the Load event of the page.
            // Use the OnLoad event method to set properties in controls and to establish database connections.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // The  Page calls the  OnLoad event method on the  Page, then recursively does the same for each child
            // control, which does the same for each of its child controls until the page and all controls are loaded.
            // Use the OnLoad event method to set properties in controls and establish database connections.

            // The Page object calls the OnLoad method on the Page object, and then recursively does the same 
            // for each child control until the page and all controls are loaded. The Load event of individual controls
            // occurs after the Load event of the page.
            // Use the OnLoad event method to set properties in controls and to establish database connections.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }



        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Use this event for tasks that require that all other controls on the page be loaded.

            // Raised at the end of the event-handling stage.
            //
            // Use this event for tasks that require that all other controls on the page be loaded.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected override void OnPreRender(EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Raised after the Page object has created all controls that are required in order to render the page, 
            // including child controls of composite controls. (To do this, the Page object calls EnsureChildControls 
            // for each control and for the page.)
            // The Page object raises the PreRender event on the Page object, and then recursively does the same for each child control.
            // The PreRender event of individual controls occurs after the PreRender event of the page.
            //
            // Use the event to make final changes to the contents of the page or its controls before the rendering stage begins.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Use this event for tasks that require that all other controls on the page be loaded.

            // Raised at the end of the event-handling stage.
            // Use this event for tasks that require that all other controls on the page be loaded.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void Render(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // This is not an event; instead, at this stage of processing, the Page object calls this method on each control.
            // All ASP.NET Web server controls have a Render method that writes out the control's markup to send to the browser.
            // If you create a custom control, you typically override this method to output the control's markup. 
            // However, if your custom control incorporates only standard ASP.NET Web server controls and no custom markup, 
            // you do not need to override the Render method. For more information, see Developing Custom ASP.NET Server Controls.
            // A user control (an.ascx file) automatically incorporates rendering, so you do not need to explicitly render the control in code.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }



        protected void OnPreRenderComplete(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Raised after each data bound control whose DataSourceID property is set calls its DataBind method. 
            // For more information, see Data Binding Events for Data-Bound Controls later in this topic.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Raised after each data bound control whose DataSourceID property is set calls its DataBind method. 
            // For more information, see Data Binding Events for Data-Bound Controls later in this topic.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            // Before this event occurs,  ViewState has been saved for the page and for all controls. 
            // Any changes to the page or controls at this point will be ignored.
            // Use this event perform tasks that require view state to be saved, but that do not make any changes to controls.
            //
            // Raised after view state and control state have been saved for the page and for all controls. 
            // Any changes to the page or controls at this point affect rendering, but the changes will not be retrieved on the next postback.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void Page_SaveStateComplete(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        // Unload
        //
        // The Unload event is raised after the page has been fully rendered, sent to the client, and is ready to be discarded. 
        // At this point, page properties such as Response and Request are unloaded and cleanup is performed

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);
            // This event occurs for each control and then for the page. In controls, use this event to do final cleanup 
            // for specific controls, such as closing control-specific database connections.
            // During the unload stage, the page and its controls have been rendered, so you cannot make further 
            // changes to the response stream.
            //If you attempt to call a method such as the Response.Write method, the page will throw an exception.

            // Raised for each control and then for the page.
            // In controls, use this event to do final cleanup for specific controls, such as closing control-specific database connections.
            // For the page itself, use this event to do final cleanup work, such as closing open files and database connections, 
            // or finishing up logging or other request-specific tasks.

            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        //btnButton.Click
        //btnButton.ClientClick
        //btnButton.Command
        //btnButton.DataBinding
        //btnButton.Disposed
        //btnButton.Init
        //btnButton.Load
        //btnButton.PreRender
        //btnButton.Unload

        protected void btnButton_Click(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btnButton_ClientClick(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btnButton_Command(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btnButton_DataBinding(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btnButton_Disposed(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btnButton_Init(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btnButton_Load(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btnButton_PreRender(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void btnButton_UnLoad(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        //form1.DataBinding
        //form1.Disabled
        //form1.Disposed
        //form1.Load
        //form1.PreRender
        //form1.Unload

        protected void form1_DataBinding(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void form1_Disabled(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void form1_Disposed(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void form1_Init(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void form1_Load(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void form1_PreRender(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }

        protected void form1_UnLoad(object sender, EventArgs e)
        {
            long startTicks = AppLog.Info("Enter", LOG_APPNAME, BASE_ERRORNUMBER + 0);


            AppLog.Info("Exit", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
        }


    }
}
