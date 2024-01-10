using DomLibrary;
using SchoolBones.Common;
using SchoolBones.Enums;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SchoolBones.Teacher
{
    public class TeacherDML
    {
        string strQry = string.Empty;
        SaveStatus oSaveStatus = new SaveStatus();
        CommonExecuteDML oCommonExc = new CommonExecuteDML();
        TeacherExecuteDML oTeacherExc = new TeacherExecuteDML();

        SqlCommand cmd = null;
        SqlConnection oCon = null;
        SqlTransaction oTrans = null;

        #region POST
        public SaveStatus StudentAdmission(AdmissionDetails oInput)
        {
            int iStdId = 0;
            try
            {
                strQry = string.Format(@"If EXISTS (SELECT iRegistrationId FROM tbl_Students WHERE iRegistrationId = {0}) SELECT 1 ELSE SELECT 0", oInput.RegistrationId);

                using (oCon = ConnectionHelper.Stablish)
                {
                    oTrans = oCon.BeginTransaction();

                    cmd = new SqlCommand(strQry, oCon, oTrans);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                    {
                        //update
                        strQry = string.Format(@"UPDATE tbl_Students SET sAddress = '{1}', iAdmissionDate = {2}
                                        WHERE iRegistrationId = {0}",
                                    oInput.RegistrationId,
                                    oInput.StdName,
                                    oInput.AdmissionDate
                                    );
                        return oCommonExc.DeleteOrUpdate(strQry);
                    }
                    else
                    {
                        //insert
                        //tbl_Students
                        strQry = string.Format(@"INSERT INTO tbl_Students(iRegistrationId, sAddress, sCity, iContact, iAdmissionInClass, iDOB, bIsActive, iAdmissionDate, biStudentImage)
                                    values(@iRegistrationId, @sAddress, @sCity, @iContact, @iAdmissionInClass, @iDOB, @bIsActive, @iAdmissionDate, @biStudentImage)");

                        cmd = new SqlCommand(strQry, oCon, oTrans);
                        cmd.Parameters.AddWithValue("@iRegistrationId", oInput.RegistrationId);
                        cmd.Parameters.AddWithValue("@sAddress", oInput.Address);
                        cmd.Parameters.AddWithValue("@sCity", oInput.City);
                        cmd.Parameters.AddWithValue("@iContact", oInput.Contact);
                        cmd.Parameters.AddWithValue("@iAdmissionInClass", oInput.AdmissionInClass);
                        cmd.Parameters.AddWithValue("@iDOB", oInput.DOB);
                        cmd.Parameters.AddWithValue("@bIsActive", oInput.IsActive);
                        cmd.Parameters.AddWithValue("@iAdmissionDate", oInput.AdmissionDate);
                        if (oInput.StudentImage == null)
                        {
                            cmd.Parameters.AddWithValue("@biStudentImage", new byte[0]).SqlDbType = SqlDbType.Binary;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@biStudentImage", oInput.StudentImage).SqlDbType = SqlDbType.Binary;
                        }
                        oCommonExc.CallExecuteNonQuery(cmd);

                        //tbl_StudentClasswiseInfo
                        strQry = string.Format(@"INSERT INTO tbl_StudentClasswiseInfo (iRegistrationId, iCurrentClass, iCurrentYear, bIsActive, bResultPrepared)
                                    VALUES (@iRegistrationId, @iCurrentClass, @iCurrentYear, @bIsActive, @bResultPrepared)  SELECT @@IDENTITY");

                        cmd.Parameters.Clear();
                        cmd.CommandText = strQry;
                        cmd.Parameters.AddWithValue("@iRegistrationId", oInput.RegistrationId);
                        cmd.Parameters.AddWithValue("@iCurrentClass", oInput.AdmissionInClass);
                        cmd.Parameters.AddWithValue("@iCurrentYear", oInput.AdmissionDate);
                        cmd.Parameters.AddWithValue("@bIsActive", oInput.IsActive);
                        cmd.Parameters.AddWithValue("@bResultPrepared", 0);
                        iStdId = oCommonExc.GetIdentityOnSave(cmd);

                        //tbl_StudentAttendance
                        strQry = string.Format(@"INSERT INTO tbl_StudentAttendance (iStdId, iClass, iDate, iStatus)
                                    VALUES (@iStdId, @iClass, @iDate, @iStatus)");

                        cmd.Parameters.Clear();
                        cmd.CommandText = strQry;
                        cmd.Parameters.AddWithValue("@iStdId", iStdId);
                        cmd.Parameters.AddWithValue("@iClass", oInput.AdmissionInClass);
                        cmd.Parameters.AddWithValue("@iDate", oInput.AdmissionDate);
                        cmd.Parameters.AddWithValue("@iStatus", oInput.AdmissionDate);
                        oCommonExc.CallExecuteNonQuery(cmd);

                        //tbl_Fees
                        strQry = string.Format(@"INSERT INTO tbl_Fees (iStdId, iClass, iForMonth, iYear, iDate, iFee, iPending, iYearlyFee, iConcession) 
                                        values (@iStdId, @iClass, @iForMonth, @iYear, @iDate, @iFee, @iPending, @iYearlyFee, @iConcession)");

                        cmd.Parameters.Clear();
                        cmd.CommandText = strQry;
                        cmd.Parameters.AddWithValue("@iStdId", iStdId);
                        cmd.Parameters.AddWithValue("@iClass", oInput.AdmissionInClass);
                        cmd.Parameters.AddWithValue("@iForMonth", oInput.AdmissionInClass);
                        cmd.Parameters.AddWithValue("@iYear", oInput.AdmissionInClass);
                        cmd.Parameters.AddWithValue("@iDate", oInput.AdmissionDate);
                        cmd.Parameters.AddWithValue("@iFee", oInput.AdmissionInClass);
                        cmd.Parameters.AddWithValue("@iPending", oInput.AdmissionInClass);
                        cmd.Parameters.AddWithValue("@iYearlyFee", oInput.AdmissionInClass);
                        cmd.Parameters.AddWithValue("@iConcession", oInput.AdmissionInClass);
                        oSaveStatus.iStatus = oCommonExc.CallExecuteNonQuery(cmd);
                        oTrans.Commit();
                        return oSaveStatus;
                    }
                }

                //if (oInput.RegistrationId > 0)
                //{
                //    strQry = string.Format(@"UPDATE tbl_Students SET sAddress = '{1}', iAdmissionDate = {2}
                //                        WHERE iRegistrationId = {0}",
                //                     oInput.RegistrationId,
                //                     oInput.StdName,
                //                     oInput.AdmissionDate
                //                     );
                //    return oCommonExc.DeleteOrUpdate(strQry);
                //}
                //using (oCon = ConnectionHelper.GetDBConnectionString())
                //{
                //    oCon.Open();
                //    oTrans = oCon.BeginTransaction();

                //    //tbl_Students
                //    strQry = string.Format(@"INSERT INTO tbl_Students(iRegistrationId, sAddress, sCity, iContact, iAdmissionInClass, iDOB, bIsActive, iAdmissionDate, biStudentImage)
                //                    values(@iRegistrationId, @sAddress, @sCity, @iContact, @iAdmissionInClass, @iDOB, @bIsActive, @iAdmissionDate, @biStudentImage)");

                //    cmd = new SqlCommand(strQry, oCon, oTrans);
                //    cmd.Parameters.AddWithValue("@iRegistrationId", oInput.RegistrationId);
                //    cmd.Parameters.AddWithValue("@sAddress", oInput.Address);
                //    cmd.Parameters.AddWithValue("@sCity", oInput.City);
                //    cmd.Parameters.AddWithValue("@iContact", oInput.Contact);
                //    cmd.Parameters.AddWithValue("@iAdmissionInClass", oInput.AdmissionInClass);
                //    cmd.Parameters.AddWithValue("@iDOB", oInput.DOB);
                //    cmd.Parameters.AddWithValue("@bIsActive", oInput.IsActive);
                //    cmd.Parameters.AddWithValue("@iAdmissionDate", oInput.AdmissionDate);
                //    if (oInput.StudentImage == null)
                //    {
                //        cmd.Parameters.AddWithValue("@biStudentImage", new byte[0]).SqlDbType = SqlDbType.Binary;
                //    }
                //    else
                //    {
                //        cmd.Parameters.AddWithValue("@biStudentImage", oInput.StudentImage).SqlDbType = SqlDbType.Binary;
                //    }
                //    oCommonExc.CallExecuteNonQuery(cmd);

                //    //tbl_StudentClasswiseInfo
                //    strQry = string.Format(@"INSERT INTO tbl_StudentClasswiseInfo (iRegistrationId, iCurrentClass, iCurrentYear, bIsActive, bResultPrepared)
                //                    VALUES (@iRegistrationId, @iCurrentClass, @iCurrentYear, @bIsActive, @bResultPrepared)  SELECT @@IDENTITY");

                //    cmd.Parameters.Clear();
                //    cmd.CommandText = strQry;
                //    cmd.Parameters.AddWithValue("@iRegistrationId", oInput.RegistrationId);
                //    cmd.Parameters.AddWithValue("@iCurrentClass", oInput.AdmissionInClass);
                //    cmd.Parameters.AddWithValue("@iCurrentYear", oInput.AdmissionDate);
                //    cmd.Parameters.AddWithValue("@bIsActive", oInput.IsActive);
                //    cmd.Parameters.AddWithValue("@bResultPrepared", 0);
                //    iStdId = oCommonExc.GetIdentityOnSave(cmd);

                //    //tbl_StudentAttendance
                //    strQry = string.Format(@"INSERT INTO tbl_StudentAttendance (iStdId, iClass, iDate, iStatus)
                //                    VALUES (@iStdId, @iClass, @iDate, @iStatus)");

                //    cmd.Parameters.Clear();
                //    cmd.CommandText = strQry;
                //    cmd.Parameters.AddWithValue("@iStdId", iStdId);
                //    cmd.Parameters.AddWithValue("@iClass", oInput.AdmissionInClass);
                //    cmd.Parameters.AddWithValue("@iDate", oInput.AdmissionDate);
                //    cmd.Parameters.AddWithValue("@iStatus", oInput.AdmissionDate);
                //    oCommonExc.CallExecuteNonQuery(cmd);

                //    //tbl_Fees
                //    strQry = string.Format(@"INSERT INTO tbl_Fees (iStdId, iClass, iForMonth, iYear, iDate, iFee, iPending, iYearlyFee, iConcession) 
                //                        values (@iStdId, @iClass, @iForMonth, @iYear, @iDate, @iFee, @iPending, @iYearlyFee, @iConcession)");

                //    cmd.Parameters.Clear();
                //    cmd.CommandText = strQry;
                //    cmd.Parameters.AddWithValue("@iStdId", iStdId);
                //    cmd.Parameters.AddWithValue("@iClass", oInput.AdmissionInClass);
                //    cmd.Parameters.AddWithValue("@iForMonth", oInput.AdmissionInClass);
                //    cmd.Parameters.AddWithValue("@iYear", oInput.AdmissionInClass);
                //    cmd.Parameters.AddWithValue("@iDate", oInput.AdmissionDate);
                //    cmd.Parameters.AddWithValue("@iFee", oInput.AdmissionInClass);
                //    cmd.Parameters.AddWithValue("@iPending", oInput.AdmissionInClass);
                //    cmd.Parameters.AddWithValue("@iYearlyFee", oInput.AdmissionInClass);
                //    cmd.Parameters.AddWithValue("@iConcession", oInput.AdmissionInClass);
                //    oSaveStatus.iStatus = oCommonExc.CallExecuteNonQuery(cmd);
                //    oTrans.Commit();
                //    return oSaveStatus;
                //}
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                }
                return oSaveStatus;
            }
        }

        public SaveStatus SaveAttendance(StudentAttendance[] arrStudentAttendance, int iDate)
        {
            try
            {
                using (oCon = ConnectionHelper.Stablish)
                {
                    oTrans = oCon.BeginTransaction();
                    if (arrStudentAttendance != null)
                    {
                        strQry = string.Format(@"DELETE FROM tbl_StudentAttendance WHERE iDate = {0}", iDate);
                        cmd = new SqlCommand(strQry, oCon, oTrans);
                        oCommonExc.CallExecuteNonQuery(cmd);

                        for (int iRow = 0; iRow < arrStudentAttendance.Length; iRow++)
                        {
                            StudentAttendance oStudentAttandence = arrStudentAttendance[iRow];
                            strQry = string.Format(@"INSERT INTO tbl_StudentAttendance (iStdId, iClass, iDate, iStatus) 
                                                    VALUES (@iStdId, @iClass, @iDate, @iStatus)");

                            cmd.Parameters.Clear();
                            cmd.CommandText = strQry;
                            cmd.Parameters.AddWithValue("@iStdId", oStudentAttandence.StdId);
                            cmd.Parameters.AddWithValue("@iClass", oStudentAttandence.Class);
                            cmd.Parameters.AddWithValue("@iDate", iDate);
                            cmd.Parameters.AddWithValue("@iStatus", oStudentAttandence.Status);

                            oSaveStatus.iStatus = oCommonExc.CallExecuteNonQuery(cmd);
                        }
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

        public SaveStatus SaveTeacherAttandence(TeacherAttendance[] arrTeacherAttendance, int iDate)
        {
            TeacherAttendance oTeacherAttendance = null;
            oSaveStatus = new SaveStatus();

            try
            {
                if (arrTeacherAttendance != null)
                {
                    using (oCon = ConnectionHelper.Stablish)
                    {
                        oTrans = oCon.BeginTransaction();

                        strQry = string.Format(@"DELETE FROM tbl_TeacherAttandence WHERE iDate = {0}", iDate);
                        cmd = new SqlCommand(strQry, oCon, oTrans);
                        oCommonExc.CallExecuteNonQuery(cmd);

                        for (int iRow = 0; iRow < arrTeacherAttendance.Length; iRow++)
                        {
                            oTeacherAttendance = arrTeacherAttendance[iRow];

                            strQry = string.Format(@"INSERT INTO tbl_TeacherAttandence (iRegistrationId, iDate, iStatus) 
                                                    VALUES (@iRegistrationId, @iDate, @iStatus);");

                            cmd.Parameters.Clear();
                            cmd.CommandText = strQry;
                            cmd.Parameters.AddWithValue("@iRegistrationId", oTeacherAttendance.RegistrationId);
                            cmd.Parameters.AddWithValue("@iDate", iDate);
                            cmd.Parameters.AddWithValue("@iStatus", oTeacherAttendance.Status);
                            oSaveStatus = oCommonExc.Save(cmd);
                        }
                        oTrans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                }
            }          
            return oSaveStatus;
        }

        public SaveStatus SaveMarks(Marks[] arrMarks)
        {
            int iMarkSheetId = 0;
            Marks oMarks = null;

            try
            {
                if (arrMarks != null)
                {
                    using (oCon = ConnectionHelper.Stablish)
                    {
                        oTrans = oCon.BeginTransaction();

                        for (int iRow = 0; iRow < arrMarks.Length; iRow++)
                        {
                            oMarks = arrMarks[iRow];

                            strQry = string.Format(@"SELECT iMarkSheetId FROM tbl_MarkSheet WHERE iStdId = {0} AND iClass = {1} AND iYear = {2}",
                                oMarks.StdId, oMarks.Class, oMarks.Year);
                            cmd = new SqlCommand(strQry, oCon, oTrans);
                            iMarkSheetId = Convert.ToInt32(oCommonExc.CallExecuteScalar(cmd));

                            if (iMarkSheetId == 0)
                            {
                                strQry = string.Format(@"INSERT INTO tbl_MarkSheet (iStdId, iClass, iYear, bResultPrepared)
                                                    VALUES (@iStdId, @iClass, @iYear, @bResultPrepared)  Select @@IDENTITY");
                                cmd.CommandText = strQry;
                                cmd.Parameters.AddWithValue("@iStdId", oMarks.StdId);
                                cmd.Parameters.AddWithValue("@iClass", oMarks.Class);
                                cmd.Parameters.AddWithValue("@iYear", oMarks.Year);
                                cmd.Parameters.AddWithValue("@bResultPrepared", oMarks.ResultPrepared == true ? 1 : 0);
                                iMarkSheetId = oCommonExc.GetIdentityOnSave(cmd);
                            }
                            if (iMarkSheetId > 0)
                            {
                                string sCols = "";
                                string sColValues = "";
                                for (int iCol = 0; iCol < oMarks.Subjects.Length; iCol++)
                                {
                                    sCols += oMarks.Subjects[iCol].SubjectName + ",";
                                    sColValues += "@" + oMarks.Subjects[iCol].SubjectName + ",";
                                }
                                sCols = sCols.Remove(sCols.Length - 1, 1);
                                sColValues = sColValues.Remove(sColValues.Length - 1, 1);

                                strQry = string.Format(@"DELETE FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {0} AND iExamType = {1}",
                                                        iMarkSheetId, oMarks.ExamType);
                                cmd.CommandText = strQry;
                                oCommonExc.CallExecuteNonQuery(cmd);

                                strQry = string.Format("INSERT INTO tbl_MarksOfSubjects (iMarkSheetId, iExamType, {0}) VALUES (@iMarkSheetId, @iExamType, {1})",
                                                        sCols,
                                                        sColValues);
                                cmd.Parameters.Clear();
                                cmd.CommandText = strQry;
                                cmd.Parameters.AddWithValue("@iMarkSheetId", iMarkSheetId);
                                cmd.Parameters.AddWithValue("@iExamType", oMarks.ExamType);
                                for (int iCol = 0; iCol < oMarks.Subjects.Length; iCol++)
                                {
                                    string sColName = string.Format("@{0}", oMarks.Subjects[iCol].SubjectName);
                                    cmd.Parameters.AddWithValue(sColName, oMarks.Subjects[iCol].SubjectMark);
                                }
                                oCommonExc.CallExecuteNonQuery(cmd);

                            }
                            if (oMarks.ExamType == 3)
                            {
                                strQry = string.Format(@"UPDATE tbl_StudentClasswiseInfo SET bResultPrepared = {0} WHERE iStdId = {1}",
                                         oMarks.ResultPrepared == true ? 1 : 0,
                                         oMarks.StdId);
                                cmd.CommandText = strQry;
                                oCommonExc.CallExecuteNonQuery(cmd);
                            }
                        }
                        oSaveStatus.iStatus = 1;
                        oTrans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                }
            }            
            return oSaveStatus;
        }
        public SaveStatus SaveSubject(Subject oSubject)
        {
            try
            {
                using (oCon = ConnectionHelper.Stablish)
                {
                    oTrans = oCon.BeginTransaction();

                    strQry = string.Format(@"INSERT INTO tbl_Subjects (iClass, sSubjectName, iMaxMark) VALUES (@iClass, @sSubjectName, @iMaxMark) SELECT @@IDENTITY");
                    cmd = new SqlCommand(strQry, oCon, oTrans);
                    cmd.Parameters.AddWithValue("@iClass", oSubject.Class);
                    cmd.Parameters.AddWithValue("@sSubjectName", oSubject.SubjectName);
                    cmd.Parameters.AddWithValue("@iMaxMark", oSubject.MaximumMarks);
                    oCommonExc.GetIdentityOnSave(cmd);

                    strQry = string.Format(@"ALTER TABLE tbl_MarksOfSubjects ADD [{0}] INT NOT NULL DEFAULT(0)", oSubject.SubjectName);
                    cmd.CommandText = strQry;
                    oCommonExc.CallExecuteScalar(cmd);
                    oSaveStatus.iStatus = 1;
                    oTrans.Commit();
                }
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                }
            }

          return oSaveStatus;
        }
        //@@@@
        #endregion

        #region GET
        //public List<AdmissionDetails> LoadAllStudents()
        //{
        //    strQry = string.Format(@"SELECT DISTINCT a.iRegistrationId, sStdName, sFirstName, sLastName, sAddress, sCity, iContact, iAdmissionInClass, iDOB, b.bIsActive, iAdmissionDate, biStudentImage 
        //                                FROM tbl_Students a
        //                                JOIN tbl_StudentClasswiseInfo b ON a.iRegistrationId = b.iRegistrationId
        //                                ORDER BY 1");
        //    return oTeacherExc.LoadAllStudentsDML(strQry);
        //}

        public List<StudentCurrentInfo> LoadStdForDeRegister(bool bPermamnentDeletion)
        {
            if (bPermamnentDeletion)
            {
                strQry = string.Format("SELECT a.iRegistrationId, a.sStdName, b.iCurrentClass,b.iCurrentYear FROM tbl_Students a JOIN tbl_StudentClasswiseInfo b ON a.iRegistrationId = b.iRegistrationId");
            }
            else
            {
                strQry = string.Format(@"SELECT a.iRegistrationId, a.sStdName, b.iCurrentClass,b.iCurrentYear FROM tbl_Students a JOIN tbl_StudentClasswiseInfo b ON a.iRegistrationId = b.iRegistrationId 
                                            WHERE b.bIsActive <> 0; ; ");
            }
            return oTeacherExc.LoadStdForDeRegisterDML(strQry);
        }

        public StudentCurrentInfo[] LoadActiveInActiveStudents(bool bActive)
        {
            strQry = string.Format(@"SELECT a.iRegistrationId, c.iStdId, b.sUserName, c.iCurrentClass,c.iCurrentYear 
                                        FROM tbl_Students a 
                                        join tbl_UserRegistration b on a.iRegistrationId =b.iRegistrationId
                                        JOIN tbl_StudentClasswiseInfo c ON c.iRegistrationId= b.iRegistrationId
                                        WHERE c.bIsActive = {0}", bActive == true ? 1 : 0);
            return oTeacherExc.LoadActiveStudentsDML(strQry);
        }
        public List<StudentAttendance> LoadMonthlyAttendance(int iClass, int iMonth, int iYear)
        {
            if (iMonth <= 0)
            {
                iMonth = DateTime.Now.Month;
            }
            if (iYear <= 0)
            {
                iYear = DateTime.Now.Year;
            }
            strQry = string.Format(@"SELECT a.iStdId, d.sUserName, a.iClass, a.iDate, a.iStatus
                                        FROM tbl_StudentAttendance a
                                        JOIN tbl_StudentClasswiseInfo b on a.iStdId = b.iStdID
                                        JOIN tbl_Students c on c.iRegistrationId = b.iRegistrationId
                                        join tbl_UserRegistration d on d.iRegistrationId =b.iRegistrationId
										WHERE iClass = {0}
                                        AND dbo.getdatepart('m',132581633) = {1}
                                        AND dbo.getdatepart('y',132581633) = {2}",
                                        iClass, iMonth, iYear);
            return oTeacherExc.LoadMonthlyAttendanceDML(strQry);
        }

        public TeachersAttendance LoadTeachersAttendance(int iMonth, int iYear)
        {
            TeachersAttendance oTeachersAttendance = new TeachersAttendance();
            if (iMonth <= 0)
            {
                iMonth = DateTime.Now.Month;
            }
            if (iYear <= 0)
            {
                iYear = DateTime.Now.Year;
            }

            strQry = string.Format(@"select a.iRegistrationId, a.sUserName from tbl_UserRegistration  a
                                    join tbl_Teachers b on a.iRegistrationId=b.iRegistrationId
                                    where b.bActive = 1 ORDER BY a.sUserName");
            oTeachersAttendance.Teachers = oTeacherExc.LoadActiveTeachersDML(strQry);

            strQry = string.Format(@"SELECT iRegistrationId, iDate, iStatus FROM tbl_TeacherAttandence 
                                        where dbo.getdatepart('m', iDate) = {0} and dbo.getdatepart('y', iDate) = {1}",
                                        iMonth,
                                        iYear);

            oTeachersAttendance.Attendance = oTeacherExc.LoadTeachersAttendanceDML(strQry);
            return oTeachersAttendance;
        }

        public List<PassFailStatus> AllResults()
        {
            strQry = string.Format(@"select iStdId, (iPhysics+iChemistry+iMathmatics) [ObtainedMarks], 
                                    case 
                                     when (iPhysics+iChemistry+iMathmatics) <= 150 and (iPhysics+iChemistry+iMathmatics) >= 100 then 'First'
                                     when (iPhysics+iChemistry+iMathmatics) <= 100 and (iPhysics+iChemistry+iMathmatics) >= 90 then 'Second'
                                     else
                                     'Failed'
                                    end [PassingStatus]
                                    from tbl_MarkSheet 
                                    where bResultPrepared = 1");
            return oTeacherExc.AllResultsDML(strQry);
        }

        public MarkSheet LoadClasswiseStudentAndSubjects(int iClass, int iYear)
        {
            MarkSheet oMarkSheet = new MarkSheet();
            oMarkSheet.StudentAttendance = LoadClassStudent(iClass, iYear);
            oMarkSheet.Subjects = LoadSubjects(iClass);
            return oMarkSheet;
        }
        public StudentAttendance[] LoadClassStudent(int iClass, int iYear)
        {
            strQry = string.Format(@"SELECT b.iStdId, c.sUserName, b.iCurrentClass, b.iCurrentYear
                                        FROM tbl_Students a
                                        JOIN tbl_StudentClasswiseInfo b ON a.iRegistrationId = b.iRegistrationId
										join tbl_UserRegistration c on c.iRegistrationId =b.iRegistrationId
                                        WHERE b.bIsActive = 1 AND b.bResultPrepared = 0
                                        AND b.iCurrentClass = {0} AND dbo.GETDATEPART('Y',b.iCurrentYear) = {1}
                                        ORDER BY 1",
                                        iClass,
                                        iYear);
            return oTeacherExc.LoadStudentDML(strQry);
        }
        public Subject[] LoadSubjects(int iClass)
        {
            strQry = string.Format(@"SELECT iSubjectId, iClass, sSubjectName, iMaxMark, ISNULL(bIsDefaultSubject, 0)[bIsDefaultSubject]
                                        FROM tbl_Subjects 
                                        WHERE iClass = {0}",
                                    iClass);
            return oTeacherExc.LoadSubjectsDML(strQry, iClass);
        }

        public Marks[] LoadCreateDMarksheet(int iClass, int iYear)
        {
            strQry = string.Format(@"select a.iMarkSheetId, a.iStdId, d.sUserName, a.iClass, a.iYear from tbl_MarkSheet a
                                        join tbl_StudentClasswiseInfo b on b.iStdId=a.iStdId
                                        JOIN tbl_Students c ON c.iRegistrationId = b.iRegistrationId
										join tbl_UserRegistration d on d.iRegistrationId =b.iRegistrationId
                                        where a.iClass = {0} and a.iYear = {1}",
                                        iClass,
                                        iYear);
            return oTeacherExc.LoadCreateDMarksheetDML(strQry);
        }
        public IdValuePair[] LoadMarks(int iStdId)
        {
            strQry = string.Format(@"SELECT b.iExamType, b.English, b.Math, (b.English + b.Math)[Total] FROM tbl_MarkSheet a
                                    JOIN tbl_MarksOfSubjects b ON a.iMarkSheetId = b.iMarkSheetId
                                    WHERE a.iStdId = {0}",
                                    iStdId);
            return oTeacherExc.LoadMarkOfStudentDML(strQry);
        }

        //@@@
        #endregion

        #region Delete
        public SaveStatus DeleteSubject(int iSubjectId)
        {
            string strSubject = "";
            string sConstraintName = "";

            try
            {
                using (oCon = ConnectionHelper.Stablish)
                {
                    oTrans = oCon.BeginTransaction();
                    strQry = string.Format(@"SELECT sSubjectName FROM tbl_Subjects WHERE iSubjectId = {0}", iSubjectId);
                    cmd = new SqlCommand(strQry, oCon, oTrans);
                    strSubject = Convert.ToString(oCommonExc.CallExecuteScalar(cmd));

                    strQry = string.Format(@"DELETE FROM tbl_Subjects WHERE iSubjectId = {0}", iSubjectId);
                    cmd.CommandText = strQry;
                    oCommonExc.CallExecuteNonQuery(cmd);


                    sConstraintName = oCommonExc.SelectConstraintColumn("tbl_MarksOfSubjects", strSubject);
                    strQry = string.Format("ALTER TABLE tbl_MarksOfSubjects DROP CONSTRAINT {0}", sConstraintName);
                    cmd.CommandText = strQry;
                    oCommonExc.CallExecuteScalar(cmd);

                    strQry = string.Format("ALTER TABLE tbl_MarksOfSubjects DROP COLUMN [{0}]", strSubject);
                    cmd.CommandText = strQry;
                    oCommonExc.CallExecuteScalar(cmd);

                    oSaveStatus.iStatus = 1;
                    oTrans.Commit();
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
        #endregion































        public SaveStatus DeleteStudents(IdStatusPair[] arrIdStatusPair, bool bDeletePermanent)
        {
            string tbl_StudentAttendance = "";
            string tbl_Fees = "";
            string tbl_MarkSheet = "";
            string tbl_MarksOfSubjects = "";
            string tbl_Wallet = "";
            string tbl_MonthlyFee = "";
            string tbl_StudentFees = "";
            string tbl_StudentClasswiseInfo = "";
            string tbl_Students = "";
            //string tbl_UserRegistration = "";

            if (!bDeletePermanent)
            {
                if (arrIdStatusPair != null)
                {
                    for (int i = 0; i < arrIdStatusPair.Length; i++)
                    {
                        strQry += string.Format(@"UPDATE tbl_StudentClasswiseInfo SET bisactive = {0} WHERE iRegistrationId = {1}",
                            arrIdStatusPair[i].Status == true ? 0 : 1,
                            arrIdStatusPair[i].ID);
                    }
                }
            }
            else
            {
                if (arrIdStatusPair != null)
                {
                    for (int i = 0; i < arrIdStatusPair.Length; i++)
                    {
                        tbl_StudentAttendance += string.Format(@"DELETE FROM tbl_StudentAttendance WHERE iStdId in (SELECT iStdId FROM tbl_StudentClasswiseInfo WHERE iRegistrationId = {0})", arrIdStatusPair[i].ID);

                        tbl_Fees += string.Format(@"DELETE from tbl_Fees WHERE iStdId in (SELECT iStdId FROM tbl_StudentClasswiseInfo WHERE iRegistrationId = {0})", arrIdStatusPair[i].ID);

                        
                        tbl_MarksOfSubjects += string.Format(@"DELETE FROM tbl_MarksOfSubjects WHERE iMarkSheetId IN (SELECT iMarkSheetId FROM tbl_MarkSheet
                            WHERE iStdId IN (SELECT iStdId FROM tbl_StudentClasswiseInfo WHERE iRegistrationId = {0})
                        )", arrIdStatusPair[i].ID);

                        tbl_MarkSheet += string.Format(@"DELETE FROM tbl_MarkSheet WHERE iStdId in (SELECT iStdId FROM tbl_StudentClasswiseInfo WHERE iRegistrationId = {0})", arrIdStatusPair[i].ID);

                        tbl_Wallet += string.Format(@"Delete from tbl_Wallet where iMonthlyFeeId in (
                        select iMonthlyFeeId from tbl_MonthlyFee where iStudentFeesId  in (
                        select iStudentFeesId FROM tbl_StudentFees WHERE iStdId in (SELECT iStdId FROM tbl_StudentClasswiseInfo WHERE iRegistrationId = {0})
                        ))", arrIdStatusPair[i].ID);

                        tbl_MonthlyFee += string.Format(@"DELETE FROM tbl_MonthlyFee WHERE iStudentFeesId in (
                            select iStudentFeesId FROM tbl_StudentFees WHERE iStdId in (SELECT iStdId FROM tbl_StudentClasswiseInfo WHERE iRegistrationId = {0})
                        )", arrIdStatusPair[i].ID);

                        tbl_StudentFees += string.Format(@"DELETE FROM tbl_StudentFees WHERE iRegistrationId = {0}", arrIdStatusPair[i].ID);

                        tbl_StudentClasswiseInfo += string.Format(@"DELETE FROM tbl_StudentClasswiseInfo WHERE iRegistrationId = {0}", arrIdStatusPair[i].ID);

                        tbl_Students += string.Format(@"DELETE FROM tbl_Students WHERE iRegistrationId = {0};", arrIdStatusPair[i].ID);

                        strQry += string.Format(@"DELETE FROM tbl_UserRegistration WHERE iRegistrationId = {0};", arrIdStatusPair[i].ID);
                    }
                }
                oCommonExc.DeleteOrUpdate(tbl_StudentAttendance);
                oCommonExc.DeleteOrUpdate(tbl_Fees);
                oCommonExc.DeleteOrUpdate(tbl_MarksOfSubjects);
                oCommonExc.DeleteOrUpdate(tbl_MarkSheet);
                oCommonExc.DeleteOrUpdate(tbl_Wallet);
                oCommonExc.DeleteOrUpdate(tbl_MonthlyFee);
                oCommonExc.DeleteOrUpdate(tbl_StudentFees);
                oCommonExc.DeleteOrUpdate(tbl_StudentClasswiseInfo);
                oCommonExc.DeleteOrUpdate(tbl_Students);
            }
            return oCommonExc.DeleteOrUpdate(strQry); //Final exec
        }
        
        public SaveStatus DoActiveInActiveStudent(int[] arrStudentIds, bool bActive)
        {
            try
            {
                using (oCon = ConnectionHelper.Stablish)
                {
                    if (arrStudentIds != null)
                    {
                        for (int i = 0; i < arrStudentIds.Length; i++)
                        {
                            strQry += string.Format(@"UPDATE tbl_StudentClasswiseInfo set bIsActive = {0} where iStdId = {1};",
                                                bActive ? 1 : 0, arrStudentIds[i]);
                        }
                        cmd = new SqlCommand(strQry, oCon);
                        oSaveStatus.iStatus = oCommonExc.CallExecuteNonQuery(cmd);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return oSaveStatus;
        }
                
        public int GetIdentity(string strQuery, SqlParameter[] param)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQuery, oCon);
                foreach (var item in param)
                    cmd.Parameters.Add(item);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        

        //public List<Marks> LoadPreparedMarkSheets(int iClass, int iYear)
        //{
        //    strQry = string.Format(@"SELECT m.iStdId, a.sStdName, m.iClass, m.iYear FROM tbl_MarkSheet m
        //                                JOIN tbl_StudentClasswiseInfo i ON i.iStdId=m.iStdId
        //                                JOIN tbl_Students a ON a.iRegistrationId = i.iRegistrationId
        //                                WHERE m.iClass = {0} and m.iYear = {1}",
        //                                iClass,
        //                                iYear);
        //    return oTeacherExc.LoadPreparedMarkSheetsDML(strQry);
        //}

        


        
        public SaveStatus DeleteTeachers(IdStatusPair[] arrTeachers, bool bDeletePermanent)
        {
            //NOTE : Dont Delete from db set it as DeActive.
            strQry = "";
            for (int iRow = 0; iRow < arrTeachers.Length; iRow++)
            {
                if (bDeletePermanent)
                {
                    strQry += string.Format("Delete from tbl_Teachers WHERE iTeacherId = {0}\n",
                        arrTeachers[iRow].ID);
                }
                else
                {
                    strQry += string.Format("UPDATE tbl_Teachers SET bActive = {0} WHERE iTeacherId = {1}\n",
                        Convert.ToBoolean(arrTeachers[iRow].Status) != true ? 1 : 0,
                        arrTeachers[iRow].ID);
                }
            }

            return oCommonExc.DeleteOrUpdate(strQry);
        }

        public SaveStatus DoActiveInActiveTeacher(int[] arrTeacherIds, bool bDoActive)
        {
            if (arrTeacherIds != null)
            {
                for (int i = 0; i < arrTeacherIds.Length; i++)
                {
                    strQry += string.Format(@"UPDATE tbl_Teachers set bActive = {0} where iRegistrationId = {1};",
                                        bDoActive ? 1 : 0, arrTeacherIds[i]);
                }
                return oCommonExc.DeleteOrUpdate(strQry);
            }
            return new SaveStatus();
        }
        



        
        
        
        
        public SaveStatus DeleteSubjectold(int iSubjectId)
        {
            string strSubject = "";
            strQry = string.Format(@"SELECT sSubjectName FROM tbl_Subjects WHERE iSubjectId = {0}", iSubjectId);
            strSubject = Convert.ToString(oCommonExc.GetScalerValue(strQry));

            strQry = string.Format(@"DELETE FROM tbl_Subjects WHERE iSubjectId = {0}", iSubjectId);
            if (oCommonExc.DeleteOrUpdate(strQry).iStatus > 0)
            {
                strQry = string.Format("ALTER TABLE tbl_MarksOfSubjects DROP COLUMN [{0}]", strSubject);
                return oCommonExc.Alter(strQry);
            }
            return oSaveStatus;
        }
        public SaveStatus UpdateSubject(Subject oSubject)
        {
            strQry = string.Format(@"UPDATE tbl_Subjects SET sSubjectName='{0}', iMaxMark = {1} WHERE iSubjectId = {2}",
                oSubject.SubjectName,
                oSubject.MaximumMarks,
                oSubject.SubjectId);
            return oCommonExc.DeleteOrUpdate(strQry);
        }

        public List<StudentAttendance> LoadStudentForAttendance(int iClass)
        {
            strQry = string.Format(@"SELECT c.iStdId, b.sUserName, c.iCurrentClass
                                        FROM tbl_Students a
										join tbl_UserRegistration b on b.iRegistrationId = a.iRegistrationId
                                        JOIN tbl_StudentClasswiseInfo c ON c.iRegistrationId = b.iRegistrationId 
                                        WHERE a.bIsActive = 1 and c.iCurrentClass = {0}
                                        ORDER BY 1", iClass);
            return oTeacherExc.LoadStudentForAttendanceDML(strQry);
        }
        
        
        public StudentMarksheet LoadStudentsMarksheet(int iClass, int iYear)
        {
            StudentMarksheet oStudentMarksheet = new StudentMarksheet();
            oStudentMarksheet.Inputs = new Inputs(Class: iClass, Year: iYear);
            oStudentMarksheet.StudentsMarks = LoadStudentsMarks(iClass, iYear);
            oStudentMarksheet.ExamTypeSubjects = oStudentMarksheet.StudentsMarks[0].SemesterData;
            return oStudentMarksheet;
        }
        private Marks[] LoadStudentsMarks(int iClass, int iYear)
        {
            strQry = "";
            string sColName = "";
            string sColNameTotal = "";
            SemesterData oSemesterData = null;
            List<string> lstSubjectColumns = null;
            StudentCurrentInfo oStudentCurrentInfo = null;
            StudentCurrentInfo[] arrStudentCurrentInfo = null;
            Marks oMarks = null;
            List<Marks> lstMarks = new List<Marks>();

            strQry = string.Format(@"SELECT m.iMarkSheetId, m.iStdId, a.sUserName, m.LoadCreateDMarksheetiClass, m.iYear FROM tbl_MarkSheet m
                                    JOIN tbl_StudentClasswiseInfo i ON i.iStdId = m.iStdId
                                    JOIN tbl_Students a ON a.iRegistrationId = i.iRegistrationId
                                    WHERE m.iClass = {0} AND m.iYear = {1}",
                                    iClass, iYear);

            arrStudentCurrentInfo = oTeacherExc.LoadStudentCurrentInfo(strQry);//1

            if (arrStudentCurrentInfo != null)
            {
                strQry = @"SELECT name [sSubjectColumnName] FROM sys.columns WHERE object_id in 
                                    (
                                        SELECT object_id FROM sys.tables WHERE NAME ='tbl_MarksOfSubjects'
                                    ) AND name <> 'iMarkSheetId' AND name <> 'iExamType'";
                lstSubjectColumns = oTeacherExc.LoadSubjectColumns(strQry);//2

                for (int iStd = 0; iStd < arrStudentCurrentInfo.Length; iStd++)
                {
                    oStudentCurrentInfo = arrStudentCurrentInfo[iStd];

                    oMarks = new Marks();
                    oMarks.StdId = oStudentCurrentInfo.StdId;
                    oMarks.StdName = oStudentCurrentInfo.StdName;
                    oMarks.SubjectColumns = lstSubjectColumns.ToArray();
                    oMarks.MarkSheetId = oStudentCurrentInfo.MarkSheetId;
                    oMarks.StdCurrentInfo = oStudentCurrentInfo;//Fill Rowwise std.

                    if (arrStudentCurrentInfo[iStd].MarkSheetId > 0)
                    {
                        sColName = sColNameTotal = "";

                        for (int iCol = 0; iCol < lstSubjectColumns.Count; iCol++)
                        {
                            sColName += lstSubjectColumns[iCol] + ",";
                            sColNameTotal += string.Format("Sum({0})[{0}],", lstSubjectColumns[iCol]); //Sum(English)[English]
                        }
                        if (sColName != null)
                        {
                            sColName = sColName.Remove(sColName.Length - 1, 1);
                            sColNameTotal = sColNameTotal.Remove(sColNameTotal.Length - 1, 1);
                        }
                        strQry = string.Format("SELECT iExamType, {0} FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {1} ORDER BY iExamType",
                            sColName,
                            oStudentCurrentInfo.MarkSheetId);

                        //oSemesterData = oTeacherExc.LoadExamTypeMarks(strQry, lstSubjectColumns); //3
                        oMarks.SemesterData = oTeacherExc.LoadExamTypeMarks(strQry, lstSubjectColumns); //3;

                        lstMarks.Add(oMarks);
                    }
                }
                if (lstMarks != null && lstMarks.Count > 0)
                {
                    //Total of subjects marks
                    strQry = string.Format(@"SELECT iExamType, {0} FROM tbl_MarksOfSubjects GROUP BY iExamType", sColNameTotal);
                    lstMarks.Add(oTeacherExc.LoadSubjectTotal(strQry, lstSubjectColumns, null));
                }
            }
            return lstMarks.ToArray();
        }
        public Marks[] FillMarks2(List<Marks> lstMarks)
        {
            Marks oMarks = null;
            IdNamePair[] MM = null;
            List <string> lstColumns = null;
            string sColName = "";
            string sColNameTotal = "";


            if (lstMarks != null && lstMarks.Count > 0)
            {
                for (int iStd = 0; iStd < lstMarks.Count; iStd++)
                {
                    oMarks = lstMarks[iStd];
                    //strQry = @"SELECT name [sSubjectColumnName] FROM sys.columns WHERE object_id in 
                    //    (
                    //        SELECT object_id FROM sys.tables WHERE NAME ='tbl_MarksOfSubjects'
                    //    ) AND name <> 'iMarkSheetId' AND name <> 'iExamType'";
                    //lstColumns = oTeacherExc.LoadSubjectColumns(strQry);

                    strQry = string.Format(@"SELECT sSubjectName [sSubjectColumnName] FROM tbl_Subjects a
                            JOIN (
	                            SELECT name [sSubjectColumnName] FROM sys.columns WHERE object_id IN 
	                            (
		                            SELECT object_id FROM sys.tables WHERE NAME ='tbl_MarksOfSubjects'
	                            ) AND name <> 'iMarkSheetId' AND name <> 'iExamType'
                            )temp ON a.sSubjectName IN (temp.sSubjectColumnName)
                            WHERE a.iClass = {0}", lstMarks[iStd].Class);
                    lstColumns = oTeacherExc.LoadSubjectColumns(strQry);

                    sColName = "";
                    sColNameTotal = "";
                    for (int iCol = 0; iCol < lstColumns.Count; iCol++)
                    {
                        sColName += lstColumns[iCol] + ",";
                        sColNameTotal += string.Format("Sum({0})[{0}],", lstColumns[iCol]); //Sum(English)[English]
                    }
                    if (string.IsNullOrEmpty(sColName) == false)
                    {
                        sColName = sColName.Remove(sColName.Length - 1, 1);
                        sColNameTotal = sColNameTotal.Remove(sColNameTotal.Length - 1, 1);
                    }

                    //Start : Get MM
                    if (string.IsNullOrEmpty(sColName) == false)
                    {
                        StringBuilder sb = new StringBuilder();
                        string[] sColNameMM = sColName.Split(',');
                        for (int iSplitCol = 0; iSplitCol < sColNameMM.Length; iSplitCol++)
                        {
                            sb.Append("'");
                            sb.Append(sColNameMM[iSplitCol]);
                            sb.Append("'");
                            if (iSplitCol < sColNameMM.Length - 1)
                                sb.Append(",");
                        }
                        //End : Get MM
                        strQry = string.Format(@"SELECT a.iMaxMark, a.sSubjectName FROM tbl_Subjects a
                                        --JOIN tbl_MaxMarks b ON a.iSubjectId = b.iSubjectId
                                        WHERE a.sSubjectName IN ({0})",
                                           sb.ToString());
                        MM = oTeacherExc.GetMaxMark(strQry);
                        //for (int iSub = 0; iSub < lstMarks.Count; iSub++)
                        {
                            //oMarks = lstMarks[iStd];
                            strQry = string.Format("SELECT iExamType, {0} FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {1} ORDER BY iExamType",
                                sColName,
                                lstMarks[iStd].MarkSheetId);


                            //lstMarks[iSub].Subjects = oTeacherExc.LoadMarksDML(strQry, lstColumns);
                            //oMarks.SemesterData= oTeacherExc.LoadMarksDML2(strQry, lstColumns);
                            oMarks.SemesterData = oTeacherExc.LoadMarksDML3(strQry, lstColumns, MM);
                        }
                        ////Total of subjects marks
                        //strQry = string.Format(@"SELECT iExamType, {0} FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {1} GROUP BY iExamType",
                        //    sColNameTotal, lstMarks[iStd].MarkSheetId);
                        //Update Grand Total for GrandRow
                        for (int iMM = 0; iMM < MM.Length; iMM++)
                        {
                            MM[iMM].ID *= lstMarks.Count; //here update grand total column value for all column as per MM and Total no of student rows
                        }
                        //oMarks = oTeacherExc.LoadSubjectTotal(strQry, lstColumns, MM);
                    }
                    //lstMarks.Add(oMarks);
                }
                //Total of subjects marks
                strQry = string.Format(@"SELECT iExamType, {0} FROM tbl_MarksOfSubjects GROUP BY iExamType",
                    sColNameTotal);
                oMarks = oTeacherExc.LoadSubjectTotal(strQry, lstColumns, MM);
                lstMarks.Add(oMarks);
            }
            return lstMarks.ToArray();
        }


        public Marks[] FillMarks1(List<Marks> lstMarks)
        {
            Marks oMarks = null;
            List<string> lstColumns = null;
            string sColName = "";
            string sColNameTotal = "";


            if (lstMarks != null && lstMarks.Count > 0)
            {
                //strQry = @"SELECT name [sSubjectColumnName] FROM sys.columns WHERE object_id in 
                //    (
                //        SELECT object_id FROM sys.tables WHERE NAME ='tbl_MarksOfSubjects'
                //    ) AND name <> 'iMarkSheetId' AND name <> 'iExamType'";
                //lstColumns = oTeacherExc.LoadSubjectColumns(strQry);

                strQry = string.Format(@"SELECT sSubjectName [sSubjectColumnName] FROM tbl_Subjects a
                            JOIN (
	                            SELECT name [sSubjectColumnName] FROM sys.columns WHERE object_id IN 
	                            (
		                            SELECT object_id FROM sys.tables WHERE NAME ='tbl_MarksOfSubjects'
	                            ) AND name <> 'iMarkSheetId' AND name <> 'iExamType'
                            )temp ON a.sSubjectName IN (temp.sSubjectColumnName)
                            WHERE a.iClass = {0}", lstMarks[0].Class);
                lstColumns = oTeacherExc.LoadSubjectColumns(strQry);


                for (int iCol = 0; iCol < lstColumns.Count; iCol++)
                {
                    sColName += lstColumns[iCol] + ",";
                    sColNameTotal += string.Format("Sum({0})[{0}],", lstColumns[iCol]); //Sum(English)[English]
                }
                if (sColName != null)
                {
                    sColName = sColName.Remove(sColName.Length - 1, 1);
                    sColNameTotal = sColNameTotal.Remove(sColNameTotal.Length - 1, 1);
                }

                //Start : Get MM
                StringBuilder sb = new StringBuilder();
                string[] sColNameMM = sColName.Split(',');
                for (int iSplitCol = 0; iSplitCol < sColNameMM.Length; iSplitCol++)
                {
                    sb.Append("'");
                    sb.Append(sColNameMM[iSplitCol]);
                    sb.Append("'");
                    if (iSplitCol < sColNameMM.Length - 1)
                        sb.Append(",");
                }
                //End : Get MM

                strQry = string.Format(@"SELECT a.iMaxMark, a.sSubjectName FROM tbl_Subjects a
                                        --JOIN tbl_MaxMarks b ON a.iSubjectId = b.iSubjectId
                                        WHERE a.sSubjectName IN ({0})",
                                        sb.ToString());
                IdNamePair[] MM = oTeacherExc.GetMaxMark(strQry);
                for (int iSub = 0; iSub < lstMarks.Count; iSub++)
                {
                    oMarks = lstMarks[iSub];
                    strQry = string.Format("SELECT iExamType, {0} FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {1} ORDER BY iExamType",
                        sColName,
                        lstMarks[iSub].MarkSheetId);


                    //lstMarks[iSub].Subjects = oTeacherExc.LoadMarksDML(strQry, lstColumns);
                    //oMarks.SemesterData= oTeacherExc.LoadMarksDML2(strQry, lstColumns);
                    oMarks.SemesterData = oTeacherExc.LoadMarksDML3(strQry, lstColumns, MM);
                }

                //Total of subjects marks
                strQry = string.Format(@"SELECT iExamType, {0} FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {1} GROUP BY iExamType", 
                    sColNameTotal, lstMarks[0].MarkSheetId);
                //Update Grand Total for GrandRow
                for (int iMM = 0; iMM < MM.Length; iMM++)
                {
                    MM[iMM].ID *= lstMarks.Count; //here update grand total column value for all column as per MM and Total no of student rows
                }
                oMarks = oTeacherExc.LoadSubjectTotal(strQry, lstColumns, MM);
                lstMarks.Add(oMarks);
            }
            return lstMarks.ToArray();
        }
        public SaveStatus SaveSubjectMaxMarks(SubjectMaxMarks[] arrSubjectMaxMarks)
        {
            //Update iMaxMark in tbl_Subjects table 
            strQry = "";
            if (arrSubjectMaxMarks != null)
            {
                //cmd = new SqlCommand();

                //NOTE IT SHOULD BE WORK
                //for (int iMark = 0; iMark < arrSubjectMaxMarks.Length; iMark++)
                //{
                //    strQry += string.Format(@"INSERT INTO tbl_MaxMarks (iClass, iSubjectId, iMaxMark) VALUES (@iClass_{0}, @iSubjectId_{0}, @iMaxMark_{0});\n", iMark);
                //    cmd.Parameters.AddWithValue("@iClass_{0}", arrSubjectMaxMarks[iMark].Class);
                //    cmd.Parameters.AddWithValue("@iSubjectId_{0}", arrSubjectMaxMarks[iMark].SubjectId);
                //    cmd.Parameters.AddWithValue("@iMaxMark_{0}", Convert.ToInt32(arrSubjectMaxMarks[iMark].MaxMark));
                //}

                for (int iMark = 0; iMark < arrSubjectMaxMarks.Length; iMark++)
                {
                    //strQry += string.Format(@"INSERT INTO tbl_MaxMarks (iClass, iSubjectId, iMaxMark) VALUES ({0}, {1}, {2});",
                    //    arrSubjectMaxMarks[iMark].Class,
                    //    arrSubjectMaxMarks[iMark].SubjectId,
                    //    arrSubjectMaxMarks[iMark].MaxMark);

                    strQry += string.Format(@"Update tbl_Subjects set iMaxMark = {0} where iSubjectId = {1}", 
                        arrSubjectMaxMarks[iMark].MaxMark, 
                        arrSubjectMaxMarks[iMark].SubjectId);
                }
            }
            //cmd = new SqlCommand(strQry, ConnectionHelper.ConOpen());
            return oCommonExc.DeleteOrUpdate(strQry);
        }
        public IdNamePair[] LoadSubjects()
        {
            strQry = string.Format(@"select sSubjectName from tbl_Subjects");
            return oTeacherExc.LoadSubjectsDML(strQry);
        }
        
        public int SaveRegistration(Registration oRegistration)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                strQry = string.Format(@"INSERT INTO tbl_UserRegistration (sUserName, sLoginName, sUserPassword, iUserType) 
                                    VALUES (@sUserName, @sLoginName, @sUserPassword, @iUserType) Select @@IDENTITY");

                cmd = new SqlCommand(strQry, oCon);
                cmd.Parameters.AddWithValue("@sUserName", oRegistration.UserName);
                cmd.Parameters.AddWithValue("@sLoginName", oRegistration.LoginName);
                cmd.Parameters.AddWithValue("@sUserPassword", oRegistration.UserPassword);
                cmd.Parameters.AddWithValue("@iUserType", oRegistration.UserType);
                return oCommonExc.GetIdentityOnSave(cmd);
            }
        }
        public IdNamePair[] LoadTeachers(int iRegId)
        {
            strQry = string.Format(@"select iRegistrationId,sUserName from tbl_UserRegistration where iUserType = 2 and iRegistrationId <> {0}", iRegId);
            return oTeacherExc.LoadTeachersDML(strQry);
        }
        public IdNamePair[] LoadChats(int iFrom, int iTo)
        {
            strQry = string.Format(@"select iUserIdFrom, sMessage from tbl_Chatting
                                    where(iUserIdFrom = {0} and iUserIdTo = {1})
                                    or(iUserIdFrom = {1} and iUserIdTo = {0})
                                    order by iChatTime",
                                    iFrom, iTo);
            return oTeacherExc.LoadChatsDML(strQry);
        }
        public SaveStatus SaveChats(Chatting oChat)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                strQry = "INSERT INTO tbl_Chatting (iUserIdFrom, iUserIdTo, sMessage, iChatTime) VALUES (@iUserIdFrom, @iUserIdTo, @sMessage, @iChatTime)";
                cmd = new SqlCommand(strQry, oCon);
                cmd.Parameters.AddWithValue("@iUserIdFrom", oChat.iUserIdFrom);
                cmd.Parameters.AddWithValue("@iUserIdTo", oChat.iUserIdTo);
                cmd.Parameters.AddWithValue("@sMessage", oChat.sMessage);
                cmd.Parameters.AddWithValue("@iChatTime", oChat.iChatTime);
                return oCommonExc.Save(cmd);
            }
        }

        public List<AdmissionDetails> LoadAllStudents()
        {
            strQry = string.Format(@"SELECT DISTINCT a.iRegistrationId, b.sUserName, sAddress, sCity, iContact, iAdmissionInClass, iDOB, c.bIsActive, iAdmissionDate, biStudentImage 
                                        FROM tbl_Students a
										join tbl_UserRegistration b on a.iRegistrationId =b.iRegistrationId
                                        JOIN tbl_StudentClasswiseInfo c ON c.iRegistrationId = b.iRegistrationId
                                        ORDER BY 1");
            return oTeacherExc.LoadAllStudentsDML(strQry);
        }
        public Registration[] LoadRegisteredUsers(int iUserType)
        {
            string sSubQuery = "";
            if (iUserType == 2)//LoadTeacher
            {
                sSubQuery = "SELECT iRegistrationId FROM tbl_Teachers";
            }
            else if(iUserType == 3 || iUserType == (int)Modules.Admission)//LoadStudent
            {
                sSubQuery = "SELECT iRegistrationId FROM tbl_Students";
            }
            strQry = string.Format(@"SELECT iRegistrationId, sUserName, iUserType FROM tbl_UserRegistration WHERE iUserType = {0} AND iRegistrationId NOT IN 
                                    (
                                     {1}
                                    )ORDER BY sUserName", iUserType, sSubQuery);
            return oTeacherExc.LoadRegisteredUsersDML(strQry);
        }

        //public Marks[] LoadSemesterMarksheet(int iClass, int iYear, int iExamType)
        //{
        //    strQry = string.Format(@"select a.iRegistrationId, d.iStdId, a.sUserName, e.HindiForClass1, c.iCurrentClass
        //                        from tbl_UserRegistration a 
        //                        join tbl_Students b on b.iRegistrationId=a.iRegistrationId
        //                        join tbl_StudentClasswiseInfo c on c.iRegistrationId=a.iRegistrationId
        //                        join tbl_MarkSheet d on d.iStdId=c.iStdId
        //                        left join tbl_MarksOfSubjects e on e.iMarkSheetId = d.iMarkSheetId
        //                        where a.iUserType = 3 and b.bIsActive = 1 
        //                        and c.bResultPrepared = 1 
        //                        and dbo.GetDatepart('y', c.iCurrentYear) = {0}
        //                        and c.iCurrentClass = {1} 
        //                        --and c.iCurrentClass = d.iClass
        //                        and e.iExamType = {2}", iYear, iClass, iExamType);
        //    return oTeacherExc.LoadSemesterMarksheetDML(strQry);
        //}

        public Marks[] LoadSemesterMarksheet(int iClass, int iYear, int iExamType)
        {
            strQry = "";
            string sColName = "";
            string sColNameTotal = "";
            SemesterData oSemesterData = null;
            List<string> lstSubjectColumns = null;
            StudentCurrentInfo oStudentCurrentInfo = null;
            StudentCurrentInfo[] arrStudentCurrentInfo = null;
            Marks oMarks = null;
            List<Marks> lstMarks = new List<Marks>();

            strQry = string.Format(@"SELECT m.iMarkSheetId, m.iStdId, a.sUserName, m.iClass, m.iYear FROM tbl_MarkSheet m
                                    JOIN tbl_StudentClasswiseInfo i ON i.iStdId = m.iStdId
                                    JOIN tbl_Students a ON a.iRegistrationId = i.iRegistrationId
                                    WHERE m.iClass = {0} AND m.iYear = {1}",
                                    iClass, iYear);

            arrStudentCurrentInfo = oTeacherExc.LoadStudentCurrentInfo(strQry);//1

            if (arrStudentCurrentInfo != null)
            {
                strQry = @"SELECT name [sSubjectColumnName] FROM sys.columns WHERE object_id in 
                                    (
                                        SELECT object_id FROM sys.tables WHERE NAME ='tbl_MarksOfSubjects'
                                    ) AND name <> 'iMarkSheetId' AND name <> 'iExamType'";
                lstSubjectColumns = oTeacherExc.LoadSubjectColumns(strQry);//2

                for (int iStd = 0; iStd < arrStudentCurrentInfo.Length; iStd++)
                {
                    oStudentCurrentInfo = arrStudentCurrentInfo[iStd];

                    oMarks = new Marks();
                    oMarks.StdId = oStudentCurrentInfo.StdId;
                    oMarks.StdName = oStudentCurrentInfo.StdName;
                    oMarks.SubjectColumns = lstSubjectColumns.ToArray();
                    oMarks.MarkSheetId = oStudentCurrentInfo.MarkSheetId;
                    oMarks.StdCurrentInfo = oStudentCurrentInfo;//Fill Rowwise std.

                    if (arrStudentCurrentInfo[iStd].MarkSheetId > 0)
                    {
                        sColName = sColNameTotal = "";

                        for (int iCol = 0; iCol < lstSubjectColumns.Count; iCol++)
                        {
                            sColName += lstSubjectColumns[iCol] + ",";
                            sColNameTotal += string.Format("Sum({0})[{0}],", lstSubjectColumns[iCol]); //Sum(English)[English]
                        }
                        if (sColName != null)
                        {
                            sColName = sColName.Remove(sColName.Length - 1, 1);
                            sColNameTotal = sColNameTotal.Remove(sColNameTotal.Length - 1, 1);
                        }
                        strQry = string.Format("SELECT iExamType, {0} FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {1} ORDER BY iExamType",
                            sColName,
                            oStudentCurrentInfo.MarkSheetId);

                        //oSemesterData = oTeacherExc.LoadExamTypeMarks(strQry, lstSubjectColumns); //3
                        oMarks.SemesterData = oTeacherExc.LoadExamTypeMarks(strQry, lstSubjectColumns); //3;

                        lstMarks.Add(oMarks);
                    }
                }
                if (lstMarks != null && lstMarks.Count > 0)
                {
                    //Total of subjects marks
                    strQry = string.Format(@"SELECT iExamType, {0} FROM tbl_MarksOfSubjects GROUP BY iExamType", sColNameTotal);
                    lstMarks.Add(oTeacherExc.LoadSubjectTotal(strQry, lstSubjectColumns, null));
                }
            }
            return lstMarks.ToArray();
        }

        public Marks[] FillMarks(List<Marks> lstMarks, int iExamType = 0)
        {
            Marks oMarks = null;
            IdNamePair[] MM = null;
            List<string> lstColumns = null;
            string sColName = "";
            string sColNameTotal = "";


            if (lstMarks != null && lstMarks.Count > 0)
            {
                for (int iStd = 0; iStd < lstMarks.Count; iStd++)
                {
                    oMarks = lstMarks[iStd];
                    //strQry = @"SELECT name [sSubjectColumnName] FROM sys.columns WHERE object_id in 
                    //    (
                    //        SELECT object_id FROM sys.tables WHERE NAME ='tbl_MarksOfSubjects'
                    //    ) AND name <> 'iMarkSheetId' AND name <> 'iExamType'";
                    //lstColumns = oTeacherExc.LoadSubjectColumns(strQry);

                    strQry = string.Format(@"SELECT sSubjectName [sSubjectColumnName] FROM tbl_Subjects a
                            JOIN (
	                            SELECT name [sSubjectColumnName] FROM sys.columns WHERE object_id IN 
	                            (
		                            SELECT object_id FROM sys.tables WHERE NAME ='tbl_MarksOfSubjects'
	                            ) AND name <> 'iMarkSheetId' AND name <> 'iExamType'
                            )temp ON a.sSubjectName IN (temp.sSubjectColumnName)
                            WHERE a.iClass = {0}", lstMarks[iStd].Class);
                    lstColumns = oTeacherExc.LoadSubjectColumns(strQry);

                    sColName = "";
                    sColNameTotal = "";
                    for (int iCol = 0; iCol < lstColumns.Count; iCol++)
                    {
                        sColName += lstColumns[iCol] + ",";
                        sColNameTotal += string.Format("Sum({0})[{0}],", lstColumns[iCol]); //Sum(English)[English]
                    }
                    if (string.IsNullOrEmpty(sColName) == false)
                    {
                        sColName = sColName.Remove(sColName.Length - 1, 1);
                        sColNameTotal = sColNameTotal.Remove(sColNameTotal.Length - 1, 1);
                    }

                    //Start : Get MM
                    if (string.IsNullOrEmpty(sColName) == false)
                    {
                        StringBuilder sb = new StringBuilder();
                        string[] sColNameMM = sColName.Split(',');
                        for (int iSplitCol = 0; iSplitCol < sColNameMM.Length; iSplitCol++)
                        {
                            sb.Append("'");
                            sb.Append(sColNameMM[iSplitCol]);
                            sb.Append("'");
                            if (iSplitCol < sColNameMM.Length - 1)
                                sb.Append(",");
                        }
                        //End : Get MM
                        strQry = string.Format(@"SELECT a.iMaxMark, a.sSubjectName FROM tbl_Subjects a
                                        --JOIN tbl_MaxMarks b ON a.iSubjectId = b.iSubjectId
                                        WHERE a.sSubjectName IN ({0})",
                                           sb.ToString());
                        MM = oTeacherExc.GetMaxMark(strQry);
                        //for (int iSub = 0; iSub < lstMarks.Count; iSub++)
                        {
                            //oMarks = lstMarks[iStd];
                            if (iExamType == 0)
                            {
                                strQry = string.Format("SELECT iExamType, {0} FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {1} ORDER BY iExamType",
                                sColName,
                                lstMarks[iStd].MarkSheetId);

                            }
                            else
                            {
                                //When loading Semester wise results for all students as per class and year.
                                strQry = string.Format("SELECT iExamType, {0} FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {1} and iExamType = {2}",
                                sColName,
                                lstMarks[iStd].MarkSheetId,
                                iExamType);
                            }
                            //lstMarks[iSub].Subjects = oTeacherExc.LoadMarksDML(strQry, lstColumns);
                            //oMarks.SemesterData= oTeacherExc.LoadMarksDML2(strQry, lstColumns);
                            oMarks.SemesterData = oTeacherExc.LoadMarksDML3(strQry, lstColumns, MM);
                        }
                        ////Total of subjects marks
                        //strQry = string.Format(@"SELECT iExamType, {0} FROM tbl_MarksOfSubjects WHERE iMarkSheetId = {1} GROUP BY iExamType",
                        //    sColNameTotal, lstMarks[iStd].MarkSheetId);
                        //Update Grand Total for GrandRow
                        for (int iMM = 0; iMM < MM.Length; iMM++)
                        {
                            MM[iMM].ID *= lstMarks.Count; //here update grand total column value for all column as per MM and Total no of student rows
                        }
                        //oMarks = oTeacherExc.LoadSubjectTotal(strQry, lstColumns, MM);
                    }
                    //lstMarks.Add(oMarks);
                }
                //Total of subjects marks
                if (iExamType == 0)
                {
                    strQry = string.Format(@"SELECT iExamType, {0} FROM tbl_MarksOfSubjects GROUP BY iExamType",
                        sColNameTotal);
                    oMarks = oTeacherExc.LoadSubjectTotal(strQry, lstColumns, MM);
                    lstMarks.Add(oMarks);
                }
                else
                {
                    strQry = string.Format(@"SELECT iExamType, {0} FROM tbl_MarksOfSubjects where iExamType = {1} GROUP BY iExamType",
                        sColNameTotal, iExamType);
                    oMarks = oTeacherExc.LoadSubjectTotal(strQry, lstColumns, MM);
                    lstMarks.Add(oMarks);
                }
            }
            return lstMarks.ToArray();
        }
    }
}
