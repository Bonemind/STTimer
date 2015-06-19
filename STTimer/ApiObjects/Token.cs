using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTimer.ApiObjects
{
    class Token
    {
        //public int dateCreation {get; set;}
        public string token { get; set; }
        public User user { get; set; }
        public bool valid { get; set; }
    }
}
