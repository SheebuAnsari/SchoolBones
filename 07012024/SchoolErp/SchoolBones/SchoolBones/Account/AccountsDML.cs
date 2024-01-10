using DomLibrary;
using SchoolBones.Common;
using SchoolConfiguration;
using System;
using System.Data.SqlClient;

namespace SchoolBones.Account
{
    public class AccountsDML
    {
        string strQuery = "";
        SaveStatus oSaveStatus = new SaveStatus();
        CommonExecuteDML oCommonExc = new CommonExecuteDML();
        AccountsExecuteDML oAccountsExc = new AccountsExecuteDML();

        SqlCommand cmd = null;
        SqlConnection oCon = null;
        SqlTransaction oTrans = null;


        #region DefineSalary
        public TeacherSalary[] LoadSalary(int iYear)
        {
            strQuery = string.Format(@"SELECT b.iRegistrationId, b.sUserName, ISNULL(c.iPerMonthSalary,0)[iPerMonthSalary] from tbl_Teachers a
                                        JOIN tbl_UserRegistration b ON a.iRegistrationId = b.iRegistrationId
                                        LEFT JOIN tbl_Salary c ON c.iRegistrationId = a.iRegistrationId
                                        WHERE a.bActive = 1 And CASE WHEN c.iYear = 0 THEN a.iCurrentYear ELSE a.iCurrentYear END = {0}", 
                                        iYear);
            return oAccountsExc.LoadSalaryDML(strQuery);
        }
        public SaveStatus SaveSalary(TeacherSalary[] arrTeacherSalary)
        {
            try
            {
                if (arrTeacherSalary != null && arrTeacherSalary.Length > 0)
                {
                    using (oCon = ConnectionHelper.Stablish)
                    {
                        for (int iSal = 0; iSal < arrTeacherSalary.Length; iSal++)
                        {
                            oTrans = oCon.BeginTransaction();
                            TeacherSalary oSalary = arrTeacherSalary[iSal];

                            if (oSalary.SalaryId == 0)
                            {
                                strQuery = string.Format(@"DELETE FROM tbl_Salary WHERE iYear = @iYear And iRegistrationId = {0}", oSalary.RegId);
                                cmd = new SqlCommand(strQuery, oCon, oTrans);
                                cmd.Parameters.AddWithValue("@iYear", oSalary.Year);
                                cmd.Parameters.AddWithValue("@iRegistrationId", oSalary.RegId);
                                cmd.ExecuteNonQuery();

                                strQuery = string.Format(@"INSERT INTO tbl_Salary (iRegistrationId,iYear,iPerMonthSalary) VALUES (@iRegistrationId, @iYear, @iPerMonthSalary)");
                                cmd.Parameters.Clear();
                                cmd.CommandText = strQuery;
                                cmd.Parameters.AddWithValue("@iRegistrationId", oSalary.RegId);
                                cmd.Parameters.AddWithValue("@iYear", oSalary.Year);
                                cmd.Parameters.AddWithValue("@iPerMonthSalary", oSalary.MonthlySalary);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                strQuery = string.Format(@"UPDATE tbl_Salary SET iPerMonthSalary = {0} WHERE iSalaryId = {1}",
                                                           oSalary.MonthlySalary,
                                                           oSalary.SalaryId);
                                cmd = new SqlCommand(strQuery, oCon);
                                cmd.ExecuteNonQuery();
                            }
                            oTrans.Commit();
                        }
                        oSaveStatus.iStatus = 1;
                       
                    }
                }
                return oSaveStatus;
            }
            catch (Exception ex)
            {
                oTrans.Rollback();
                return oSaveStatus;
            }
        }
        #endregion

        #region DefineFee
        public FeesStructure[] LoadFeesStructure(int iYear)
        {
            strQuery = string.Format(@"SELECT iFeeStructureId, iClass, iYear, iPerMonthFee, iPerYearFee FROM tbl_FeeStructure Where iYear = {0} ORDER BY iClass", iYear);
            return oAccountsExc.LoadFeesStructureDML(strQuery);
        }
        public SaveStatus SaveFeesStructure(FeesStructure oFeesStructure)
        {
            try
            {
                using (oCon = ConnectionHelper.Stablish)
                {
                    oTrans = oCon.BeginTransaction();

                    if (oFeesStructure.FeeStructureId == 0)
                    {
                        strQuery = string.Format(@"DELETE FROM tbl_FeeStructure WHERE iClass = @iClass");
                        cmd = new SqlCommand(strQuery, oCon, oTrans);
                        cmd.Parameters.AddWithValue("@iClass", oFeesStructure.Class);
                        cmd.ExecuteNonQuery();

                        strQuery = string.Format(@"INSERT INTO tbl_FeeStructure (iClass, iYear, iPerMonthFee, iPerYearFee) VALUES (@iClass, @iYear, @iPerMonthFee, @iPerYearFee)");
                        cmd.Parameters.Clear();
                        cmd.CommandText = strQuery;
                        cmd.Parameters.AddWithValue("@iClass", oFeesStructure.Class);
                        cmd.Parameters.AddWithValue("@iYear", oFeesStructure.Year);
                        cmd.Parameters.AddWithValue("@iPerMonthFee", oFeesStructure.MonthlyFee);
                        cmd.Parameters.AddWithValue("@iPerYearFee", oFeesStructure.YearlyFee);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        strQuery = string.Format(@"UPDATE tbl_FeeStructure SET iPerMonthFee = {0}, iPerYearFee = {1} WHERE iFeeStructureId = {2}",
                                                   oFeesStructure.MonthlyFee,
                                                   oFeesStructure.YearlyFee,
                                                   oFeesStructure.FeeStructureId);
                        cmd = new SqlCommand(strQuery, oCon, oTrans);
                        cmd.ExecuteNonQuery();
                    }
                    oSaveStatus.iStatus = 1;
                    oTrans.Commit();
                }

                return oSaveStatus;
            }
            catch (Exception ex)
            {
                oTrans.Rollback();
                return oSaveStatus;
            }
        }
        public SaveStatus DeleteFeesStructure(int iFeeStructureId)
        {
            strQuery = string.Format(@"Delete from tbl_FeeStructure where iFeeStructureId={0}", iFeeStructureId);
            //cmd = new SqlCommand(strQuery, ConnectionHelper.ConOpen());
            using (oCon = ConnectionHelper.Stablish)
            {
                oSaveStatus.iStatus = cmd.ExecuteNonQuery();
            }
            return oSaveStatus;
        }
        #endregion

        public FeeDetails LoadFeeDetails(int iRegistrationId, int iRollno)
        {
            strQuery = string.Format(@"SELECT a.iRegistrationId, b.iStdId, a.sUserName, b.iCurrentClass, dbo.GETDATEPART('y' ,b.iCurrentYear)[iYear], c.iForMonth, c.iFee
                                        from tbl_UserRegistration a
                                        join tbl_StudentClasswiseInfo b on b.iRegistrationId=a.iRegistrationId
                                        join tbl_Fees c on c.iStdId=b.istdId
                                        where a.iRegistrationId= {0}
                                        or b.iStdId = {1}",
                                        iRegistrationId, iRollno);

            return oAccountsExc.LoadFeeDetailsDML(strQuery);

        }
        public ReportData LoadFeeCollection(int iClass, int iMonth, int iYear)
        {
            strQuery = string.Format(@"SELECT a.iMonthlyFeeid [Monthly Fee id], a.iStudentFeesId [Student Fees Id], a.iForMonth [For Month], a.iFee [Fee Collection], b.isettlement [Settlement] FROM tbl_StudentFees c
                                        JOIN tbl_MonthlyFee a ON a.iStudentFeesId = c.iStudentFeesId
                                        JOIN tbl_Wallet  b ON b.iMonthlyFeeid = a.iMonthlyFeeid
                                        WHERE c.iClass = {0} AND iForMonth = {1} AND dbo.GetDatePart('y', a.iDate) =  0",
                                        iClass, iMonth, iYear);

            return oAccountsExc.LoadFeeCollectionDML(strQuery);

        }

        public SaveStatus SaveFeeDetails(FeeInfo oFeeInfo)
        {
            try
            {
                if (oFeeInfo != null)
                {
                    using (oCon = ConnectionHelper.Stablish)
                    {
                        oTrans = oCon.BeginTransaction();

                        strQuery = string.Format(@"INSERT INTO tbl_Fees (iStdId, iClass, iForMonth, iDate, iYear, iFee)
                                                    VALUES (@iStdId, @iClass, @iForMonth, @iDate, @iYear, @iFee)");

                        cmd = new SqlCommand(strQuery, oCon, oTrans);
                        cmd.Parameters.AddWithValue("@iStdId", oFeeInfo.StdId);
                        cmd.Parameters.AddWithValue("@iClass", oFeeInfo.Class);
                        cmd.Parameters.AddWithValue("@iForMonth", oFeeInfo.ForMonth);
                        cmd.Parameters.AddWithValue("@iDate", oFeeInfo.Date);
                        cmd.Parameters.AddWithValue("@iYear", oFeeInfo.Year);
                        cmd.Parameters.AddWithValue("@iFee", oFeeInfo.MonthlyFee);
                        oCommonExc.CallExecuteNonQuery(cmd);
                        oSaveStatus.iStatus = 1;
                        oTrans.Commit();
                    }
                }
            }
            catch(Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                }
            }
            return oSaveStatus;
        }
        public StudentDetails[] LoadClasswiseStudents(int iClass, int iYear)
        {
            strQuery = string.Format(@"SELECT a.iRegistrationId, b.iStdId, a.sUserName FROM tbl_UserRegistration a
                                        JOIN tbl_StudentClasswiseInfo b ON a.iRegistrationId = b.iRegistrationId
                                        WHERE b.iCurrentClass = {0} AND dbo.GETDATEPART('y' ,b.iCurrentYear) = {1}",
                                                    iClass, iYear);

            return oAccountsExc.LoadClasswiseStudentsDML(strQuery);

        }

        public TeacherSalary[] LoadSalaryDetails(int iMonth, int iYear)
        {
            strQuery = string.Format(@"select a.iRegistrationId, c.sUserName, 30 [iDayOfMonths], COUNT(a.iRegistrationId)[iNoOfWorkingDays] , 30 - COUNT(a.iRegistrationId) [iDeductionDays], 
                                        b.iPerMonthSalary/12 [iPerDaySalary], COUNT(a.iRegistrationId) * b.iPerMonthSalary/12 [iNetMonthlySalary], b.iPerMonthSalary [iMaxMonthlySalary]
                                        from tbl_TeacherAttandence a
                                        join tbl_Salary b on a.iRegistrationId = b.iRegistrationId
                                        join tbl_UserRegistration c on a.iRegistrationId = c.iRegistrationId
                                        join tbl_Teachers d on  a.iRegistrationId = d.iRegistrationId
                                        where dbo.GetDatePart('m',iDate) = {0} and dbo.GetDatePart('y',iDate) = {1} And d.bActive = 1
                                        group by a.iRegistrationId ,c.sUserName,  b.iPerMonthSalary
                                        order by c.sUserName", 
                                        iMonth, iYear);
            return oAccountsExc.LoadSalaryDetailsDML(strQuery);
        }
    }
}
