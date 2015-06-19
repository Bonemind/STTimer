using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTimer.ApiObjects
{
    class LoginData
    {
        public string user {get; set; }
        public string password { get; set; }

        public LoginData(string user, string pass) {
            this.user = user;
            this.password = pass;
        }
    }
}
