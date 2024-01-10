using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolConfiguration
{
    public class ServerHall
    {
        public static string ServerCoreFilePath { get; set; }

        public static string GetFilePathFromServer(string sFileName)
        {
            if (string.IsNullOrEmpty(sFileName) == false)
            {
                return Path.Combine(ServerCoreFilePath + sFileName);//dynamic
            }
            else
            {
                return "";
            }
        }
    }
}
