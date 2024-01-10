using DomLibrary;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBones.Common
{
    class CRUDMethods
    {
        static SaveStatus oSaveStatus = null;
        static SqlCommand cmd = null;
        static SqlDataReader reader = null;
        static SqlConnection oCon = null;


        //public SaveStatus Save(string strQuery, SqlParameter[] param)
        //{
        //    oSaveStatus = new SaveStatus();
        //    cmd = new SqlCommand();
        //    cmd.CommandText = strQuery;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = ConnectionHelper.ConOpen();
        //    foreach (var item in param)
        //        cmd.Parameters.Add(item);
        //    oSaveStatus.iStatus = cmd.ExecuteNonQuery();
        //    return oSaveStatus;
        //}
        public static int CallExecuteNonQuery(string strQry)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                return cmd.ExecuteNonQuery();
            }
        }
        //public SqlDataReader CallExecuteReader(SqlCommand SqlCmd)
        //{
        //    return SqlCmd.ExecuteReader();
        //}
        //public object CallExecuteScalar(SqlCommand SqlCmd)
        //{
        //    return SqlCmd.ExecuteScalar();
        //}
        //public int GetIdentityOnSave(SqlCommand cmd)
        //{
        //    return Convert.ToInt32(cmd.ExecuteScalar());
        //}
    }
}
