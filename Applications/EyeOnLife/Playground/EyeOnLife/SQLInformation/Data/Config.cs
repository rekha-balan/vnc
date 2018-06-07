using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

using System.Configuration;
using System.Reflection;
using System.Text;

using PacificLife.Life;

namespace SQLInformation.Data
{
    /// <summary>
    /// This class loads information from the app.config/web.config file
    /// and presents it as properties off the Config class.
    /// There should not be any access to AppSettings in other classes.
    /// All configuration information should be provided by this class.
    /// All validation of settings and throwing of exceptions should be done in this class.
    /// The Set() methods get called if an external program, e.g. MCR_EAC needs to set the
    /// values from another source, so keep them!
    /// </summary>
    public static class Config
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "SQLINFOAGENT";

        // Please keep these in alphabetical order
        //
        // Please keep the Property names in sync with the setting string.
        // Spaces get replaced with underscores.  Case matters.
        // e.g. "AD Path" --> AD_Path and _AD_Path.

        private static string _AD_Path = null;
        public static string AD_Path
        {
            get
            {
                if(_AD_Path == null)
                {
                    _AD_Path = ValidateExists_String("AD Path");
                }

                return _AD_Path;
            }
            set
            {
                _AD_Path = value;
            }
        }

        private static string _AD_Server = null;
        public static string AD_Server
        {
            get
            {
                if(_AD_Server == null)
                {
                    _AD_Server = ValidateExists_String("AD Server");
                }

                return _AD_Server;
            }
            set
            {
                _AD_Server = value;
            }
        }

        private static string _Admin_Tools_Redirect =  null;
        public static string Admin_Tools_Redirect
        {
            get
            {
                if(_Admin_Tools_Redirect == null)
                {
                    _Admin_Tools_Redirect = ValidateExists_String("Admin Tools Redirect");
                }

                return _Admin_Tools_Redirect;
            }
            set
            {
                _Admin_Tools_Redirect = value;
            }
        }

        private static string _ADMSSQLDBConnection =  null;
        public static string ADMSSQLDBConnection
        {
            get
            {
                if(_ADMSSQLDBConnection == null)
                {
                    _ADMSSQLDBConnection = ValidateExists_String("ADMSSQLDBConnection");
                }

                return _ADMSSQLDBConnection;
            }
            set
            {
                _ADMSSQLDBConnection = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DatabaseInfo = null;
        public static Boolean ADSLoad_DatabaseInfo
        {
            get
            {
                if (_ADSLoad_DatabaseInfo == null)
                {
                    _ADSLoad_DatabaseInfo = ValidateExists_Boolean("ADSLoad_DatabaseInfo");
                }

                return (bool)_ADSLoad_DatabaseInfo;
            }
            set
            {
                _ADSLoad_DatabaseInfo = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_Databases = null;
        public static Boolean ADSLoad_Databases
        {
            get
            {
                if (_ADSLoad_Databases == null)
                {
                    _ADSLoad_Databases = ValidateExists_Boolean("ADSLoad_Databases");
                }

                return (bool)_ADSLoad_Databases;
            }
            set
            {
                _ADSLoad_Databases = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBDataFileInfo = null;
        public static Boolean ADSLoad_DBDataFileInfo
        {
            get
            {
                if (_ADSLoad_DBDataFileInfo == null)
                {
                    _ADSLoad_DBDataFileInfo = ValidateExists_Boolean("ADSLoad_DBDataFileInfo");
                }

                return (bool)_ADSLoad_DBDataFileInfo;
            }
            set
            {
                _ADSLoad_DBDataFileInfo = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBDataFiles = null;
        public static Boolean ADSLoad_DBDataFiles
        {
            get
            {
                if (_ADSLoad_DBDataFiles == null)
                {
                    _ADSLoad_DBDataFiles = ValidateExists_Boolean("ADSLoad_DBDataFiles");
                }

                return (bool)_ADSLoad_DBDataFiles;
            }
            set
            {
                _ADSLoad_DBDataFiles = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBDdlTriggers = null;
        public static Boolean ADSLoad_DBDdlTriggers
        {
            get
            {
                if (_ADSLoad_DBDdlTriggers == null)
                {
                    _ADSLoad_DBDdlTriggers = ValidateExists_Boolean("ADSLoad_DBDdlTriggers");
                }

                return (bool)_ADSLoad_DBDdlTriggers;
            }
            set
            {
                _ADSLoad_DBDdlTriggers = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBFileGroups = null;
        public static Boolean ADSLoad_DBFileGroups
        {
            get
            {
                if (_ADSLoad_DBFileGroups == null)
                {
                    _ADSLoad_DBFileGroups = ValidateExists_Boolean("ADSLoad_DBFileGroups");
                }

                return (bool)_ADSLoad_DBFileGroups;
            }
            set
            {
                _ADSLoad_DBFileGroups = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBLogFileInfo = null;
        public static Boolean ADSLoad_DBLogFileInfo
        {
            get
            {
                if (_ADSLoad_DBLogFileInfo == null)
                {
                    _ADSLoad_DBLogFileInfo = ValidateExists_Boolean("ADSLoad_DBLogFileInfo");
                }

                return (bool)_ADSLoad_DBLogFileInfo;
            }
            set
            {
                _ADSLoad_DBLogFileInfo = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBLogFiles = null;
        public static Boolean ADSLoad_DBLogFiles
        {
            get
            {
                if (_ADSLoad_DBLogFiles == null)
                {
                    _ADSLoad_DBLogFiles = ValidateExists_Boolean("ADSLoad_DBLogFiles");
                }

                return (bool)_ADSLoad_DBLogFiles;
            }
            set
            {
                _ADSLoad_DBLogFiles = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBRoles = null;
        public static Boolean ADSLoad_DBRoles
        {
            get
            {
                if (_ADSLoad_DBRoles == null)
                {
                    _ADSLoad_DBRoles = ValidateExists_Boolean("ADSLoad_DBRoles");
                }

                return (bool)_ADSLoad_DBRoles;
            }
            set
            {
                _ADSLoad_DBRoles = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBStoredProcedures = null;
        public static Boolean ADSLoad_DBStoredProcedures
        {
            get
            {
                if (_ADSLoad_DBStoredProcedures == null)
                {
                    _ADSLoad_DBStoredProcedures = ValidateExists_Boolean("ADSLoad_DBStoredProcedures");
                }

                return (bool)_ADSLoad_DBStoredProcedures;
            }
            set
            {
                _ADSLoad_DBStoredProcedures = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBStoredProcedureContent = null;
        public static Boolean ADSLoad_DBStoredProcedureContent
        {
            get
            {
                if (_ADSLoad_DBStoredProcedureContent == null)
                {
                    _ADSLoad_DBStoredProcedureContent = ValidateExists_Boolean("ADSLoad_DBStoredProcedureContent");
                }

                return (bool)_ADSLoad_DBStoredProcedureContent;
            }
            set
            {
                _ADSLoad_DBStoredProcedureContent = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBTables = null;
        public static Boolean ADSLoad_DBTables
        {
            get
            {
                if (_ADSLoad_DBTables == null)
                {
                    _ADSLoad_DBTables = ValidateExists_Boolean("ADSLoad_DBTables");
                }

                return (bool)_ADSLoad_DBTables;
            }
            set
            {
                _ADSLoad_DBTables = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBUserDefinedFunctions = null;
        public static Boolean ADSLoad_DBUserDefinedFunctions
        {
            get
            {
                if (_ADSLoad_DBUserDefinedFunctions == null)
                {
                    _ADSLoad_DBUserDefinedFunctions = ValidateExists_Boolean("ADSLoad_DBUserDefinedFunctions");
                }

                return (bool)_ADSLoad_DBUserDefinedFunctions;
            }
            set
            {
                _ADSLoad_DBUserDefinedFunctions = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBUsers = null;
        public static Boolean ADSLoad_DBUsers
        {
            get
            {
                if (_ADSLoad_DBUsers == null)
                {
                    _ADSLoad_DBUsers = ValidateExists_Boolean("ADSLoad_DBUsers");
                }

                return (bool)_ADSLoad_DBUsers;
            }
            set
            {
                _ADSLoad_DBUsers = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_DBViews = null;
        public static Boolean ADSLoad_DBViews
        {
            get
            {
                if (_ADSLoad_DBViews == null)
                {
                    _ADSLoad_DBViews = ValidateExists_Boolean("ADSLoad_DBViews");
                }

                return (bool)_ADSLoad_DBViews;
            }
            set
            {
                _ADSLoad_DBViews = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_InstanceInfo = null;
        public static Boolean ADSLoad_InstanceInfo
        {
            get
            {
                if (_ADSLoad_InstanceInfo == null)
                {
                    _ADSLoad_InstanceInfo = ValidateExists_Boolean("ADSLoad_InstanceInfo");
                }

                return (bool)_ADSLoad_InstanceInfo;
            }
            set
            {
                _ADSLoad_InstanceInfo = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_Instances = null;
        public static Boolean ADSLoad_Instances
        {
            get
            {
                if (_ADSLoad_Instances == null)
                {
                    _ADSLoad_Instances = ValidateExists_Boolean("ADSLoad_Instances");
                }

                return (bool)_ADSLoad_Instances;
            }
            set
            {
                _ADSLoad_Instances = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_Jobs = null;
        public static Boolean ADSLoad_Jobs
        {
            get
            {
                if (_ADSLoad_Jobs == null)
                {
                    _ADSLoad_Jobs = ValidateExists_Boolean("ADSLoad_Jobs");
                }

                return (bool)_ADSLoad_Jobs;
            }
            set
            {
                _ADSLoad_Jobs = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_Logins = null;
        public static Boolean ADSLoad_Logins
        {
            get
            {
                if (_ADSLoad_Logins == null)
                {
                    _ADSLoad_Logins = ValidateExists_Boolean("ADSLoad_Logins");
                }

                return (bool)_ADSLoad_Logins;
            }
            set
            {
                _ADSLoad_Logins = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_ServerInfo = null;
        public static Boolean ADSLoad_ServerInfo
        {
            get
            {
                if (_ADSLoad_ServerInfo == null)
                {
                    _ADSLoad_ServerInfo = ValidateExists_Boolean("ADSLoad_ServerInfo");
                }

                return (bool)_ADSLoad_ServerInfo;
            }
            set
            {
                _ADSLoad_ServerInfo = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_ServerRoles = null;
        public static Boolean ADSLoad_ServerRoles
        {
            get
            {
                if (_ADSLoad_ServerRoles == null)
                {
                    _ADSLoad_ServerRoles = ValidateExists_Boolean("ADSLoad_ServerRoles");
                }

                return (bool)_ADSLoad_ServerRoles;
            }
            set
            {
                _ADSLoad_ServerRoles = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_Servers = null;
        public static Boolean ADSLoad_Servers
        {
            get
            {
                if (_ADSLoad_Servers == null)
                {
                    _ADSLoad_Servers = ValidateExists_Boolean("ADSLoad_Servers");
                }

                return (bool)_ADSLoad_Servers;
            }
            set
            {
                _ADSLoad_Servers = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_TBTriggers = null;
        public static Boolean ADSLoad_TBTriggers
        {
            get
            {
                if (_ADSLoad_TBTriggers == null)
                {
                    _ADSLoad_TBTriggers = ValidateExists_Boolean("ADSLoad_TBTriggers");
                }

                return (bool)_ADSLoad_TBTriggers;
            }
            set
            {
                _ADSLoad_TBTriggers = value;
            }
        }

        private static Nullable<Boolean> _ADSLoad_VWTriggers = null;
        public static Boolean ADSLoad_VWTriggers
        {
            get
            {
                if (_ADSLoad_VWTriggers == null)
                {
                    _ADSLoad_VWTriggers = ValidateExists_Boolean("ADSLoad_VWTriggers");
                }

                return (bool)_ADSLoad_VWTriggers;
            }
            set
            {
                _ADSLoad_VWTriggers = value;
            }
        }
        
        private static Nullable<Int32> _BizTalkTimeoutInSecs =  null;
        public static Int32 BizTalkTimeoutInSecs
        {
            get
            {
                if(_BizTalkTimeoutInSecs == null)
                {
                    _BizTalkTimeoutInSecs = ValidateExists_Int32("BiztalkURLTimeOutInSecs");
                }

                return (Int32)_BizTalkTimeoutInSecs;
            }
            set
            {
                _BizTalkTimeoutInSecs = value;
            }
        }

        private static string _BizTalkURL =  null;
        public static string BizTalkURL
        {
            get
            {
                if(_BizTalkURL == null)
                {
                    _BizTalkURL = ValidateExists_String("BiztalkURL");
                }

                return _BizTalkURL;
            }
            set
            {
                _BizTalkURL = value;
            }
        }

        private static string _Create_Instance_SP =  null;
        public static string Create_Instance_SP
        {
            get
            {
                if(_Create_Instance_SP == null)
                {
                    _Create_Instance_SP = ValidateExists_String("Create Instance SP");
                }

                return _Create_Instance_SP;
            }
            set
            {
                _Create_Instance_SP = value;
            }
        }

        // This is a special case where we want the backing field initialized from the config file.
        // ValidateExists_DateTime returns null if cannot find or parse a "CurrentDate" value from config.
        private static Nullable<DateTime> _CurrentDate = ValidateExists_DateTime("CurrentDate");
        public static DateTime CurrentDate
        {
            get
            {
                if(_CurrentDate == null)
                {
                    return DateTime.Today;
                }
                else
                {
                    PLLog.Trace5(string.Format("Current Date: {0}", _CurrentDate.ToString()), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                    return (DateTime)_CurrentDate;
                }
            }

            set
            {
                try
                {
                    _CurrentDate = value;
                    PLLog.Info(string.Format("Setting CurrentDate to {0}", _CurrentDate.ToString()), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                }
                catch(Exception ex)
                {
                    PLLog.Error(ex, PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                    throw ex;
                }
            }
        }

        private static string _Daily_ReStartTime =  null;
        public static string Daily_ReStartTime
        {
            get
            {
                if(_Daily_ReStartTime == null)
                {
                    _Daily_ReStartTime = ValidateExists_String("Daily ReStartTime");
                }

                return _Daily_ReStartTime;
            }
            set
            {
                _Daily_ReStartTime = value;
            }
        }

        private static string _Daily_StartTime =  null;
        public static string Daily_StartTime
        {
            get
            {
                if(_Daily_StartTime == null)
                {
                    _Daily_StartTime = ValidateExists_String("Daily StartTime");
                }

                return _Daily_StartTime;
            }
            set
            {
                _Daily_StartTime = value;
            }
        }

        private static Nullable<Int32> _ExpandSetting_Database_Default = null;
        public static Int32 ExpandSetting_Database_Default
        {
            get
            {
                if (_ExpandSetting_Database_Default == null)
                {
                    _ExpandSetting_Database_Default = ValidateExists_Int32("ExpandSetting_Database_Default");
                }

                return (Int32)_ExpandSetting_Database_Default;
            }
            set
            {
                _ExpandSetting_Database_Default = value;
            }
        }

        private static Nullable<Int32> _ExpandSetting_Daily_Database = null;
        public static Int32 ExpandSetting_Daily_Database
        {
            get
            {
                if (_ExpandSetting_Daily_Database == null)
                {
                    _ExpandSetting_Daily_Database = ValidateExists_Int32("ExpandSetting_Daily_Database");
                }

                return (Int32)_ExpandSetting_Daily_Database;
            }
            set
            {
                _ExpandSetting_Daily_Database = value;
            }
        }

        private static Nullable<Int32> _ExpandSetting_Daily_Instance = null;
        public static Int32 ExpandSetting_Daily_Instance
        {
            get
            {
                if (_ExpandSetting_Daily_Instance == null)
                {
                    _ExpandSetting_Daily_Instance = ValidateExists_Int32("ExpandSetting_Daily_Instance");
                }

                return (Int32)_ExpandSetting_Daily_Instance;
            }
            set
            {
                _ExpandSetting_Daily_Instance = value;
            }
        }
        
        private static Nullable<Int32> _ExpandSetting_IntraDay_Database = null;
        public static Int32 ExpandSetting_IntraDay_Database
        {
            get
            {
                if (_ExpandSetting_IntraDay_Database == null)
                {
                    _ExpandSetting_IntraDay_Database = ValidateExists_Int32("ExpandSetting_IntraDay_Database");
                }

                return (Int32)_ExpandSetting_IntraDay_Database;
            }
            set
            {
                _ExpandSetting_IntraDay_Database = value;
            }
        }

        private static Nullable<Int32> _ExpandSetting_IntraDay_Instance = null;
        public static Int32 ExpandSetting_IntraDay_Instance
        {
            get
            {
                if (_ExpandSetting_IntraDay_Instance == null)
                {
                    _ExpandSetting_IntraDay_Instance = ValidateExists_Int32("ExpandSetting_IntraDay_Instance");
                }

                return (Int32)_ExpandSetting_IntraDay_Instance;
            }
            set
            {
                _ExpandSetting_IntraDay_Instance = value;
            }
        }

        private static Nullable<Int32> _ExpandSetting_Weekly_Database = null;
        public static Int32 ExpandSetting_Weekly_Database
        {
            get
            {
                if (_ExpandSetting_Weekly_Database == null)
                {
                    _ExpandSetting_Weekly_Database = ValidateExists_Int32("ExpandSetting_Weekly_Database");
                }

                return (Int32)_ExpandSetting_Weekly_Database;
            }
            set
            {
                _ExpandSetting_Weekly_Database = value;
            }
        }

        private static Nullable<Int32> _ExpandSetting_Weekly_Instance = null;
        public static Int32 ExpandSetting_Weekly_Instance
        {
            get
            {
                if (_ExpandSetting_Weekly_Instance == null)
                {
                    _ExpandSetting_Weekly_Instance = ValidateExists_Int32("ExpandSetting_Weekly_Instance");
                }

                return (Int32)_ExpandSetting_Weekly_Instance;
            }
            set
            {
                _ExpandSetting_Weekly_Instance = value;
            }
        }
        
        private static string _DefaultUITheme = null;
        public static string DefaultUITheme
        {
            get
            {
                if (_DefaultUITheme == null)
                {
                    _DefaultUITheme = ValidateExists_String("DefaultUITheme");
                }

                return _DefaultUITheme;
            }
            set
            {
                _DefaultUITheme = value;
            }
        }

        private static string _Email_CC =  null;
        public static string Email_CC
        {
            get
            {
                if(_Email_CC == null)
                {
                    _Email_CC = ValidateExists_String("Email CC");
                }

                return _Email_CC;
            }
            set
            {
                _Email_CC = value;
            }
        }

        private static string _Email_From =  null;
        public static string Email_From
        {
            get
            {
                if(_Email_From == null)
                {
                    _Email_From = ValidateExists_String("Email From");
                }

                return _Email_From;
            }
            set
            {
                _Email_From = value;
            }
        }

        private static string _Email_Subject_Backdated_Requests =  null;
        public static string Email_Subject_Backdated_Requests
        {
            get
            {
                if(_Email_Subject_Backdated_Requests == null)
                {
                    _Email_Subject_Backdated_Requests = ValidateExists_String("Email Subject Backdated Requests");
                }

                return _Email_Subject_Backdated_Requests;
            }
            set
            {
                _Email_Subject_Backdated_Requests = value;
            }
        }

        private static string _Email_Subject_ManualRequests =  null;
        public static string Email_Subject_ManualRequests
        {
            get
            {
                if(_Email_Subject_ManualRequests == null)
                {
                    _Email_Subject_ManualRequests = ValidateExists_String("Email Subject Manual Requests");
                }

                return _Email_Subject_ManualRequests;
            }
            set
            {
                _Email_Subject_ManualRequests = value;
            }
        }

        private static string _Email_Subject_PEReport =  null;
        public static string Email_Subject_PEReport
        {
            get
            {
                if(_Email_Subject_PEReport == null)
                {
                    _Email_Subject_PEReport = ValidateExists_String("Email Subject PEReport");
                }

                return _Email_Subject_PEReport;
            }
            set
            {
                _Email_Subject_PEReport = value;
            }
        }

        private static string _Email_To =  null;
        public static string Email_To
        {
            get
            {
                if(_Email_To == null)
                {
                    _Email_To = ValidateExists_String("Email To");
                }

                return _Email_To;
            }
            set
            {
                _Email_To = value;
            }
        }

        private static string _Enforce_Security =  null;
        public static string Enforce_Security
        {
            get
            {
                if(_Enforce_Security == null)
                {
                    _Enforce_Security = ValidateExists_String("Enforce Security");
                }

                return _Enforce_Security;
            }
            set
            {
                _Enforce_Security = value;
            }
        }

        private static Nullable<Boolean> _Generate_Automated_Reports = null;
        public static Boolean Generate_Automated_Reports
        {
            get
            {
                if(_Generate_Automated_Reports == null)
                {
                    _Generate_Automated_Reports = ValidateExists_Boolean("Generate Automated Reports");
                }

                return (bool)_Generate_Automated_Reports;
            }
            set
            {
                _Generate_Automated_Reports = value;
            }
        }

        private static Nullable<Boolean> _Get_New_Requests = null;
        public static Boolean Get_New_Requests
        {
            get
            {
                if(_Get_New_Requests == null)
                {
                    _Get_New_Requests = ValidateExists_Boolean("Get New Requests");
                }

                return (bool)_Get_New_Requests;
            }
            set
            {
                _Get_New_Requests = value;
            }
        }

        private static string _IntraDay_ReStartTime = null;
        public static string IntraDay_ReStartTime
        {
            get
            {
                if (_IntraDay_ReStartTime == null)
                {
                    _IntraDay_ReStartTime = ValidateExists_String("IntraDay ReStartTime");
                }

                return _IntraDay_ReStartTime;
            }
            set
            {
                _IntraDay_ReStartTime = value;
            }
        }

        private static string _IntraDay_StartTime = null;
        public static string IntraDay_StartTime
        {
            get
            {
                if (_IntraDay_StartTime == null)
                {
                    _IntraDay_StartTime = ValidateExists_String("IntraDay StartTime");
                }

                return _IntraDay_StartTime;
            }
            set
            {
                _IntraDay_StartTime = value;
            }
        }

        
        private static string _LKUP_FileName = null;
        public static string LKUP_FileName
        {
            get
            {
                if (_LKUP_FileName == null)
                {
                    _LKUP_FileName = ValidateExists_String("LKUP_FileName");
                }

                return _LKUP_FileName;
            }
            set
            {
                _LKUP_FileName = value;
            }
        }
        
        private static string _Manual_Requests_RestartTime =  null;
        public static string Manual_Requests_RestartTime
        {
            get
            {
                if(_Manual_Requests_RestartTime == null)
                {
                    _Manual_Requests_RestartTime = ValidateExists_String("Manual Requests RestartTime");
                }

                return _Manual_Requests_RestartTime;
            }
            set
            {
                _Manual_Requests_RestartTime = value;
            }
        }

        private static string _Manual_Requests_StartTime =  null;
        public static string Manual_Requests_StartTime
        {
            get
            {
                if(_Manual_Requests_StartTime == null)
                {
                    _Manual_Requests_StartTime = ValidateExists_String("Manual Requests StartTime");
                }

                return _Manual_Requests_StartTime;
            }
            set
            {
                _Manual_Requests_StartTime = value;
            }
        }

        private static string _PE_Debug =  null;
        public static string PE_Debug
        {
            get
            {
                if(_PE_Debug == null)
                {
                    _PE_Debug = ValidateExists_String("PE Debug");
                }

                return _PE_Debug;
            }
            set
            {
                _PE_Debug = value;
            }
        }

        private static Nullable<Boolean> _Process_Manual_Requests  = null;
        public static Boolean Process_Manual_Requests
        {
            get
            {
                if(_Process_Manual_Requests == null)
                {
                    _Process_Manual_Requests = ValidateExists_Boolean("Process Manual Requests");
                }

                return (bool)_Process_Manual_Requests;
            }
            set
            {
                _Process_Manual_Requests = value;
            }
        }

        private static string _ReportShare =  null;
        public static string ReportShare
        {
            get
            {
                if(_ReportShare == null)
                {
                    _ReportShare = ValidateExists_String("ReportShare");
                }

                return _ReportShare;
            }
            set
            {
                _ReportShare = value;
            }
        }

        private static Nullable<Boolean> _Route_Requests = null;
        public static Boolean Route_Requests
        {
            get
            {
                if(_Route_Requests == null)
                {
                    _Route_Requests = ValidateExists_Boolean("Route Requests");
                }

                return (bool)_Route_Requests;
            }
            set
            {
                _Route_Requests = value;
            }
        }

        private static Nullable<Int32> _ScreenHeight_Admin = null;
        public static Int32 ScreenHeight_Admin
        {
            get
            {
                if (_ScreenHeight_Admin == null)
                {
                    _ScreenHeight_Admin = ValidateExists_Int32("ScreenHeight_Admin");
                }

                return (Int32)_ScreenHeight_Admin;
            }
            set
            {
                _ScreenHeight_Admin = value;
            }
        }
        
        private static Nullable<Int32> _ScreenWidth_Admin = null;
        public static Int32 ScreenWidth_Admin
        {
            get
            {
                if (_ScreenWidth_Admin == null)
                {
                    _ScreenWidth_Admin = ValidateExists_Int32("ScreenWidth_Admin");
                }

                return (Int32)_ScreenWidth_Admin;
            }
            set
            {
                _ScreenWidth_Admin = value;
            }
        }

        private static Nullable<Int32> _ScreenHeight_Explore = null;
        public static Int32 ScreenHeight_Explore
        {
            get
            {
                if (_ScreenHeight_Explore == null)
                {
                    _ScreenHeight_Explore = ValidateExists_Int32("ScreenHeight_Explore");
                }

                return (Int32)_ScreenHeight_Explore;
            }
            set
            {
                _ScreenHeight_Explore = value;
            }
        }

        private static Nullable<Int32> _ScreenWidth_Explore = null;
        public static Int32 ScreenWidth_Explore
        {
            get
            {
                if (_ScreenWidth_Explore == null)
                {
                    _ScreenWidth_Explore = ValidateExists_Int32("ScreenWidth_Explore");
                }

                return (Int32)_ScreenWidth_Explore;
            }
            set
            {
                _ScreenWidth_Explore = value;
            }
        }
        
        private static Nullable<Int32> _ScreenWidth_SplashScreen = null;
        public static Int32 ScreenWidth_SplashScreen
        {
            get
            {
                if (_ScreenWidth_SplashScreen == null)
                {
                    _ScreenWidth_SplashScreen = ValidateExists_Int32("ScreenWidth_SplashScreen");
                }

                return (Int32)_ScreenWidth_SplashScreen;
            }
            set
            {
                _ScreenWidth_SplashScreen = value;
            }
        }
        
        private static Nullable<Int32> _ScreenHeight_SplashScreen = null;
        public static Int32 ScreenHeight_SplashScreen
        {
            get
            {
                if (_ScreenHeight_SplashScreen == null)
                {
                    _ScreenHeight_SplashScreen = ValidateExists_Int32("ScreenHeight_SplashScreen");
                }

                return (Int32)_ScreenHeight_SplashScreen;
            }
            set
            {
                _ScreenHeight_SplashScreen = value;
            }
        }
        
        private static Nullable<Boolean> _SendRequest_To_EAI =  null;
        public static Boolean SendRequest_To_EAI
        {
            get
            {
                if(_SendRequest_To_EAI == null)
                {
                    _SendRequest_To_EAI = ValidateExists_Boolean("SendRequest To EAI");
                }

                return (Boolean)_SendRequest_To_EAI;
            }
            set
            {
                _SendRequest_To_EAI = value;
            }
        }

        private static string _SMTP_Server =  null;
        public static string SMTP_Server
        {
            get
            {
                if(_SMTP_Server == null)
                {
                    _SMTP_Server = ValidateExists_String("SMTP Server");
                }

                return _SMTP_Server;
            }
            set
            {
                _SMTP_Server = value;
            }
        }

        private static Nullable<Int32> _SQLInformationAgent_ConnectionTimeout = null;
        public static Int32 SQLInformationAgent_ConnectionTimeout
        {
            get
            {
                if (_SQLInformationAgent_ConnectionTimeout == null)
                {
                    _SQLInformationAgent_ConnectionTimeout = ValidateExists_Int32("SQLInformationAgent ConnectionTimeout");
                }

                return (Int32)_SQLInformationAgent_ConnectionTimeout;
            }
            set
            {
                _SQLInformationAgent_ConnectionTimeout = value;
            }
        }

        private static Nullable<Boolean> _SQLInformationAgent_LoginSecure = null;
        public static Boolean SQLInformationAgent_LoginSecure
        {
            get
            {
                if (_SQLInformationAgent_LoginSecure == null)
                {
                    _SQLInformationAgent_LoginSecure = ValidateExists_Boolean("SQLInformationAgent LoginSecure");
                }

                return (bool)_SQLInformationAgent_LoginSecure;
            }
            set
            {
                _SQLInformationAgent_LoginSecure = value;
            }
        }

        private static string _SQLInformationAgent_Password = null;
        public static string SQLInformationAgent_Password
        {
            get
            {
                if (_SQLInformationAgent_Password == null)
                {
                    _SQLInformationAgent_Password = ValidateExists_String("SQLInformationAgent Password");
                }

                return _SQLInformationAgent_Password;
            }
            set
            {
                _SQLInformationAgent_Password = value;
            }
        }

        private static string _SQLInformationAgent_UserName = null;
        public static string SQLInformationAgent_UserName
        {
            get
            {
                if (_SQLInformationAgent_UserName == null)
                {
                    _SQLInformationAgent_UserName = ValidateExists_String("SQLInformationAgent UserName");
                }

                return _SQLInformationAgent_UserName;
            }
            set
            {
                _SQLInformationAgent_UserName = value;
            }
        }

        private static string _SQLMonitorDBConnection = null;
        public static string SQLMonitorDBConnection
        {
            get
            {
                if (_SQLMonitorDBConnection == null)
                {
                    _SQLMonitorDBConnection = ValidateExists_String("SQLMonitorDBConnection");
                }

                return _SQLMonitorDBConnection;
            }
            set
            {
                _SQLMonitorDBConnection = value;
            }
        }

        private static string _SS_Membership =  null;
        public static string SS_Membership
        {
            get
            {
                if(_SS_Membership == null)
                {
                    _SS_Membership = ValidateExists_String("SS Membership");
                }

                return _SS_Membership;
            }
            set
            {
                _SS_Membership = value;
            }
        }

        private static string _SuperSQLDBConnection =  null;
        public static string SuperSQLDBConnection
        {
            get
            {
                if(_SuperSQLDBConnection == null)
                {
                    _SuperSQLDBConnection = ValidateExists_String("SuperSQLDBConnection");
                }

                return _SuperSQLDBConnection;
            }
            set
            {
                _SuperSQLDBConnection = value;
            }
        }

        private static string _TempLocationPath =  null;
        public static string TempLocationPath
        {
            get
            {
                if(_TempLocationPath == null)
                {
                    _TempLocationPath = ValidateExists_String("Temp Location");
                }

                return _TempLocationPath;
            }
            set
            {
                _TempLocationPath = value;
            }
        }

        private static string _Weekly_ReStartTime = null;
        public static string Weekly_ReStartTime
        {
            get
            {
                if (_Weekly_ReStartTime == null)
                {
                    _Weekly_ReStartTime = ValidateExists_String("Weekly ReStartTime");
                }

                return _Weekly_ReStartTime;
            }
            set
            {
                _Weekly_ReStartTime = value;
            }
        }

        private static string _Weekly_StartTime = null;
        public static string Weekly_StartTime
        {
            get
            {
                if (_Weekly_StartTime == null)
                {
                    _Weekly_StartTime = ValidateExists_String("Weekly StartTime");
                }

                return _Weekly_StartTime;
            }
            set
            {
                _Weekly_StartTime = value;
            }
        }


        #region Validaters

        private static Boolean ValidateExists_Boolean(string configString)
        {
            string configValue = ConfigurationManager.AppSettings[configString];

            if(configValue == null)
            {
                throw new ApplicationException(string.Format("Missing Config File Information: {0}", configString));
            }

            return Convert.ToBoolean(configValue);
        }

        private static Nullable<DateTime> ValidateExists_DateTime(string configString)
        {
            string configValue = ConfigurationManager.AppSettings[configString];

            if(configValue == null)
            {
                return null;
            }

            try
            {
                PLLog.Info(string.Format("Using CurrentDate {0} from Config File", configValue), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                return Convert.ToDateTime(configValue);
            }
            catch(Exception ex)
            {
                PLLog.Error(string.Format("Cannot convert {0} to DateTime {1}", configString, ex.ToString()), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                return null;
            }
        }

        private static int ValidateExists_Int32(string configString)
        {
            string configValue = ConfigurationManager.AppSettings[configString];

            if(configValue == null)
            {
                throw new ApplicationException(string.Format("Missing Config File Information: {0}", configString));
            }

            return Convert.ToInt32(configValue);
        }

        private static string ValidateExists_String(string configString)
        {
            string configValue = ConfigurationManager.AppSettings[configString];

            if(configValue == null)
            {
                throw new ApplicationException(string.Format("Missing Config File Information: {0}", configString));
            }

            // TODO(crhodes): Decide if want to bother reporting that come Config item is empty.
            // Some settings, e.g. Email_CC can be empty.  Don't throw exception or catch it in the
            // particular items that can be empty.  For now just log to get a feel if this is important at all.
            // May just want to remove the next lines completely.

            if(configValue.Length <= 0)
            {
                PLLog.Info(string.Format("Empty Config File Information: {0}", configString), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
                //throw new ApplicationException(string.Format("Empty Config File Information: {0}", configString));
            }

            return configValue;
        }

        #endregion

        #region Helpers

        public static string GetAllConfigInfo()
        {
            StringBuilder info = new StringBuilder(4096);

            Type ty = typeof(SQLInformation.Data.Config);

            object[] methodArgs = new object[1];

            MethodInfo[] methods = ty.GetMethods();

            foreach(MethodInfo method in methods)
            {
                //Common.WriteToDebugWindow(string.Format("Name: {0} ReturnType: {1}", method.Name, method.ReturnType));

                if(method.Name.StartsWith("get"))
                {
                    string value;

                    switch(method.ReturnType.ToString())
                    {
                        case "System.Boolean":
                            try
                            {
                                value = ((Boolean)method.Invoke(ty, null)).ToString();
                            }
                            catch(Exception)
                            {
                                value = "<NULL>";
                            }
                            break;

                        case "System.String":
                            try
                            {
                                value = (string)method.Invoke(ty, null);
                            }
                            catch(Exception)
                            {
                                value = "<NULL>";
                            }

                            break;

                        case "System.Int32":
                            try
                            {
                                value = ((Int32)method.Invoke(ty, null)).ToString();
                            }
                            catch(Exception)
                            {
                                value = "<NULL>";
                            }
                            break;

                        default:
                            value = "Unknown Return Type:" + method.ReturnType.ToString();
                            break;
                    }

                    // Display get_X() as ReturnType Config.X >Value<
                    info.AppendLine(string.Format("{0,16} {1,50} >{2}<",
                        method.ReturnType, "Config." + method.Name.Substring(4), value));
                }
            }

            return info.ToString();
        }

        public static bool IsValid_ConfigInformation()
        {
            bool result = true;

            Type ty = typeof(SQLInformation.Data.Config);
            object[] methodArgs = new object[1];

            //PropertyInfo[] properties = ty.GetProperties();

            //foreach(PropertyInfo property in properties)
            //{
            //    System.Diagnostics.Debug.WriteLine(property.Name);
            //    System.Diagnostics.Debug.WriteLine(property.PropertyType.ToString());
            //}

            // Read every property out of the Config class to ensure everything needed is available.
            // It is the responsibility of the properties to validate themselves
            // and throw exceptions if not valid/available.

            MethodInfo[] methods = ty.GetMethods();

            foreach(MethodInfo method in methods)
            {
                //Common.WriteToDebugWindow(string.Format("Name: {0} ReturnType: {1}", method.Name, method.ReturnType));

                if(method.Name.StartsWith("get"))
                {
                    string value = "Unknown Return Type";

                    switch(method.ReturnType.ToString())
                    {
                        case "System.Boolean":
                            try
                            {
                                value = ((Boolean)method.Invoke(ty, null)).ToString();
                            }
                            catch(Exception)
                            {
                                PLLog.Error(string.Format("Invalid Config File Information: {0}", method.Name), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                                result = false;
                            }

                            break;

                        case "System.String":
                            try
                            {
                                value = (string)method.Invoke(ty, null);
                                if(value == null) value = "<NULL>";
                            }
                            catch(Exception)
                            {
                                PLLog.Error(string.Format("Invalid Config File Information: {0}", method.Name), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                                result = false;
                            }

                            break;

                        case "System.Int32":
                            try
                            {
                                value = ((Int32)method.Invoke(ty, null)).ToString();
                            }
                            catch(Exception)
                            {
                                PLLog.Error(string.Format("Invalid Config File Information: {0}", method.Name), PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                                result = false;
                            }

                            break;
                    }

                    //Common.WriteToDebugWindow(string.Format("Name:{0}  ReturnType:{1} Value:>{2}<", 
                    //    method.Name, method.ReturnType, value));                   
                }
            }

            return result;
        }

        #endregion

    }
}
