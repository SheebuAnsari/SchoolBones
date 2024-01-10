using DomLibrary;
using SchoolBones.Common;
using SchoolConfiguration;
using System;
using System.Data.SqlClient;

namespace Focus8W.BL
{
    public class TestingDML
    {
        string strQry = "";
        SqlCommand cmd = null;
        SqlConnection oCon = null;
        SaveStatus oSaveStatus = new SaveStatus();

        CommonExecuteDML oCommonExc = new CommonExecuteDML();
        TestingExecuteDML oTestingExc = new TestingExecuteDML();

        public SaveStatus SaveFeeDetails(FeeInfo oFeeInfo) //SaveMonthlyFees
        {
            int iStudentFeesId = -1;
            int iMonthlyFee = -1;
            using (oCon = ConnectionHelper.Stablish)
            {
                if (oFeeInfo != null)
                {
                    strQry = string.Format(@"SELECT iStudentFeesId FROM tbl_StudentFees where iRegistrationId = {0} or iStdId = {1}",
                      oFeeInfo.RegistrationId, oFeeInfo.StdId, oFeeInfo.Class);
                    cmd = new SqlCommand(strQry, oCon);
                    iStudentFeesId = Convert.ToInt32(cmd.ExecuteScalar());

                    if (iStudentFeesId == 0)
                    {
                        strQry = string.Format(@"INSERT INTO tbl_StudentFees(iRegistrationId, iStdId, iClass)values(@iRegistrationId, @iStdId, @iClass) SELECT @@IDENTITY");
                        cmd = new SqlCommand(strQry, oCon);
                        cmd.Parameters.AddWithValue("@iRegistrationId", oFeeInfo.RegistrationId);
                        cmd.Parameters.AddWithValue("@iStdId", oFeeInfo.StdId);
                        cmd.Parameters.AddWithValue("@iClass", oFeeInfo.Class);
                        iStudentFeesId = Convert.ToInt32(cmd.ExecuteScalar());
                    }


                    if (iStudentFeesId > 0)
                    {
                        //strQry = string.Format(@"DELETE FROM tbl_MonthlyFee WHERE iStudentFeesId = @iStudentFeesId AND iForMonth = @iForMonth",
                        //            iStudentFeesId, oFeeInfo.ForMonth);
                        //cmd = new SqlCommand(strQry, oCon);
                        //cmd.Parameters.AddWithValue("@iStudentFeesId", iStudentFeesId);
                        //cmd.Parameters.AddWithValue("@iForMonth", oFeeInfo.ForMonth);
                        //cmd.ExecuteNonQuery();

                        strQry = string.Format(@"INSERT INTO tbl_MonthlyFee(iStudentFeesId, iDate, iForMonth, iFee, iConcession)
                                                    values (@iStudentFeesId, @iDate, @iForMonth, @iFee, @iConcession) SELECT @@IDENTITY");
                        cmd = new SqlCommand(strQry, oCon);
                        cmd.Parameters.AddWithValue("@iStudentFeesId", iStudentFeesId);
                        cmd.Parameters.AddWithValue("@iDate", oFeeInfo.Date);
                        cmd.Parameters.AddWithValue("@iForMonth", oFeeInfo.ForMonth);
                        cmd.Parameters.AddWithValue("@iFee", oFeeInfo.MonthlyFee);
                        cmd.Parameters.AddWithValue("@iConcession", 0);
                        iMonthlyFee = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    strQry = string.Format(@"SELECT iWalletId FROM tbl_Wallet WHERE iMonthlyFeeId = {0}", iMonthlyFee);
                    cmd = new SqlCommand(strQry, oCon);
                    int iWalletId = Convert.ToInt32(cmd.ExecuteScalar());
                    if (iWalletId == 0)
                    {
                        strQry = string.Format(@"INSERT INTO tbl_Wallet (iMonthlyFeeId, iSettlement) VALUES (@iMonthlyFeeId, @iSettlement)");
                        cmd = new SqlCommand(strQry, oCon);
                        cmd.Parameters.AddWithValue("@iMonthlyFeeId", iMonthlyFee);
                        cmd.Parameters.AddWithValue("@iSettlement", oFeeInfo.Settlement);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        strQry = string.Format(@"UPDATE tbl_Wallet SET iSettlement = @iSettlement WHERE iMonthlyFeeId = @iMonthlyFeeId");
                        cmd = new SqlCommand(strQry, oCon);
                        cmd.Parameters.AddWithValue("@iMonthlyFeeId", iMonthlyFee);
                        cmd.Parameters.AddWithValue("@iSettlement", oFeeInfo.Settlement);
                        cmd.ExecuteNonQuery();
                    }
                    oSaveStatus.iStatus = 1;
                }
                return oSaveStatus;
            }
        }
        public FeeDetails LoadFeeDetails(int iEnrollmentno, int iRollno)
        {
            FeeDetails oFeeDetails = new FeeDetails();
            strQry = string.Format(@"select c.iStudentFeesId, a.iRegistrationId ,a.iStdId ,b.sUserName ,a.iCurrentClass ,dbo.GETDATEPART('y' ,a.iCurrentYear)[iYear] ,
                                            d.iPerMonthFee, ISNULL(ccc.iSettlement, 0)[iSettlement]
                                            FROM tbl_StudentClasswiseInfo a
                                            JOIN tbl_UserRegistration b on a.iRegistrationId=b.iRegistrationId
                                            JOIN tbl_StudentFees c on c.iRegistrationId = a.iRegistrationId or c.iStdId = a.iStdId
                                            join tbl_MonthlyFee cc on cc.iStudentFeesId=c.iStudentFeesId
                                            LEFT JOIN tbl_Wallet ccc on ccc.iMonthlyFeeId = cc.iMonthlyFeeId
                                            JOIN tbl_FeeStructure d on d.iClass = a.iCurrentClass
                                            where a.iRegistrationId = {0} or a.iStdId= {1}",
                                            iEnrollmentno, iRollno);
            oFeeDetails = oTestingExc.LoadStudentDetailsDML(strQry);
            if (oFeeDetails != null)
            {
                strQry = string.Format(@"SELECT a.iMonthlyFeeId, a.iForMonth, c.iPerMonthFee, a.iFee, (a.iFee - c.iPerMonthFee)[iSettlement], a.iConcession,
                                        CASE 
	                                        WHEN (a.iFee - c.iPerMonthFee) = 0 THEN 'Full paid' 
	                                        WHEN (a.iFee - c.iPerMonthFee) > 0 THEN 'Advance' 
	                                        ELSE 'Partially' 
                                        END [Status] 
                                        FROM tbl_MonthlyFee a
                                        JOIN tbl_StudentFees b ON b.iStudentFeesId = a.iStudentFeesId
                                        JOIN tbl_FeeStructure c ON c.iClass = b.iClass
                                        WHERE a.iStudentFeesId = {0}",
                            oFeeDetails.StudentFeesId);

                oFeeDetails = oTestingExc.LoadFeesDetailsDML(strQry, oFeeDetails);
            }
            else
            {
                strQry = string.Format(@"select 0[iStudentFeesId], a.iRegistrationId ,a.iStdId ,b.sUserName ,a.iCurrentClass ,dbo.GETDATEPART('y' ,a.iCurrentYear)[iYear] ,d.iPerMonthFee ,0[iSettlement]
                                        FROM tbl_StudentClasswiseInfo a
                                        JOIN tbl_UserRegistration b on a.iRegistrationId=b.iRegistrationId
                                        JOIN tbl_FeeStructure d on d.iClass = a.iCurrentClass
                                        where a.iRegistrationId = {0} or a.iStdId= {1}",
                                        iEnrollmentno, iRollno);
                oFeeDetails = oTestingExc.LoadStudentDetailsDML(strQry);
            }
            return oFeeDetails;
        }
        public PrintInvoice GetPrintObject(int iEnrollmentNo, int iStdId, int iMonthlyFeeId)
        {
            strQry = string.Format(@"select a.iRegistrationId, b.iStdId,  a.sUserName,  b.iCurrentClass, d.iDate, d.iFee [iFeeAmount], MaxFee.iPerMonthFee [iPerMonthFee], d.iFee-MaxFee.iPerMonthFee [iAdvanceFee], d.iForMonth, d.iMonthlyFeeId
                                    FROM tbl_UserRegistration a
                                    join tbl_StudentClasswiseInfo b on a.iRegistrationId = b.iRegistrationId
                                    join tbl_FeeStructure MaxFee on MaxFee.iClass = b.iCurrentClass
                                    join tbl_StudentFees c on c.iStdId = b.iStdId
                                    join tbl_MonthlyFee d on d.iStudentFeesId = c.iStudentFeesId
                                    where (a.iRegistrationId = {0} or b.iStdId = {1}) and d.iMonthlyFeeId = {2}", iEnrollmentNo, iStdId, iMonthlyFeeId);
            return oTestingExc.GetPrintObject(strQry);
        }

        //public SaveStatus SchoolRegistration()//SchoolDetails oSchoolDetails)//, UserRegistration oUserRegistration)
        //{
        //    //First Create a new db
        //    CreateDatabase("TestDb");
        //    //Use CreateDatabase
        //    //UseCreatedDatabase("TestDb");


        //    //Create Tables
        //    CreateTables("TestDb");
        //    oSaveStatus.iStatus = 1;
        //    return oSaveStatus;
        //}

        //private static void CreateDatabase(string sDbName)
        //{
        //    DbFactory oDbFactory = new DbFactory();

        //    SqlConnection oCon = null;
        //    using (oCon = new SqlConnection(ConnectionHelper.MasterConnectionString))
        //    {
        //        oCon.Open();
        //        string strQry = string.Format("IF EXISTS (SELECT name FROM sys.databases WHERE name = '{0}') SELECT 1 ELSE SELECT 0", sDbName);
        //        using (SqlCommand cmd = new SqlCommand(strQry, oCon))
        //        {
        //            int value = (int)cmd.ExecuteScalar();

        //            if (value != 1)
        //            {
        //                //Database doesn't exist
        //                strQry = string.Format("CREATE DATABASE [{0}]", sDbName);
        //                try
        //                {
        //                    cmd.CommandText = strQry;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                catch
        //                {

        //                }
        //            }
        //        }
        //    }
        //}
        //private static void CreateTables(string sDbName)
        //{
        //    SqlConnection oCon = null;

        //    string tableCreateScript = @"CREATE TABLE [dbo].[Article]
        //                                (
        //                                    [ItemName] [nvarchar](50) NOT NULL,
        //                                    [Barcode] [nvarchar](50) NOT NULL,
        //                                    [Price] [money] NOT NULL
        //                                )";

        //    try
        //    {
        //        using (oCon = ConnectionHelper.GetNewConnection(sDbName))
        //        {
        //            oCon.Open();
        //            using (SqlCommand cmd = new SqlCommand(tableCreateScript, oCon))
        //            {
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}


        //public List<AdmissionDetails> LoadAllStudents()
        //{
        //    strQry = string.Format(@"SELECT DISTINCT a.iRegistrationId, b.sUserName, sAddress, sCity, iContact, iAdmissionInClass, iDOB, c.bIsActive, iAdmissionDate, biStudentImage 
        //                                FROM tbl_Students a
        //		join tbl_UserRegistration b on a.iRegistrationId =b.iRegistrationId
        //                                JOIN tbl_StudentClasswiseInfo c ON c.iRegistrationId = b.iRegistrationId
        //                                ORDER BY 1");
        //    return oTestingExc.TLoadAllStudentsDML(strQry);
        //}




        //public SaveStatus SaveTeacherDetails(TeacherDetails oTeacherDetails)
        //{
        //    strQry = string.Format("Delete from tbl_Teachers where iRegistrationId = {0}", oTeacherDetails.RegistrationId);
        //    oCommonExc.DeleteOrUpdate(strQry);

        //    strQry = string.Format(@"INSERT INTO tbl_Teachers(iRegistrationId, bIntMobile, sQualification, iJoiningDate, iExperiance, bActive) 
        //                                VALUES (@iRegistrationId, @bIntMobile, @sQualification, @iJoiningDate, @iExperiance, @bActive)");

        //    cmd = new SqlCommand(strQry, oCon);
        //    cmd.Parameters.AddWithValue("@iRegistrationId", oTeacherDetails.RegistrationId);
        //    cmd.Parameters.AddWithValue("@bIntMobile", oTeacherDetails.Mobile);
        //    cmd.Parameters.AddWithValue("@sQualification", oTeacherDetails.Qualification);
        //    cmd.Parameters.AddWithValue("@iJoiningDate", oTeacherDetails.JoiningDate);
        //    cmd.Parameters.AddWithValue("@iExperiance", oTeacherDetails.Experiance);
        //    cmd.Parameters.AddWithValue("@bActive ", oTeacherDetails.Active);
        //    oSaveStatus = oCommonExc.Save(cmd);
        //    return oSaveStatus;
        //}
    }
}