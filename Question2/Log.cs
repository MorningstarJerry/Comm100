using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comm100.Question2
{
    public class Log
    {
        public Log(long time, int action)
        {
            this.Time = time;
            this.Action = action;
        }

        public long Time { get; set; }
        public int Action { get; set; } //1:login 0:logout
    }
}
