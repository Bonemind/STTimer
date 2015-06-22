using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTimer.ApiObjects
{
    /// <summary>
    /// Wraps the login data we want to post
    /// </summary>
    class LoginData
    {
        /// <summary>
        /// The username we want to post
        /// </summary>
        public string user {get; set; }

        /// <summary>
        /// The password we want to post
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="user">The username</param>
        /// <param name="pass">The password</param>
        public LoginData(string user, string pass) {
            this.user = user;
            this.password = pass;
        }
    }
}
