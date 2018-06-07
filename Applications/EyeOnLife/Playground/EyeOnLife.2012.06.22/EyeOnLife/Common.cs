using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using SQLInformation.User_Interface;
//using DebugHelp;

namespace SQLInformation
{
    ///<summary>
    ///Common items declared at the Class level.
    ///</summary>
    ///<remarks>
    ///Use this class for any thing you want globally available.
    ///Place only Static items in this class.  This Class cannot not be instantiated.
    ///</remarks>    
    public static class Common
    {
        const string TYPE_NAME = "Common";

        public static void Initialize()
        {
//#if TRACE
//            Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
//#endif
            //SwapScreenBaseControl = null;
            //InitializedUserControls = null;

            // TODO: Add code to (re)Initialize anything that needs to start clear
            // when ucBase.Reload() is called.

        }

        #region Core

        // TODO: Add as many DebugLevels as needed.
        // Add accompanying checkboxes on frmDebugWindow

        public static bool DebugLevel1
        {
            get;
            set;
        }
        public static bool DebugLevel2
        {
            get;
            set;
        }
        public static bool DebugSQL
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether the UI is running in DeveloperMode
        /// </summary>
        public static bool DeveloperMode
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether the UI is running in DebugMode
        /// </summary>
        public static bool DebugMode
        {
            get;
            set;
        }

        //private static OutputWindow _outputWindow;

        //public static OutputWindow OutputWindow
        //{
        //    get
        //    {
        //        if(_outputWindow == null)
        //        {
        //            _outputWindow = new OutputWindow();
        //        }
        //        return _outputWindow;
        //    }
        //    set
        //    {
        //        _outputWindow = value;
        //    }
        //}

        //public static long WriteToDebugWindow(string message)
        //{
        //    return OutputWindow.Write(message);
        //}

        //public static double WriteToDebugWindow(string message, double startTime)
        //{
        //    return OutputWindow.Write(message, startTime);
        //}

        #endregion

        private static Data.ApplicationDataSet _ApplicationDataSet;
        public static Data.ApplicationDataSet ApplicationDataSet
        {
            get
            {
                if (_ApplicationDataSet == null)
                {
                    _ApplicationDataSet = new Data.ApplicationDataSet();

                    // TODO: Add any other initialization of things related to the ApplicationDS
                }

                return _ApplicationDataSet;
            }
            set
            {
                _ApplicationDataSet = value;
            }
        }
    }
}
