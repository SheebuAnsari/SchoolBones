using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DomLibrary;

namespace SchoolConfiguration
{
    class FileCRUD
    {
        public static DbInformation ReadDbInformation(string sMasterDbConfig, ref string sError)
        {
            int iNode = 0;
            StringBuilder sb = null;
            DbInformation oDbInformation = null;
            XmlDataDocument xmldoc = null;
            XmlNodeList xmlnode1, xmlnode2, xmlnode3, xmlnode4, xmlnode5;

            sb = new StringBuilder();
            xmldoc = new XmlDataDocument();

            try
            {
                FileStream fs = new FileStream(sMasterDbConfig, FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                xmlnode1 = xmldoc.GetElementsByTagName("Server");
                xmlnode2 = xmldoc.GetElementsByTagName("DbName");
                xmlnode3 = xmldoc.GetElementsByTagName("IntegratedSecurity");
                xmlnode4 = xmldoc.GetElementsByTagName("UserId");
                xmlnode5 = xmldoc.GetElementsByTagName("Password");

                oDbInformation = new DbInformation();
                oDbInformation.ServerName = Convert.ToString(xmlnode1[iNode].ChildNodes.Item(0).InnerText.Trim());
                oDbInformation.DbName = Convert.ToString(xmlnode2[iNode].ChildNodes.Item(0).InnerText.Trim());
                oDbInformation.IntegratedSecurity = Convert.ToString(xmlnode3[iNode].ChildNodes.Item(0).InnerText.Trim()) == "1";
                oDbInformation.UserId = Convert.ToString(xmlnode4[iNode].ChildNodes.Item(0).InnerText.Trim());
                oDbInformation.Password = Convert.ToString(xmlnode5[iNode].ChildNodes.Item(0).InnerText.Trim());

                fs.Close();
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return oDbInformation;
        }
        public static void DeleteDbConfig(ref string sError)
        {
            string sDbConfigPath = "";
            try
            {
                sDbConfigPath = ServerHall.GetFilePathFromServer("DbConfig.xml");
                if (File.Exists(sDbConfigPath))
                {
                    File.Delete(sDbConfigPath);
                }
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }

        }
        public static void CreateDbConfig(string sDbName, ref string sError)
        {
            string sDbConfig = "DbConfig.xml";
            string sDbConfigPath = "";

            string sMasterDbConfig = "MasterDbConfig.xml";
            string sMasterDbConfigPath = "";

            sMasterDbConfigPath = ServerHall.GetFilePathFromServer(sMasterDbConfig);
            sDbConfigPath = ServerHall.GetFilePathFromServer(sDbConfig);
            if (File.Exists(sDbConfigPath))
            {
                File.Delete(sDbConfigPath);
            }
            if (File.Exists(sMasterDbConfigPath))
            {
                File.Copy(sMasterDbConfigPath, sDbConfigPath);
            }
            UpdateTagValue(sDbConfigPath, sDbName);
        }
        private static void UpdateTagValue(string sDbConfigPath, string sDbName)
        {
            XmlDocument doc = null;
            XmlNode root = null;
            XmlNode DbNameNode;

            doc = new XmlDocument();
            doc.Load(sDbConfigPath);
            root = doc.DocumentElement;
            DbNameNode = root.SelectSingleNode("DbName");
            DbNameNode.InnerXml = DbNameNode.InnerText = sDbName;
            doc.Save(sDbConfigPath);
        }

        public static Menu[] ReadMenuXML(ref string sError)
        {
            string sMenuFile = "";
            XmlDataDocument xmldoc = null;
            FileStream fs = null;
            XmlNodeList xmlnode1, xmlnode2, xmlnode3, xmlnode4, xmlnode5;

            sMenuFile = ServerHall.GetFilePathFromServer("MenuItems.xml");
            if (File.Exists(sMenuFile))
            {
                xmldoc = new XmlDataDocument();

                int iNode = 0;
                fs = new FileStream(sMenuFile, FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                xmlnode1 = xmldoc.GetElementsByTagName("iMenuId");
                xmlnode2 = xmldoc.GetElementsByTagName("sMenuName");
                xmlnode3 = xmldoc.GetElementsByTagName("bIsGroup");
                xmlnode4 = xmldoc.GetElementsByTagName("iModule");
                xmlnode5 = xmldoc.GetElementsByTagName("iSubModule");

                List<Menu> lstMenuItem = new List<Menu>();
                for (iNode = 0; iNode <= xmlnode1.Count - 1; iNode++)
                {
                    Menu objMenuItems = new Menu();
                    objMenuItems.MenuId = Convert.ToInt32(xmlnode1[iNode].ChildNodes.Item(0).InnerText.Trim());
                    objMenuItems.MenuName = xmlnode2[iNode].ChildNodes.Item(0).InnerText.Trim();
                    objMenuItems.Caption = xmlnode2[iNode].ChildNodes.Item(0).InnerText.Trim();
                    objMenuItems.IsGroup = Convert.ToBoolean(xmlnode3[iNode].ChildNodes.Item(0).InnerText.Trim());
                    objMenuItems.Module = Convert.ToInt32(xmlnode4[iNode].ChildNodes.Item(0).InnerText.Trim());
                    objMenuItems.SubModule = Convert.ToInt32(xmlnode5[iNode].ChildNodes.Item(0).InnerText.Trim());
                    lstMenuItem.Add(objMenuItems);
                }
                fs.Close();
                return lstMenuItem.ToArray();
            }
            else
            {
                sError = "MenuItems file is missing.";
                return null;
            }
        }
    }
}
