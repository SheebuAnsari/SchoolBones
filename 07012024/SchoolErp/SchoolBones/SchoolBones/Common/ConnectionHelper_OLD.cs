//using SchoolBones.DataStructs;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SchoolBones.Common
//{
//    public class ConnectionHelper_OLD
//    {
//        private static string DbName = "";
//        private static string strConnection = "";
//        private static string sDBConnection = "";

//        private static SqlConnection connection = null;
//        public static SqlConnection sqlConnectionString = null;
//        static string _DBConnectionString = null;

//        static SqlConnection _SqlConnection = null;




//        public static string GetMasterConString(ref string sError)
//        {
//            string MasterConnectionString = "";
//            string sMasterDbConfig = "";
//            DbInformation oDbInformation = null;

//            sMasterDbConfig = Server.GetFilePathFromServer("MasterDbConfig.xml");

//            try
//            {
//                oDbInformation = CoreDML.ReadXMLMasterConString(sMasterDbConfig);
//                if (oDbInformation != null)
//                {
//                    //static string _masterConnectionString = $"Data Source={databaseServer};Initial Catalog={masterDefaultCatalog};Integrated Security=True";
//                    MasterConnectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security={2}", oDbInformation.ServerName, oDbInformation.DbName, true);
//                }
//            }
//            catch(Exception ex)
//            {
//                return sError = ex.Message;
//            }
//            return MasterConnectionString;
//        }
//        public static string GetDBConString(ref string sError)
//        {
//            string sDbConfig = "";
//            DbInformation oDbInformation = null;

//            try
//            {
//                sDbConfig = Server.GetFilePathFromServer("DbConfig.xml");
//                oDbInformation = CoreDML.ReadXMLDBConString(sDbConfig);
//                if (oDbInformation != null)
//                {
//                    _DBConnectionString = string.Format(@"server={0};database={1};uid={2};password={3}", oDbInformation.ServerName, oDbInformation.DbName, oDbInformation.UserId, oDbInformation.Password);
//                }
//            }
//            catch(Exception ex)
//            {
//                return sError = ex.Message;
//            }
//            return sDBConnection;
//        }

//        public static SqlConnection GetDBConnectionString()
//        {
//            return new SqlConnection(DBConnectionString);
//        }

//        public static string DBConnectionString
//        {
//            get
//            {
//                return _DBConnectionString;
//            }
//            set
//            {
//                _DBConnectionString = value;
//            }
//        }















//        //public static string XMLConnectionString(ref string sError)
//        //{
//        //    string sDbConfig = "";
//        //    DbInformation oDbInformation = null;

//        //    sDbConfig = Server.GetFilePathFromServer("DbConfig.xml");

//        //    if(!File.Exists(sDbConfig))
//        //    {
//        //        sError = "Lack of references in Configuration file.";
//        //        return "";
//        //    }

//        //    oDbInformation = CoreDML.ReadXMLConString(sDbConfig);
//        //    if (oDbInformation != null)
//        //    {
//        //        strConnection = string.Format(@"server={0};database={1};uid={2};password={3}", oDbInformation.ServerName, oDbInformation.DbName, oDbInformation.UserId, oDbInformation.Password);
//        //    }
//        //    return strConnection;
//        //}
//        public static SqlConnection ConOpen()
//        {
//            try
//            {
//                //MOHD-SHEEBU\SQLSERVER2019
//                connection = new SqlConnection(@"server=MOHD-SHEEBU\SQLSERVER2019;database=School;uid=sa;password=focus");
//                connection = new SqlConnection(@"server=MOHD-SHEEBU\SQLSERVER2019;database=Madarsa;uid=sa;password=focus");

//                if(!string.IsNullOrEmpty(Server.DbName))
//                {
//                    connection = GetNewConnection(Server.DbName);
//                }
//                //FOCUS-DEV-20\MSSQL2019
//                //connection = new SqlConnection("server=FOCUS-DEV-20\\MSSQL2019;database=School;uid=sa;password=focus");
//                connection.Open();
//                return connection;
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }


//        public static SqlConnection GetDBConnectionString()
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(strConnection))
//                {
//                    strConnection = @"server=MOHD-SHEEBU\SQLSERVER2019;database=School;uid=sa;password=focus";
//                    strConnection = @"server=MOHD-SHEEBU\SQLSERVER2019;database=Madarsa;uid=sa;password=focus";
//                }
                
//                //DbName = Server.DbName;
//                //if (string.IsNullOrEmpty(strConnection) == false)
//                //{
//                //    sqlConnectionString  = GetNewConnection(Server.DbName);
//                //}
//                //else
//                {
//                    sqlConnectionString = new SqlConnection(strConnection);
//                }
//                return sqlConnectionString;
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }
//        public static SqlConnection GetNewConnection(string sDbName)
//        {
//            try
//            {
//                strConnection = string.Format(@"server=MOHD-SHEEBU\SQLSERVER2019;database={0};uid=sa;password=focus", sDbName);
//                sqlConnectionString = new SqlConnection(strConnection);
//                return sqlConnectionString;
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }
        


//        //========
//        //static string databaseServer = @"MOHD-SHEEBU\SQLSERVER2019";
//        //static string masterDefaultCatalog = "Master";
//        //static string _masterConnectionString = $"Data Source={databaseServer};Initial Catalog={masterDefaultCatalog};Integrated Security=True";
//        //public static string MasterConnectionString
//        //{
//        //    get
//        //    {
//        //        return _masterConnectionString;
//        //    }
//        //    set
//        //    {
//        //        _masterConnectionString = value;
//        //    }
//        //}
//    }

//    public class Server
//    {
//        public static string GetFilePathFromServer(string sFileName = "")
//        {
//            if (string.IsNullOrEmpty(sFileName) == false && Directory.Exists(ServerPath))
//            {
//                return Path.Combine(ServerPath, sFileName);
//            }
//            return ServerPath;
//        }

//        public static string ServerPath { get; set; }
//        public static string DbName { get; set; }
//        private static SqlConnection m_ServerDBSqlConnection = null;
//        public static SqlConnection ServerDBSqlConnection { get; set; }
//    }
//}
