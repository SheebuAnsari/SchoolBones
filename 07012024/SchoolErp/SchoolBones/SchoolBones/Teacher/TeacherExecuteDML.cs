using DomLibrary;
using SchoolBones.Common;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolBones.Teacher
{
    public class TeacherExecuteDML
    {
        SaveStatus oSaveStatus = new SaveStatus();
        CommonExecuteDML oCommonExc = new CommonExecuteDML();

        SqlCommand cmd = null;
        SqlDataReader reader = null;
        SqlConnection oCon = null;

        #region GET
        public List<AdmissionDetails> LoadAllStudentsDML(string strQry)
        {
            List<AdmissionDetails> lstAdmissionDetails = new List<AdmissionDetails>();
            AdmissionDetails oAdmissionDetails = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oAdmissionDetails = new AdmissionDetails();
                            oAdmissionDetails.RegistrationId = Convert.ToInt32(reader["iRegistrationId"]);
                            oAdmissionDetails.StdName = Convert.ToString(reader["sUserName"]);
                            oAdmissionDetails.Address = Convert.ToString(reader["sAddress"]);
                            oAdmissionDetails.City = Convert.ToString(reader["sCity"]);
                            oAdmissionDetails.Contact = Convert.ToInt64(reader["iContact"]);
                            oAdmissionDetails.AdmissionInClass = Convert.ToInt32(reader["iAdmissionInClass"]);
                            oAdmissionDetails.DOB = Convert.ToInt32(reader["iDOB"]);
                            oAdmissionDetails.IsActive = Convert.ToBoolean(reader["bIsActive"]);
                            oAdmissionDetails.AdmissionDate = Convert.ToInt32(reader["iAdmissionDate"]);
                            if (reader["biStudentImage"] != DBNull.Value)
                            {
                                oAdmissionDetails.StudentImage = (byte[])reader["biStudentImage"];
                            }
                            lstAdmissionDetails.Add(oAdmissionDetails);
                        }
                    }
                }
            }
            return lstAdmissionDetails;
        }

        public List<StudentCurrentInfo> LoadStdForDeRegisterDML(string strQry)
        {
            List<StudentCurrentInfo> lstStudentCurrentInfo = new List<StudentCurrentInfo>();
            StudentCurrentInfo oStudentCurrentInfo = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oStudentCurrentInfo = new StudentCurrentInfo();
                            oStudentCurrentInfo.StdId = Convert.ToInt32(reader["iEnrollmentNo"]);
                            oStudentCurrentInfo.StdName = Convert.ToString(reader["sUserName"]);
                            oStudentCurrentInfo.Class = Convert.ToInt32(reader["iCurrentClass"]);
                            oStudentCurrentInfo.Year = Convert.ToInt32(reader["iCurrentYear"]);
                            lstStudentCurrentInfo.Add(oStudentCurrentInfo);
                        }
                    }
                }
            }
            return lstStudentCurrentInfo;
        }
        
        public StudentCurrentInfo[] LoadActiveStudentsDML(string strQry)
        {
            List<StudentCurrentInfo> lstStudentCurrentInfo = null;
            StudentCurrentInfo oStudentCurrentInfo = null;

            lstStudentCurrentInfo = new List<StudentCurrentInfo>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oStudentCurrentInfo = new StudentCurrentInfo();
                            oStudentCurrentInfo.StdId = Convert.ToInt32(reader["iRegistrationId"]);
                            oStudentCurrentInfo.StdId = Convert.ToInt32(reader["iStdId"]);
                            oStudentCurrentInfo.StdName = Convert.ToString(reader["sUserName"]);
                            oStudentCurrentInfo.Class = Convert.ToInt32(reader["iCurrentClass"]);
                            oStudentCurrentInfo.Year = Convert.ToInt32(reader["iCurrentYear"]);
                            lstStudentCurrentInfo.Add(oStudentCurrentInfo);
                        }
                    }
                }
            }
            return lstStudentCurrentInfo.ToArray();
        }

        public List<StudentAttendance> LoadMonthlyAttendanceDML(string strQry)
        {
            List<StudentAttendance> lstStudentAttendance = new List<StudentAttendance>();
            StudentAttendance oStudentAttendance = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oStudentAttendance = new StudentAttendance();
                            oStudentAttendance.StdId = Convert.ToInt32(reader["iStdId"]);
                            oStudentAttendance.StdName = Convert.ToString(reader["sUserName"]);
                            oStudentAttendance.Class = Convert.ToInt32(reader["iClass"]);
                            oStudentAttendance.Date = Convert.ToInt32(reader["iDate"]);
                            oStudentAttendance.Status = Convert.ToInt32(reader["iStatus"]);
                            lstStudentAttendance.Add(oStudentAttendance);
                        }
                    }
                }
            }
            return lstStudentAttendance;
        }

        public IdNamePair[] LoadActiveTeachersDML(string strQry)
        {
            List<IdNamePair> lstTeachers = new List<IdNamePair>();
            IdNamePair oTeacher = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oTeacher = new IdNamePair();
                            oTeacher.ID = Convert.ToInt32(reader["iRegistrationId"]);
                            oTeacher.Name = Convert.ToString(reader["sUserName"]);
                            lstTeachers.Add(oTeacher);
                        }
                    }
                }
            }
            return lstTeachers.ToArray();
        }

        public Attendance[] LoadTeachersAttendanceDML(string strQry)
        {
            List<Attendance> lstAttendance = new List<Attendance>();
            Attendance oAttendance = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oAttendance = new Attendance();
                            oAttendance.TeacherId = Convert.ToInt32(reader["iRegistrationId"]);
                            oAttendance.Date = Convert.ToInt32(reader["iDate"]);
                            oAttendance.Status = Convert.ToInt32(reader["iStatus"]);
                            lstAttendance.Add(oAttendance);
                        }
                    }
                }
            }
            return lstAttendance.ToArray();
        }
        public List<PassFailStatus> AllResultsDML(string strQry)
        {
            List<PassFailStatus> lstPassFailStatus = new List<PassFailStatus>();
            PassFailStatus oResultStatus = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                ;
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oResultStatus = new PassFailStatus();
                            oResultStatus.StdId = Convert.ToInt32(reader["iStdId"]);
                            oResultStatus.ObtainedMarks = Convert.ToInt32(reader["ObtainedMarks"]);
                            oResultStatus.PassingStatus = Convert.ToString(reader["PassingStatus"]);
                            lstPassFailStatus.Add(oResultStatus);
                        }
                    }
                }
            }
            return lstPassFailStatus;
        }

        public StudentAttendance[] LoadStudentDML(string strQry)
        {
            StudentAttendance oStudentAttendance = null;
            List<StudentAttendance> lstStudentAttendance = null;

            lstStudentAttendance = new List<StudentAttendance>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oStudentAttendance = new StudentAttendance();
                            oStudentAttendance.StdId = Convert.ToInt32(reader["iStdId"]);
                            oStudentAttendance.StdName = Convert.ToString(reader["sUserName"]);
                            oStudentAttendance.Class = Convert.ToInt32(reader["iCurrentClass"]);
                            lstStudentAttendance.Add(oStudentAttendance);
                        }
                    }
                }
            }
            return lstStudentAttendance.ToArray();
        }

        public Subject[] LoadSubjectsDML(string strQry, int iClass)
        {
            Subject oSubject = null;
            List<Subject> lstSubject = null;

            lstSubject = new List<Subject>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oSubject = new Subject();
                            oSubject.SubjectId = Convert.ToInt32(reader["iSubjectId"]);
                            oSubject.Class = iClass;
                            oSubject.SubjectName = Convert.ToString(reader["sSubjectName"]);
                            oSubject.MaximumMarks = Convert.ToInt32(reader["iMaxMark"]);
                            oSubject.IsDefaultSubject = Convert.ToBoolean(reader["bIsDefaultSubject"]);
                            lstSubject.Add(oSubject);
                        }
                    }
                }
            }
            return lstSubject.ToArray();
        }
        public IdValuePair[] LoadMarkOfStudentDML(string strQry)
        {
            List<IdValuePair> lstData = new List<IdValuePair>();
            IdValuePair oIdValuePair = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oIdValuePair = new IdValuePair();
                            oIdValuePair.Id = Convert.ToInt32(reader["iExamType"]);
                            List<int> arrMark = new List<int>();
                            arrMark.Add(Convert.ToInt32(reader["English"]));
                            arrMark.Add(Convert.ToInt32(reader["Math"]));
                            arrMark.Add(Convert.ToInt32(reader["Total"]));
                            oIdValuePair.Value = arrMark;
                            lstData.Add(oIdValuePair);
                        }
                    }
                }
            }
            return lstData.ToArray();
        }
        //@@
        #endregion




















        public List<Marks> LoadStudentForResultsDML(string strQry)
        {
            List<Marks> lstMarks = new List<Marks>();
            Marks oMarks = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oMarks = new Marks();
                        oMarks.StdId = Convert.ToInt32(reader["iStdId"]);
                        //oMarks.n = Convert.ToString(reader["sUserName"]);
                        oMarks.Year = Convert.ToInt32(reader["iCurrentClass"]);
                        lstMarks.Add(oMarks);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            return lstMarks;
        }















        public List<StringNamePair> LoadAttendance(string strQry, CommandType cmdType)
        {
            List<StringNamePair> lstAttendance = new List<StringNamePair>();
            StringNamePair oAttendance = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oAttendance = new StringNamePair();
                        oAttendance.str1 = reader.GetSqlValue(1).ToString();
                        oAttendance.str1 = reader.GetSqlValue(1).ToString();
                        lstAttendance.Add(oAttendance);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            return lstAttendance;
        }

        //public List<TeacherDetails> LoadTeachers(string txt, CommandType cmdType)
        //{
        //    List<TeacherDetails> lstTeacherDetails = new List<TeacherDetails>();

        //    TeacherDetails oTeacher = null;

        //    SqlDataReader reader = null;
        //    cmd = new SqlCommand();
        //    cmd.CommandText = txt;
        //    cmd.CommandType = cmdType;
        //    cmd.Connection = ConnectionHelper.ConOpen();

        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            oTeacher = new TeacherDetails();
        //            oTeacher.TeacherId = Convert.ToInt32(reader["iTeacherId"]);
        //            oTeacher.Mobile = Convert.ToInt64(reader["bIntMobile"]);
        //            oTeacher.Email = Convert.ToString(reader["sEmail"]);
        //            oTeacher.Password = Convert.ToString(reader["sPassword"]);
        //            oTeacher.Name = Convert.ToString(reader["sName"]);
        //            oTeacher.Qualification = Convert.ToString(reader["sQualification"]);
        //            oTeacher.JoiningDate = Convert.ToInt32(reader["iJoiningDate"]);
        //            oTeacher.Package = Convert.ToInt32(reader["iPackage"]);
        //            oTeacher.MonthlySalary = Convert.ToInt32(reader["iMonthlySalary"]);
        //            oTeacher.Experiance = Convert.ToInt32(reader["iExperiance"]);
        //            oTeacher.Active = Convert.ToBoolean(reader["bActive"]);

        //            lstTeacherDetails.Add(oTeacher);
        //        }
        //    }
        //    reader.Close();
        //    return lstTeacherDetails;
        //}
        public List<StudentAttendance> LoadStudentAttandenceDML(string strQry)
        {
            List<StudentAttendance> lstStudentAttendance = new List<StudentAttendance>();

            StudentAttendance oStudentAttendance = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oStudentAttendance = new StudentAttendance();
                        oStudentAttendance.StdId = Convert.ToInt32(reader["iStdId"]);
                        oStudentAttendance.Date = Convert.ToInt32(reader["iDate"]);
                        oStudentAttendance.Status = Convert.ToInt32(reader["iStatus"]);
                        lstStudentAttendance.Add(oStudentAttendance);
                    }
                }
                reader.Close();
            }
            return lstStudentAttendance;
        }
        #region Teacher

        //public List<AttendanceRegister> LoadAttendanceRegisterDML(string strQry)
        //{
        //    List<AttendanceRegister> lstAttendance = new List<AttendanceRegister>();
        //    AttendanceRegister oAttendance = null;

        //    SqlDataReader reader = null;
        //    cmd = new SqlCommand();
        //    cmd.CommandText = strQry;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = ConnectionHelper.ConOpen();

        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            oAttendance = new AttendanceRegister();
        //            oAttendance.Id = Convert.ToInt32(reader["iTeacherRegId"]);
        //            oAttendance.TeacherId = Convert.ToInt32(reader["iTeacherId"]);
        //            oAttendance.Date = Convert.ToString(reader["sDate"]);
        //            oAttendance.Status = Convert.ToBoolean(reader["bAttendanceStatus"]);
        //            oAttendance.AdminApproval = Convert.ToBoolean(reader["bAdminApproval"]);
        //            lstAttendance.Add(oAttendance);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No rows found.");
        //    }
        //    reader.Close();
        //    return lstAttendance;
        //}
        

        public List<StudentCurrentInfo> LoadStudentForMarksDML(string strQry)
        {
            List<StudentCurrentInfo> lstStudentCurrentInfo = new List<StudentCurrentInfo>();

            StudentCurrentInfo oStudentCurrentInfo = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // b.stdName, a.iStdId,a.sclass, a.iYear
                        oStudentCurrentInfo = new StudentCurrentInfo();
                        oStudentCurrentInfo.StdId = Convert.ToInt32(reader["iStdId"]);// reader.GetInt64(0);
                        oStudentCurrentInfo.StdName = Convert.ToString(reader["sUserName"]);
                        oStudentCurrentInfo.Class = Convert.ToInt32(reader["iCurrentClass"]);
                        oStudentCurrentInfo.Year = Convert.ToInt32(reader["iCurrentYear"]);

                        lstStudentCurrentInfo.Add(oStudentCurrentInfo);
                    }
                }
                reader.Close();
            }
            return lstStudentCurrentInfo;
        }
        //public List<StudentAttendance> LoadStudentDML(string strQry, int iClass, int iYear)
        //{
        //    List<StudentAttendance> lstStudentAttandence = new List<StudentAttendance>();

        //    StudentAttendance oStudentAttandence = null;

        //    cmd = new SqlCommand(strQry, ConnectionHelper.ConOpen());
        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            oStudentAttandence = new StudentAttendance();
        //            oStudentAttandence.StdId = Convert.ToInt32(reader["iStdId"]);
        //            oStudentAttandence.StdName = Convert.ToString(reader["sUserName"]);
        //            oStudentAttandence.Class = iClass;      //Convert.ToInt32(reader["iCurrentClass"]);
        //            oStudentAttandence.Year = iYear;        //Convert.ToInt32(reader["iCurrentYear"]);
        //            lstStudentAttandence.Add(oStudentAttandence);
        //        }
        //    }
        //    reader.Close();
        //    return lstStudentAttandence;
        //}

        public List<StudentAttendance> LoadStudentForAttendanceDML(string strQry)
        {
            List<StudentAttendance> lstStudentAttendance = new List<StudentAttendance>();

            StudentAttendance oStudentAttendance = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oStudentAttendance = new StudentAttendance();
                        oStudentAttendance.StdId = Convert.ToInt32(reader["iStdId"]);
                        oStudentAttendance.StdName = Convert.ToString(reader["sUserName"]);
                        oStudentAttendance.Class = Convert.ToInt32(reader["iCurrentClass"]);
                        lstStudentAttendance.Add(oStudentAttendance);
                    }
                }
                reader.Close();
            }
            return lstStudentAttendance;
        }

        public bool IsDataInTable(string strQry)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        public int StdDMLSave(string strQry, SqlParameter[] param)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                foreach (var item in param)
                    cmd.Parameters.Add(item);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public SaveStatus DeleteStudentsDML(string strQry)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                SaveStatus oSaveStatus = new SaveStatus();
                cmd = new SqlCommand(strQry, oCon);
                oSaveStatus.iStatus = Convert.ToInt32(cmd.ExecuteNonQuery());
                return oSaveStatus;
            }
        }
        public List<Marks> LoadPreparedMarkSheetsDML(string strQry)
        {
            List<Marks> lstMarks = new List<Marks>();
            Marks oMarks = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oMarks = new Marks();
                        oMarks.StdId = Convert.ToInt32(reader["iStdId"]);
                        oMarks.StdName = Convert.ToString(reader["sUserName"]);
                        oMarks.Class = Convert.ToInt32(reader["iClass"]);
                        oMarks.Year = Convert.ToInt32(reader["iYear"]);
                        //for (int iCol = 0; iCol < lstColumns.Count; iCol++)
                        //{
                        //    if (oMarks.Subjects == null)
                        //    {
                        //        oMarks.Subjects = new Subject[lstColumns.Count];
                        //    }
                        //    if (oMarks.Subjects[iCol] == null)
                        //    {
                        //        oMarks.Subjects[iCol] = new Subject();
                        //    }


                        //    oMarks.Subjects[iCol].SubjectMark = Convert.ToInt32(reader[lstColumns[iCol]]);
                        //}
                        //oMarks.Physics = Convert.ToInt32(reader["iPhysics"]);
                        //oMarks.Chemistry = Convert.ToInt32(reader["iChemistry"]);
                        //oMarks.Mathmatics = Convert.ToInt32(reader["iMathmatics"]);

                        lstMarks.Add(oMarks);
                    }
                }
                reader.Close();
            }
            return lstMarks;
        }
        public List<Marks> LoadPreparedMarkSheetsDML(string strQry, List<string> lstColumns)
        {
            List<Marks> lstMarks = new List<Marks>();
            Marks oMarks = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oMarks = new Marks();
                        oMarks.StdId = Convert.ToInt32(reader["iStdId"]);
                        oMarks.StdName = Convert.ToString(reader["sUserName"]);
                        oMarks.Class = Convert.ToInt32(reader["iClass"]);
                        oMarks.Year = Convert.ToInt32(reader["iYear"]);
                        for (int iCol = 0; iCol < lstColumns.Count; iCol++)
                        {
                            if (oMarks.Subjects == null)
                            {
                                oMarks.Subjects = new Subject[lstColumns.Count];
                            }
                            if (oMarks.Subjects[iCol] == null)
                            {
                                oMarks.Subjects[iCol] = new Subject();
                            }


                            oMarks.Subjects[iCol].SubjectMark = Convert.ToInt32(reader[lstColumns[iCol]]);
                        }
                        //oMarks.Physics = Convert.ToInt32(reader["iPhysics"]);
                        //oMarks.Chemistry = Convert.ToInt32(reader["iChemistry"]);
                        //oMarks.Mathmatics = Convert.ToInt32(reader["iMathmatics"]);

                        lstMarks.Add(oMarks);
                    }
                }
                reader.Close();
            }
            return lstMarks;
        }
        
        public Marks LoadSubjectTotal(string strQry, List<string> lstColumns, IdNamePair[] MaxMarks)
        {
            int iGrandMM = 0;
            int iGrand = 0;
            int iCol = 0;
            Marks oMarks = null;
            Subject oSubject = null;
            List<Subject> arrSubjectsTotal = null;

            SemesterData oMarksOfSubjects = null;
            List<IdValuePair> arrSubMark = new List<IdValuePair>();
            List<SemesterData> lstMarksOfSubjects = new List<SemesterData>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oMarksOfSubjects = new SemesterData();
                        arrSubMark = new List<IdValuePair>();
                        arrSubjectsTotal = new List<Subject>();
                        for (iCol = 0; iCol < lstColumns.Count; iCol++)
                        {
                            oSubject = new Subject();
                            oSubject.ExamType = Convert.ToInt32(reader["iExamType"]);
                            iGrand += oSubject.SubjectMark = Convert.ToInt32(reader[lstColumns[iCol]]);
                            arrSubjectsTotal.Add(oSubject);

                            oMarksOfSubjects.ExamType = Convert.ToInt32(reader["iExamType"]);
                            IdValuePair oSubMark = null;
                            //Start Set MM Cols
                            for (int iMM = 0; iMM < MaxMarks.Length; iMM++)
                            {
                                if (MaxMarks[iMM].Name == lstColumns[iCol])
                                {
                                    oSubMark = new IdValuePair();
                                    oSubMark.Value = "Max Marks";
                                    iGrandMM += oSubMark.Id = MaxMarks[iMM].ID;
                                    arrSubMark.Add(oSubMark);
                                }
                            }
                            //End
                            oSubMark = new IdValuePair();
                            oSubMark.Value = lstColumns[iCol];
                            //iGrand += oSubMark.Id = Convert.ToInt32(reader[lstColumns[iCol]]);
                            oSubMark.Id = Convert.ToInt32(reader[lstColumns[iCol]]);
                            arrSubMark.Add(oSubMark);

                            //IdValuePair oSubMark = new IdValuePair();
                            //oSubMark.Value = lstColumns[iCol];
                            //iGrand += oSubMark.Id = Convert.ToInt32(reader[lstColumns[iCol]]);
                            //arrSubMark.Add(oSubMark);
                        }
                        oMarksOfSubjects.arrSubjectMarks = arrSubMark.ToArray();
                        lstMarksOfSubjects.Add(oMarksOfSubjects);
                    }
                }
                if (iGrand > 0)
                {
                    oSubject = new Subject();
                    oSubject.ExamType = 4;      //After annual
                    oSubject.SubjectMark = iGrand;
                    arrSubjectsTotal.Add(oSubject);

                    oMarksOfSubjects = new SemesterData();
                    oMarksOfSubjects.ExamType = -1; //ie Total

                    IdValuePair[] oTotalSubMark = new IdValuePair[1];
                    oTotalSubMark[0] = new IdValuePair();
                    oTotalSubMark[0].Value = "Grand Total";
                    oTotalSubMark[0].Id = iGrand;
                    oMarksOfSubjects.arrSubjectMarks = oTotalSubMark;
                    lstMarksOfSubjects.Add(oMarksOfSubjects);
                }

                if (arrSubjectsTotal != null)
                {
                    oMarks = new Marks();
                    oMarks.StdId = -1;
                    oMarks.Subjects = arrSubjectsTotal.ToArray();
                    oMarks.SemesterData = lstMarksOfSubjects.ToArray();
                }
                reader.Close();
            }
            return oMarks;
        }
        public SemesterData[] LoadMarksDML3(string strQry, List<string> lstColumns, IdNamePair[] MaxMarks)
        {
            int iGrandMM = 0;
            int iGrand = 0;
            List<SemesterData> lstMarksOfSubjects = new List<SemesterData>();
            SemesterData oMarksOfSubjects = null;
            List<IdValuePair> arrSubMark = new List<IdValuePair>();
            //List<Subject> lstSubject = new List<Subject>();
            //Subject oSubject = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oMarksOfSubjects = new SemesterData();
                        arrSubMark = new List<IdValuePair>();
                        oMarksOfSubjects.ExamType = Convert.ToInt32(reader[lstColumns[0]]);

                        for (int iCol = 0; iCol < lstColumns.Count; iCol++)
                        {
                            oMarksOfSubjects.ExamType = Convert.ToInt32(reader["iExamType"]);
                            IdValuePair oSubMark = null;
                            //Start Set MM Cols
                            for (int iMM = 0; iMM < MaxMarks.Length; iMM++)
                            {
                                if (MaxMarks[iMM].Name == lstColumns[iCol])
                                {
                                    oSubMark = new IdValuePair();
                                    oSubMark.Value = "Max Marks";
                                    iGrandMM += oSubMark.Id = MaxMarks[iMM].ID;
                                    arrSubMark.Add(oSubMark);
                                }
                            }
                            //End
                            oSubMark = new IdValuePair();
                            oSubMark.Value = lstColumns[iCol];
                            iGrand += oSubMark.Id = Convert.ToInt32(reader[lstColumns[iCol]]);
                            arrSubMark.Add(oSubMark);


                        }
                        oMarksOfSubjects.arrSubjectMarks = arrSubMark.ToArray();
                        lstMarksOfSubjects.Add(oMarksOfSubjects);
                    }
                    if (iGrand > 0)
                    {
                        oMarksOfSubjects = new SemesterData();
                        oMarksOfSubjects.ExamType = -1; //ie Total

                        IdValuePair[] oTotalSubMark = new IdValuePair[1];
                        oTotalSubMark[0] = new IdValuePair();
                        oTotalSubMark[0].Value = "Grand Total";
                        oTotalSubMark[0].Id = iGrand;
                        oMarksOfSubjects.arrSubjectMarks = oTotalSubMark;
                        lstMarksOfSubjects.Add(oMarksOfSubjects);
                    }
                    //oMarksOfSubjects.arrSubjectMarks = arrSubMark.ToArray();
                    //lstMarksOfSubjects.Add(oMarksOfSubjects);
                }
                reader.Close();
            }
            return lstMarksOfSubjects.ToArray();
        }
        public Subject[] LoadMarksDMLOLD(string strQry, List<string> lstColumns)
        {
            int iGrand = 0;
            List<Subject> lstSubject = new List<Subject>();
            Subject oSubject = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int iCol = 1; iCol < lstColumns.Count; iCol++)
                        {
                            oSubject = new Subject();
                            oSubject.SubjectName = lstColumns[iCol];
                            oSubject.ExamType = Convert.ToInt32(reader[lstColumns[iCol]]);
                            iGrand += oSubject.SubjectMark = Convert.ToInt32(reader[lstColumns[iCol]]);
                            lstSubject.Add(oSubject);
                        }
                    }
                    if (iGrand > 0)
                    {
                        oSubject = new Subject();
                        oSubject.SubjectName = "Grand Total";
                        //oSubject.ExamType = Convert.ToInt32(reader[lstColumns[iCol]]);
                        oSubject.SubjectMark = iGrand;
                        lstSubject.Add(oSubject);
                    }
                }
                reader.Close();
            }
            return lstSubject.ToArray();
        }
        public List<string> LoadSubjectColumns(string strQry)
        {
            List<string> lstSubjectColumnName = new List<string>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstSubjectColumnName.Add(Convert.ToString(reader["sSubjectColumnName"]));
                    }
                }
                reader.Close();
            }
            return lstSubjectColumnName;
        }
        public Subject[] LoadMarksDML(string strQry, List<string> lstColumns)
        {
            List<Subject> lstSubject = new List<Subject>();
            Subject oSubject = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int iCol = 0; iCol < lstColumns.Count; iCol++)
                        {
                            oSubject = new Subject();
                            oSubject.ExamType = Convert.ToInt32(reader["iExamType"]);
                            oSubject.SubjectName = lstColumns[iCol];
                            oSubject.SubjectMark = Convert.ToInt32(reader[lstColumns[iCol]]);
                            lstSubject.Add(oSubject);
                        }
                    }
                }
                reader.Close();
            }
            return lstSubject.ToArray();
        }
        
        
        public List<TeacherAttendance> LoadTeacherAttendanceDML(string strQry)
        {
            List<TeacherAttendance> lstTeacherAttendance = new List<TeacherAttendance>();

            TeacherAttendance oTeacherAttendance = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oTeacherAttendance = new TeacherAttendance();
                        oTeacherAttendance.TeacherId = Convert.ToInt32(reader["iTeacherId"]);
                        oTeacherAttendance.TeacherName = Convert.ToString(reader["sName"]);
                        oTeacherAttendance.Date = Convert.ToInt32(reader["iDate"]);
                        oTeacherAttendance.Status = Convert.ToInt32(reader["iStatus"]);
                        lstTeacherAttendance.Add(oTeacherAttendance);
                    }
                }
                reader.Close();
            }
            return lstTeacherAttendance;
        }
        
        
        
        
        public Marks[] LoadCreateDMarksheetDML(string strQry)
        {
            Marks oMarks = null;
            List<Marks> lstMarks = new List<Marks>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oMarks = new Marks();
                            oMarks.MarkSheetId = Convert.ToInt32(reader["iMarkSheetId"]);
                            oMarks.StdId = Convert.ToInt32(reader["iStdId"]);
                            oMarks.StdName = Convert.ToString(reader["sUserName"]);
                            int iClass = Convert.ToInt32(reader["iClass"]);
                            oMarks.Class = iClass;
                            oMarks.Year = Convert.ToInt32(reader["iYear"]);
                            lstMarks.Add(oMarks);
                        }
                    }
                }
            }

            return lstMarks.ToArray();
        }
        public List<Marks> LoadStudentMarksheetDMLOLD(string strQry)
        {
            Marks oMarks = null;
            List<Marks> lstMarks = new List<Marks>();

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oMarks = new Marks();
                        oMarks.MarkSheetId = Convert.ToInt32(reader["iMarkSheetId"]);
                        oMarks.StdId = Convert.ToInt32(reader["iStdId"]);
                        oMarks.StdName = Convert.ToString(reader["sUserName"]);
                        int iClass = Convert.ToInt32(reader["iClass"]);
                        oMarks.Class = iClass;
                        oMarks.Year = Convert.ToInt32(reader["iYear"]);
                        lstMarks.Add(oMarks);
                    }
                }
                reader.Close();
            }
            return lstMarks;
        }
        

        public List<string> LoadSubjectColumnsOLD(string strQry)
        {
            List<string> lstSubjectColumnName = new List<string>();

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstSubjectColumnName.Add(Convert.ToString(reader["sSubjectColumnName"]));
                    }
                }
                reader.Close();
            }
            return lstSubjectColumnName;
        }
        
        

        public string[] LoadClasswiseSubjects(string strQry)
        {
            List<string> lstSubjects = new List<string>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstSubjects.Add(Convert.ToString(reader["iTeacherId"]));
                    }
                }
                reader.Close();
            }
            return null;
        }
        public SemesterData[] LoadSemesterData(string strQry, List<string> lstColumns)
        {
            int iGrand = 0;
            List<SemesterData> lstMarksOfSubjects = new List<SemesterData>();
            SemesterData oMarksOfSubjects = null;
            List<IdValuePair> arrSubMark = new List<IdValuePair>();
            //List<Subject> lstSubject = new List<Subject>();
            //Subject oSubject = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oMarksOfSubjects = new SemesterData();
                        arrSubMark = new List<IdValuePair>();
                        oMarksOfSubjects.ExamType = Convert.ToInt32(reader[lstColumns[0]]);

                        for (int iCol = 0; iCol < lstColumns.Count; iCol++)
                        {
                            oMarksOfSubjects.ExamType = Convert.ToInt32(reader["iExamType"]);

                            IdValuePair oSubMark = new IdValuePair();
                            oSubMark.Value = lstColumns[iCol];
                            iGrand += oSubMark.Id = Convert.ToInt32(reader[lstColumns[iCol]]);
                            arrSubMark.Add(oSubMark);
                        }
                        oMarksOfSubjects.arrSubjectMarks = arrSubMark.ToArray();
                        lstMarksOfSubjects.Add(oMarksOfSubjects);
                    }
                    if (iGrand > 0)
                    {
                        oMarksOfSubjects = new SemesterData();
                        oMarksOfSubjects.ExamType = -1; //ie Total

                        IdValuePair[] oTotalSubMark = new IdValuePair[1];
                        oTotalSubMark[0] = new IdValuePair();
                        oTotalSubMark[0].Value = "Grand Total";
                        oTotalSubMark[0].Id = iGrand;
                        oMarksOfSubjects.arrSubjectMarks = oTotalSubMark;
                        lstMarksOfSubjects.Add(oMarksOfSubjects);
                    }
                    //oMarksOfSubjects.arrSubjectMarks = arrSubMark.ToArray();
                    //lstMarksOfSubjects.Add(oMarksOfSubjects);
                }
                reader.Close();
            }
            return lstMarksOfSubjects.ToArray();
        }
        public StudentCurrentInfo[] LoadStudentCurrentInfo(string strQry)
        {
            StudentCurrentInfo oStudentCurrentInfo = null;
            List<StudentCurrentInfo> lstStudentCurrentInfo = new List<StudentCurrentInfo>();

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oStudentCurrentInfo = new StudentCurrentInfo();
                        oStudentCurrentInfo.MarkSheetId = Convert.ToInt32(reader["iMarkSheetId"]);
                        oStudentCurrentInfo.StdId = Convert.ToInt32(reader["iStdId"]);
                        oStudentCurrentInfo.StdName = Convert.ToString(reader["sUserName"]);
                        oStudentCurrentInfo.Class = Convert.ToInt32(reader["iClass"]);
                        oStudentCurrentInfo.Year = Convert.ToInt32(reader["iYear"]);
                        lstStudentCurrentInfo.Add(oStudentCurrentInfo);
                    }
                }
                reader.Close();
            }
            return lstStudentCurrentInfo.ToArray();
        }
        

        public IdNamePair[] GetMaxMark(string strQry)
        {
            List<IdNamePair> lstData = new List<IdNamePair>();
            IdNamePair oIdNamePair = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oIdNamePair = new IdNamePair();
                        oIdNamePair.ID = Convert.ToInt32(reader["iMaxMark"]);
                        oIdNamePair.Name = Convert.ToString(reader["sSubjectName"]);
                        lstData.Add(oIdNamePair);
                    }
                }
                reader.Close();
            }
            return lstData.ToArray();
        }
        public IdNamePair[] LoadSubjectsDML(string strQry)
        {
            List<IdNamePair> lstData = new List<IdNamePair>();
            IdNamePair oIdNamePair = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oIdNamePair = new IdNamePair();
                        oIdNamePair.ID = -1;
                        oIdNamePair.Name = Convert.ToString(reader["sSubjectName"]);
                        lstData.Add(oIdNamePair);
                    }
                }
                reader.Close();
            }
            return lstData.ToArray();
        }
        
        #endregion
        public IdNamePair[] LoadTeachersDML(string strQry)
        {
            List<IdNamePair> lstIdNamePair = new List<IdNamePair>();

            IdNamePair oIdNamePair = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oIdNamePair = new IdNamePair();
                        oIdNamePair.ID = Convert.ToInt32(reader["iRegistrationId"]);// reader.GetInt64(0);
                        oIdNamePair.Name = Convert.ToString(reader["sUserName"]);

                        lstIdNamePair.Add(oIdNamePair);
                    }
                }
                reader.Close();
            }
            return lstIdNamePair.ToArray();
        }
        public IdNamePair[] LoadChatsDML(string strQry)
        {
            List<IdNamePair> lstIdNamePair = new List<IdNamePair>();
            IdNamePair oIdNamePair = null;

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        oIdNamePair = new IdNamePair();
                        oIdNamePair.ID = Convert.ToInt32(reader["iUserIdFrom"]);// reader.GetInt64(0);
                        oIdNamePair.Name = Convert.ToString(reader["sMessage"]);

                        lstIdNamePair.Add(oIdNamePair);
                    }
                }
                reader.Close();
            }
            return lstIdNamePair.ToArray();
        }

        public Registration[] LoadRegisteredUsersDML(string strQry)
        {
            List<Registration> lstRegistration = new List<Registration>();
            Registration oRegistration = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oRegistration = new Registration();
                            oRegistration.RegistrationId = Convert.ToInt32(reader["iRegistrationId"]);
                            oRegistration.UserName = Convert.ToString(reader["sUserName"]);
                            oRegistration.UserType = Convert.ToInt32(reader["iUserType"]);
                            lstRegistration.Add(oRegistration);
                        }
                    }
                }
            }
            return lstRegistration.ToArray();
        }
        public SemesterData[] LoadExamTypeMarks(string strQry, List<string> lstSubjectColumns)
        {
            int iGrand = 0;
            SemesterData oSemesterData = null;
            List<SemesterData> lstSemesterData = new List<SemesterData>();
            IdValuePair oSubMark = null;
            List<IdValuePair> arrSubMark = new List<IdValuePair>();

            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    arrSubMark = new List<IdValuePair>();
                    while (reader.Read())
                    {
                        oSemesterData = new SemesterData();
                        oSemesterData.ExamType = Convert.ToInt32(reader["iExamType"]);

                        for (int iCol = 0; iCol < lstSubjectColumns.Count; iCol++)
                        {
                            oSubMark = new IdValuePair();
                            oSubMark.Value = lstSubjectColumns[iCol];
                            iGrand += oSubMark.Id = Convert.ToInt32(reader[lstSubjectColumns[iCol]]);
                            arrSubMark.Add(oSubMark);
                        }
                    }
                    if (iGrand > 0)
                    {
                        oSubMark = new IdValuePair();
                        oSubMark.Value = "Grand";
                        oSubMark.Id = iGrand;
                        arrSubMark.Add(oSubMark);
                    }
                    oSemesterData.arrSubjectMarks = arrSubMark.ToArray();

                    lstSemesterData.Add(oSemesterData);
                }
                reader.Close();
            }
            return lstSemesterData.ToArray();
        }
        //public Marks[] LoadSemesterMarksheetDML(string strQry)
        //{
        //    List<Marks> lstStudentMarks = new List<Marks>();
        //    Marks oStudentMarks = null;

        //    using (oCon = ConnectionHelper.GetDBConnectionString())
        //    {
        //        oCon.Open();
        //        cmd = new SqlCommand(strQry, oCon);
        //        using (reader = oCommonExc.CallExecuteReader(cmd))
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    oStudentMarks = new Marks();
        //                    oStudentMarks.StdId = Convert.ToInt32(reader["iStdId"]);
        //                    oStudentMarks.StdName = Convert.ToString(reader["sUserName"]);
        //                    oStudentMarks.SemesterData = GetSemesterData();
        //                    lstStudentMarks.Add(oStudentMarks);
        //                }
        //            }
        //        }
        //    }
            
        //    return lstStudentMarks.ToArray();
        //}
        //private static void GetSemesterData()
        //{

        //}
    }
}
