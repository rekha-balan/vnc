﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VNC;

namespace SupportTools_PowerPoint
{
    class Common : VNC.AddinHelper.Common
    {
        new public const string PROJECT_NAME = "SupportTools_PowerPoint";

        public static VNC.AddinHelper.PowerPoint PowerPointHelper = new VNC.AddinHelper.PowerPoint();
        public static Events.PowerPointAppEvents AppEvents;

        //public static Microsoft.Office.Tools.CustomTaskPane TaskPaneConfig;

        //public static Microsoft.Office.Tools.CustomTaskPane TaskPaneHelp;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneAppUtil;
        public static Microsoft.Office.Tools.CustomTaskPane TaskPanePowerPointUtil;


        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneComplianceUtil;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneSharePointInfo;

        private static Data.ApplicationDS _ApplicationDS;
        public static Data.ApplicationDS ApplicationDS
        {
            get
            {
                if(_ApplicationDS == null)
                {
                    _ApplicationDS = new Data.ApplicationDS();

                    // TODO: Add any other initialization of things related to the ApplicationDS
                }

                return _ApplicationDS;
            }
            set
            {
                _ApplicationDS = value;
            }
        }
    }
}
