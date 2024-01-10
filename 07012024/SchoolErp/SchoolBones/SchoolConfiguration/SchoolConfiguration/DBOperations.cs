using DomLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolConfiguration
{
    class DBOperations
    {
        public static void CreateMenuUsingXML(Menu[] arrMenu, ref string sError)
        {
            string strQry = "";
            SqlCommand cmd = null;
            SqlConnection oCon = null;
            cmd = new SqlCommand();

            try
            {
                //using (oCon = new SqlConnection(ConnectionHelper.GetConnectionString))
                using (oCon = new SqlConnection(ConnectionHelper.GetConString))
                {
                    oCon.Open();
                    strQry = string.Format("Delete from tbl_Menu");
                    cmd = new SqlCommand(strQry, oCon);
                    cmd.ExecuteNonQuery();

                    strQry = "";
                    for (int iMenu = 0; iMenu < arrMenu.Length; iMenu++)
                    {
                        strQry += string.Format(@"INSERT INTO tbl_Menu (iMenuId, sMenuName, sCaption, iModule, iSubModule, bIsGroup, bIsActive) VALUES (@iMenuId_{0}, @sMenuName_{0}, @sCaption_{0}, @iModule_{0}, @iSubModule_{0}, @bIsGroup_{0}, @bIsActive_{0})", iMenu);
                        strQry += "\r\n";
                        cmd.Parameters.AddWithValue(string.Format("@iMenuId_{0}", iMenu), arrMenu[iMenu].MenuId);     //ie ActionMethod Name which cant be change.
                        cmd.Parameters.AddWithValue(string.Format("@sMenuName_{0}", iMenu), arrMenu[iMenu].MenuName.Replace(" ", ""));     //ie ActionMethod Name which cant be change.
                        cmd.Parameters.AddWithValue(string.Format("@sCaption_{0}", iMenu), arrMenu[iMenu].MenuName);//Menu name changable
                        cmd.Parameters.AddWithValue(string.Format("@iModule_{0}", iMenu), arrMenu[iMenu].Module);
                        cmd.Parameters.AddWithValue(string.Format("@iSubModule_{0}", iMenu), arrMenu[iMenu].SubModule);
                        cmd.Parameters.AddWithValue(string.Format("@bIsGroup_{0}", iMenu), arrMenu[iMenu].IsGroup);
                        cmd.Parameters.AddWithValue(string.Format("@bIsActive_{0}", iMenu), arrMenu[iMenu].IsActive);
                    }


                    cmd.CommandText = strQry;
                    cmd.Connection = oCon;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
        }

        public static void DeleteDuplicateMenu(ref string sError)
        {
            string strQry = "";
            SqlCommand cmd = null;
            SqlConnection oCon = null;
            cmd = new SqlCommand();

            try
            {
                //using (oCon = new SqlConnection(ConnectionHelper.GetConnectionString))
                using (oCon = new SqlConnection(ConnectionHelper.GetConString))
                {
                    oCon.Open();
                    strQry = string.Format(@"DELETE FROM tbl_Menu
                                            WHERE iMenuId NOT IN(
                                                SELECT MIN(iMenuId)
                                                FROM tbl_Menu
                                                GROUP BY sMenuName
                                            );");
                    cmd = new SqlCommand(strQry, oCon);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
        }
    }
}
