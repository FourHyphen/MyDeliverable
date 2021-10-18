using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility
{
    public static class Time
    {
        /// <summary>
        /// ex) format = "d.hh:mm:ss.fff" / time = "1.01:23:45.678"
        /// </summary>
        /// <param name="format"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static TimeSpan CreateTimeSpan(string format, string time)
        {
            string formatRe = format.Replace(":", @"\:").Replace(".", @"\.");
            return TimeSpan.ParseExact(time, formatRe, null);
        }

        /// <summary>
        /// return (basis - subtract).toMillisec
        /// </summary>
        /// <param name="basis"></param>
        /// <param name="subtract"></param>
        /// <returns></returns>
        public static double DiffMillisecond(TimeSpan basis, TimeSpan subtract)
        {
            return basis.Subtract(subtract).TotalMilliseconds;
        }
    }
}
