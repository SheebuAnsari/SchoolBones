using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolConfiguration
{
    class DefaultTables
    {
        private static string GetCoreTables()
        {
            return @"If NOT EXISTS (select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'tbl_SchoolInfo')
                    BEGIN
                        CREATE TABLE [dbo].[tbl_SchoolInfo](
	                            [iSchoolId] [int] IDENTITY(1,1) NOT NULL,
	                            [sSchoolName] [nvarchar](250) NULL,
	                            [sPrincipal] [nvarchar](250) NULL,
	                            [sEstablish] [nvarchar](250) NULL,
	                            [iRegistrationDate] [int] NULL,
	                            [sRegistrationNo] [nvarchar](250) NULL,
	                            [sAddress] [nvarchar](250) NULL,
	                            [iContact] [bigint] NULL
                            )
                    END
                    If NOT EXISTS (select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'tbl_LicenseInfo')
                    BEGIN
                        CREATE TABLE [dbo].[tbl_LicenseInfo](
	                        [iLicenseInfo] [int] IDENTITY(1,1) NOT NULL,
	                        [sDbName] [nvarchar](255) NULL,
	                        [sProvider] [nvarchar](255) NULL,
	                        [sKey] [nvarchar](255) NULL,
	                        [iValidity] [int] NULL,
	                        [iInstallationDate] [int] NULL,
	                        [iExpiryDate] [int] NULL
                        )
                    END
                    If NOT EXISTS (select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'tbl_UserRegistration')
                    BEGIN
                        CREATE TABLE [dbo].[tbl_UserRegistration] 
                        (
                            [iRegistrationId] INT IDENTITY (1,1) PRIMARY KEY, 
                            [sUserName] NVARCHAR (255), 
                            [sLoginName] NVARCHAR (255) UNIQUE NOT NULL,  
                            [sUserPassword] NVARCHAR(255),
                            [iUserType] INT
                        )
                    END
                    If NOT EXISTS (select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'tbl_Menu')
                    BEGIN
                    CREATE TABLE [dbo].[tbl_Menu](
	                    [iMenuId] [int],
	                    [sMenuName] [nvarchar](255) NULL,
	                    [sCaption] [nvarchar](255) NULL,
	                    [iModule] [int] NULL,
	                    [iSubModule] [int] NULL,
	                    [bIsGroup] [bit] NULL,
	                    [bIsActive] [bit] NULL
                    )
                    END
                    If NOT EXISTS (select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'tbl_Students')
                    BEGIN
                    CREATE TABLE [dbo].[tbl_Students](
	                    iRegistrationId int FOREIGN KEY REFERENCES tbl_UserRegistration,
                        --sStdName nvarchar(255),
                        sAddress nvarchar(255),
                        sCity nvarchar(255),
	                    iContact bigint,
	                    iAdmissionInClass int,
	                    iAdmissionDate bigint,
	                    iDOB bigint,
	                    bIsActive bit,
	                    biStudentImage varbinary(MAX)
                    );
                    END
                    If NOT EXISTS (select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'tbl_StudentClasswiseInfo')
                    BEGIN
                    CREATE TABLE [dbo].[tbl_StudentClasswiseInfo](
	                    iStdId int IDENTITY(1,1) PRIMARY KEY,   
	                    iRegistrationId int FOREIGN KEY REFERENCES tbl_UserRegistration,
	                    iCurrentClass int, 
	                    iCurrentYear int, 
	                    bIsActive bit, 
	                    bResultPrepared bit
                    );
                    END

                    If NOT EXISTS (select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'tbl_StudentAttendance')
                    BEGIN
                    CREATE TABLE [dbo].[tbl_StudentAttendance] (
	                    iStdId int FOREIGN KEY REFERENCES tbl_StudentClasswiseInfo,
	                    iRegistrationId int FOREIGN KEY REFERENCES tbl_UserRegistration,
	                    iClass int,
	                    iDate bigint,
	                    iStatus int --0: Absent 1: Present 3: onLeave
                    );
                    END

";
        }

        private static string GetAdminTables()
        {
            return "";
        }
        private static string GetTeacherTables()
        {
            return "";
        }
        private static string GetAccountTables()
        {
            return "";
        }
        private static string GetStudentTables()
        {
            return "";
        }
        public static void CreateTables(string sDbName, ref string sError)
        {
            StringBuilder sbTables = new StringBuilder();

            SqlConnection oCon = null;

            sbTables.Append(GetCoreTables());
            sbTables.Append(GetAdminTables());
            sbTables.Append(GetTeacherTables());
            sbTables.Append(GetAccountTables());
            sbTables.Append(GetStudentTables());


            try
            {
                //using (oCon = new SqlConnection(ConnectionHelper.GetConnectionString))
                using (oCon = new SqlConnection(ConnectionHelper.GetConString))
                {
                    oCon.Open();

                    using (SqlCommand cmd = new SqlCommand(sbTables.ToString(), oCon))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
        }
    }
}
