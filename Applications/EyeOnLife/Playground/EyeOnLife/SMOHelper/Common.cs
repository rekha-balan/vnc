using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DebugHelp;

namespace SMOHelper
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
        const string CONTROL_NAME = "Common";

        public static void Initialize()
        {
#if TRACE_BASE
            Common.WriteToDebugWindow(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
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

        private static OutputWindow _outputWindow;

        public static OutputWindow OutputWindow
        {
            get
            {
                if(_outputWindow == null)
                {
                    _outputWindow = new OutputWindow();
                }
                return _outputWindow;
            }
            set
            {
                _outputWindow = value;
            }
        }

        public static long WriteToDebugWindow(string message)
        {
            return OutputWindow.Write(message);
        }

        public static long WriteToDebugWindow(string message, long startTime)
        {
            return OutputWindow.Write(message, startTime);
        }

        #endregion

    }
}
