using DomLibrary;
using SchoolConfiguration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SchoolBones.Common
{
    public class CommonExecuteDML
    {
        SaveStatus oSaveStatus = new SaveStatus();
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        SqlConnection oCon = null;


        //public IdNamePair[] LoadCreatedDatabase(string strQry)
        //{
        //    string sError = "";
        //    IdNamePair oDbInfo = null;
        //    List<IdNamePair> lstDbInfo = new List<IdNamePair>();
        //    string sMasterConString = "";
        //    sMasterConString = ConnectionHelper.GetDBConnectionString(ref sError);
        //    if(string.IsNullOrEmpty(sError) != true)
        //    {
        //        return null;
        //    }
        //    using (oCon = new SqlConnection(sMasterConString))
        //    {
        //        oCon.Open();
        //        cmd = new SqlCommand(strQry, oCon);
        //        using (reader = cmd.ExecuteReader())
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    oDbInfo = new IdNamePair();
        //                    oDbInfo.ID = Convert.ToInt32(reader["database_id"]);
        //                    oDbInfo.Name= Convert.ToString(reader["name"]);
        //                    lstDbInfo.Add(oDbInfo);
        //                }
        //            }
        //        }
        //    }
        //    return lstDbInfo.ToArray();
        //}
        //public SaveStatus DropCreatedDatabase(string strQry)
        //{
        //    string sError = "";
        //    string sMasterConString = "";
        //    sMasterConString = ConnectionHelper.GetMasterConString(ref sError);
        //    if (string.IsNullOrEmpty(sError) != true)
        //    {
        //        return oSaveStatus;
        //    }

        //    using (oCon = new SqlConnection(sMasterConString))
        //    {
        //        oCon.Open();
        //        cmd = new SqlCommand(strQry, oCon);
        //        cmd.ExecuteNonQuery();
        //        oSaveStatus.iStatus = 1;
        //        return oSaveStatus;
        //    }
        //}


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
        public int CallExecuteNonQuery(SqlCommand SqlCmd)
        {
            return SqlCmd.ExecuteNonQuery();
        }
        public SqlDataReader CallExecuteReader(SqlCommand SqlCmd)
        {
            return SqlCmd.ExecuteReader();
        }
        public object CallExecuteScalar(SqlCommand SqlCmd)
        {
            return SqlCmd.ExecuteScalar();
        }
        public int GetIdentityOnSave(SqlCommand cmd)
        {
            return Convert.ToInt32(cmd.ExecuteScalar());
        }













        public SaveStatus Save(SqlCommand cmd)
        {
            oSaveStatus = new SaveStatus();
            cmd.CommandType = CommandType.Text;
            oSaveStatus.iStatus = cmd.ExecuteNonQuery();
            return oSaveStatus;
        }
        public object GetScalerValue(string strQuery)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                oSaveStatus = new SaveStatus();
                cmd = new SqlCommand(strQuery, oCon);
                return cmd.ExecuteScalar();
            }
        }
        public SaveStatus DeleteOrUpdate(string strQuery)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                oSaveStatus = new SaveStatus();
                cmd = new SqlCommand(strQuery, oCon);
                oSaveStatus.iStatus = cmd.ExecuteNonQuery();
                return oSaveStatus;
            }
        }
       
        
        public SaveStatus Alter(string strQuery)
        {
            try
            {
                oSaveStatus = new SaveStatus();
                using (oCon = ConnectionHelper.Stablish)
                {
                    cmd = new SqlCommand(strQuery, oCon);
                    cmd.ExecuteScalar();
                }
                oSaveStatus.iStatus = 1;
                return oSaveStatus;
            }
            catch (Exception ex)
            {
                return oSaveStatus;
            }
        }
        

        public LicenseInfo LoadLicenseInfo(string strQry)
        {
            LicenseInfo oLicenseInfo = null;
            //List<LicenseInfo> lstLicenseInfo = new List<LicenseInfo>();

            //using (oCon = ConnectionHelper.GetDBConnectionString())
            //using (oCon = Server.ServerDBSqlConnection)   ConnectionHelper.GetDBConnectionString()
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oLicenseInfo = new LicenseInfo();
                            oLicenseInfo.LicenseInfoId = Convert.ToInt32(reader["iLicenseInfo"]);
                            oLicenseInfo.DbName = Convert.ToString(reader["sDbName"]);
                            oLicenseInfo.Provider = Convert.ToString(reader["sProvider"]);
                            oLicenseInfo.Key = Convert.ToString(reader["sKey"]);
                            oLicenseInfo.Validity = Convert.ToInt32(reader["iValidity"]);
                            oLicenseInfo.InstallationDate = Convert.ToInt32(reader["iInstallationDate"]);
                            oLicenseInfo.ExpiryDate = Convert.ToInt32(reader["iExpiryDate"]);
                            //lstLicenseInfo.Add(oLicenseInfo);
                        }
                    }
                }
            }
            return oLicenseInfo;
        }
        public Registration LoadRegisteredUserDML(string strQry)
        {
            Registration oRegistration = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oRegistration = new Registration();
                        oRegistration.RegistrationId = Convert.ToInt32(reader["iRegistrationId"]);
                        oRegistration.UserName = Convert.ToString(reader["sUserName"]);
                        oRegistration.LoginName = Convert.ToString(reader["sLoginName"]);
                        oRegistration.UserPassword = Convert.ToString(reader["sUserPassword"]);
                        oRegistration.UserType = Convert.ToInt32(reader["iUserType"]);
                    }
                }
                reader.Close();
            }
            return oRegistration;
        }

        public string SelectConstraintColumn(string sTableName, string sColumnName)
        {
            string strQuery = string.Format(@"SELECT
                                                --t.name 'Table Name',
                                                --c.NAME 'Column Name',
                                                df.name 'Constraint Name'
                                            FROM sys.default_constraints df
                                            INNER JOIN sys.tables t ON df.parent_object_id = t.object_id
                                            INNER JOIN sys.columns c ON df.parent_object_id = c.object_id AND df.parent_column_id = c.column_id
                                            where t.name='{0}' and c.name = '{1}'",
                                            sTableName, sColumnName);
            return Convert.ToString(GetScalerValue(strQuery));
        }
    }
}
