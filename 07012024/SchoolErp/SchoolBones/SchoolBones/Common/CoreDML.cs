using DomLibrary;
using SchoolBones.Enums;
using SchoolConfiguration;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

namespace SchoolBones.Common
{
    public class CoreDML
    {
        string strQry = "";
        DateDS oDateDS = new DateDS();
        SaveStatus oSaveStatus = new SaveStatus();
        CoreExecuteDML oCoreExc = new CoreExecuteDML();
        CommonExecuteDML oCommonExc = new CommonExecuteDML();

        SqlCommand cmd = null;
        SqlDataReader reader = null;
        SqlConnection oCon = null;
        SqlTransaction oTrans = null;

        public int Login(string sUserId = "", string sUserPassword = "", int iUserType = 0)
        {
            string sError = "";
            SchoolConfiguration.ConnectionHelper.SetConString(ref sError);
            strQry = string.Format("SELECT iRegistrationId FROM tbl_UserRegistration WHERE sLoginName = '{0}' and sUserPassword = '{1}' and iUserType = {2}",
                sUserId, sUserPassword, iUserType);
            return Convert.ToInt32(oCommonExc.GetScalerValue(strQry));
        }

        //public IdNamePair[] LoadCreatedDatabases()
        //{
        //    strQry = "SELECT database_id, name FROM sys.databases WHERE database_id > 4";
        //    return oCommonExc.LoadCreatedDatabase(strQry);
        //}
        //public SaveStatus DropCreatedDatabases(string sDbName)
        //{
        //    strQry = string.Format("DROP DATABASE {0}", sDbName);
        //    oSaveStatus = oCommonExc.DropCreatedDatabase(strQry);
        //    return oSaveStatus;
        //}


        //public string CheckLicense(int iExpiryDate)
        //{
        //    //string sDbRefFile = "";
        //    //DbInformation oDbInformation = null;

        //    //sDbRefFile = Server.GetFilePathFromServer("DbRef.xml");//if not deleted this file

        //    //if (!File.Exists(sDbRefFile))
        //    //{
        //    //    return "License exp";
        //    //}



        //    //Security : 2
        //    string sFileFactory = "";
        //    LicenseInfo oLicenseInfo = null;

        //    strQry = "SELECT iLicenseInfo, sDbName,sProvider, sKey, iValidity, iInstallationDate, iExpiryDate from tbl_LicenseInfo";
        //    oLicenseInfo = oCommonExc.LoadLicenseInfo(strQry);

        //    if (iExpiryDate == oLicenseInfo.ExpiryDate || oLicenseInfo.ExpiryDate == 0)
        //    {
        //        strQry = "update tbl_LicenseInfo set iExpiryDate = 0";
        //        CRUDMethods.CallExecuteNonQuery(strQry);

        //        //Delete FileFactory Folder

        //        sFileFactory = Server.GetFilePathFromServer();
        //        if (Directory.Exists(sFileFactory))
        //        {
        //            //Directory.Delete(sFileFactory, true);
        //        }
        //        return "License is expired";
        //    }
        //    return "";
        //}
        #region CreateNewSchoolDb
        //public SaveStatus SchoolRegistration(ProjectConfiguration oProjectConfiguration)
        //{
        //    string sDbName = oProjectConfiguration.LicenseInfo.DbName;
        //    //Create XML file for GetDBConnectionString
        //    //WriteXMLConString(sDbName);

        //    //First Create a new db
        //    CreateDatabase(sDbName);

        //    //Create Tables
        //    CreateTables(sDbName);

        //    //InsertDataInCreatedTables
        //    InsertDataInCreatedTables(sDbName, oProjectConfiguration);

        //    //Note: Delete Tool, MasterDbConfig file // For backup we can change the name of these files.

        //    oSaveStatus.iStatus = 1;
        //    //Delete Tools.xml file
        //    return oSaveStatus;
        //}
        //private void WriteXMLConString(string sDbName)
        //{
        //    StringBuilder sb = null;
        //    string sFilePath = "";

        //    sb = new StringBuilder();
        //    sFilePath = string.Format(@"{0}{1}", Server.ServerPath, "DbRef.xml");


        //    if (!File.Exists(sFilePath))
        //    {
        //        sb.Append("<?xml version='1.0' encoding='utf-8' ?>");
        //        sb.AppendLine();
        //        sb.Append("<SqlServer>");
        //        sb.AppendLine();
        //        sb.AppendFormat("\t<DbName>");
        //        sb.Append(sDbName);
        //        sb.AppendFormat("</DbName>");
        //        sb.AppendLine();
        //        sb.Append("</SqlServer>");

        //        if (sb.Length > 0)
        //        {
        //            using (StreamWriter file = new StreamWriter(sFilePath))
        //            {
        //                file.WriteLine(sb.ToString());
        //            }
        //        }
        //    }

        //}
        public static DbInformation ReadXMLDBConString(string sDbConfig)
        {
            StringBuilder sb = null;
            DbInformation oDbInformation = null;

            sb = new StringBuilder();
            oDbInformation = new DbInformation();

            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode1, xmlnode2, xmlnode3, xmlnode4, xmlnode5;

            int iNode = 0;
            FileStream fs = new FileStream(sDbConfig, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode1 = xmldoc.GetElementsByTagName("Server");
            xmlnode2 = xmldoc.GetElementsByTagName("DbName");
            xmlnode3 = xmldoc.GetElementsByTagName("UserId");
            xmlnode4 = xmldoc.GetElementsByTagName("Password");

            oDbInformation.ServerName = Convert.ToString(xmlnode1[iNode].ChildNodes.Item(0).InnerText.Trim());
            oDbInformation.DbName = Convert.ToString(xmlnode2[iNode].ChildNodes.Item(0).InnerText.Trim());
            oDbInformation.UserId = Convert.ToString(xmlnode3[iNode].ChildNodes.Item(0).InnerText.Trim());
            oDbInformation.Password = Convert.ToString(xmlnode4[iNode].ChildNodes.Item(0).InnerText.Trim());

            fs.Close();
            return oDbInformation;
        }
        public static DbInformation ReadXMLMasterConString(string sMasterDbConfig)
        {
            int iNode = 0;
            StringBuilder sb = null;
            DbInformation oDbInformation = null;
            XmlDataDocument xmldoc = null;
            XmlNodeList xmlnode1, xmlnode2, xmlnode3, xmlnode4;

            sb = new StringBuilder();
            oDbInformation = new DbInformation();
            xmldoc = new XmlDataDocument();

            try
            {
                using (FileStream fs = new FileStream(sMasterDbConfig, FileMode.Open, FileAccess.Read))
                {
                    xmldoc.Load(fs);
                    xmlnode1 = xmldoc.GetElementsByTagName("Server");
                    xmlnode2 = xmldoc.GetElementsByTagName("DbName");
                    xmlnode3 = xmldoc.GetElementsByTagName("IntegratedSecurity");

                    oDbInformation.ServerName = Convert.ToString(xmlnode1[iNode].ChildNodes.Item(0).InnerText.Trim());
                    oDbInformation.DbName = Convert.ToString(xmlnode2[iNode].ChildNodes.Item(0).InnerText.Trim());
                    oDbInformation.IntegratedSecurity = Convert.ToBoolean(xmlnode3[iNode].ChildNodes.Item(0).InnerText.Trim());
                }

            }
            catch (Exception ex)
            {

            }
            return oDbInformation;
        }

        //private void CreateDatabase(string sDbName)
        //{
        //    SqlConnection oCon = null;
        //    string sError = "";
        //    string sMasterConString = "";
        //    sMasterConString  = ConnectionHelper.GetMasterConString(ref sError);
        //    if (string.IsNullOrEmpty(sError) == true)
        //    {
        //        using (oCon = new SqlConnection(sMasterConString))
        //        {
        //            oCon.Open();
        //            string strQuery = string.Format("IF EXISTS (SELECT name FROM sys.databases WHERE name = '{0}') SELECT 1 ELSE SELECT 0", sDbName);
        //            using (SqlCommand cmd = new SqlCommand(strQuery, oCon))
        //            {
        //                int value = (int)cmd.ExecuteScalar();

        //                if (value != 1)
        //                {
        //                    //Database doesn't exist
        //                    strQuery = string.Format("CREATE DATABASE [{0}]", sDbName);
        //                    try
        //                    {
        //                        cmd.CommandText = strQuery;
        //                        cmd.ExecuteNonQuery();
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //private static void CreateTables(string sDbName)
        //{
        //    StringBuilder sbTables = new StringBuilder();
        //    SqlConnection oCon = null;

        //    //CREATETABLES
        //    sbTables.Append(CoreTables.CreateLicenseInfo("tbl_LicenseInfo"));
        //    sbTables.AppendLine();
        //    sbTables.Append(CoreTables.CreateSchoolInfo("tbl_SchoolInfo"));
        //    sbTables.AppendLine();
        //    sbTables.Append(CoreTables.CreateMenu("tbl_Menu"));
        //    sbTables.AppendLine();
        //    sbTables.Append(CoreTables.CreateUsers("tbl_UserRegistration"));

        //    try
        //    {
        //        using (oCon = ConnectionHelper.GetNewConnection(sDbName))
        //        {
        //            oCon.Open();
        //            using (SqlCommand cmd = new SqlCommand(sbTables.ToString(), oCon))
        //            {
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void InsertDataInCreatedTables(string sDbName, ProjectConfiguration oProjectConfiguration/*, string sFileFactoryPath*/)
        //{
        //    try
        //    {

        //        using (oCon = ConnectionHelper.GetNewConnection(sDbName))
        //        {
        //            oCon.Open();
        //            oTrans = oCon.BeginTransaction();

        //            IntoLicenseInfo("tbl_LicenseInfo", oProjectConfiguration.LicenseInfo);
        //            IntoSchoolInfo("tbl_SchoolInfo", oProjectConfiguration.SchoolInfo);
        //            IntoMenus("tbl_Menu"/*, sFileFactoryPath*/);
        //            IntoUser("tbl_UserRegistration");

        //            oTrans.Commit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (oTrans != null)
        //        {
        //            oTrans.Rollback();
        //        }
        //    }
        //}
        public void IntoLicenseInfo(string strTableName, LicenseInfo oLicenseInfo)
        {
            strQry = string.Format(@"IF EXISTS (SELECT table_Name FROM INFORMATION_SCHEMA.TABLES WHERE table_Name = '{0}')
                            BEGIN
                                INSERT INTO {0} (sDbName, sProvider, sKey, iValidity, iInstallationDate, iExpiryDate) VALUES 
                                         (@sDbName, @sProvider, @sKey, @iValidity, @iInstallationDate, @iExpiryDate)
                            END", strTableName);

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                cmd.Parameters.AddWithValue("@sDbName", oLicenseInfo.DbName);
                cmd.Parameters.AddWithValue("@sProvider", oLicenseInfo.Provider);
                cmd.Parameters.AddWithValue("@sKey", oLicenseInfo.Key);
                cmd.Parameters.AddWithValue("@iValidity", oLicenseInfo.Validity);
                cmd.Parameters.AddWithValue("@iInstallationDate", oLicenseInfo.InstallationDate);
                cmd.Parameters.AddWithValue("@iExpiryDate", oLicenseInfo.ExpiryDate);
                cmd.ExecuteNonQuery();
            }
        }
        public void IntoSchoolInfo(string strTableName, SchoolInfo oSchoolInfo)
        {
            strQry = string.Format(@"IF EXISTS (SELECT table_Name FROM INFORMATION_SCHEMA.TABLES WHERE table_Name = '{0}')
                        BEGIN
	                        INSERT INTO {0} (sSchoolName, sPrincipal, sEstablish,  iRegistrationDate, sRegistrationNo, sAddress, iContact) VALUES 
                                (@sSchoolName, @sPrincipal, @sEstablish, @iRegistrationDate, @sRegistrationNo, @sAddress, @iContact)
                        END", strTableName);

            cmd = new SqlCommand(strQry, oCon, oTrans);
            cmd.Parameters.AddWithValue("@sSchoolName", oSchoolInfo.SchoolName);
            cmd.Parameters.AddWithValue("@sPrincipal", oSchoolInfo.Principal);
            cmd.Parameters.AddWithValue("@sEstablish", oSchoolInfo.Establish);
            cmd.Parameters.AddWithValue("@iRegistrationDate", oSchoolInfo.RegistrationDate);
            cmd.Parameters.AddWithValue("@sRegistrationNo", oSchoolInfo.RegistrationNo);
            cmd.Parameters.AddWithValue("@sAddress", oSchoolInfo.Address);
            cmd.Parameters.AddWithValue("@iContact", oSchoolInfo.Contact);
            cmd.ExecuteNonQuery();
        }
        //public void IntoMenus(string strTableName = "")
        //{
        //    string sFilePath = "";
        //    Menu oMenu = null;
        //    Menu[] arrMenu = null;

        //    {
        //        sFilePath = string.Format(@"{0}{1}", Server.ServerPath, "MenuItems.xml");

        //        if (!System.IO.File.Exists(sFilePath))
        //        {
        //            //Log
        //            return;
        //        }
        //        arrMenu = ReadXMLMenuFile();
        //        if (arrMenu != null)
        //        {
        //            strQry = string.Format(@"Delete from {0}", strTableName);
        //            cmd = new SqlCommand(strQry, oCon, oTrans);
        //            cmd.ExecuteNonQuery();

        //            for (int iMenu = 0; iMenu < arrMenu.Length; iMenu++)
        //            {
        //                strQry = "";
        //                oMenu = arrMenu[iMenu];

        //                strQry = string.Format(@"INSERT INTO {0} (sMenuName, sCaption, iModule, iSubModule, bIsGroup, bIsActive) VALUES (@sMenuName, @sCaption, @iModule, @iSubModule, @bIsGroup, @bIsActive)", strTableName);
        //                cmd = new SqlCommand(strQry, oCon, oTrans);

        //                cmd.Parameters.AddWithValue("@sMenuName", oMenu.MenuName.Replace(" ", ""));     //ie ActionMethod Name which cant be change.
        //                cmd.Parameters.AddWithValue("@sCaption", oMenu.MenuName);
        //                cmd.Parameters.AddWithValue("@iModule", oMenu.Module);
        //                cmd.Parameters.AddWithValue("@iSubModule", oMenu.SubModule);
        //                cmd.Parameters.AddWithValue("@bIsGroup", oMenu.IsGroup);
        //                cmd.Parameters.AddWithValue("@bIsActive", oMenu.IsActive);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        public void IntoUser(string strTableName = "")
        {
            strQry = string.Format(@"Delete from {0}", strTableName);
            cmd = new SqlCommand(strQry, oCon, oTrans);
            cmd.ExecuteNonQuery();

            strQry = "";
            strQry = string.Format(@"INSERT INTO {0} (sUserName, sLoginName,sUserPassword, iUserType) values ('su','su','su',1)", strTableName);
            cmd = new SqlCommand(strQry, oCon, oTrans);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region CreateMenuNewSchoolDb

        //private Menu[] ReadXMLMenuFile()
        //{
        //    string sFilePath = "";

        //    sFilePath = string.Format(@"{0}{1}", Server.ServerPath, "MenuItems.xml");
        //    XmlDataDocument xmldoc = new XmlDataDocument();
        //    XmlNodeList xmlnode1, xmlnode2, xmlnode3, xmlnode4, xmlnode5;

        //    int iNode = 0;
        //    FileStream fs = new FileStream(sFilePath, FileMode.Open, FileAccess.Read);
        //    xmldoc.Load(fs);
        //    xmlnode1 = xmldoc.GetElementsByTagName("iMenuId");
        //    xmlnode2 = xmldoc.GetElementsByTagName("sMenuName");
        //    xmlnode3 = xmldoc.GetElementsByTagName("bIsGroup");
        //    xmlnode4 = xmldoc.GetElementsByTagName("iModule");
        //    xmlnode5 = xmldoc.GetElementsByTagName("iSubModule");

        //    List<Menu> lstMenuItem = new List<Menu>();
        //    for (iNode = 0; iNode <= xmlnode1.Count - 1; iNode++)
        //    {
        //        Menu objMenuItems = new Menu();
        //        objMenuItems.MenuId = Convert.ToInt32(xmlnode1[iNode].ChildNodes.Item(0).InnerText.Trim());
        //        objMenuItems.MenuName = xmlnode2[iNode].ChildNodes.Item(0).InnerText.Trim();
        //        objMenuItems.Caption = xmlnode2[iNode].ChildNodes.Item(0).InnerText.Trim();
        //        objMenuItems.IsGroup = Convert.ToBoolean(xmlnode3[iNode].ChildNodes.Item(0).InnerText.Trim());
        //        objMenuItems.Module = Convert.ToInt32(xmlnode4[iNode].ChildNodes.Item(0).InnerText.Trim());
        //        objMenuItems.SubModule = Convert.ToInt32(xmlnode5[iNode].ChildNodes.Item(0).InnerText.Trim());
        //        lstMenuItem.Add(objMenuItems);
        //    }
        //    fs.Close();
        //    return lstMenuItem.ToArray();
        //}

        public void WriteXMLMenuFile()
        {
            string sFilePath = "";

            //sFilePath = string.Format("{0}{1}", Server.ServerPath, "MenuItems.xml");
            sFilePath = @"K:\MySchoolLatest\Focus8W\Focus8W\Focus8W\FileFactory\MenuItems.xml";
            Menu oMenu = null;
            Menu[] arrMenu = null;
            StringBuilder sb = new StringBuilder();

            arrMenu = LoadMenu((int)Modules.Admin);
            if (arrMenu != null)
            {
                sb.Append("<?xml version='1.0' encoding='utf-8' ?>");
                sb.AppendLine();
                sb.Append("<Menu>");
                sb.AppendLine();
                for (int iMenu = 0; iMenu < arrMenu.Length; iMenu++)
                {
                    oMenu = arrMenu[iMenu];
                    sb.AppendFormat("\t<MenuItem>");
                    sb.AppendLine();
                    sb.Append(string.Format("\t\t<iMenuId>{0}</iMenuId>\n\t\t<sMenuName>{1}</sMenuName>\n\t\t<sCaption>{2}</sCaption>\n\t\t<iModule>{3}</iModule>\n\t\t<iSubModule>{4}</iSubModule>\n\t\t<bIsGroup>{5}</bIsGroup>\n\t\t<bIsActive>{6}</bIsActive>",
                        oMenu.MenuId,
                        oMenu.MenuName,
                        oMenu.Caption,
                        oMenu.Module,
                        oMenu.SubModule,
                        oMenu.IsGroup,
                        oMenu.IsActive
                        ));
                    sb.AppendLine();
                    sb.AppendFormat("\t</MenuItem>");
                    sb.AppendLine();
                }
                sb.Append("</Menu>");
                if (sb.Length > 0)
                {
                    using (StreamWriter file = new StreamWriter(sFilePath))
                    {
                        file.WriteLine(sb.ToString());
                    }
                }
            }
        }

     
        #endregion


















        
        public SchoolDetails LoadSchoolDetails()
        {
            int iSchoolId = -1;
            strQry = "SELECT iSchoolId, iRegistrationDate, iContact from tbl_SchoolInfo";
            return oCoreExc.LoadSchoolDetailsDML(strQry);
        }

        //public SaveStatus SaveMenu(Menu oMenu)
        //{
        //    using (oCon = ConnectionHelper.Stablish)
        //    {
        //        strQry = @"INSERT INTO tbl_Menu (sMenuName, sCaption, iModule, iSubModule, bIsGroup, bIsActive) VALUES (@sMenuName, @sCaption, @iModule, @iSubModule, @bIsGroup, @bIsActive)";

        //        cmd = new SqlCommand(strQry, oCon);
        //        cmd.Parameters.AddWithValue("@sMenuName", oMenu.MenuName.Replace(" ", ""));     //ie ActionMethod Name which cant be change.
        //        cmd.Parameters.AddWithValue("@sCaption", oMenu.MenuName);
        //        cmd.Parameters.AddWithValue("@iModule", oMenu.Module);
        //        cmd.Parameters.AddWithValue("@iSubModule", oMenu.SubModule);
        //        cmd.Parameters.AddWithValue("@bIsGroup", oMenu.IsGroup);
        //        cmd.Parameters.AddWithValue("@bIsActive", oMenu.IsActive);
        //        cmd.ExecuteNonQuery();
        //        oSaveStatus.iStatus = 1;
        //        return oSaveStatus;
        //    }
        //}

        public SaveStatus SaveMenu(Menu oMenu)
        {
            int iNewMenuId = 0;     
            using (oCon = ConnectionHelper.Stablish)
            {
                strQry = "SELECT COUNT(*) + 1 [NewMenuId] FROM tbl_Menu";
                cmd = new SqlCommand(strQry, oCon);
                iNewMenuId = Convert.ToInt32(cmd.ExecuteScalar());

                strQry = @"INSERT INTO tbl_Menu (iMenuId, sMenuName, sCaption, iModule, iSubModule, bIsGroup, bIsActive) VALUES 
                        (@iMenuId, @sMenuName, @sCaption, @iModule, @iSubModule, @bIsGroup, @bIsActive)";
                cmd.CommandText = strQry;
                cmd.Parameters.AddWithValue("@iMenuId", iNewMenuId);     //ie ActionMethod Name which cant be change.           
                cmd.Parameters.AddWithValue("@sMenuName", oMenu.MenuName.Replace(" ", ""));     //ie ActionMethod Name which cant be change.    
                cmd.Parameters.AddWithValue("@sCaption", oMenu.MenuName);//Menu name changable              
                cmd.Parameters.AddWithValue("@iModule", oMenu.Module);
                cmd.Parameters.AddWithValue("@iSubModule", oMenu.SubModule);
                cmd.Parameters.AddWithValue("@bIsGroup", oMenu.IsGroup);
                cmd.Parameters.AddWithValue("@bIsActive", oMenu.IsActive);
                cmd.ExecuteNonQuery();
                oSaveStatus.iStatus = 1;
            }            
            return oSaveStatus;        
        }
        public Menu[] LoadMenu(int iModule)
        {
            string strFilter = "";
            switch ((Modules)iModule)
            {
                case Modules.Admin:
                    strFilter = "";
                    break;
                case Modules.Teacher:
                    strFilter = @"WHERE iModule <> 1 AND iSubModule <> 1 AND iModule <> 3 AND iSubModule <> 3";
                    break;
                case Modules.Account:
                    strFilter = "WHERE iModule = 3 OR iSubModule = 3";
                    break;
                default:
                    strFilter = "WHERE iModule = 4 OR iSubModule = 4";
                    break;
            }
            strQry = string.Format("SELECT iMenuId, sMenuName, ISNULL(sCaption,'')[sCaption], iModule, iSubModule, bIsGroup, bIsActive FROM tbl_Menu {0}",
                strFilter);
            return oCoreExc.LoadMenu(strQry);
        }

        public SaveStatus DeleteMenus(IdValuePair oMenu)
        {
            strQry = string.Format(@"DELETE FROM tbl_Menu WHERE iMenuId = {0}", oMenu.Id);
            return oCommonExc.DeleteOrUpdate(strQry);
        }

        ///Common Model
        public string LoadCalender()
        {
            DateTime dt = DateTime.Now;
            int iMon = dt.Month;
            return "";
        }
    }
}
