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
    //[ResourceDescription(typeof(DesignResources), "PerformanceFilterDataDescription")]
    //[ResourceDisplayName(typeof(DesignResources), "PerformanceFilterDataDisplayName")]
    //[ElementValidation(LoggingDesignTime.ValidatorTypes.LogPriorityMinMaxValidatorType)]
    public class DurationFilterData : Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LogFilterData
    {
        //private const string minimumPriorityProperty = "minimumPriority";
        //private const string maximumPriorityProperty = "maximumPriority";
        private const string maxDurationProperty = "maxDuration";

        /// <summary>
        /// Initializes a new <see cref="PriorityFilterData"/>.
        /// </summary>
        public DurationFilterData()
        {
            Type = typeof(DurationFilter);
        }

        /// <summary>
        /// Initializes a new <see cref="PriorityFilterData"/> with a minimum priority.
        /// </summary>
        /// <param name="minimumPriority">The minimum priority.</param>
        public DurationFilterData(double maxDuration)
            : this("maxDuration", maxDuration)
        {
        }

        /// <summary>
        /// Initializes a new named <see cref="PriorityFilterData"/> with a minimum priority.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="minimumPriority">The minimum priority.</param>
        public DurationFilterData(string name, double maxDuration)
            : base(name, typeof(DurationFilter))
        {
            this.MaxDuration = maxDuration;
        }

        [ConfigurationProperty(maxDurationProperty)]
        //[ResourceDescription(typeof(DesignResources), "PerformanceFilterDataMaxDurationDescription")]
        //[ResourceDisplayName(typeof(DesignResources), "PerformanceFilterDataMaxDurationDisplayName")]
        public double MaxDuration
        {
            get
            {
                return (double)this[maxDurationProperty];
            }
            set
            {
                this[maxDurationProperty] = value;
            }
        }

        /// <summary>
        /// Creates an enumeration of <see cref="TypeRegistration"/> instances describing the filter represented by 
        /// this configuration object.
        /// </summary>
        /// <returns>A an enumeration of <see cref="TypeRegistration"/> instance describing a filter.</returns>
        public override IEnumerable<TypeRegistration> GetRegistrations()
        {
            yield return
                new TypeRegistration<ILogFilter>(
                    () => new DurationFilter(this.Name, this.MaxDuration))
                {
                    Name = this.Name,
                    Lifetime = TypeRegistrationLifetime.Transient
                };
        }
    }
}
