using DomLibrary;
using SchoolBones.Common;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolBones.Admin
{
    public class AdminExecuteDML
    {
        SaveStatus oSaveStatus = new SaveStatus();
        CommonExecuteDML oCommonExc = new CommonExecuteDML();

        SqlCommand cmd = null;
        SqlDataReader reader = null;
        SqlConnection oCon = null;

        //public Registration LoadRegisteredTeacherDML(string strQry)
        //{
        //    Registration oRegistration = null;

        //    cmd = new SqlCommand();
        //    cmd.CommandText = strQry;
        //    cmd.Connection = ConnectionHelper.ConOpen();

        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            oRegistration = new Registration();
        //            oRegistration.RegistrationId = Convert.ToInt32(reader["iRegistrationId"]);
        //            oRegistration.UserName = Convert.ToString(reader["sUserName"]);
        //            oRegistration.LoginName = Convert.ToString(reader["sLoginName"]);
        //            oRegistration.UserPassword = Convert.ToString(reader["sUserPassword"]);
        //        }
        //    }
        //    reader.Close();
        //    return oRegistration;
        //}

        public TimeTableLayout[] LoadTimeTableLayoutDML(string strQry)
        {
            List<TimeTableLayout> lstTimeTableLayout = new List<TimeTableLayout>();
            TimeTableLayout oTimeTableLayout = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        oTimeTableLayout = new TimeTableLayout();
                        oTimeTableLayout.iLayoutId= Convert.ToInt32(reader["iLayoutId"]);
                        oTimeTableLayout.LayoutName = Convert.ToString(reader["sLayoutName"]);
                        oTimeTableLayout.NoOfPeriod = Convert.ToInt32(reader["iNoOfPeriod"]);
                        oTimeTableLayout.FirstPeriod = Convert.ToInt32(reader["iFirstPeriod"]);
                        oTimeTableLayout.Duration = Convert.ToInt32(reader["iDuration"]);
                        oTimeTableLayout.LunchBreak = Convert.ToInt32(reader["iLunchBreak"]);
                        oTimeTableLayout.Class = Convert.ToInt32(reader["iClass"]);
                        oTimeTableLayout.CreatedDate = Convert.ToInt32(reader["iCreatedDate"]);
                        lstTimeTableLayout.Add(oTimeTableLayout);
                    }
                }
            }
            return lstTimeTableLayout.ToArray();
        }

        public TeacherDetails[] LoadTeachers(string strQry)
        {
            TeacherDetails oTeacher = null;
            List<TeacherDetails> lstTeacherDetails = new List<TeacherDetails>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oTeacher = new TeacherDetails();
                            oTeacher.RegistrationId = Convert.ToInt32(reader["iRegistrationId"]);
                            oTeacher.TeacherId = Convert.ToInt32(reader["iRegistrationId"]);
                            oTeacher.Name = Convert.ToString(reader["sUserName"]);
                            oTeacher.Mobile = Convert.ToInt64(reader["bIntMobile"]);
                            oTeacher.Qualification = Convert.ToString(reader["sQualification"]);
                            oTeacher.JoiningDate = Convert.ToInt32(reader["iJoiningDate"]);
                            oTeacher.Experiance = Convert.ToInt32(reader["iExperiance"]);
                            oTeacher.Active = Convert.ToBoolean(reader["bActive"]);
                            lstTeacherDetails.Add(oTeacher);
                        }
                    }
                }
            }
            return lstTeacherDetails.ToArray();
        }
        public SchoolDetails LoadSchoolDetailsDML(string strQry)
        {
            SchoolDetails oSchoolDetails = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                using (reader = oCommonExc.CallExecuteReader(cmd))
                {
                    if (reader.HasRows)
                    {
                        oSchoolDetails = new SchoolDetails();
                        oSchoolDetails.SchoolId = Convert.ToInt32(reader["iSchoolId"]);
                        oSchoolDetails.RegistrationDate = Convert.ToInt32(reader["iRegistrationDate"]);
                        oSchoolDetails.bIntContact = Convert.ToInt64(reader["iContact"]);
                    }
                }
            }
            return oSchoolDetails;

            //List<SchoolDetails> lstSchoolDetails = new List<SchoolDetails>();

            //SchoolDetails oSchoolDetails = null;

            //SqlDataReader reader = null;
            //SqlCommand cmd = null;

            //cmd = new SqlCommand();
            //cmd.CommandText = txt;
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = ConnectionHelper.ConOpen();

            //reader = cmd.ExecuteReader();
            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        oSchoolDetails = new SchoolDetails();
            //        oSchoolDetails.SchoolId = Convert.ToInt32(reader["iSchoolId"]);
            //        oSchoolDetails.RegistrationDate = Convert.ToInt32(reader["iRegistrationDate"]);
            //        oSchoolDetails.bIntContact = Convert.ToInt64(reader["iContact"]);

            //        lstSchoolDetails.Add(oSchoolDetails);
            //    }
            //}
            //reader.Close();
            //return lstSchoolDetails;
        }

        //public Registration[] LoadRegisteredStudentsDML(string strQuery)
        //{
        //    List<Registration> lstRegistration = new List<Registration>();
        //    Registration oRegistration = null;

        //    using (oCon = ConnectionHelper.GetDBConnectionString())
        //    {
        //        oCon.Open();
        //        cmd = new SqlCommand(strQuery, oCon);
        //        using (reader = oCommonExc.CallExecuteReader(cmd))
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    oRegistration = new Registration();
        //                    oRegistration.RegistrationId = Convert.ToInt32(reader["iRegistrationId"]);
        //                    oRegistration.UserName = Convert.ToString(reader["sUserName"]);
        //                    oRegistration.UserType = Convert.ToInt32(reader["iUserType"]);
        //                    lstRegistration.Add(oRegistration);
        //                }
        //            }
        //        }
        //    }
        //    return lstRegistration.ToArray();
        //}

        #region TimeTable
        public Cell[] LoadTimeTableData(string strQry)
        {
            List<Cell> arrCell = new List<Cell>();
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cell oCell = new Cell();
                        oCell.RowId = Convert.ToInt32(reader["iRowInd"]);
                        oCell.ColId = Convert.ToInt32(reader["iColInd"]);
                        oCell.RegId = Convert.ToInt32(reader["iTeacherId"]);
                        oCell.Name = Convert.ToString(reader["sTeacherName"]);
                        oCell.SubjectName = Convert.ToString(reader["sSubjectName"]);
                        arrCell.Add(oCell);
                    }
                }
                reader.Close();
            } return arrCell.ToArray();
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
        #endregion
    }
}
