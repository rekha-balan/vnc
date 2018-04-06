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
    [ConfigurationElementType(typeof(DurationFilterData))]
    public class DurationFilter : Microsoft.Practices.EnterpriseLibrary.Logging.Filters.LogFilter
    {
        private double maxDuration = -1; // Log if greater than this time.

        string[] _supportedCustomAttributes = new string[] {
            "maxDuration", "MaxDuration", "maxduration" };

        /// <summary>
        /// Initializes a new instance of <see cref="LogFilter"/>.
        /// </summary>
        /// <param name="name">The name for the log filter.</param>
        public DurationFilter(string name) : base(name)
        {
        }

        public DurationFilter(NameValueCollection nvPairs) : this("DurationFilter", -1)
        {

        }

        public DurationFilter(string name, double maxDuration) : base(name)
        {
            this.maxDuration = maxDuration;
        }

        /// <summary>
        /// Test to see if a message meets the criteria to be processed. 
        /// </summary>
        /// <param name="log">Log entry to test.</param>
        /// <returns><b>true</b> if the message passes through the filter and should be distributed, <b>false</b> otherwise.</returns>
        public override bool Filter(LogEntry log)
        {
            if (log.ExtendedProperties.ContainsKey("Duration"))
            {
                var foo = log.ExtendedProperties["Duration"];

                double duration = (double)log.ExtendedProperties["Duration"];

                if (duration > MaxDuration)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        public double MaxDuration
        {
            get { return maxDuration; }
        }
    }
}
