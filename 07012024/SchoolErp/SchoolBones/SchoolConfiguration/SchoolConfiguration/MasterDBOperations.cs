using DomLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolConfiguration
{
    class MasterDBOperations
    {
        public static void CreateDatabase(string sDbName, ref string sError)
        {
            int value = 0;
            string strQuery = "";
            SqlConnection oCon = null;
            try
            {
                //using (oCon = new SqlConnection(ConnectionHelper.GetMasterConnectionString))
                using (oCon = new SqlConnection(ConnectionHelper.GetConString))
                {
                    oCon.Open();

                    strQuery = string.Format("IF EXISTS (SELECT name FROM sys.databases WHERE NAME = '{0}') SELECT 1 ELSE SELECT 0", sDbName);
                    using (SqlCommand cmd = new SqlCommand(strQuery, oCon))
                    {
                        value = (int)cmd.ExecuteScalar();

                        if (value != 1)
                        {
                            //Database doesn't exist
                            strQuery = string.Format("CREATE DATABASE [{0}]", sDbName);
                            try
                            {
                                cmd.CommandText = strQuery;
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                sError = ex.Message;
                            }
                        }
                        else
                        {
                            sError = string.Format("School : {0} is already exist, Please try with other name.", sDbName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
        }

        public static Databases[] LoadDatabase(ref string sError)
        {
            SqlConnection oCon = null;
            Databases oDatabases = null;
            List<Databases> lstDatabases = new List<Databases>();

            try
            {
                //using (oCon = new SqlConnection(ConnectionHelper.GetMasterConnectionString))
                using (oCon = new SqlConnection(ConnectionHelper.GetConString))
                {
                    oCon.Open();

                    string strQuery = string.Format("SELECT DATABASE_ID, NAME FROM SYS.DATABASES WHERE DATABASE_ID > 4");
                    using (SqlCommand cmd = new SqlCommand(strQuery, oCon))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                oDatabases = new Databases();
                                oDatabases.DbId = Convert.ToInt32(reader["DATABASE_ID"]);
                                oDatabases.DbName = Convert.ToString(reader["NAME"]);
                                lstDatabases.Add(oDatabases);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return lstDatabases.ToArray();
        }

        public static bool DeleteDatabase(string sDBName, ref string sError)
        {
            SqlConnection oCon = null;

            try
            {
                if (ConnectionHelper.GetConString.Contains(sDBName))
                {
                    sError = "Not able to delete becoz Currently it is in use.";
                    return false;
                }
                else
                {
                    using (oCon = new SqlConnection(ConnectionHelper.GetConString))
                    {
                        oCon.Open();
                        //Delete DbConfig file
                        //FileCRUD.DeleteDbConfig();

                        string strQuery = string.Format("DROP DATABASE {0}", sDBName);
                        using (SqlCommand cmd = new SqlCommand(strQuery, oCon))
                        {
                            cmd.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                return false;
            }
        }
    }
}
