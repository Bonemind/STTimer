using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTimer
{
    /// <summary>
    /// Defined the interface all switchable pages should implement
    /// </summary>
    public interface ISwitchable
    {
        /// <summary>
        /// Passes the state to the target page
        /// </summary>
        /// <param name="state">The state to use</param>
        void UtilizeState(object state);
    }
}
