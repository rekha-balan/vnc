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
    [ConfigurationElementType(typeof(PerformanceFilterData))]
    public class PerformanceFilter : Microsoft.Practices.EnterpriseLibrary.Logging.Filters.LogFilter
    {
        //private double maxDuration = -1; // Log if greater than this time.

        string[] _supportedCustomAttributes = new string[] {
            "maxDuration", "MaxDuration", "maxduration",
            "suppressNoDuration", "SuppressNoDuration", "suppressnoduration" };

        /// <summary>
        /// Initializes a new instance of <see cref="LogFilter"/>.
        /// </summary>
        /// <param name="name">The name for the log filter.</param>
        public PerformanceFilter(string name) : base(name)
        {
        }

        public PerformanceFilter(NameValueCollection nvPairs) : this("PerformanceFilter", -1)
        {

        }

        public PerformanceFilter(string name, double maxDuration) : base(name)
        {

        }

        //public double MaxDuration
        //{
        //    get
        //    {
        //        if (maxDuration < 0)
        //        {
        //            // Initialize from the custom attributes

                    
                    
        //            //var key = Attributes.Keys.Cast<string>().
        //            //FirstOrDefault(s => string.Equals(s, "maxduration", StringComparison.InvariantCultureIgnoreCase));

        //            //if (!string.IsNullOrWhiteSpace(key))
        //            //{
        //            //    double.TryParse(Attributes[key], out maxDuration);
        //            //}
        //        }

        //        return maxDuration;
        //    }
        //}
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

                if (duration > 0.100)
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
    }
}
