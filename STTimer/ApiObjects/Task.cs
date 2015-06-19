using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTimer.ApiObjects
{
    class Task
    {
        public string description { get; set; }
        public double estimate { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int owner { get; set; }
        public string status { get; set; }
        public int story { get; set; }
        public double timeSpent { get; set; }
    }
}
