//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Focus8W.DataStruct
//{
//    public class DateDS
//    {
//        public static int DateToInt(DateTime dt)
//        {
//            return dt.Year << 16 | dt.Month << 8 | dt.Day;
//        }
//        public DateTime IntToDate(int dt, int iCalenderType)
//        {
//            if (dt <= DateToInt(DateTime.MinValue) || dt > DateToInt(DateTime.MaxValue))
//            {
//                return DateTime.Now;
//            }
//            else
//            {
//                switch ((CalenderType)iCalenderType)
//                {
//                    case CalenderType.None:
//                        return DateTime.Now;
//                    case CalenderType.Gregorean:
//                        return new DateTime((dt & 0xfff0000) >> 16, (dt & 0xff00) >> 8, (dt & 0xff), 0, 0, 0);
//                    case CalenderType.Shamshi:
//                    case CalenderType.Nepali:
//                    case CalenderType.Hijri:
//                        return new DateTime((dt & 0xfff0000) >> 16, (dt & 0xff00) >> 8, (dt & 0xff), 0, 0, 0);
//                }
//            }
//            return new DateTime();
//        }
//        public enum CalenderType
//        {
//            None = 0,
//            Gregorean = 1,
//            Shamshi = 2,
//            Nepali = 3,
//            Hijri = 4,

//        }
//    }
//}