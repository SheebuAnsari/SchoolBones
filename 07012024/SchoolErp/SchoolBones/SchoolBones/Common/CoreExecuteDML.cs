using DomLibrary;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBones.Common
{
    class CoreExecuteDML
    {
        SaveStatus oSaveStatus = null;
        CommonExecuteDML oCommonExc = new CommonExecuteDML();

        SqlCommand cmd = null;
        SqlDataReader reader = null;
        SqlConnection oCon = null;



        public SchoolDetails LoadSchoolDetailsDML(string strQry)
        {
            SchoolDetails oSchoolDetails = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oSchoolDetails = new SchoolDetails();
                            oSchoolDetails.SchoolId = Convert.ToInt32(reader["iSchoolId"]);
                            oSchoolDetails.RegistrationDate = Convert.ToInt32(reader["iRegistrationDate"]);
                            oSchoolDetails.bIntContact = Convert.ToInt64(reader["iContact"]);
                        }
                    }
                }
            }
            return oSchoolDetails;

            //List<SchoolDetails> lstSchoolDetails = new List<SchoolDetails>();

            //SchoolDetails oSchoolDetails = null;

            //SqlDataReader reader = null;
            //SqlCommand cmd = null;

            //cmd = new SqlCommand();
            //cmd.CommandText = txt;
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = ConnectionHelper.ConOpen();

            //reader = cmd.ExecuteReader();
            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        oSchoolDetails = new SchoolDetails();
            //        oSchoolDetails.SchoolId = Convert.ToInt32(reader["iSchoolId"]);
            //        oSchoolDetails.RegistrationDate = Convert.ToInt32(reader["iRegistrationDate"]);
            //        oSchoolDetails.bIntContact = Convert.ToInt64(reader["iContact"]);

            //        lstSchoolDetails.Add(oSchoolDetails);
            //    }
            //}
            //reader.Close();
            //return lstSchoolDetails;
        }

        public Menu[] LoadMenu(string strQry)
        {
            List<Menu> lstMenu = new List<Menu>();
            Menu oMenu = null;

            using (oCon = SchoolConfiguration.ConnectionHelper.Stablish)
            {
                //oCon.Open();
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        oMenu = new Menu();
                        oMenu.MenuId = Convert.ToInt32(reader["iMenuId"]);
                        oMenu.MenuName = Convert.ToString(reader["sMenuName"]);
                        oMenu.Caption = Convert.ToString(reader["sCaption"]);
                        oMenu.Module = Convert.ToInt32(reader["iModule"]);
                        oMenu.SubModule = Convert.ToInt32(reader["iSubModule"]);
                        oMenu.IsGroup = Convert.ToBoolean(reader["bIsGroup"]);
                        oMenu.IsActive = Convert.ToBoolean(reader["bIsActive"]);

                        lstMenu.Add(oMenu);
                    }
                }
            }
            return lstMenu.ToArray();
        }
    }
}
