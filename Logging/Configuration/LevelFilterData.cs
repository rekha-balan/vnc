//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Logging Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design.Validation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using VNC.Logging.Filters;

namespace VNC.Logging.Configuration
{
    /// <summary>
    /// Represents the configuration for a priority filter.
    /// </summary>    
    //[ResourceDescription(typeof(DesignResources), "LevelFilterDataDescription")]
    //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataDisplayName")]
    //[ElementValidation(LoggingDesignTime.ValidatorTypes.LogPriorityMinMaxValidatorType)]
    public class LevelFilterData : Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LogFilterData
    {
        #region LevelProperties

        private const string debugLevelProperty  = "debug";
        private const string debug1LevelProperty = "debug1";
        private const string debug2LevelProperty = "debug2";
        private const string debug3LevelProperty = "debug3";
        private const string debug4LevelProperty = "debug4";

        private const string traceLevelProperty  = "trace";
        private const string trace1LevelProperty = "trace1";
        private const string trace2LevelProperty = "trace2";
        private const string trace3LevelProperty = "trace3";
        private const string trace4LevelProperty = "trace4";
        private const string trace5LevelProperty = "trace5";
        private const string trace6LevelProperty = "trace6";
        private const string trace7LevelProperty = "trace7";
        private const string trace8LevelProperty = "trace8";
        private const string trace9LevelProperty = "trace9";

        private const string trace10LevelProperty = "trace10";
        private const string trace11LevelProperty = "trace11";
        private const string trace12LevelProperty = "trace12";
        private const string trace13LevelProperty = "trace13";
        private const string trace14LevelProperty = "trace14";
        private const string trace15LevelProperty = "trace15";
        private const string trace16LevelProperty = "trace16";
        private const string trace17LevelProperty = "trace17";
        private const string trace18LevelProperty = "trace18";
        private const string trace19LevelProperty = "trace19";

        private const string trace20LevelProperty = "trace20";
        private const string trace21LevelProperty = "trace21";
        private const string trace22LevelProperty = "trace22";
        private const string trace23LevelProperty = "trace23";
        private const string trace24LevelProperty = "trace24";
        private const string trace25LevelProperty = "trace25";
        private const string trace26LevelProperty = "trace26";
        private const string trace27LevelProperty = "trace27";
        private const string trace28LevelProperty = "trace28";
        private const string trace29LevelProperty = "trace29";

        private const string trace30LevelProperty = "trace30";

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new <see cref="LevelFilterData"/>.
        /// </summary>
        public LevelFilterData()
        {
            Type = typeof(LevelFilter);
        }

        /// <summary>
        /// Initializes a new <see cref="LevelFilterData"/> 
        /// </summary>
        /// <param name="minimumLevel">The minimum priority.</param>
        public LevelFilterData(bool debugLevel, bool debug1Level, bool debug2Level, bool debug3Level, bool debug4Level,
            bool traceLevel, bool trace1Level, bool trace2Level, bool trace3Level, bool trace4Level,
            bool trace5Level, bool trace6Level, bool trace7Level, bool trace8Level, bool trace9Level,
            bool trace10Level, bool trace11Level, bool trace12Level, bool trace13Level, bool trace14Level,
            bool trace15Level, bool trace16Level, bool trace17Level, bool trace18Level, bool trace19Level,
            bool trace20Level, bool trace21Level, bool trace22Level, bool trace23Level, bool trace24Level,
            bool trace25Level, bool trace26Level, bool trace27Level, bool trace28Level, bool trace29Level,
            bool trace30Level)
            : this("debugLevel", debugLevel, debug1Level, debug2Level, debug3Level, debug4Level,
                  traceLevel, trace1Level, trace2Level, trace3Level, trace4Level,
                  trace5Level, trace6Level, trace7Level, trace8Level, trace9Level,
                  trace10Level, trace11Level, trace12Level, trace13Level, trace14Level,
                  trace15Level, trace16Level, trace17Level, trace18Level, trace19Level,
                  trace20Level, trace21Level, trace22Level, trace23Level, trace24Level,
                  trace25Level, trace26Level, trace27Level, trace28Level, trace29Level,
                  trace30Level )
        {
        }

        /// <summary>
        /// Initializes a new named <see cref="LevelFilterData"/> with a minimum priority.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="minimumLevel">The minimum priority.</param>
        public LevelFilterData(string name, bool debugLevel, bool debug1Level, bool debug2Level, bool debug3Level, bool debug4Level,
            bool traceLevel, bool trace1Level, bool trace2Level, bool trace3Level, bool trace4Level,
            bool trace5Level, bool trace6Level, bool trace7Level, bool trace8Level, bool trace9Level,
            bool trace10Level, bool trace11Level, bool trace12Level, bool trace13Level, bool trace14Level,
            bool trace15Level, bool trace16Level, bool trace17Level, bool trace18Level, bool trace19Level,
            bool trace20Level, bool trace21Level, bool trace22Level, bool trace23Level, bool trace24Level,
            bool trace25Level, bool trace26Level, bool trace27Level, bool trace28Level, bool trace29Level,
            bool trace30Level)
            : base(name, typeof(LevelFilter))
        {
            this.DebugLevel = debugLevel;
            this.Debug1Level = debug1Level;
            this.Debug2Level = debug2Level;
            this.Debug3Level = debug3Level;
            this.Debug4Level = debug4Level;

            this.TraceLevel = traceLevel;
            this.Trace1Level = trace1Level;
            this.Trace2Level = trace2Level;
            this.Trace3Level = trace3Level;
            this.Trace4Level = trace4Level;
            this.Trace5Level = trace5Level;
            this.Trace6Level = trace6Level;
            this.Trace7Level = trace7Level;
            this.Trace8Level = trace8Level;
            this.Trace9Level = trace9Level;

            this.Trace10Level = trace10Level;
            this.Trace11Level = trace11Level;
            this.Trace12Level = trace12Level;
            this.Trace13Level = trace13Level;
            this.Trace14Level = trace14Level;
            this.Trace15Level = trace15Level;
            this.Trace16Level = trace16Level;
            this.Trace17Level = trace17Level;
            this.Trace18Level = trace18Level;
            this.Trace19Level = trace19Level;

            this.Trace20Level = trace20Level;
            this.Trace21Level = trace21Level;
            this.Trace22Level = trace22Level;
            this.Trace23Level = trace23Level;
            this.Trace24Level = trace24Level;
            this.Trace25Level = trace25Level;
            this.Trace26Level = trace26Level;
            this.Trace27Level = trace27Level;
            this.Trace28Level = trace28Level;
            this.Trace29Level = trace29Level;

            this.Trace30Level = trace30Level;
        }

        #endregion

        #region Properties

        #region debugLevel Properties

        [ConfigurationProperty(debugLevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool DebugLevel
        {
            get
            {
                return (bool)this[debugLevelProperty];
            }
            set
            {
                this[debugLevelProperty] = value;
            }
        }

        [ConfigurationProperty(debug1LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Debug1Level
        {
            get
            {
                return (bool)this[debug1LevelProperty];
            }
            set
            {
                this[debug1LevelProperty] = value;
            }
        }

        [ConfigurationProperty(debug2LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Debug2Level
        {
            get
            {
                return (bool)this[debug2LevelProperty];
            }
            set
            {
                this[debug2LevelProperty] = value;
            }
        }

        [ConfigurationProperty(debug3LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Debug3Level
        {
            get
            {
                return (bool)this[debug3LevelProperty];
            }
            set
            {
                this[debug3LevelProperty] = value;
            }
        }

        [ConfigurationProperty(debug4LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Debug4Level
        {
            get
            {
                return (bool)this[debug4LevelProperty];
            }
            set
            {
                this[debug4LevelProperty] = value;
            }
        }

        #endregion

        #region Trace Level - Trace9 Level

        [ConfigurationProperty(traceLevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool TraceLevel
        {
            get
            {
                return (bool)this[traceLevelProperty];
            }
            set
            {
                this[traceLevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace1LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace1Level
        {
            get
            {
                return (bool)this[trace1LevelProperty];
            }
            set
            {
                this[trace1LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace2LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace2Level
        {
            get
            {
                return (bool)this[trace2LevelProperty];
            }
            set
            {
                this[trace2LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace3LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace3Level
        {
            get
            {
                return (bool)this[trace3LevelProperty];
            }
            set
            {
                this[trace3LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace4LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace4Level
        {
            get
            {
                return (bool)this[trace4LevelProperty];
            }
            set
            {
                this[trace4LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace5LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace5Level
        {
            get
            {
                return (bool)this[trace5LevelProperty];
            }
            set
            {
                this[trace5LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace6LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace6Level
        {
            get
            {
                return (bool)this[trace6LevelProperty];
            }
            set
            {
                this[trace6LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace7LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace7Level
        {
            get
            {
                return (bool)this[trace7LevelProperty];
            }
            set
            {
                this[trace7LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace8LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace8Level
        {
            get
            {
                return (bool)this[trace8LevelProperty];
            }
            set
            {
                this[trace8LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace9LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace9Level
        {
            get
            {
                return (bool)this[trace9LevelProperty];
            }
            set
            {
                this[trace9LevelProperty] = value;
            }
        }

        #endregion

        #region Trace10 Level - Trace19 Level

        [ConfigurationProperty(trace10LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace10Level
        {
            get
            {
                return (bool)this[trace10LevelProperty];
            }
            set
            {
                this[trace10LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace11LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace11Level
        {
            get
            {
                return (bool)this[trace11LevelProperty];
            }
            set
            {
                this[trace11LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace12LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace12Level
        {
            get
            {
                return (bool)this[trace12LevelProperty];
            }
            set
            {
                this[trace12LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace13LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace13Level
        {
            get
            {
                return (bool)this[trace13LevelProperty];
            }
            set
            {
                this[trace13LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace14LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace14Level
        {
            get
            {
                return (bool)this[trace14LevelProperty];
            }
            set
            {
                this[trace14LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace15LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace15Level
        {
            get
            {
                return (bool)this[trace15LevelProperty];
            }
            set
            {
                this[trace15LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace16LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace16Level
        {
            get
            {
                return (bool)this[trace16LevelProperty];
            }
            set
            {
                this[trace16LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace17LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace17Level
        {
            get
            {
                return (bool)this[trace17LevelProperty];
            }
            set
            {
                this[trace17LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace18LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace18Level
        {
            get
            {
                return (bool)this[trace18LevelProperty];
            }
            set
            {
                this[trace18LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace19LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace19Level
        {
            get
            {
                return (bool)this[trace19LevelProperty];
            }
            set
            {
                this[trace19LevelProperty] = value;
            }
        }

        #endregion

        #region Trace20 Level - Trace29 Level

        [ConfigurationProperty(trace20LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace20Level
        {
            get
            {
                return (bool)this[trace20LevelProperty];
            }
            set
            {
                this[trace20LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace21LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace21Level
        {
            get
            {
                return (bool)this[trace21LevelProperty];
            }
            set
            {
                this[trace21LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace22LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace22Level
        {
            get
            {
                return (bool)this[trace22LevelProperty];
            }
            set
            {
                this[trace22LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace23LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace23Level
        {
            get
            {
                return (bool)this[trace23LevelProperty];
            }
            set
            {
                this[trace23LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace24LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace24Level
        {
            get
            {
                return (bool)this[trace24LevelProperty];
            }
            set
            {
                this[trace24LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace25LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace25Level
        {
            get
            {
                return (bool)this[trace25LevelProperty];
            }
            set
            {
                this[trace25LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace26LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace26Level
        {
            get
            {
                return (bool)this[trace26LevelProperty];
            }
            set
            {
                this[trace26LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace27LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace27Level
        {
            get
            {
                return (bool)this[trace27LevelProperty];
            }
            set
            {
                this[trace27LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace28LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace28Level
        {
            get
            {
                return (bool)this[trace28LevelProperty];
            }
            set
            {
                this[trace28LevelProperty] = value;
            }
        }

        [ConfigurationProperty(trace29LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace29Level
        {
            get
            {
                return (bool)this[trace29LevelProperty];
            }
            set
            {
                this[trace29LevelProperty] = value;
            }
        }

        #endregion

        #region Trace30 Level

        [ConfigurationProperty(trace30LevelProperty)]
        //[ResourceDescription(typeof(DesignResources), "LevelFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "LevelFilterDataMaxDurationDisplayName")]
        public bool Trace30Level
        {
            get
            {
                return (bool)this[trace30LevelProperty];
            }
            set
            {
                this[trace30LevelProperty] = value;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Creates an enumeration of <see cref="TypeRegistration"/> instances describing the filter represented by 
        /// this configuration object.
        /// </summary>
        /// <returns>A an enumeration of <see cref="TypeRegistration"/> instance describing a filter.</returns>
        public override IEnumerable<TypeRegistration> GetRegistrations()
        {
            yield return
                new TypeRegistration<ILogFilter>(
                    () => new LevelFilter(this.Name, 
                                            this.DebugLevel, this.Debug1Level, this.Debug2Level, this.Debug3Level, this.Debug4Level,
                                            this.TraceLevel, this.Trace1Level, this.Trace2Level, this.Trace3Level, this.Trace4Level,
                                            this.Trace5Level, this.Trace6Level, this.Trace7Level, this.Trace8Level, this.Trace9Level,
                                            this.Trace10Level, this.Trace11Level, this.Trace12Level, this.Trace13Level, this.Trace14Level,
                                            this.Trace15Level, this.Trace16Level, this.Trace17Level, this.Trace18Level, this.Trace19Level,
                                            this.Trace20Level, this.Trace21Level, this.Trace22Level, this.Trace23Level, this.Trace24Level,
                                            this.Trace25Level, this.Trace26Level, this.Trace27Level, this.Trace28Level, this.Trace29Level,
                                            this.Trace30Level))
                {
                    Name = this.Name,
                    Lifetime = TypeRegistrationLifetime.Transient
                };
        }
    }
}
