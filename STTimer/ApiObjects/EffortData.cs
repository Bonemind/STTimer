using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTimer.ApiObjects
{
    class EffortData
    {
        public double effort;
        public EffortData(double hours)
        {
            this.effort = hours;
        }
    }
}
