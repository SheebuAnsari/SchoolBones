using DomLibrary;
using SchoolBones.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBones.Common
{
    public class CommonDML
    {
        string strQry = "";
        SaveStatus oSaveStatus = null;
        SqlCommand cmd = null;
        CommonExecuteDML oCommonExc = new CommonExecuteDML();

        public Registration LoadRegisteredUser(int iRegId)
        {
            strQry = string.Format(@"SELECT iRegistrationId, sUserName, sLoginName, sUserPassword, iUserType FROM tbl_UserRegistration WHERE iRegistrationId = {0}", iRegId);
            return oCommonExc.LoadRegisteredUserDML(strQry);
        }

        public Registration LoadRegisteredUser(int iRegId, int iUserType)
        {
            strQry = string.Format(@"SELECT iRegistrationId, sUserName, sLoginName, sUserPassword FROM tbl_UserRegistration WHERE iRegistrationId = {0} and iUserType = {1}", 
                iRegId, iUserType);
            return oCommonExc.LoadRegisteredUserDML(strQry);
        }

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
    }
}
