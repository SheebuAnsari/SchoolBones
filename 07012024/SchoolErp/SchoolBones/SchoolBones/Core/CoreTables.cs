namespace SchoolConfiguration.Core
{
    public class CoreTables
    {

        static string sTable = string.Empty;

        public static string CreateLicenseInfo(string strTableName)
        {
            sTable = string.Format(@"IF Not EXISTS (SELECT table_Name FROM INFORMATION_SCHEMA.TABLES WHERE table_Name = '{0}')
                        BEGIN
	                        CREATE TABLE [dbo].[{0}] (
		                        iLicenseInfo int IDENTITY (1,1) PRIMARY KEY, 
		                        sDbName nvarchar (255), 
		                        sProvider nvarchar (255),  
		                        sKey nvarchar(255),
		                        iValidity int,
		                        iInstallationDate bigint,
		                        iExpiryDate bigint,
	                        )
                        END",
                        strTableName);
            return sTable;
        }
        public static string CreateSchoolInfo(string strTableName)
        {
            sTable = string.Format(@"IF Not EXISTS (SELECT table_Name FROM INFORMATION_SCHEMA.TABLES WHERE table_Name = '{0}')
                        BEGIN
	                        CREATE TABLE [dbo].[{0}] (
		                        iSchoolId int IDENTITY(1,1) PRIMARY KEY,    
	                            sSchoolName nvarchar (250),
	                            sPrincipal nvarchar(250),
                                sEstablish nvarchar(250),
	                            iRegistrationDate bigint,
                                sRegistrationNo nvarchar (250),
                                sAddress nvarchar(250),
	                            iContact bigint,
	                        )
                        END",
            strTableName);

            return sTable;
        }
        public static string CreateMenu(string strTableName)
        {
            sTable = string.Format(@"IF Not EXISTS (SELECT table_Name FROM INFORMATION_SCHEMA.TABLES WHERE table_Name = '{0}')
                                BEGIN
	                                CREATE TABLE [dbo].[{0}] (
		                                iMenuId int IDENTITY(1,1) PRIMARY KEY,    
	                                    sMenuName nvarchar(255),
                                        sCaption nvarchar(255),
	                                    iModule int,
	                                    iSubModule int,
	                                    bIsGroup bit,
	                                    bIsActive bit,
	                                )
                                END",
                        strTableName);
            return sTable;
        }
        public static string CreateUsers(string strTableName)
        {
            sTable = string.Format(@"IF Not EXISTS (SELECT table_Name FROM INFORMATION_SCHEMA.TABLES WHERE table_Name = '{0}')
                                BEGIN
	                                CREATE TABLE [dbo].[{0}] (
		                                iRegistrationId int IDENTITY (1,1) PRIMARY KEY, 
                                        sUserName NVARCHAR (255), 
                                        sLoginName NVARCHAR (255) unique Not null,  
                                        sUserPassword NVARCHAR (255),
                                        iUserType INT
	                                )
                                END",
                        strTableName);
            return sTable;
        }
        
    }
}
