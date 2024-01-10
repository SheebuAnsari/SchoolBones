using DomLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolConfiguration
{
    public class ConnectionHelper
    {
        private static string m_ConString = "";
        public static string GetConString
        {
            get { return m_ConString; }
        }


        public static void SetConString(ref string sError, int iDbType = 0, string sFilePath = "")
        {
            if (string.IsNullOrEmpty(sFilePath) == false && Directory.Exists(sFilePath) && string.IsNullOrEmpty(ServerHall.ServerCoreFilePath) == true)
            {
                ServerHall.ServerCoreFilePath = sFilePath + "\\";
            }
            if (Directory.Exists(ServerHall.ServerCoreFilePath))
            {
                string sDbConfig = "";
                DbInformation oDbInformation = null;

                sDbConfig = ServerHall.GetFilePathFromServer("DbConfig.xml");

                if (string.IsNullOrEmpty(sDbConfig) == true || !File.Exists(sDbConfig))
                {
                    sDbConfig = ServerHall.GetFilePathFromServer("MasterDbConfig.xml");
                }
                if (string.IsNullOrEmpty(sDbConfig) == true)
                {
                    sError = "FilePath of dbConfig is wrong";
                    return;
                }
                oDbInformation = FileCRUD.ReadDbInformation(sDbConfig, ref sError);
                if (string.IsNullOrEmpty(sError) == true)
                {
                    m_ConString = string.Format("SERVER = {0}; DATABASE = {1}; UID = {2}; PASSWORD = {3}",
                                        oDbInformation.ServerName, oDbInformation.DbName, oDbInformation.UserId, oDbInformation.Password);
                    //Or
                    m_ConString = string.Format(@"DATA SOURCE = {0}; INITIAL CATALOG = {1}; UID = {2}; PASSWORD = {3}",
                        oDbInformation.ServerName, oDbInformation.DbName, oDbInformation.UserId, oDbInformation.Password);
                }
                else
                {
                    sError = string.Format("Something is wrong when reading DbConfig xml file.\nError : {0}", sError);
                    return;
                }
            }
        }
        public static System.Data.SqlClient.SqlConnection Stablish
        {
            get
            {
                System.Data.SqlClient.SqlConnection oCon = null;
                oCon = new System.Data.SqlClient.SqlConnection(GetConString);
                oCon.Open();
                return oCon;
            }
        }
    }
}
