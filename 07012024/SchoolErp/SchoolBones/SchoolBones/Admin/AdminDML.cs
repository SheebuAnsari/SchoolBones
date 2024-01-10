using DomLibrary;
using SchoolBones.Common;
using SchoolConfiguration;
using System;
using System.Data.SqlClient;

namespace SchoolBones.Admin
{
    public class AdminDML
    {
        string strQry = string.Empty;
        SaveStatus oSaveStatus = new SaveStatus();
        CommonExecuteDML oCommonExc = new CommonExecuteDML();
        AdminExecuteDML oAdminExc = new AdminExecuteDML();

        SqlCommand cmd = null;
        SqlConnection oCon = null;
        SqlTransaction oTrans = null;


        public SaveStatus SaveTeacherDetails(TeacherDetails oTeacherDetails)
        {
            //DateDS oDateDS = new DateDS();
            try
            {
                using (oCon = ConnectionHelper.Stablish)
                {
                    oTrans = oCon.BeginTransaction();

                    strQry = string.Format("Delete from tbl_Teachers where iRegistrationId = {0}", oTeacherDetails.RegistrationId);
                    cmd = new SqlCommand(strQry, oCon, oTrans);
                    cmd.ExecuteNonQuery();

                    strQry = string.Format(@"INSERT INTO tbl_Teachers(iRegistrationId, bIntMobile, sQualification, iJoiningDate, iExperiance, bActive, iCurrentYear) 
                                        VALUES (@iRegistrationId, @bIntMobile, @sQualification, @iJoiningDate, @iExperiance, @bActive, @iCurrentYear)");
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@iRegistrationId", oTeacherDetails.RegistrationId);
                    cmd.Parameters.AddWithValue("@bIntMobile", oTeacherDetails.Mobile);
                    cmd.Parameters.AddWithValue("@sQualification", oTeacherDetails.Qualification);
                    cmd.Parameters.AddWithValue("@iJoiningDate", oTeacherDetails.JoiningDate);
                    cmd.Parameters.AddWithValue("@iExperiance", oTeacherDetails.Experiance);
                    cmd.Parameters.AddWithValue("@bActive", oTeacherDetails.Active);
                    cmd.Parameters.AddWithValue("@iCurrentYear", DateDS.IntToDate(oTeacherDetails.JoiningDate, 0).Year);
                    cmd.ExecuteNonQuery();
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

        public TeacherDetails[] LoadTeacherDetails()
        {
            strQry = string.Format(@"SELECT a.iRegistrationId, a.sUserName, ISNULL(bIntMobile, 0)bIntMobile, sQualification, iJoiningDate, iExperiance,ISNULL(bActive,0)[bActive] 
                                    FROM tbl_UserRegistration a
                                    JOIN tbl_Teachers  b on a.iRegistrationId = b.iRegistrationId
                                    ORDER BY a.sUserName");
            return oAdminExc.LoadTeachers(strQry);
        }

        public TeacherDetails[] LoadActiveInActiveTeachers(bool bActive)
        {
            strQry = string.Format(@"SELECT a.iRegistrationId, a.sUserName, ISNULL(bIntMobile, 0)bIntMobile, sQualification, iJoiningDate, iExperiance,ISNULL(bActive,0)[bActive] 
                                    FROM tbl_UserRegistration a
                                    JOIN tbl_Teachers  b on a.iRegistrationId = b.iRegistrationId
                                    WHERE bActive = {0} ORDER BY a.sUserName",
                                    bActive == true ? 1 : 0);
            return oAdminExc.LoadTeachers(strQry);
        }

        //NOTE: use single loop in DeleteTeachers() and DeleteStudents()
        public SaveStatus DeleteTeachers(IdStatusPair[] arrTeachers, bool bDeletePermanent)
        {
            string strQryAttandence = "";

            if (!bDeletePermanent)
            {
                if (arrTeachers != null)
                {
                    for (int iRow = 0; iRow < arrTeachers.Length; iRow++)
                    {
                        strQry += string.Format("UPDATE tbl_Teachers SET bActive = {0} WHERE iRegistrationId = {1}",
                        Convert.ToBoolean(arrTeachers[iRow].Status) != true ? 1 : 0,
                        arrTeachers[iRow].ID);
                    }
                }
            }
            else
            {
                if (arrTeachers != null)
                {
                    for (int iRow = 0; iRow < arrTeachers.Length; iRow++)
                    {
                        strQryAttandence += string.Format(@"DELETE FROM tbl_TeacherAttandence WHERE iRegistrationId = {0}", arrTeachers[iRow].ID);

                        strQry += string.Format(@"DELETE FROM tbl_Teachers WHERE iRegistrationId = {0};", arrTeachers[iRow].ID);
                    }
                }
                oCommonExc.DeleteOrUpdate(strQryAttandence);
            }
            return oCommonExc.DeleteOrUpdate(strQry); //Final exec
        }


        public SaveStatus DoActiveInActiveTeacher(int[] arrTeacherIds, bool bActive)
        {
            try
            {
                using (oCon = ConnectionHelper.Stablish)
                {
                    if (arrTeacherIds != null)
                    {
                        for (int i = 0; i < arrTeacherIds.Length; i++)
                        {
                            strQry += string.Format(@"UPDATE tbl_Teachers set bActive = {0} where iRegistrationId = {1};",
                                                bActive ? 1 : 0, arrTeacherIds[i]);
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

















        //public IdNamePair[] LoadTeachers(int iRegId)
        //{
        //    strQry = string.Format(@"SELECT iuserid, sName FROM tbl_Teachers WHERE iuserid <> {0} OR iuserid IS NULL", iRegId);
        //    return oAdminExc.LoadTeachersDML(strQry);
        //}
        //public IdNamePair[] LoadChats(int iFrom, int iTo)
        //{
        //    strQry = string.Format(@"select iUserIdFrom, sMessage from tbl_Chatting
        //                            where(iUserIdFrom = {0} and iUserIdTo = {1})
        //                            or(iUserIdFrom = {1} and iUserIdTo = {0})
        //                            order by iChatTime",
        //                            iFrom, iTo);
        //    return oAdminExc.LoadChatsDML(strQry);
        //}
        //public SaveStatus SaveChats(Chatting oChat)
        //{
        //    strQry = "INSERT INTO tbl_Chatting (iUserIdFrom, iUserIdTo, sMessage, iChatTime) VALUES (@iUserIdFrom, @iUserIdTo, @sMessage, @iChatTime)";
        //    cmd = new SqlCommand(strQry, ConnectionHelper.ConOpen());
        //    cmd.Parameters.AddWithValue("@iUserIdFrom", oChat.iUserIdFrom);
        //    cmd.Parameters.AddWithValue("@iUserIdTo", oChat.iUserIdTo);
        //    cmd.Parameters.AddWithValue("@sMessage", oChat.sMessage);
        //    cmd.Parameters.AddWithValue("@iChatTime", oChat.iChatTime);
        //    return oCommonExc.Save(cmd);
        //}


        #region TimeTable
        public SaveStatus SaveTimeTableLayout(TimeTableLayout oLayout)
        {
            try
            {
                using (oCon = ConnectionHelper.Stablish)
                {
                    oTrans = oCon.BeginTransaction();
                    if (oLayout != null)
                    {
                        //strQry = string.Format(@"DELETE FROM tbl_StudentAttendance WHERE iDate = {0}", iDate);
                        //cmd = new SqlCommand(strQry, oCon, oTrans);
                        //oCommonExc.CallExecuteNonQuery(cmd);

                        strQry = string.Format(@"INSERT INTO tbl_TimeTableLayout (sLayoutName, iNoOfPeriod, iFirstPeriod, iDuration, iLunchBreak, iClass, iCreatedDate) 
                                                    VALUES (@sLayoutName, @iNoOfPeriod, @iFirstPeriod, @iDuration, @iLunchBreak, @iClass, @iCreatedDate)");

                        cmd = new SqlCommand(strQry, oCon, oTrans);
                        cmd.CommandText = strQry;
                        cmd.Parameters.AddWithValue("@sLayoutName", oLayout.LayoutName);
                        cmd.Parameters.AddWithValue("@iNoOfPeriod", oLayout.NoOfPeriod);
                        cmd.Parameters.AddWithValue("@iFirstPeriod", oLayout.FirstPeriod);
                        cmd.Parameters.AddWithValue("@iDuration", oLayout.Duration);
                        cmd.Parameters.AddWithValue("@iLunchBreak", oLayout.LunchBreak);
                        cmd.Parameters.AddWithValue("@iClass", oLayout.Class);
                        cmd.Parameters.AddWithValue("@iCreatedDate", oLayout.CreatedDate);

                        oSaveStatus.iStatus = oCommonExc.CallExecuteNonQuery(cmd);
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

        //public TimeTableLayout[] LoadTimeTableLayout(string sLayoutName = "" , int iClass = 0)
        //{
        //    string sFilter = "";
        //    if(string.IsNullOrEmpty(sLayoutName) == false || iClass > 0)
        //    {
        //        sFilter = string.Format("WHERE sLayoutName = {0} and iClass = {1}", sLayoutName, iClass);
        //    }
        //    strQry = string.Format(@"SELECT iLayoutId, sLayoutName, iNoOfPeriod, iFirstPeriod, iDuration, iLunchBreak, iClass, iCreatedDate 
        //                                FROM tbl_TimeTableLayout 
        //                                {0}", 
        //                                sFilter);
        //    return oAdminExc.LoadTimeTableLayoutDML(strQry);
        //}
        public TimeTableLayout[] LoadTimeTableLayout(int iLayoutId = 0)
        {
            string sFilter = "";
            if (iLayoutId > 0)
            {
                sFilter = string.Format("WHERE iLayoutId = {0}", iLayoutId);
            }
            strQry = string.Format(@"SELECT iLayoutId, sLayoutName, iNoOfPeriod, iFirstPeriod, iDuration, iLunchBreak, iClass, iCreatedDate 
                                        FROM tbl_TimeTableLayout 
                                        {0}",
                                        sFilter);
            return oAdminExc.LoadTimeTableLayoutDML(strQry);
        }
        public SaveStatus DeleteTimeTableLayout(int iLayoutId = 0)
        {
            strQry = string.Format("Delete FROM tbl_TimeTableRowData WHERE iLayoutId = {0}", iLayoutId);
            oCommonExc.DeleteOrUpdate(strQry);

            strQry = string.Format(@"Delete from tbl_TimeTableLayout where iLayoutId= {0}", iLayoutId);
            return oCommonExc.DeleteOrUpdate(strQry);
        }
        //public SaveStatus DeleteTimeTable(int iClass, int iLayout)
        //{
        //    strQry= "Delete from tbl_TimeTableRowData where"
        //    RowWiseData[] arrRowWiseData = oTimeTable.RowData;
        //    int iTimeTableId = 0;
        //    strQry = string.Format(@"SELECT iTimeTableId FROM tbl_TimeTable WHERE iClass = {0}", oTimeTable.Class);
        //    cmd = new SqlCommand(strQry, ConnectionHelper.ConOpen());
        //    iTimeTableId = Convert.ToInt32(cmd.ExecuteScalar());
        //    if (iTimeTableId == 0)
        //    {
        //        //insert                
        //        strQry = string.Format(@"INSERT into tbl_TimeTable (iClass, iCreatedDate) VALUES ({0}, {1}) SELECT @@IDENTITY",
        //            oTimeTable.Class,
        //            oTimeTable.CreatedDate);
        //        cmd = new SqlCommand(strQry, ConnectionHelper.ConOpen());
        //        iTimeTableId = Convert.ToInt32(cmd.ExecuteScalar());
        //    }
        //    else
        //    {
        //        //Update date only                
        //        strQry = string.Format(@"UPDATE tbl_TimeTable SET iCreatedDate = {1} WHERE iTimeTableId = {0}", iTimeTableId, oTimeTable.CreatedDate);
        //        cmd = new SqlCommand(strQry, ConnectionHelper.ConOpen());
        //        cmd.ExecuteNonQuery();
        //        strQry = string.Format(@"DELETE FROM tbl_TimeTableRowData WHERE iTimeTableId = {0}", iTimeTableId);
        //        cmd = new SqlCommand(strQry, ConnectionHelper.ConOpen());
        //        cmd.ExecuteNonQuery();
        //    }
        //    for (int iRow = 0; iRow < arrRowWiseData.Length; iRow++)
        //    {
        //        if (arrRowWiseData[iRow].CellData != null)
        //        {
        //            for (int iCol = 0; iCol < arrRowWiseData[iRow].CellData.Length; iCol++)
        //            {
        //                if (arrRowWiseData[iRow].CellData[iCol].RegId > 0 && arrRowWiseData[iRow].CellData[iCol].Name != "")
        //                {
        //                    strQry = string.Format(@"INSERT INTO tbl_TimeTableRowData (iRowInd, iColInd, iTeacherId, sTeacherName, sSubjectName, iTimeTableId) 
        //                                    VALUES ({0}, {1}, {2}, '{3}', '{4}', {5})",
        //                        arrRowWiseData[iRow].Day,
        //                        arrRowWiseData[iRow].CellData[iCol].ColId,
        //                        arrRowWiseData[iRow].CellData[iCol].RegId,
        //                        arrRowWiseData[iRow].CellData[iCol].Name,
        //                        arrRowWiseData[iRow].CellData[iCol].SubjectName,
        //                        iTimeTableId);
        //                    cmd = new SqlCommand(strQry, ConnectionHelper.ConOpen());
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //    }
        //    oSaveStatus.iStatus = 1; return oSaveStatus;
        //}
        public SaveStatus SaveTimeTable(TimeTable oTimeTable, int iLayoutId)
        {
            RowWiseData[] arrRowWiseData = oTimeTable.RowData;
            //int iTimeTableId = 0;

            using (oCon = ConnectionHelper.Stablish)
            {
                //strQry = string.Format(@"SELECT iTimeTableId FROM tbl_TimeTable WHERE iClass = {0}", oTimeTable.Class);
                //cmd = new SqlCommand(strQry, oCon);
                //iTimeTableId = Convert.ToInt32(cmd.ExecuteScalar());
                //if (iTimeTableId == 0)
                //{
                //    //insert                
                //    strQry = string.Format(@"INSERT into tbl_TimeTable (iClass, iCreatedDate) VALUES ({0}, {1}) SELECT @@IDENTITY",
                //        oTimeTable.Class,
                //        oTimeTable.CreatedDate);
                //    cmd = new SqlCommand(strQry, oCon);
                //    iTimeTableId = Convert.ToInt32(cmd.ExecuteScalar());
                //}
                //else
                //{
                //    //Update date only                
                //    strQry = string.Format(@"UPDATE tbl_TimeTable SET iCreatedDate = {1} WHERE iTimeTableId = {0}", iTimeTableId, oTimeTable.CreatedDate);
                //    cmd = new SqlCommand(strQry, oCon);
                //    cmd.ExecuteNonQuery();
                //    strQry = string.Format(@"DELETE FROM tbl_TimeTableRowData WHERE iTimeTableId = {0}", iTimeTableId);
                //    cmd = new SqlCommand(strQry, oCon);
                //    cmd.ExecuteNonQuery();
                //}
                if (arrRowWiseData != null && arrRowWiseData.Length > 0)
                {
                    DeleteTimeTable(iLayoutId);
                    for (int iRow = 0; iRow < arrRowWiseData.Length; iRow++)
                    {
                        if (arrRowWiseData[iRow].CellData != null)
                        {
                            for (int iCol = 0; iCol < arrRowWiseData[iRow].CellData.Length; iCol++)
                            {
                                if (arrRowWiseData[iRow].CellData[iCol].RegId > 0 && arrRowWiseData[iRow].CellData[iCol].Name != "")
                                {
                                    strQry = string.Format(@"INSERT INTO tbl_TimeTableRowData (iRowInd, iColInd, iTeacherId, sTeacherName, sSubjectName, iLayoutId) 
                                            VALUES ({0}, {1}, {2}, '{3}', '{4}', {5})",
                                        arrRowWiseData[iRow].Day,
                                        arrRowWiseData[iRow].CellData[iCol].ColId,
                                        arrRowWiseData[iRow].CellData[iCol].RegId,
                                        arrRowWiseData[iRow].CellData[iCol].Name,
                                        arrRowWiseData[iRow].CellData[iCol].SubjectName,
                                        iLayoutId);
                                    cmd = new SqlCommand(strQry, oCon);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            oSaveStatus.iStatus = 1; return oSaveStatus;
        }

        public SaveStatus DeleteTimeTable(int iLayoutId)
        {
            strQry = string.Format("Delete from tbl_TimeTableRowData WHERE iLayoutId = {0}", iLayoutId);
            return oCommonExc.DeleteOrUpdate(strQry);
        }
        public Cell[] LoadTimeTableData(int iLayoutId = 0)
        {
            //using (oCon = ConnectionHelper.Stablish)
            //{
            //    //strQry = string.Format("SELECT iCreatedDate FROM tbl_TimeTable WHERE iClass = {0}", iClass);
            //    //cmd = new SqlCommand(strQry, oCon);
            //    //iDate = Convert.ToInt32(cmd.ExecuteScalar());

            //}
            string sFilter = "";
            if (iLayoutId > 0)
            {
                sFilter = string.Format("Where iLayoutId = {0}", iLayoutId);
            }
            strQry = string.Format(@"SELECT * FROM tbl_TimeTableRowData {0}", sFilter);
            return oAdminExc.LoadTimeTableData(strQry);
        }

        public Subject[] LoadSubjects(int iClass = 0)
        {
            if(iClass == 0)
            {
                strQry = string.Format(@"SELECT iSubjectId, iClass, sSubjectName, iMaxMark, ISNULL(bIsDefaultSubject, 0)[bIsDefaultSubject]
                                        FROM tbl_Subjects");
            }
            else
            {
                strQry = string.Format(@"SELECT iSubjectId, iClass, sSubjectName, iMaxMark, ISNULL(bIsDefaultSubject, 0)[bIsDefaultSubject]
                                        FROM tbl_Subjects 
                                        WHERE iClass = {0}",
                        iClass);
            }
            return oAdminExc.LoadSubjectsDML(strQry, iClass);
        }
        #endregion
        #region Registration
        public SaveStatus DeleteRegistration(int iRegId)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                strQry += string.Format("Delete from tbl_UserRegistration WHERE iRegistrationId = {0}\n",
                     iRegId);
                cmd = new SqlCommand(strQry, oCon);
                cmd.ExecuteNonQuery();
                oSaveStatus.iStatus = 1;
                return oSaveStatus;
            }
        }
        #endregion
    }
}
