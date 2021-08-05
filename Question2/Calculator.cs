using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comm100.Question2
{
    public class Calculator
    {
        public static long GetOnLineTime(long start, long end, Log[] logs)
        {
            long total = 0;
            if ((1).Equals(logs.Length))
            {
                if ((0).Equals(logs.Last().Action))
                {//last is logout
                    //total = logs.Last().Time;
                    total = 0;
                }
                else
                {
                    //last is login
                    total = (end - logs.Last().Time);
                }
            }
            else
            {
                if ((0).Equals(logs.Last().Action))
                {//last is logout
                    total += 0;
                }
                else
                {
                    //last is login
                    total += (end - logs.Last().Time);
                }

                end = logs.Last().Time;
                var newLen = logs.Length - 1;
                Log[] newLogs = new Log[newLen];
                Array.ConstrainedCopy(logs, 0, newLogs, 0, newLen);

                total += GetOnLineTime(start, end, newLogs);
            }

            return total;
        }


        public static long[] GetOnlineTimePerDay(long start, long end, Log[] logs)
        {
            List<long> arys = new List<long>();
            GetOnlineTimeLists(start, end, logs, arys);
            arys.RemoveAll(x => x.Equals(0));
            return arys.ToArray().Reverse().ToArray();
        }


        public static void GetOnlineTimeLists(long start, long end, Log[] logs, List<long> arys)
        {
            if ((1).Equals(logs.Length))
            {
                if ((0).Equals(logs.Last().Action))
                {//last is logout
                    //total = logs.Last().Time;
                    arys.Add(0);
                }
                else
                {
                    //last is login
                    arys.Add(end - logs.Last().Time);
                }
            }
            else
            {
                if ((0).Equals(logs.Last().Action))
                {//last is logout
                    arys.Add(0);
                }
                else
                {
                    //last is login
                    arys.Add(end - logs.Last().Time);
                }

                end = logs.Last().Time;
                var newLen = logs.Length - 1;
                Log[] newLogs = new Log[newLen];
                Array.ConstrainedCopy(logs, 0, newLogs, 0, newLen);

                GetOnlineTimeLists(start, end, newLogs, arys);
            }
        }
    }
}
