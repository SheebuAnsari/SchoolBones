using DomLibrary;
using SchoolBones.Common;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SchoolBones.Account
{
    public class AccountsExecuteDML
    {
        SaveStatus oSaveStatus = new SaveStatus();
        CommonExecuteDML oCommonExc = new CommonExecuteDML();

        SqlCommand cmd = null;
        SqlDataReader reader = null;
        SqlConnection oCon = null;

        #region DefineSalary
        public TeacherSalary[] LoadSalaryDML(string strQry)
        {
            TeacherSalary oTeacherSalary = null;
            List<TeacherSalary> lstTeacherSalary = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        lstTeacherSalary = new List<TeacherSalary>();
                        while (reader.Read())
                        {
                            oTeacherSalary = new TeacherSalary();

                            oTeacherSalary.RegId = Convert.ToInt32(reader["iRegistrationId"]);
                            oTeacherSalary.Name = Convert.ToString(reader["sUserName"]);
                            oTeacherSalary.MonthlySalary = Convert.ToInt32(reader["iPerMonthSalary"]);
                            lstTeacherSalary.Add(oTeacherSalary);
                        }
                    }
                }
            }
            if (lstTeacherSalary == null)
            {
                return null;
            }
            return lstTeacherSalary.ToArray();
        }
        #endregion

        #region Fees
        public FeesStructure[] LoadFeesStructureDML(string strQry)
        {
            FeesStructure oFeesStructure = null;
            List<FeesStructure> lstFeesStructure = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        lstFeesStructure = new List<FeesStructure>();
                        while (reader.Read())
                        {
                            oFeesStructure = new FeesStructure();

                            oFeesStructure.FeeStructureId = Convert.ToInt32(reader["iFeeStructureId"]);
                            oFeesStructure.Class = Convert.ToInt32(reader["iClass"]);
                            oFeesStructure.Year = Convert.ToInt32(reader["iYear"]);
                            oFeesStructure.MonthlyFee = Convert.ToInt32(reader["iPerMonthFee"]);
                            oFeesStructure.YearlyFee = Convert.ToInt32(reader["iPerYearFee"]);

                            lstFeesStructure.Add(oFeesStructure);
                        }
                    }
                }
            }
            if (lstFeesStructure == null)
            {
                return null;
            }
            return lstFeesStructure.ToArray();
        }
        #endregion

        #region Fees
        //public FeesCollection[] LoadFeesCollectionDML(string strQuery)
        //{
        //    FeesCollection oFeesCollection = null;
        //    List<FeesCollection> lstFeesCollection = new List<FeesCollection>();

        //    cmd = new SqlCommand();
        //    cmd.CommandText = strQuery;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = ConnectionHelper.ConOpen();

        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        oFeesCollection = new FeesCollection();
        //        while (reader.Read())
        //        {
        //            oFeesCollection.Class = Convert.ToInt32(reader["Class"]);
        //            oFeesCollection.MaxAmt = Convert.ToInt32(reader["MaxAmt"]);
        //            oFeesCollection.PaidAmt = Convert.ToInt32(reader["PaidAmt"]);
        //            oFeesCollection.PendingAmt = Convert.ToInt32(reader["PendingAmt"]);
        //            lstFeesCollection.Add(oFeesCollection);
        //        }
        //    }
        //    reader.Close();
        //    return lstFeesCollection.ToArray();
        //}

        //public FeeDetails LoadStudentDetailsDML(string strQuery)
        //{
        //    StudentDetails oStudentDetails = null;
        //    List<IdValuePair> arrMonthsAndFee = new List<IdValuePair>();
        //    FeeDetails objFeeDetails = null;

        //    cmd = new SqlCommand();
        //    cmd.CommandText = strQuery;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = ConnectionHelper.ConOpen();

        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        objFeeDetails = new FeeDetails();
        //        while (reader.Read())
        //        {
        //            if (oStudentDetails == null)
        //            {
        //                oStudentDetails = new StudentDetails();
        //                oStudentDetails.EnrollmentNo = Convert.ToInt32(reader["iEnrollmentNo"]);
        //                oStudentDetails.StdId = Convert.ToInt32(reader["iStdId"]);
        //                oStudentDetails.StdName = Convert.ToString(reader["sStdName"]);
        //                oStudentDetails.Class = Convert.ToInt32(reader["iCurrentClass"]);
        //                oStudentDetails.Year = Convert.ToInt32(reader["iYear"]);
        //                objFeeDetails.MaxFee = Convert.ToInt32(reader["iMonthlyFee"]);
        //                objFeeDetails.StudentFeesId = Convert.ToInt32(reader["iStudentFeesId"]);
        //                objFeeDetails.StdInfo = oStudentDetails;
        //            }
        //        }
        //    }
        //    reader.Close();
        //    return objFeeDetails;
        //}
        //public FeeDetails LoadFeesDetailsDML(string strQuery, FeeDetails objFeeDetails)
        //{
        //    List<FeesInfo> lstFeesInfo = new List<FeesInfo>();

        //    using (cmd = new SqlCommand(strQuery, ConnectionHelper.ConOpen()))
        //    {
        //        using (reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                FeesInfo oFeesInfo = new FeesInfo();
        //                oFeesInfo.Date = 0;
        //                oFeesInfo.ForMonth = Convert.ToInt32(reader["iForMonth"]);
        //                oFeesInfo.MonthlyFee = Convert.ToInt32(reader["iFee"]);
        //                oFeesInfo.Settlement = Convert.ToInt32(reader["iSettlement"]);
        //                oFeesInfo.YearlyFee = Convert.ToInt32(reader["iYearlyFee"]);
        //                oFeesInfo.Concession = Convert.ToInt32(reader["iConcession"]);
        //                lstFeesInfo.Add(oFeesInfo);
        //            }
        //            objFeeDetails.ArrFeesInfo = lstFeesInfo.ToArray();
        //        }
        //    }
        //    return objFeeDetails;
        //}


        public StudentDetails[] LoadClasswiseStudentsDML(string strQuery)
        {
            StudentDetails oStudentDetails = null;
            List<StudentDetails> lstStudents = null;

            //SqlDataReader reader = null;
            //cmd = new SqlCommand();
            //cmd.CommandText = strQuery;
            //cmd.CommandType = CommandType.Text;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQuery, oCon);
                lstStudents = new List<StudentDetails>();
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        oStudentDetails = new StudentDetails();
                        oStudentDetails.EnrollmentNo = Convert.ToInt32(reader["iRegistrationId"]);
                        oStudentDetails.StdId = Convert.ToInt32(reader["iStdId"]);
                        oStudentDetails.StdName = Convert.ToString(reader["sUserName"]);
                        lstStudents.Add(oStudentDetails);
                    }
                }
            }
            return lstStudents.ToArray();
        }
        #endregion
        public FeeDetails LoadFeeDetailsDML(string strQry)
        {

            StudentDetails oStudentDetails = null;
            List<IdValuePair> arrMonth = new List<IdValuePair>();
            FeeDetails objFeeDetails = new FeeDetails();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        if (oStudentDetails == null)
                        {
                            oStudentDetails = new StudentDetails();

                            oStudentDetails.EnrollmentNo = Convert.ToInt32(reader["iRegistrationId"]);
                            oStudentDetails.StdId = Convert.ToInt32(reader["iStdId"]);
                            oStudentDetails.StdName = Convert.ToString(reader["sUserName"]);
                            oStudentDetails.Class = Convert.ToInt32(reader["iCurrentClass"]);
                            oStudentDetails.Year = Convert.ToInt32(reader["iYear"]);
                            objFeeDetails.StdInfo = oStudentDetails;
                        }
                        arrMonth.Add(new IdValuePair(
                                Id: Convert.ToInt32(reader["iForMonth"]),
                                Value: Convert.ToInt32(reader["iFee"])
                        ));
                    }
                    objFeeDetails.MonthsAndFee = arrMonth.ToArray();
                }
            }
            return objFeeDetails;
        }
        private ColumnInfo[] GetColumn(ref int[] arrColForSum)
        {
            List<int> arrColSum = new List<int>();
            List<ColumnInfo> lstColInfo = new List<ColumnInfo>();

            for (int iCol = 0; iCol < reader.FieldCount; iCol++)
            {
                //MOST IMPORTANT
                //Console.Write(reader.GetName(col).ToString());         // Gets the column name
                //Console.Write(reader.GetFieldType(col).ToString());    // Gets the column type
                //Console.Write(reader.GetDataTypeName(col).ToString()); // Gets the column database type
                

                if(iCol==3 || iCol == 4)
                {
                    arrColSum.Add(iCol);
                }
                lstColInfo.Add(new ColumnInfo(CollId: iCol, CollName: reader.GetName(iCol).ToString()));
            }
            arrColForSum = arrColSum.ToArray();
            return lstColInfo.ToArray();
        }
        private LineData SetTotal(List<LineData> lstLineData, int[] arrColForSum)
        {
            LineData oLineData = null;
            object[] arrObject = null;
            object oValue = null;

            if (lstLineData != null)
            {
                oLineData = new LineData();
                oLineData.RowNo = -1;
                arrObject = new object[lstLineData[0].CellData.Length];

                if (arrColForSum != null)
                {
                    for (int iColSum = 0; iColSum < arrColForSum.Length; iColSum++)
                    {
                        oValue = lstLineData.Sum(a => Convert.ToInt32(a.CellData[arrColForSum[iColSum]]));
                        arrObject[arrColForSum[iColSum]] = oValue;
                    }
                }
                oLineData.CellData = arrObject;
            }
            return oLineData;
        }
        public ReportData LoadFeeCollectionDML(string strQry)
        {
            int[] arrColForSum = null;
            ReportData oReportData = null;
            List<LineData> lstLineData = new List<LineData>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    oReportData = new ReportData();
                    oReportData.Columns = GetColumn(ref arrColForSum);



                    if (reader.HasRows)
                    {
                        int iRowNo = 1;
                        while (reader.Read())
                        {
                            List<object> lstObject = new List<object>();
                            for (int iCol = 0; iCol < reader.FieldCount; iCol++)
                            {
                                lstObject.Add(Convert.ToInt32(reader[iCol]));
                            }
                            LineData oLineData = new LineData();
                            oLineData.RowNo = iRowNo;
                            oLineData.CellData = lstObject.ToArray();

                            lstLineData.Add(oLineData);
                        }
                        lstLineData.Add(SetTotal(lstLineData, arrColForSum));
                        oReportData.LineData = lstLineData.ToArray();
                    }
                }
            }
            return oReportData;
        }

        public TeacherSalary[] LoadSalaryDetailsDML(string strQry)
        {
            TeacherSalary oTeacherSalary = null;
            List<TeacherSalary> lstTeacherSalary = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        lstTeacherSalary = new List<TeacherSalary>();
                        while (reader.Read())
                        {
                            oTeacherSalary  = new TeacherSalary();
                            oTeacherSalary.RegId = Convert.ToInt32(reader["iRegistrationId"]);
                            oTeacherSalary.Name = Convert.ToString(reader["sUserName"]);
                            oTeacherSalary.DayOfMonths = Convert.ToInt32(reader["iDayOfMonths"]);
                            oTeacherSalary.NoOfWorkingDays = Convert.ToInt32(reader["iNoOfWorkingDays"]);
                            oTeacherSalary.DeductionDays = Convert.ToInt32(reader["iDeductionDays"]);
                            oTeacherSalary.PerDaySalary = Convert.ToInt32(reader["iPerDaySalary"]);
                            oTeacherSalary.NetMonthlySalary = Convert.ToInt32(reader["iNetMonthlySalary"]);
                            oTeacherSalary.MaxMonthlySalary = Convert.ToInt32(reader["iMaxMonthlySalary"]);
                            lstTeacherSalary.Add(oTeacherSalary);
                        }
                    }
                }
            }
            if (lstTeacherSalary == null)
            {
                return null;
            }
            return lstTeacherSalary.ToArray();
        }
    }
}
