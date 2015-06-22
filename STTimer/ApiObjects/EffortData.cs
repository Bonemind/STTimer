using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTimer.ApiObjects
{
    /// <summary>
    /// Wraps the effortdata to be posted to the server
    /// </summary>
    class EffortData
    {
        /// <summary>
        /// The effort we have set
        /// </summary>
        public double effort;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="hours">The hours format we have posted</param>
        public EffortData(double hours)
        {
            this.effort = hours;
        }
    }
}
