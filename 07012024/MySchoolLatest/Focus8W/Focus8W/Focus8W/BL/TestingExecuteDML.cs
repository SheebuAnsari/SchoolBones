using DomLibrary;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Focus8W.BL
{
    public class TestingExecuteDML
    {
        SqlDataReader reader = null;
        SqlCommand cmd = null;
        SqlConnection oCon = null;

        public FeeDetails LoadFeesDetailsDML(string strQuery, FeeDetails objFeeDetails)
        {
            List<FeesInfo> lstFeesInfo = new List<FeesInfo>();
            using (oCon = ConnectionHelper.Stablish)
            {
                using (cmd = new SqlCommand(strQuery, oCon))
                {
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FeesInfo oFeesInfo = new FeesInfo();
                            oFeesInfo.Date = 0;
                            oFeesInfo.MonthlyFeeId = Convert.ToInt32(reader["iMonthlyFeeId"]);
                            oFeesInfo.ForMonth = Convert.ToInt32(reader["iForMonth"]);
                            oFeesInfo.MonthlyFee = Convert.ToInt32(reader["iFee"]);
                            oFeesInfo.Settlement = Convert.ToInt32(reader["iSettlement"]);
                            oFeesInfo.Status = Convert.ToString(reader["Status"]);
                            //oFeesInfo.YearlyFee = Convert.ToInt32(reader["iYearlyFee"]);
                            oFeesInfo.Concession = Convert.ToInt32(reader["iConcession"]);
                            lstFeesInfo.Add(oFeesInfo);
                        }
                        objFeeDetails.ArrFeesInfo = lstFeesInfo.ToArray();
                    }
                }
                return objFeeDetails;
            }
        }
        public PrintInvoice GetPrintObject(string strQuery)
        {
            PrintInvoice oPrintInvoice = new PrintInvoice();
            

            using (oCon = ConnectionHelper.Stablish)
            {
                using (cmd = new SqlCommand(strQuery, oCon))
                {
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            oPrintInvoice.iRegistrationId = Convert.ToInt32(reader["iRegistrationId"]);
                            oPrintInvoice.iStdId = Convert.ToInt32(reader["iStdId"]);
                            oPrintInvoice.UserName = Convert.ToString(reader["sUserName"]);
                            oPrintInvoice.iCurrentClass = Convert.ToInt32(reader["iCurrentClass"]);
                            oPrintInvoice.iDate = Convert.ToInt32(reader["iDate"]);
                            oPrintInvoice.iFeeAmount = Convert.ToInt32(reader["iFeeAmount"]);
                            oPrintInvoice.iPerMonthFee = Convert.ToInt32(reader["iPerMonthFee"]);
                            oPrintInvoice.iAdvanceFee = Convert.ToInt32(reader["iAdvanceFee"]);
                            oPrintInvoice.iForMonth = Convert.ToInt32(reader["iForMonth"]);
                            oPrintInvoice.iMonthlyFeeId = Convert.ToInt32(reader["iMonthlyFeeId"]);
                        }
                    }
                }
                return oPrintInvoice;
            }
        }
        public List<StudentDetails> LoadClasswiseStudentsDML(string strQuery)
        {
            StudentDetails oStudentDetails = null;
            List<StudentDetails> lstStudents = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                using (cmd = new SqlCommand(strQuery, oCon))
                {
                    reader = cmd.ExecuteReader();
                    lstStudents = new List<StudentDetails>();

                    while (reader.Read())
                    {
                        oStudentDetails = new StudentDetails();
                        oStudentDetails.EnrollmentNo = Convert.ToInt32(reader["iEnrollmentNo"]);
                        oStudentDetails.StdId = Convert.ToInt32(reader["iStdId"]);
                        oStudentDetails.StdName = Convert.ToString(reader["sStdName"]);
                        lstStudents.Add(oStudentDetails);
                    }
                    reader.Close();
                    return lstStudents;
                }
            }
        }
        public FeeDetails LoadStudentDetailsDML(string strQuery)
        {
            StudentDetails oStudentDetails = null;
            List<IdValuePair> arrMonthsAndFee = new List<IdValuePair>();
            FeeDetails objFeeDetails = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                using (cmd = new SqlCommand(strQuery, oCon))
                {
                    cmd = new SqlCommand(strQuery, oCon);
                    using (reader = cmd.ExecuteReader())
                    {
                        objFeeDetails = new FeeDetails();
                        while (reader.Read())
                        {
                            if (oStudentDetails == null)
                            {
                                oStudentDetails = new StudentDetails();
                                objFeeDetails.StudentFeesId = Convert.ToInt32(reader["iStudentFeesId"]);
                                oStudentDetails.EnrollmentNo = Convert.ToInt32(reader["iRegistrationId"]);
                                oStudentDetails.StdId = Convert.ToInt32(reader["iStdId"]);
                                oStudentDetails.StdName = Convert.ToString(reader["sUserName"]);
                                oStudentDetails.Class = Convert.ToInt32(reader["iCurrentClass"]);
                                oStudentDetails.Year = Convert.ToInt32(reader["iYear"]);
                                objFeeDetails.MaxFee = Convert.ToInt32(reader["iPerMonthFee"]);
                                objFeeDetails.StdInfo = oStudentDetails;


                            }
                            objFeeDetails.Wallet = Convert.ToInt32(reader["iSettlement"]);
                        }
                        return objFeeDetails;
                    }
                }
            }
        }
        //public List<AdmissionDetails> TLoadAllStudentsDML(string strQuery)
        //{
        //    List<AdmissionDetails> lstAdmissionDetails = new List<AdmissionDetails>();
        //    AdmissionDetails oAdmissionDetails = null;

        //    SqlDataReader reader = null;
        //    cmd = new SqlCommand();
        //    cmd.CommandText = strQuery;
        //    cmd.Connection = ConnectionHelper.ConOpen();

        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            oAdmissionDetails = new AdmissionDetails();
        //            oAdmissionDetails.RegistrationId = Convert.ToInt32(reader["iRegistrationId"]);
        //            oAdmissionDetails.StdName = Convert.ToString(reader["sUserName"]);
        //            oAdmissionDetails.Address = Convert.ToString(reader["sAddress"]);
        //            oAdmissionDetails.City = Convert.ToString(reader["sCity"]);
        //            oAdmissionDetails.Contact = Convert.ToInt64(reader["iContact"]);
        //            oAdmissionDetails.AdmissionInClass = Convert.ToInt32(reader["iAdmissionInClass"]);
        //            oAdmissionDetails.DOB = Convert.ToInt32(reader["iDOB"]);
        //            oAdmissionDetails.IsActive = Convert.ToBoolean(reader["bIsActive"]);
        //            oAdmissionDetails.AdmissionDate = Convert.ToInt32(reader["iAdmissionDate"]);
        //            if (reader["biStudentImage"] != DBNull.Value)
        //            {
        //                oAdmissionDetails.StudentImage = (byte[])reader["biStudentImage"];
        //            }
        //            lstAdmissionDetails.Add(oAdmissionDetails);
        //        }
        //    }
        //    reader.Close();
        //    return lstAdmissionDetails;
        //}

        //public Registration[] TLoadRegisteredStudentsDML(string strQuery)
        //{
        //    List<Registration> lstRegistration = new List<Registration>();
        //    Registration oRegistration = null;

        //    SqlDataReader reader = null;
        //    cmd = new SqlCommand();
        //    cmd.CommandText = strQuery;
        //    cmd.Connection = ConnectionHelper.ConOpen();

        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            oRegistration = new Registration();
        //            oRegistration.RegistrationId = Convert.ToInt32(reader["iRegistrationId"]);
        //            oRegistration.UserName = Convert.ToString(reader["sUserName"]);
        //            oRegistration.UserType = Convert.ToInt32(reader["iUserType"]);
        //            lstRegistration.Add(oRegistration);
        //        }
        //    }
        //    reader.Close();
        //    return lstRegistration.ToArray();
        //}
        
    }
}