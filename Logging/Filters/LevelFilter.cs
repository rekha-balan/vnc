using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using VNC.Logging.Configuration;

namespace VNC.Logging.Filters
{
    [ConfigurationElementType(typeof(LevelFilterData))]
    public class LevelFilter : Microsoft.Practices.EnterpriseLibrary.Logging.Filters.LogFilter
    {
        #region Fields

        private bool debugLevel = false;
        private bool debug1Level = false;
        private bool debug2Level = false;
        private bool debug3Level = false;
        private bool debug4Level = false;

        private bool traceLevel = false;
        private bool trace1Level = false;
        private bool trace2Level = false;
        private bool trace3Level = false;
        private bool trace4Level = false;
        private bool trace5Level = false;
        private bool trace6Level = false;
        private bool trace7Level = false;
        private bool trace8Level = false;
        private bool trace9Level = false;

        private bool trace10Level = false;
        private bool trace11Level = false;
        private bool trace12Level = false;
        private bool trace13Level = false;
        private bool trace14Level = false;
        private bool trace15Level = false;
        private bool trace16Level = false;
        private bool trace17Level = false;
        private bool trace18Level = false;
        private bool trace19Level = false;

        private bool trace20Level = false;
        private bool trace21Level = false;
        private bool trace22Level = false;
        private bool trace23Level = false;
        private bool trace24Level = false;
        private bool trace25Level = false;
        private bool trace26Level = false;
        private bool trace27Level = false;
        private bool trace28Level = false;
        private bool trace29Level = false;

        private bool trace30Level = false;

        #endregion

        #region Support Custom Attributes

        string[] _supportedCustomAttributes = new string[] {
            "debugLevel",  "DebugLevel",  "debuglevel",
            "debug1Level", "Debug1Level", "debug1level",
            "debug2level", "Debug2level", "debug2level",
            "debug3level", "Debug3level", "debug3level",
            "debug4level", "Debug4level", "debug4level",
             
            "traceLevel",  "TraceLevel",  "tracelevel",
            "trace1Level", "Trace1Level", "trace1level",
            "trace2level", "Trace2level", "trace2level",
            "trace3level", "Trace3level", "trace3level",
            "trace4level", "Trace4level", "trace4level",
            "trace5level", "Trace5level", "trace5level",
            "trace6level", "Trace6level", "trace6level",
            "trace7level", "Trace7level", "trace7level",
            "trace8level", "Trace8level", "trace8level",
            "trace9level", "Trace9level", "trace9level",

            "trace10Level", "Trace10Level", "trace10level",
            "trace11Level", "Trace11Level", "trace11level",
            "trace12level", "Trace12level", "trace12level",
            "trace13level", "Trace13level", "trace13level",
            "trace14level", "Trace14level", "trace14level",
            "trace15level", "Trace15level", "trace15level",
            "trace16level", "Trace16level", "trace16level",
            "trace17level", "Trace17level", "trace17level",
            "trace18level", "Trace18level", "trace18level",
            "trace19level", "Trace19level", "trace19level",

            "trace20Level", "Trace20Level", "trace20level",
            "trace21Level", "Trace21Level", "trace21level",
            "trace22level", "Trace22level", "trace22level",
            "trace23level", "Trace23level", "trace23level",
            "trace24level", "Trace24level", "trace24level",
            "trace25level", "Trace25level", "trace25level",
            "trace26level", "Trace26level", "trace26level",
            "trace27level", "Trace27level", "trace27level",
            "trace28level", "Trace28level", "trace28level",
            "trace29level", "Trace29level", "trace29level",

            "trace30Level", "Trace30Level", "trace30level"
        };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="LogFilter"/>.
        /// </summary>
        /// <param name="name">The name for the log filter.</param>
        public LevelFilter(string name) : base(name)
        {
        }

        // TODO(crhodes)
        // How is this used.  Do we need it.

        //public LevelFilter(NameValueCollection nvPairs) : this("LevelFilter", 
        //    true, true, true, true, true, 
        //    true, true, true, true, true,
        //    true, true, true, true, true)
        //{

        //}

        //public LevelFilter(NameValueCollection nvPairs) : this("LevelFilter",
        //    true, true, true, true, true,
        //    true, true, true, true, true,
        //    true, true, true, true, true)
        //{

        //}

        public LevelFilter(string name, 
            bool debugLevel, bool debug1Level, bool debug2Level, bool debug3Level, bool debug4Level,
            bool traceLevel, bool trace1Level, bool trace2Level, bool trace3Level, bool trace4Level,
            bool trace5Level, bool trace6Level, bool trace7Level, bool trace8Level, bool trace9Level,
            bool trace10Level, bool trace11Level, bool trace12Level, bool trace13Level, bool trace14Level,
            bool trace15Level, bool trace16Level, bool trace17Level, bool trace18Level, bool trace19Level,
            bool trace20Level, bool trace21Level, bool trace22Level, bool trace23Level, bool trace24Level,
            bool trace25Level, bool trace26Level, bool trace27Level, bool trace28Level, bool trace29Level,
            bool trace30Level) : base(name)
        {
            this.debugLevel = debugLevel;
            this.debug1Level = debug1Level;
            this.debug2Level = debug2Level;
            this.debug3Level = debug3Level;
            this.debug4Level = debug4Level;

            this.traceLevel = traceLevel;
            this.trace1Level = trace1Level;
            this.trace2Level = trace2Level;
            this.trace3Level = trace3Level;
            this.trace4Level = trace4Level;
            this.trace5Level = trace5Level;
            this.trace6Level = trace6Level;
            this.trace7Level = trace7Level;
            this.trace8Level = trace8Level;
            this.trace9Level = trace9Level;

            this.trace10Level = trace10Level;
            this.trace11Level = trace11Level;
            this.trace12Level = trace12Level;
            this.trace13Level = trace13Level;
            this.trace14Level = trace14Level;
            this.trace15Level = trace15Level;
            this.trace16Level = trace16Level;
            this.trace17Level = trace17Level;
            this.trace18Level = trace18Level;
            this.trace19Level = trace19Level;

            this.trace20Level = trace20Level;
            this.trace21Level = trace21Level;
            this.trace22Level = trace22Level;
            this.trace23Level = trace23Level;
            this.trace24Level = trace24Level;
            this.trace25Level = trace25Level;
            this.trace26Level = trace26Level;
            this.trace27Level = trace27Level;
            this.trace28Level = trace28Level;
            this.trace29Level = trace29Level;

            this.trace30Level = trace30Level;

        }

        #endregion

        /// <summary>
        /// Test to see if a message meets the criteria to be processed. 
        /// </summary>
        /// <param name="log">Log entry to test.</param>
        /// <returns><b>true</b> if the message passes through the filter and should be distributed, <b>false</b> otherwise.</returns>
        public override bool Filter(LogEntry log)
        {
            int logLevel = log.Priority;

            switch (logLevel)
            { 
                case 1000:
                    return debugLevel;
                case 1001:
                    return debug1Level;
                case 1002:
                    return debug2Level;
                case 1003:
                    return debug3Level;
                case 1004:
                    return debug4Level;

                case 10000:
                    return traceLevel;
                case 10001:
                    return trace1Level;
                case 10002:
                    return trace2Level;
                case 10003:
                    return trace3Level;
                case 10004:
                    return trace4Level;
                case 10005:
                    return trace5Level;
                case 10006:
                    return trace6Level;
                case 10007:
                    return trace7Level;
                case 10008:
                    return trace8Level;
                case 10009:
                    return trace9Level;

                case 10010:
                    return trace10Level;
                case 10011:
                    return trace11Level;
                case 10012:
                    return trace12Level;
                case 10013:
                    return trace13Level;
                case 10014:
                    return trace14Level;
                case 10015:
                    return trace15Level;
                case 10016:
                    return trace16Level;
                case 10017:
                    return trace17Level;
                case 10018:
                    return trace18Level;
                case 10019:
                    return trace19Level;

                case 10020:
                    return trace20Level;
                case 10021:
                    return trace21Level;
                case 10022:
                    return trace22Level;
                case 10023:
                    return trace23Level;
                case 10024:
                    return trace24Level;
                case 10025:
                    return trace25Level;
                case 10026:
                    return trace26Level;
                case 10027:
                    return trace27Level;
                case 10028:
                    return trace28Level;
                case 10029:
                    return trace29Level;

                case 10030:
                    return trace30Level;

                default:
                    return true;
            }
        }

        #region Level Properties

        #region Debug Level

        public bool DebugLevel
        {
            get { return debugLevel; }
        }

        public bool Debug1Level
        {
            get { return debug1Level; }
        }

        public bool Debug2Level
        {
            get { return debug2Level; }
        }

        public bool Debug3Level
        {
            get { return debug3Level; }
        }

        public bool Debug4Level
        {
            get { return debug4Level; }
        }

        #endregion

        #region Trace - Trace9 Level

        public bool TraceLevel
        {
            get { return traceLevel; }
        }

        public bool Trace1Level
        {
            get { return trace1Level; }
        }

        public bool Trace2Level
        {
            get { return trace2Level; }
        }

        public bool Trace3Level
        {
            get { return trace3Level; }
        }

        public bool Trace4Level
        {
            get { return trace4Level; }
        }

        public bool Trace5Level
        {
            get { return trace5Level; }
        }

        public bool Trace6Level
        {
            get { return trace6Level; }
        }

        public bool Trace7Level
        {
            get { return trace7Level; }
        }

        public bool Trace8Level
        {
            get { return trace8Level; }
        }

        public bool Trace9Level
        {
            get { return trace9Level; }
        }

        #endregion

        #region Trace10 - Trace19 Level

        public bool Trace10Level
        {
            get { return trace10Level; }
        }

        public bool Trace11Level
        {
            get { return trace11Level; }
        }

        public bool Trace12Level
        {
            get { return trace12Level; }
        }

        public bool Trace13Level
        {
            get { return trace13Level; }
        }

        public bool Trace14Level
        {
            get { return trace14Level; }
        }

        public bool Trace15Level
        {
            get { return trace15Level; }
        }

        public bool Trace16Level
        {
            get { return trace16Level; }
        }

        public bool Trace17Level
        {
            get { return trace17Level; }
        }

        public bool Trace18Level
        {
            get { return trace18Level; }
        }

        public bool Trace19Level
        {
            get { return trace19Level; }
        }

        #endregion

        #region Trace20 = Trace29 Level

        public bool Trace20Level
        {
            get { return trace20Level; }
        }

        public bool Trace21Level
        {
            get { return trace21Level; }
        }

        public bool Trace22Level
        {
            get { return trace22Level; }
        }

        public bool Trace23Level
        {
            get { return trace23Level; }
        }

        public bool Trace24Level
        {
            get { return trace24Level; }
        }

        public bool Trace25Level
        {
            get { return trace25Level; }
        }

        public bool Trace26Level
        {
            get { return trace26Level; }
        }

        public bool Trace27Level
        {
            get { return trace27Level; }
        }

        public bool Trace28Level
        {
            get { return trace28Level; }
        }

        public bool Trace29Level
        {
            get { return trace29Level; }
        }

        #endregion

        #region Trace30 Level

        public bool Trace30Level
        {
            get { return trace30Level; }
        }

        #endregion

        #endregion
    }
}

