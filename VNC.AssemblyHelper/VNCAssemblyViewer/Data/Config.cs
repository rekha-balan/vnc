using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

using System.Configuration;
using System.Reflection;
using System.Text;

//using SQLInformation;
using VNC;

namespace VNCAssemblyViewer.Data
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
        //private static string _PropertyName;
        private static int CLASS_BASE_ERRORNUMBER = VNCAssemblyViewer.ErrorNumbers.VNCAssemblyViewer;
        private const string LOG_APPNAME = "SQLINFOAGENT";

        // Please keep these in alphabetical order
        //
        // Please keep the Property names in sync with the setting string.
        // Spaces get replaced with underscores.  Case matters.
        // e.g. "AD Path" --> AD_Path and _AD_Path.

        private static string _AD_Domain = null;
        public static string AD_Domain
        {
            get
            {
                if (_AD_Domain == null)
                {
                    _AD_Domain = ValidateExists_String("AD_Domain");
                }

                return _AD_Domain;
            }
            set
            {
                _AD_Domain = value;
            }
        }

        private static string _AD_Path = null;
        public static string AD_Path
        {
            get
            {
                if (_AD_Path == null)
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
                if (_AD_Server == null)
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

        private static Nullable<Boolean> _AD_Users_AllowAll = null;
        public static Boolean AD_Users_AllowAll
        {
            get
            {
                if (_AD_Users_AllowAll == null)
                {
                    _AD_Users_AllowAll = ValidateExists_Boolean("AD_Users_AllowAll");
                }

                return (bool)_AD_Users_AllowAll;
            }
            set
            {
                _AD_Users_AllowAll = value;
            }
        }

        private static string _ADGroup_Administrators = null;
        public static string ADGroup_Administrators
        {
            get
            {
                if (_ADGroup_Administrators == null)
                {
                    _ADGroup_Administrators = ValidateExists_String("ADGroup_Administrators");
                }

                return _ADGroup_Administrators;
            }
            set
            {
                _ADGroup_Administrators = value;
            }
        }

        private static string _ADGroup_BetaUsers = null;
        public static string ADGroup_BetaUsers
        {
            get
            {
                if (_ADGroup_BetaUsers == null)
                {
                    _ADGroup_BetaUsers = ValidateExists_String("ADGroup_BetaUsers");
                }

                return _ADGroup_BetaUsers;
            }
            set
            {
                _ADGroup_BetaUsers = value;
            }
        }

        private static string _ADGroup_Users = null;
        public static string ADGroup_Users
        {
            get
            {
                if (_ADGroup_Users == null)
                {
                    _ADGroup_Users = ValidateExists_String("ADGroup_Users");
                }

                return _ADGroup_Users;
            }
            set
            {
                _ADGroup_Users = value;
            }
        }

        private static Nullable<Boolean> _ADBypass = null;
        public static Boolean ADBypass
        {
            get
            {
                if (_ADBypass == null)
                {
                    //_ADBypass = ValidateExists_Boolean("ADBypass");

                    // Special case this so we don't throw an exception.
                    // No need to broadcast this capability.

                    string configValue = ConfigurationManager.AppSettings["ADBypass"];

                    if (configValue == null)
                    {
                        return false;
                        //throw new ApplicationException(string.Format("Missing Config File Information: {0}", configString));
                    }

                    _ADBypass = Convert.ToBoolean(configValue);
                }

                return (bool)_ADBypass;
            }
            set
            {
                _ADBypass = value;
            }
        }

        private static string _Admin_Tools_Redirect = null;
        public static string Admin_Tools_Redirect
        {
            get
            {
                if (_Admin_Tools_Redirect == null)
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


        // This is a special case where we want the backing field initialized from the config file.
        // ValidateExists_DateTime returns null if cannot find or parse a "CurrentDate" value from config.
        private static Nullable<DateTime> _CurrentDate = ValidateExists_DateTime("CurrentDate");
        public static DateTime CurrentDate
        {
            get
            {
                if (_CurrentDate == null)
                {
                    return DateTime.Today;
                }
                else
                {
                    VNC.AppLog.Trace5(string.Format("Current Date: {0}", _CurrentDate.ToString()), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                    return (DateTime)_CurrentDate;
                }
            }

            set
            {
                try
                {
                    _CurrentDate = value;
                    VNC.AppLog.Info(string.Format("Setting CurrentDate to {0}", _CurrentDate.ToString()), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                }
                catch (Exception ex)
                {
                    VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                    throw ex;
                }
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

                SaveConfigSetting(value.ToString(), "DefaultUITheme");
            }
        }

        private static Nullable<Int32> _DefaultUserMode = null;
        public static Int32 DefaultUserMode
        {
            get
            {
                if (_DefaultUserMode == null)
                {
                    _DefaultUserMode = ValidateExists_Int32("DefaultUserMode");
                }

                return (Int32)_DefaultUserMode;
            }
            set
            {
                _DefaultUserMode = value;

                SaveConfigSetting(value.ToString(), "DefaultUserMode");
            }
        }

        private static Nullable<Int32> _DefaultViewMode = null;
        public static Int32 DefaultViewMode
        {
            get
            {
                if (_DefaultViewMode == null)
                {
                    _DefaultViewMode = ValidateExists_Int32("DefaultViewMode");
                }

                return (Int32)_DefaultViewMode;
            }
            set
            {
                _DefaultViewMode = value;

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppSettingsSection app = config.AppSettings;
                app.Settings.Remove("DefaultViewMode");
                app.Settings.Add("DefaultViewMode", value.ToString());
                config.Save(ConfigurationSaveMode.Modified);
            }
        }

        private static string _Email_CC = null;
        public static string Email_CC
        {
            get
            {
                if (_Email_CC == null)
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

        private static string _Email_From = null;
        public static string Email_From
        {
            get
            {
                if (_Email_From == null)
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

        private static string _Email_To = null;
        public static string Email_To
        {
            get
            {
                if (_Email_To == null)
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

        private static string _Enforce_Security = null;
        public static string Enforce_Security
        {
            get
            {
                if (_Enforce_Security == null)
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

        //private static Nullable<Boolean> _Generate_Automated_Reports = null;
        //public static Boolean Generate_Automated_Reports
        //{
        //    get
        //    {
        //        if(_Generate_Automated_Reports == null)
        //        {
        //            _Generate_Automated_Reports = ValidateExists_Boolean("Generate Automated Reports");
        //        }

        //        return (bool)_Generate_Automated_Reports;
        //    }
        //    set
        //    {
        //        _Generate_Automated_Reports = value;
        //    }
        //}

        //private static Nullable<Boolean> _Get_New_Requests = null;
        //public static Boolean Get_New_Requests
        //{
        //    get
        //    {
        //        if(_Get_New_Requests == null)
        //        {
        //            _Get_New_Requests = ValidateExists_Boolean("Get New Requests");
        //        }

        //        return (bool)_Get_New_Requests;
        //    }
        //    set
        //    {
        //        _Get_New_Requests = value;
        //    }
        //}

        //private static string _LKUP_FileName = null;
        //public static string LKUP_FileName
        //{
        //    get
        //    {
        //        if (_LKUP_FileName == null)
        //        {
        //            _LKUP_FileName = ValidateExists_String("LKUP_FileName");
        //        }

        //        return _LKUP_FileName;
        //    }
        //    set
        //    {
        //        _LKUP_FileName = value;
        //    }
        //}

        //private static string _Manual_Requests_RestartTime =  null;
        //public static string Manual_Requests_RestartTime
        //{
        //    get
        //    {
        //        if(_Manual_Requests_RestartTime == null)
        //        {
        //            _Manual_Requests_RestartTime = ValidateExists_String("Manual Requests RestartTime");
        //        }

        //        return _Manual_Requests_RestartTime;
        //    }
        //    set
        //    {
        //        _Manual_Requests_RestartTime = value;
        //    }
        //}

        //private static string _Manual_Requests_StartTime =  null;
        //public static string Manual_Requests_StartTime
        //{
        //    get
        //    {
        //        if(_Manual_Requests_StartTime == null)
        //        {
        //            _Manual_Requests_StartTime = ValidateExists_String("Manual Requests StartTime");
        //        }

        //        return _Manual_Requests_StartTime;
        //    }
        //    set
        //    {
        //        _Manual_Requests_StartTime = value;
        //    }
        //}

        //private static string _PE_Debug =  null;
        //public static string PE_Debug
        //{
        //    get
        //    {
        //        if(_PE_Debug == null)
        //        {
        //            _PE_Debug = ValidateExists_String("PE Debug");
        //        }

        //        return _PE_Debug;
        //    }
        //    set
        //    {
        //        _PE_Debug = value;
        //    }
        //}

        //private static Nullable<Boolean> _Process_Manual_Requests  = null;
        //public static Boolean Process_Manual_Requests
        //{
        //    get
        //    {
        //        if(_Process_Manual_Requests == null)
        //        {
        //            _Process_Manual_Requests = ValidateExists_Boolean("Process Manual Requests");
        //        }

        //        return (bool)_Process_Manual_Requests;
        //    }
        //    set
        //    {
        //        _Process_Manual_Requests = value;
        //    }
        //}

        //private static string _ReportShare =  null;
        //public static string ReportShare
        //{
        //    get
        //    {
        //        if(_ReportShare == null)
        //        {
        //            _ReportShare = ValidateExists_String("ReportShare");
        //        }

        //        return _ReportShare;
        //    }
        //    set
        //    {
        //        _ReportShare = value;
        //    }
        //}

        //private static Nullable<Boolean> _Route_Requests = null;
        //public static Boolean Route_Requests
        //{
        //    get
        //    {
        //        if(_Route_Requests == null)
        //        {
        //            _Route_Requests = ValidateExists_Boolean("Route Requests");
        //        }

        //        return (bool)_Route_Requests;
        //    }
        //    set
        //    {
        //        _Route_Requests = value;
        //    }
        //}

        //private static Nullable<Int32> _ScreenHeight_Admin = null;
        //public static Int32 ScreenHeight_Admin
        //{
        //    get
        //    {
        //        if (_ScreenHeight_Admin == null)
        //        {
        //            _ScreenHeight_Admin = ValidateExists_Int32("ScreenHeight_Admin");
        //        }

        //        return (Int32)_ScreenHeight_Admin;
        //    }
        //    set
        //    {
        //        _ScreenHeight_Admin = value;
        //    }
        //}

        //private static Nullable<Int32> _ScreenWidth_Admin = null;
        //public static Int32 ScreenWidth_Admin
        //{
        //    get
        //    {
        //        if (_ScreenWidth_Admin == null)
        //        {
        //            _ScreenWidth_Admin = ValidateExists_Int32("ScreenWidth_Admin");
        //        }

        //        return (Int32)_ScreenWidth_Admin;
        //    }
        //    set
        //    {
        //        _ScreenWidth_Admin = value;
        //    }
        //}

        //private static Nullable<Int32> _ScreenHeight_Explore = null;
        //public static Int32 ScreenHeight_Explore
        //{
        //    get
        //    {
        //        if (_ScreenHeight_Explore == null)
        //        {
        //            _ScreenHeight_Explore = ValidateExists_Int32("ScreenHeight_Explore");
        //        }

        //        return (Int32)_ScreenHeight_Explore;
        //    }
        //    set
        //    {
        //        _ScreenHeight_Explore = value;
        //    }
        //}

        //private static Nullable<Int32> _ScreenWidth_Explore = null;
        //public static Int32 ScreenWidth_Explore
        //{
        //    get
        //    {
        //        if (_ScreenWidth_Explore == null)
        //        {
        //            _ScreenWidth_Explore = ValidateExists_Int32("ScreenWidth_Explore");
        //        }

        //        return (Int32)_ScreenWidth_Explore;
        //    }
        //    set
        //    {
        //        _ScreenWidth_Explore = value;
        //    }
        //}

        //private static Nullable<Int32> _ScreenWidth_SplashScreen = null;
        //public static Int32 ScreenWidth_SplashScreen
        //{
        //    get
        //    {
        //        if (_ScreenWidth_SplashScreen == null)
        //        {
        //            _ScreenWidth_SplashScreen = ValidateExists_Int32("ScreenWidth_SplashScreen");
        //        }

        //        return (Int32)_ScreenWidth_SplashScreen;
        //    }
        //    set
        //    {
        //        _ScreenWidth_SplashScreen = value;
        //    }
        //}

        //private static Nullable<Int32> _ScreenHeight_SplashScreen = null;
        //public static Int32 ScreenHeight_SplashScreen
        //{
        //    get
        //    {
        //        if (_ScreenHeight_SplashScreen == null)
        //        {
        //            _ScreenHeight_SplashScreen = ValidateExists_Int32("ScreenHeight_SplashScreen");
        //        }

        //        return (Int32)_ScreenHeight_SplashScreen;
        //    }
        //    set
        //    {
        //        _ScreenHeight_SplashScreen = value;
        //    }
        //}

        private static string _RowDetailMode = null;
        public static string RowDetailMode
        {
            get
            {
                if (_RowDetailMode == null)
                {
                    _RowDetailMode = ValidateExists_String("DefaultRowDetailMode");
                }

                return _RowDetailMode;
            }
            set
            {
                _RowDetailMode = value;

                SaveConfigSetting(value, "DefaultRowDetailMode");
            }
        }

        //private static Nullable<Boolean> _SendRequest_To_EAI =  null;
        //public static Boolean SendRequest_To_EAI
        //{
        //    get
        //    {
        //        if(_SendRequest_To_EAI == null)
        //        {
        //            _SendRequest_To_EAI = ValidateExists_Boolean("SendRequest To EAI");
        //        }

        //        return (Boolean)_SendRequest_To_EAI;
        //    }
        //    set
        //    {
        //        _SendRequest_To_EAI = value;
        //    }
        //}

        private static string _SMTP_Server = null;
        public static string SMTP_Server
        {
            get
            {
                if (_SMTP_Server == null)
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

        //private static Nullable<Int32> _SQLInformationAgent_ConnectionTimeout = null;
        //public static Int32 SQLInformationAgent_ConnectionTimeout
        //{
        //    get
        //    {
        //        if (_SQLInformationAgent_ConnectionTimeout == null)
        //        {
        //            _SQLInformationAgent_ConnectionTimeout = ValidateExists_Int32("SQLInformationAgent ConnectionTimeout");
        //        }

        //        return (Int32)_SQLInformationAgent_ConnectionTimeout;
        //    }
        //    set
        //    {
        //        _SQLInformationAgent_ConnectionTimeout = value;
        //    }
        //}

        //private static string _SQLInformationAgent_DatabaseName = null;
        //public static string SQLInformationAgent_DatabaseName
        //{
        //    get
        //    {
        //        if (_SQLInformationAgent_DatabaseName == null)
        //        {
        //            _SQLInformationAgent_DatabaseName = ValidateExists_String("SQLInformationAgent_DatabaseName");
        //        }

        //        return _SQLInformationAgent_DatabaseName;
        //    }
        //    set
        //    {
        //        _SQLInformationAgent_DatabaseName = value;
        //    }
        //}

        //private static string _SQLInformationAgent_LoginName = null;
        //public static string SQLInformationAgent_LoginName
        //{
        //    get
        //    {
        //        if (_SQLInformationAgent_LoginName == null)
        //        {
        //            _SQLInformationAgent_LoginName = ValidateExists_String("SQLInformationAgent_LoginName");
        //        }

        //        return _SQLInformationAgent_LoginName;
        //    }
        //    set
        //    {
        //        _SQLInformationAgent_LoginName = value;
        //    }
        //}

        //private static Nullable<Boolean> _SQLInformationAgent_LoginSecure = null;
        //public static Boolean SQLInformationAgent_LoginSecure
        //{
        //    get
        //    {
        //        if (_SQLInformationAgent_LoginSecure == null)
        //        {
        //            _SQLInformationAgent_LoginSecure = ValidateExists_Boolean("SQLInformationAgent LoginSecure");
        //        }

        //        return (bool)_SQLInformationAgent_LoginSecure;
        //    }
        //    set
        //    {
        //        _SQLInformationAgent_LoginSecure = value;
        //    }
        //}

        //private static string _SQLInformationAgent_NTLoginName = null;
        //public static string SQLInformationAgent_NTLoginName
        //{
        //    get
        //    {
        //        if (_SQLInformationAgent_NTLoginName == null)
        //        {
        //            _SQLInformationAgent_NTLoginName = ValidateExists_String("SQLInformationAgent_NTLoginName");
        //        }

        //        return _SQLInformationAgent_NTLoginName;
        //    }
        //    set
        //    {
        //        _SQLInformationAgent_NTLoginName = value;
        //    }
        //}

        //private static string _SQLInformationAgent_Password = null;
        //public static string SQLInformationAgent_Password
        //{
        //    get
        //    {
        //        if (_SQLInformationAgent_Password == null)
        //        {
        //            _SQLInformationAgent_Password = ValidateExists_String("SQLInformationAgent_Password");
        //        }

        //        return _SQLInformationAgent_Password;
        //    }
        //    set
        //    {
        //        _SQLInformationAgent_Password = value;
        //    }
        //}

        //private static string _SQLInformationAgent_ServerRole = null;
        //public static string SQLInformationAgent_ServerRole
        //{
        //    get
        //    {
        //        if (_SQLInformationAgent_ServerRole == null)
        //        {
        //            _SQLInformationAgent_ServerRole = ValidateExists_String("SQLInformationAgent_ServerRole");
        //        }

        //        return _SQLInformationAgent_ServerRole;
        //    }
        //    set
        //    {
        //        _SQLInformationAgent_ServerRole = value;
        //    }
        //}

        //private static string _SQLMonitorDBConnection = null;
        //public static string SQLMonitorDBConnection
        //{
        //    get
        //    {
        //        if (_SQLMonitorDBConnection == null)
        //        {
        //            _SQLMonitorDBConnection = ValidateExists_String("SQLMonitorDBConnection");
        //        }

        //        return _SQLMonitorDBConnection;
        //    }
        //    set
        //    {
        //        _SQLMonitorDBConnection = value;
        //    }
        //}

        //private static string _SS_Membership =  null;
        //public static string SS_Membership
        //{
        //    get
        //    {
        //        if(_SS_Membership == null)
        //        {
        //            _SS_Membership = ValidateExists_String("SS Membership");
        //        }

        //        return _SS_Membership;
        //    }
        //    set
        //    {
        //        _SS_Membership = value;
        //    }
        //}

        //private static string _SuperSQLDBConnection =  null;
        //public static string SuperSQLDBConnection
        //{
        //    get
        //    {
        //        if(_SuperSQLDBConnection == null)
        //        {
        //            _SuperSQLDBConnection = ValidateExists_String("SuperSQLDBConnection");
        //        }

        //        return _SuperSQLDBConnection;
        //    }
        //    set
        //    {
        //        _SuperSQLDBConnection = value;
        //    }
        //}

        private static string _TempLocationPath = null;
        public static string TempLocationPath
        {
            get
            {
                if (_TempLocationPath == null)
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

        #region Validaters

        private static Boolean ValidateExists_Boolean(string configString)
        {
            string configValue = ConfigurationManager.AppSettings[configString];

            if (configValue == null)
            {
                throw new ApplicationException(string.Format("Missing Config File Information: {0}", configString));
            }

            return Convert.ToBoolean(configValue);
        }

        private static Nullable<DateTime> ValidateExists_DateTime(string configString)
        {
            string configValue = ConfigurationManager.AppSettings[configString];

            if (configValue == null)
            {
                return null;
            }

            try
            {
                VNC.AppLog.Info(string.Format("Using CurrentDate {0} from Config File", configValue), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                return Convert.ToDateTime(configValue);
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(string.Format("Cannot convert {0} to DateTime {1}", configString, ex.ToString()), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
                return null;
            }
        }

        private static int ValidateExists_Int32(string configString)
        {
            string configValue = ConfigurationManager.AppSettings[configString];

            if (configValue == null)
            {
                throw new ApplicationException(string.Format("Missing Config File Information: {0}", configString));
            }

            return Convert.ToInt32(configValue);
        }

        private static string ValidateExists_String(string configString)
        {
            string configValue = ConfigurationManager.AppSettings[configString];

            if (configValue == null)
            {
                throw new ApplicationException(string.Format("Missing Config File Information: {0}", configString));
            }

            // TODO(crhodes): Decide if want to bother reporting that come Config item is empty.
            // Some settings, e.g. Email_CC can be empty.  Don't throw exception or catch it in the
            // particular items that can be empty.  For now just log to get a feel if this is important at all.
            // May just want to remove the next lines completely.

            if (configValue.Length <= 0)
            {
                VNC.AppLog.Info(string.Format("Empty Config File Information: {0}", configString), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 5);
                //throw new ApplicationException(string.Format("Empty Config File Information: {0}", configString));
            }

            return configValue;
        }

        #endregion

        #region Helpers

        public static string GetAllConfigInfo()
        {
            StringBuilder info = new StringBuilder(4096);

            Type ty = typeof(VNCAssemblyViewer.Data.Config);

            object[] methodArgs = new object[1];

            MethodInfo[] methods = ty.GetMethods();

            foreach (MethodInfo method in methods)
            {
                //Common.WriteToDebugWindow(string.Format("Name: {0} ReturnType: {1}", method.Name, method.ReturnType));

                if (method.Name.StartsWith("get"))
                {
                    string value;

                    switch (method.ReturnType.ToString())
                    {
                        case "System.Boolean":
                            try
                            {
                                value = ((Boolean)method.Invoke(ty, null)).ToString();
                            }
                            catch (Exception)
                            {
                                value = "<NULL>";
                            }
                            break;

                        case "System.String":
                            try
                            {
                                value = (string)method.Invoke(ty, null);
                            }
                            catch (Exception)
                            {
                                value = "<NULL>";
                            }

                            break;

                        case "System.Int32":
                            try
                            {
                                value = ((Int32)method.Invoke(ty, null)).ToString();
                            }
                            catch (Exception)
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

            Type ty = typeof(VNCAssemblyViewer.Data.Config);
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

            foreach (MethodInfo method in methods)
            {
                //Common.WriteToDebugWindow(string.Format("Name: {0} ReturnType: {1}", method.Name, method.ReturnType));

                if (method.Name.StartsWith("get"))
                {
                    string value = "Unknown Return Type";

                    switch (method.ReturnType.ToString())
                    {
                        case "System.Boolean":
                            try
                            {
                                value = ((Boolean)method.Invoke(ty, null)).ToString();
                            }
                            catch (Exception)
                            {
                                VNC.AppLog.Error(string.Format("Invalid Config File Information: {0}", method.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                                result = false;
                            }

                            break;

                        case "System.String":
                            try
                            {
                                value = (string)method.Invoke(ty, null);
                                if (value == null) value = "<NULL>";
                            }
                            catch (Exception)
                            {
                                VNC.AppLog.Error(string.Format("Invalid Config File Information: {0}", method.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
                                result = false;
                            }

                            break;

                        case "System.Int32":
                            try
                            {
                                value = ((Int32)method.Invoke(ty, null)).ToString();
                            }
                            catch (Exception)
                            {
                                VNC.AppLog.Error(string.Format("Invalid Config File Information: {0}", method.Name), LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 1);
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

        private static void SaveConfigSetting(string value, string settingName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection app = config.AppSettings;
            app.Settings.Remove(settingName);
            app.Settings.Add(settingName, value.ToString());
            config.Save(ConfigurationSaveMode.Modified);
        }

        #endregion

    }
}
