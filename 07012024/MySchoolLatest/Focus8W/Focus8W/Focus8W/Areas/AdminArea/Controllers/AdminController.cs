using Focus8W.BL;
using SchoolBones.Account;
using SchoolBones.Admin;
using SchoolBones.Common;

using SchoolBones.Teacher;
using System.Collections.Generic;
using System.Web.Mvc;
using DomLibrary;

namespace Focus8W.Areas.AdminArea.Controllers
{
    public class AdminController : Controller
    {
        TestingDML oTestingDML = new TestingDML();

        Menu[] arrMenu = null;
        CoreDML oCoreDML = new CoreDML();
        CommonDML oCommonDML = new CommonDML();
        SaveStatus oSaveStatus = new SaveStatus();
        AdminDML oAdminDML = new AdminDML();        //Can control Teacher
        TeacherDML oTeacherDML = new TeacherDML();  //Can control Student
        AccountsDML oAccountsDML = new AccountsDML();  //Can control Student

        #region ActionMethodAsPerMenu

        #region ActInActiveTeacher
        public ActionResult ActiveTeachers()
        {
            return LoadActiveInActiveTeachers(true);
        }
        public ActionResult InActiveTeachers()
        {
            return LoadActiveInActiveTeachers(false);
        }
        private ActionResult LoadActiveInActiveTeachers(bool bActive)
        {
            TeacherDetails[] lstTeachers = oAdminDML.LoadActiveInActiveTeachers(bActive);
            return PartialView("LoadActiveInActiveTeachers", new IdValuePair(Id: bActive ? 1 : 0, Value: lstTeachers));
        }
        public ActionResult DoActiveInActiveTeacher(int[] arrSelectedTechers, bool bActive)
        {
            List<TeacherDetails> lstTeacherDetails = new List<TeacherDetails>();
            oSaveStatus = oAdminDML.DoActiveInActiveTeacher(arrSelectedTechers, bActive);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DoInActive(int[] arrSelectedTechers)
        {
            List<TeacherDetails> lstTeacherDetails = new List<TeacherDetails>();
            oSaveStatus = oTeacherDML.DoActiveInActiveTeacher(arrSelectedTechers, false);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region IDCard
        public ActionResult TeachersIdCards()
        {
            TeacherDetails[] lstTeacherDetails = null;
            lstTeacherDetails = oAdminDML.LoadActiveInActiveTeachers(true);
            return PartialView("IDCardOfTeachers", lstTeacherDetails);
        }
        public ActionResult StudentsIdCards()
        {
            //Image, TName,TId,TFaculty
            //Make Id card only for thoses who is Active
            StudentCurrentInfo[] lstStudents = oTeacherDML.LoadActiveInActiveStudents(true);
            return PartialView("IDCardOfStudents", lstStudents);
        }
        #endregion

        #region Attendance
        public ActionResult TakeTeacherAttendanceInputs()
        {
            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Admin, Value: (int)SubModule.TakeTeacherAttendanceInputs);
            return PartialView("_CommonInputs", oInputs);
        }
        public ActionResult LoadTeacherAttendanceInputs()
        {
            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Admin, Value: (int)SubModule.LoadTeacherAttendanceInputs);
            return PartialView("_CommonInputs", oInputs);
        }

        public ActionResult SaveTeacherAttendance(TeacherAttendance[] arrTeacherAttendance, int iDate)
        {
            oSaveStatus = oTeacherDML.SaveTeacherAttandence(arrTeacherAttendance, iDate);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }



        public ActionResult LoadTeacherAttendance(int iMonth, int iYear)
        {
            List<IdValuePair> lstData = new List<IdValuePair>();
            TeachersAttendance lstTeacherAttendance = oTeacherDML.LoadTeachersAttendance(iMonth, iYear);
            lstTeacherAttendance.Inputs = new Inputs(Month: iMonth, Year: iYear);
            return PartialView("LoadTeachersAttandenceRegister", lstTeacherAttendance);
        }
        #endregion

        #region Details
        public ActionResult TeachersDetail()
        {
            TeacherDetails[] arrTeacherDetails = null;
            arrTeacherDetails = oAdminDML.LoadTeacherDetails();
            return PartialView("TeachersDetail", arrTeacherDetails);
        }
        #endregion

        #region TimeTable
        //public ActionResult TimeTable()
        //{
        //    //Registration[] arrRegisteredUsers = null;
        //    TeacherDetails[] lstTeachers = null;
        //    Subject[] arrSubject = null;
        //    Cell[] arrCell = null;
        //    TimeTable oTimeTable = new TimeTable();
        //    int iClass = 1, iDate = 0;
        //    lstTeachers = oAdminDML.LoadActiveInActiveTeachers(true);
        //    arrSubject = oAdminDML.LoadSubjects();
        //    arrCell = oAdminDML.LoadTimeTableData();
        //    //oTimeTable.Teacher = arrRegisteredUsers;
        //    oTimeTable.Teachers = lstTeachers;
        //    oTimeTable.Subjects = arrSubject;
        //    oTimeTable.Class = iClass;
        //    oTimeTable.CreatedDate = iDate;
        //    oTimeTable.CellData = arrCell;
        //    return PartialView("TimeTable", oTimeTable);
        //}

        //public ActionResult TimeTableData(int iClass = 1)
        //{
        //    Cell[] arrCell = null;
        //    int iDate = 0;
        //    arrCell = oAdminDML.LoadTimeTableData(iClass, ref iDate);
        //    return PartialView("_TimeTableData", arrCell);
        //}

        public ActionResult SaveTimeTable(TimeTable oTimeTable, int iLayoutId)
        {
            if (oTimeTable != null)
            {
                oSaveStatus = oAdminDML.SaveTimeTable(oTimeTable, iLayoutId);
                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Something wrong", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region DeleteTeacher
        public ActionResult DeleteTeachers(IdStatusPair[] arrSelectedTechers, bool bDeletePermanent)
        {
            List<TeacherDetails> lstTeacherDetails = new List<TeacherDetails>();
            if (arrSelectedTechers != null && arrSelectedTechers.Length > 0)
            {
                SaveStatus oSaveStatus = oAdminDML.DeleteTeachers(arrSelectedTechers, bDeletePermanent);
                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region EditTeacher
        public ActionResult CallCommonPopupAdmin(TeacherDetails oTeacherDetails = null, AdmissionDetails objStudent = null, Subject oSubject = null, PopupName iPopupName = 0)
        {
            IdValuePair popupData = new IdValuePair();
            if (iPopupName == PopupName.EditTeacherDetails)
            {
                popupData.Id = (int)iPopupName;
                popupData.Value = oTeacherDetails;
            }
            //if (iPopupName == PopupName.EditStudent)
            //{
            //    popupData.Id = (int)iPopupName;
            //    popupData.Value = objStudent;
            //}
            //else if (iPopupName == PopupName.AddSubject)
            //{
            //    popupData.Id = (int)iPopupName;
            //    popupData.Value = oSubject;
            //}
            //else if (iPopupName == PopupName.TakeStudentAttendance)
            //{
            //    List<StudentAttendance> lstStudent = oTeacherDML.LoadStudentForAttandence();
            //    popupData.Id = (int)iPopupName;
            //    popupData.Value = lstStudent;
            //}
            //else 
            if (iPopupName == PopupName.TakeTeacherAttendance)
            {
                TeacherDetails[] lstTeacherDetails = null;
                lstTeacherDetails = oAdminDML.LoadActiveInActiveTeachers(true);
                popupData.Id = (int)iPopupName;
                popupData.Value = lstTeacherDetails;
            }
            //return PartialView("_CommonPopupAdmin", popupData);
            return PartialView("_CommonPopups", popupData);
        }
        #endregion
        #endregion

        #region TimeTable
        public ActionResult TimeTableLayout()
        {
            TimeTableLayout[] arrTimeTableLayout = oAdminDML.LoadTimeTableLayout();
            return PartialView("TimeTableLayout", arrTimeTableLayout); 
        }

        public ActionResult SaveTimeTableLayout(TimeTableLayout oLayout)
        {
            oSaveStatus = oAdminDML.SaveTimeTableLayout(oLayout);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteTimeTableLayout(int iLayoutId)
        {
            oSaveStatus = oAdminDML.DeleteTimeTableLayout(iLayoutId);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult LoadTimeTableAsPerLayout(string sName, int iClass)
        //{
        //    TimeTableLayout oTimeTableLayout = oAdminDML.LoadTimeTableLayout(sName, iClass);
        //    TempData["Layout"] = oTimeTableLayout;
        //    DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Admin, Value: (int)SubModule.CreateTimeTable);
        //    return PartialView("_CommonInputs", oInputs);
        //}

        public ActionResult DeleteTimeTable(int iLayoutId)
        {
            oSaveStatus = oAdminDML.DeleteTimeTable(iLayoutId);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TimeTableNew()
        {
            //Registration[] arrRegisteredUsers = null;
            TeacherDetails[] lstTeachers = null;
            Subject[] arrSubject = null;
            Cell[] arrCell = null;
            TimeTable oTimeTable = new TimeTable();
            //int iClass = 1, iDate = 0;
            int iLayoutId = 0;

            lstTeachers = oAdminDML.LoadActiveInActiveTeachers(true);
            arrSubject = oAdminDML.LoadSubjects();
            //arrCell = oAdminDML.LoadTimeTableData(iClass, ref iDate);
            arrCell = oAdminDML.LoadTimeTableData(iLayoutId);

            //oTimeTable.Teacher = arrRegisteredUsers;
            oTimeTable.Teachers = lstTeachers;
            oTimeTable.Subjects = arrSubject;
            //oTimeTable.Class = iClass;
            //oTimeTable.CreatedDate = iDate;
            oTimeTable.CellData = arrCell;

            TimeTableLayout[] arrTimeTableLayout = oAdminDML.LoadTimeTableLayout();
            TempData["LayoutInfo"] = arrTimeTableLayout;

            return PartialView("CreateTimeTable", oTimeTable);
        }



        
      

        public ActionResult TimeTableDataNew()
        {
            TimeTableLayout[] arrTimeTableLayout = oAdminDML.LoadTimeTableLayout();
            TempData["LayoutInfo"] = arrTimeTableLayout;

            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Admin, Value: (int)SubModule.TimeTable);
            return PartialView("_CommonInputs", oInputs);
        }
        public ActionResult LoadTimeTableAsPerLayoutAndClass(int iLayoutId)
        {
            TimeTableLayout[] arrTimeTableLayout = oAdminDML.LoadTimeTableLayout(iLayoutId);
            if (arrTimeTableLayout != null && arrTimeTableLayout.Length > 0)
            {
                TempData["LayoutInfo"] = arrTimeTableLayout[0];
                Cell[] arrCell = null;
                int iDate = 0;

                arrCell = oAdminDML.LoadTimeTableData(iLayoutId);


                return PartialView("_TimeTableCellData", arrCell);
            }
            else
            {
                return Json("!Layout is not there.", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LoadClasswiseTimeTable(string sLayoutName, int iClass)
        {
            TimeTableLayout[] arrTimeTableLayouts = oAdminDML.LoadTimeTableLayout();
            return PartialView("_LoadTimeTableLayouts", arrTimeTableLayouts);
        }
        
        

        #endregion
















        ////public ActionResult FilterMenus(Modules eModule)
        ////{
        ////    arrMenu = oCoreDML.LoadMenu((int)eModule);
        ////    return PartialView("~/Views/Admin/_LoadMenus.cshtml", arrMenu);

        ////}
        ////public ActionResult MenuOpeations(CRUD eAction, IdValuePair oMenu)
        ////{
        ////    //1 : Delete, 2 : Update

        ////    oSaveStatus = new SaveStatus();
        ////    if (eAction == CRUD.Delete)
        ////    {
        ////        oSaveStatus = oCoreDML.DeleteMenus(oMenu);
        ////    }
        ////    return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        ////}

        ////public ActionResult AddTeacher()
        ////{
        ////    DoubleIntValue oDoubleIntValue = null;
        ////    //Registration[] arrRegisteredUsers = null;

        ////    //arrRegisteredUsers = oTeacherDML.LoadRegisteredUsers((int)Modules.Teacher);
        ////    //oDoubleIntValue = new DoubleIntValue(Id1: (int)Modules.Admin, Id2: (int)SubModule.AddTeacher, Value: arrRegisteredUsers);
        ////    oDoubleIntValue = CommonMethod.LoadRegUser((int)Modules.Admin, (int)SubModule.AddTeacher);
        ////    return PartialView("_LoadRegisterUsers", oDoubleIntValue);
        ////}


        ////public ActionResult SaveTeacher(SchoolBones.DataStructs.TeacherDetails oTeacherDetails)
        ////{
        ////    oSaveStatus = oAdminDML.SaveTeacherDetails(oTeacherDetails);
        ////    return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        ////}






        ////public ActionResult StudentsIdCards()
        ////{
        ////    TeacherController oTeacherController = new TeacherController();
        ////    return oTeacherController.StudentsIdCards();
        ////    //[] lstStudents = oTeacherDML.LoadActiveInActiveStudents(true);
        ////    //return PartialView("_IDCardOfStudents", lstStudents);
        ////}



        ////public ActionResult OpenAttandenceRegister()
        ////{
        ////    List<TeacherDetails> lstTeacherDetails = new List<TeacherDetails>();
        ////    lstTeacherDetails = oAdminDML.LoadTeacherDetails(true);
        ////    return Json(lstTeacherDetails, JsonRequestBehavior.AllowGet);
        ////}




        ////public ActionResult CreateTimeTableOLD()
        ////{
        ////    TeacherDetails[] lstTeacherDetails = null;
        ////    lstTeacherDetails = oAdminDML.LoadActiveInActiveTeachers(true);

        ////    return PartialView("_TimeTable", lstTeacherDetails);
        ////}






        //#region Registration
        ////public ActionResult TeacherRegistration()
        ////{
        ////    DoubleIntValue oData = null;
        ////    oData = CommonMethod.LoadRegUser((int)Modules.Admin, (int)SubModule.Registration);
        ////    return PartialView("_Registration", oData);
        ////}


        ////public ActionResult DeleteRegistration(int iRegId)
        ////{
        ////    if (iRegId > 0)
        ////    {
        ////        oSaveStatus = oAdminDML.DeleteRegistration(iRegId);
        ////        return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        ////    }
        ////    else
        ////    {
        ////        return Json("Something wrong", JsonRequestBehavior.AllowGet);
        ////    }
        ////}

        //#endregion
    }
}