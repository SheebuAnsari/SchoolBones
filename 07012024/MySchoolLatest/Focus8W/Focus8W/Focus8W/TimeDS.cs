using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Focus8W
{
    public class TimeDS
    {
        public static string IntToTime(decimal iTime)
        {
            string sH = "", sM = "", sS = "";

            decimal h = Math.Floor(iTime / 3600);
            decimal m = Math.Floor(iTime % 3600 / 60);
            decimal s = Math.Floor(iTime % 3600 % 60);

            sH = h > 9 ? h.ToString() : string.Format("{0}{1}", 0, h);
            sM = m > 9 ? m.ToString() : string.Format("{0}{1}", 0, m);
            return string.Format("{0} : {1}", sH, sM);

        }

        //IntToTime: function(iDate, iDateControl)
        //{
        //    iDate = Number(iDate);
        //    var h = Math.floor(iDate / 3600);
        //    var m = Math.floor(iDate % 3600 / 60);
        //    var s = Math.floor(iDate % 3600 % 60);
        //    h = h > 9 ? h : '0' + h;
        //    m = m > 9 ? m : '0' + m;
        //    var sTime = h + ":" + m;
        //    //document.getElementById(iDateControl).value = sTime;
        //    return sTime;
        //},
    }
    }