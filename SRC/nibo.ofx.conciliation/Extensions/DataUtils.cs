using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nibo.ofx.conciliation.Extensions
{
    public class DataUtils
    {
        public static DateTime DataEstConvert(string data)
        {
            string day = data.Substring(6, 2);
            string month = data.Substring(4, 2);
            string year = data.Substring(0, 4);
            string hour = data.Substring(8, 2);
            string min = data.Substring(10, 2);
            string sec = data.Substring(12, 2);

            DateTime dt = DateTime.Parse(string.Format("{0}-{1}-{2} {3}:{4}:{5}", year, month, day, hour, min, sec));
            string dateString = dt.ToString("ddd, dd MM yyyy HH:mm:ss") + " -3:00";
            string fromFormat = "ddd, dd MM yyyy HH:mm:ss zzz";
            DateTime timeUtc = DateTime.ParseExact(dateString, fromFormat, null);

            return timeUtc;
        }
    }
}
