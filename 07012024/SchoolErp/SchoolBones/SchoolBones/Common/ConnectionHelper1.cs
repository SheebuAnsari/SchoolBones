//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SchoolBones
//{
//    public class ConnectionHelper
//    {
//        public static SqlConnection GetDBConnectionString()
//        {
//            string DBConnectionString = "";
//            DBConnectionString = @"SERVER = MOHD-SHEEBU\SQLSERVER2019; database = Madarsa; uid = sa; password = focus";
//            return new SqlConnection(DBConnectionString);
//        }
//        //NOTE : this ConOpen() method is not required
//        public static SqlConnection ConOpen()
//        {
//            string DBConnectionString = "";
//            DBConnectionString = @"SERVER = MOHD-SHEEBU\SQLSERVER2019; database = Madarsa; uid = sa; password = focus";
//            SqlConnection oCon = new SqlConnection(DBConnectionString);
//            oCon.Open();
//            return oCon;
//        }
//    }
//}
