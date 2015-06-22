using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTimer.ApiObjects
{
    /// <summary>
    /// Represents a task object
    /// </summary>
    class Task
    {
        /// <summary>
        /// The description of the task
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// The estimate set on the task
        /// </summary>
        public double estimate { get; set; }

        /// <summary>
        /// The id of the task
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The name of the task
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The owner id of the task
        /// </summary>
        public int owner { get; set; }

        /// <summary>
        /// The status of the task
        /// Contents: "DEFINED", "IN_PROGRESS" or "DONE"
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// The story id this task belongs to
        /// </summary>
        public int story { get; set; }

        /// <summary>
        /// The time spent on this task
        /// </summary>
        public double timeSpent { get; set; }
    }
}
