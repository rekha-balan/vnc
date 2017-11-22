﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

using SupportTools_Excel.User_Interface;

using XlHlp = VNC.AddinHelper.Excel;

namespace SupportTools_Excel
{
    ///<summary>
    ///Common items declared at the Class level.
    ///</summary>
    ///<remarks>
    ///Use this class for any thing you want globally available.
    ///Place only Static items in this class.  This Class cannot not be instantiated.
    ///</remarks>  
    ///
    class Common : VNC.AddinHelper.Common
    {
        new public const string PROJECT_NAME = "SupportTools_Excel";

        public const string cCONFIG_FILE = @"C:\temp\SupportTools_Config.xml";

        public static VNC.AddinHelper.Excel ExcelHelper = new VNC.AddinHelper.Excel();
        public static Events.ExcelAppEvents AppEvents;

        public const int cMaxFileNameLength = 128;

        public static IPrincipal CurrentUser
        {
            get;
            set;
        }

        public static event EventHandler AutoHideGroupSpeedChanged;

        private static int _AutoHideGroupSpeed = 250;

        public static int AutoHideGroupSpeed
        {
            get { return _AutoHideGroupSpeed; }
            set
            {
                _AutoHideGroupSpeed = value;

                EventHandler evt = AutoHideGroupSpeedChanged;

                if (evt != null)
                {
                    evt(null, EventArgs.Empty); ;
                }
            }
        }

        // This controls the behavior of the overall application.
        // It is initialized from app.config and is updated when the user changes the mode.
        // Changes are reflected in the app.config file.

        public static ViewMode UserMode { get; set; }

        public static bool IsAdministrator { get; set; }
        public static bool IsBetaUser { get; set; }
        public static bool IsDeveloper { get; set; }
        //public static bool IsAdvancedUser { get; set; }

        public static bool AllowEditing { get; set; }

        public static string RowDetailMode { get; set; }

        private static Data.ApplicationDS _ApplicationDS;

        public static Data.ApplicationDS ApplicationDS
        {
            get
            {
                if (_ApplicationDS == null)
                {
                    _ApplicationDS = new Data.ApplicationDS();
                }
                return _ApplicationDS;
            }
            set
            {
                _ApplicationDS = value;
            }
        }

        // TaskPane specific stuff

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneActiveDirectory;

        //public static Microsoft.Office.Tools.CustomTaskPane TaskPaneConfig;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneDevelopment;


        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneExcelUtil;

        //public static Microsoft.Office.Tools.CustomTaskPane TaskPaneHelp;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneLogParser;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneLTC;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneNetworkTrace;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneSharePoint;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneSMO;
            
        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneSQLSMO;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneTFS;

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneUtilities;

        #region ITRs

        //public static Microsoft.Office.Tools.CustomTaskPane TaskPaneITRs;

        public const string cDEFAULT_FOLDER = @"G:\Integration Team";
        public static string TeamName = "Integration and Reporting Services";

        public const string cITRHeader_Cell = "A5";
        public const string cITRInfo_CommentColumns = "$O:$R";

        public const string cITRITRInfoWithResources_CommentColumns = "$O:$R";
        public const int cFirstITRRow = 6;

        public const int cFI_SecondITRRow = 11;
        public const int cAPPLICATION_COLUMN = 1;
        public const int cITRID_COLUMN = 2;
        public const int cENTEREDON_COLUMN = 3;
	        // Added by code
        private const int cAGE_COLUMN = 4;
        public const int cENTEREDBY_COLUMN = 5;
        public const int cREQUESTEDBY_COLUMN = 6;
        public const int cRELEASENBR_COLUMN = 7;
        public const int cPATRANK_COLUMN = 8;
        public const int cCATEGORY_COLUMN = 9;
        public const int cSTATUS_COLUMN = 10;
        public const int cSEVERITY_COLUMN = 11;
        public const int cLOE_COLUMN = 12;
        public const int cSUBJECT_COLUMN = 13;
        public const int cRESOURCEID_COLUMN = 14;
        public const int cCURRENTCONDITION_COLUMN = 15;
        public const int cDESIREDOUTCOME_COLUMN = 16;
        public const int cPRIORITIZATIONCOMMENTS_COLUMN = 17;
        public const int cCOMMENTS_COLUMN = 18;

        // Formatted ITRs worksheets

        public const string cFI_Application_Column_Range  = "A:A";
        public const string cFI_ITRID_Column_Range  = "B:B";
        public const string cFI_EnteredOn_Column_Range  = "C:C";
        public const string cFI_Age_Column_Range  = "D:D";
        public const string cFI_EnteredBy_Column_Range  = "E:E";
        public const string cFI_RequestedBy_Column_Range  = "F:F";
        public const string cFI_ReleaseNbr_Column_Range  = "G:G";
        public const string cFI_PatRank_Column_Range  = "H:H";
        public const string cFI_Category_Column_Range  = "I:I";
        public const string cFI_Status_Column_Range  = "J:J";
        public const string cFI_Severity_Column_Range  = "K:K";
        public const string cFI_LOE_Column_Range  = "L:L";
        public const string cFI_Subject_Column_Range  = "M:M";
        public const string cFI_Resource_Column_Range  = "N:N";

        // PivotTable worksheets

        public const string cPT_ITR_Column_Range = "A:A";
        public const string cPT_Count_Column_Range  = "B:B";

        #endregion

        #region MTreaty

        public static Microsoft.Office.Tools.CustomTaskPane TaskPaneMTreaty;

        public const string cMTREATY_FOLDER_PROD = @"\\lifenas115\DataServices\Production\M_Treaty_Reporting";
        public const string cMTREATY_FOLDER_STAGING = @"\\lifenas215\DataServices\QA_Staging\M_Treaty_Reporting";
        public const string cMTREATY_FUND_SERVICE_FEES_SHEETNAME = "Stan Tucker";
        public const string cMTREATY_FUND_ADVISORY_FEES_SHEETNAME = "Combined Fees";
        public const string cMTREATY_CASH_MANAGEMENT_FEES_SHEETNAME = "??";
        public const string cMTREATY_VITS_FEES_SHEETNAME = "??";

        #endregion

        #region SQLSMO



        #endregion


    }
}
