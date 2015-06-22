using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTimer.ApiObjects
{
    /// <summary>
    /// Represents a server token object
    /// </summary>
    class Token
    {
        /// <summary>
        /// The token string we have to use
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// The user object this token belongs to
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// Whether this token is valid
        /// </summary>
        public bool valid { get; set; }
    }
}
