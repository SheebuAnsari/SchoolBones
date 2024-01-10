//using Focus8W.BL;
//using Focus8W.Models;
//using SchoolBones;
//using SchoolBones.Common;
//using SchoolBones.DataStructs;
//using SchoolBones.Enums;
//using SchoolBones.Teacher;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Focus8W.Controllers.Teacher
//{
//    public class TeacherController : Controller
//    {
//        TestingDML oTestingDML = new TestingDML();
//        SaveStatus oSaveStatus = new SaveStatus();
//        CommonDML oCommonDML = new CommonDML();
//        TeacherDML oTeacherDML = new TeacherDML();

//        //Add
//        //Edit
//        //Delete 1
//        //Delete 2
//        //Deleted
//        //TakeAttendance
//        //LoadAttendance
//        //CreateResult
//        //PreparedResult
//        //AddSubject

//        #region ActInActiveStudent
//        public ActionResult ActiveStudents()
//        {
//            return LoadActiveInActiveStudents(true);
//        }
//        public ActionResult InActiveStudents()
//        {
//            return LoadActiveInActiveStudents(false);
//        }
//        private ActionResult LoadActiveInActiveStudents(bool bActive)
//        {
//            StudentCurrentInfo[] lstStudents = oTeacherDML.LoadActiveInActiveStudents(bActive);
//            return PartialView("_LoadActiveInActiveStudents", new IdValuePair(Id: bActive ? 1 : 0, Value: lstStudents));
//        }
//        public ActionResult DoActiveInActiveStudent(int[] arrStudnetIds, bool bActive)
//        {
//            oSaveStatus = oTeacherDML.DoActiveInActiveStudent(arrStudnetIds, bActive);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }

//        #endregion
//        #region Add
//        //public ActionResult AddStudent()
//        //{
//        //    DoubleIntValue oDoubleIntValue = null;
//        //    //Registration[] arrRegisteredUsers = null;

//        //    //arrRegisteredUsers = oTeacherDML.LoadRegisteredUsers((int)Modules.Student);
//        //    //oDoubleIntValue = new DoubleIntValue(Id1: (int)Modules.Teacher, Id2: (int)SubModule.AddStudent, Value: arrRegisteredUsers);

//        //    oDoubleIntValue = CommonMethod.LoadRegUser((int)Modules.Teacher, (int)SubModule.AddStudent);
//        //    return PartialView("_LoadRegisterUsers", oDoubleIntValue);
//        //}

//        public ActionResult StudentAdmission(AdmissionDetails oAdmissionDetails = null)
//        {
//            oSaveStatus = oTeacherDML.StudentAdmission(oAdmissionDetails);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult StudentAdmissionNEW(AdmissionDetails oAdmissionDetails = null)
//        {
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
//        #endregion

//        #region Edit
//        public ActionResult LoadAllStudents()
//        {
//            List<AdmissionDetails> lstStudents = oTeacherDML.LoadAllStudents();
//            return PartialView("_StudentsDetails", lstStudents);
//        }
//        public ActionResult CallCommonPopups(AdmissionDetails objStudent = null, Subject oSubject = null, int iClass = 0, PopupName iPopupName = 0)
//        {
//            IdValuePair popupData = new IdValuePair();
//            if (iPopupName == PopupName.EditStudent)
//            {
//                popupData.Id = (int)iPopupName;
//                popupData.Value = objStudent;
//            }
//            else if (iPopupName == PopupName.AddSubject)
//            {
//                popupData.Id = (int)iPopupName;
//                popupData.Value = oSubject;
//            }
//            else if (iPopupName == PopupName.TakeStudentAttendance)
//            {
//                List<StudentAttendance> lstStudent = oTeacherDML.LoadStudentForAttendance(iClass);
//                popupData.Id = (int)iPopupName;
//                popupData.Value = lstStudent;
//            }
//            return PartialView("_CommonPopups", popupData);
//        }
//        #endregion

        

//        #region Delete
//        public ActionResult DeleteStudents(IdStatusPair[] arrSelectedStudents, bool bDeletePermanent)
//        {
//            if (arrSelectedStudents != null && arrSelectedStudents.Length > 0)
//            {
//                SaveStatus oSaveStatus = oTeacherDML.DeleteStudents(arrSelectedStudents, bDeletePermanent);
//                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//            }
//            else
//            {
//                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult DeleteStudentsPermanant()
//        {
//            List<StudentCurrentInfo> lstStd = oTeacherDML.LoadStdForDeRegister(true);
//            return PartialView("_DeleteStudentPermanent", lstStd);
//        }
//        public ActionResult DeleteStudentPermanently(IdStatusPair[] arrIdStatusPair)
//        {
//            oSaveStatus = oTeacherDML.DeleteStudents(arrIdStatusPair, true);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
//        #endregion

//        #region Attendance
//        public ActionResult SaveStudentAttendanceInputs()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Teacher, Value: (int)SubModule.TakeAttendanceInputs);
//            return PartialView("_CommonInputs", oInputs);
//            //return PartialView("_CommonInputs", SubModule.TakeAttendanceInputs);
//        }
//        public ActionResult LoadStudentAttendanceInputs()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Teacher, Value: (int)SubModule.LoadAttendanceInputs);
//            return PartialView("_CommonInputs", oInputs);
//            //return PartialView("_CommonInputs", SubModule.LoadAttendanceInputs);
//        }
//        public ActionResult OpenAttendanceRegister(int iClass, int iYear)
//        {
//            StudentAttendance[] lstStudent = oTeacherDML.LoadClassStudent(iClass, iYear);
//            return Json(lstStudent, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult SaveAttendance(StudentAttendance[] arrStudentAttendance, int iDate)
//        {
//            oSaveStatus = oTeacherDML.SaveAttendance(arrStudentAttendance, iDate);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult LoadAttendance(int iClass, int iMonth, int iYear)
//        {
//            List<StudentAttendance> lstStudentAttendance = oTeacherDML.LoadMonthlyAttendance(iClass, iMonth, iYear);
//            return PartialView("_LoadAttendanceRegister", lstStudentAttendance);
//        }
//        #endregion

//        #region MarkSheet
//        public ActionResult CreateMarksheetInputs()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Teacher, Value: (int)SubModule.CreateMarksheet);
//            return PartialView("_CommonInputs", oInputs);
//            //return PartialView("_CommonInputs", SubModule.CreateMarksheet);
//        }
//        public ActionResult LoadStudentsForMarks(int iClass, int iYear, int iExamType)
//        {
//            MarkSheet oMarkSheet = oTeacherDML.LoadClasswiseStudentAndSubjects(iClass, iYear);
//            oMarkSheet.Inputs = new Inputs(Class: iClass, Year: iYear, ExamType: iExamType);
//            return PartialView("_CreateMarksheet", oMarkSheet);
//        }
//        public ActionResult SaveMarks(Marks[] arrMarks)
//        {
//            oSaveStatus = oTeacherDML.SaveMarks(arrMarks);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult CreateDMarksheetInputs()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Teacher, Value: (int)SubModule.CreateDMarksheet);
//            return PartialView("_CommonInputs", oInputs);
//            //return PartialView("_CommonInputs", SubModule.CreateDMarksheet);
//        }
        
        
//        public ActionResult CreatedMarksheet(int iClass, int iYear)
//        {
//            Marks[] lstStudent = oTeacherDML.LoadCreateDMarksheet(iClass, iYear);
//            //FillMarks
//            lstStudent =  oTeacherDML.FillMarks(lstStudent.ToList());
//            return PartialView("_CreatedMarksheet", lstStudent);
//        }
//        #endregion

//        #region Passed/FailedStudent
//        public ActionResult PassFailStatus()
//        {
//            List<PassFailStatus> lstMarks = oTeacherDML.AllResults();
//            return PartialView("_PassFailStatus", lstMarks);
//        }
//        #endregion

//        #region AddSubjects
//        public ActionResult AddSubjectsInputs()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Teacher, Value: (int)SubModule.AddSubjects);
//            return PartialView("_CommonInputs", oInputs);
//            //return PartialView("_CommonInputs", SubModule.AddSubjects);
//        }
//        public ActionResult LoadSubjects(int iClass)
//        {
//            Subject[] lstSubject = oTeacherDML.LoadSubjects(iClass);
//            IdValuePair objData = new IdValuePair(Id: iClass, Value: lstSubject);
//            return PartialView("_LoadSubjects", objData);
//        }
//        public ActionResult AddSubject(Subject oSubject)
//        {
//            oSaveStatus = oTeacherDML.SaveSubject(oSubject);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult DoAction(Subject oSubject, CRUD iActionType)
//        {
//            if (iActionType == CRUD.Delete)
//            {
//                oSaveStatus = oTeacherDML.DeleteSubject(oSubject.SubjectId);
//            }
//            else if (iActionType == CRUD.Update)
//            {
//                oSaveStatus = oTeacherDML.UpdateSubject(oSubject);
//            }
//            else
//            {
//                return Json("No Action", JsonRequestBehavior.AllowGet);
//            }

//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
        

//        public ActionResult DefineMaximumMarksInputs()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Teacher, Value: (int)SubModule.DefinedMaximumMarks);
//            return PartialView("_CommonInputs", oInputs);
//            //return PartialView("_CommonInputs", SubModule.DefinedMaximumMarks);
//        }
//        public ActionResult DefineMaximumMarks(int iClass)
//        {
//            SubjectDetails oSubjectDetails = null;
//            oSubjectDetails = new SubjectDetails();
//            oSubjectDetails.arrSub = oTeacherDML.LoadSubjects(iClass);
//            oSubjectDetails.Inputs = new Inputs(Class: iClass);
//            return PartialView("_DefineMaximumMarks", oSubjectDetails);
//        }
//        public ActionResult SaveDefinedMaximumMarks(SubjectMaxMarks[] arrSubjectMaxMarks)
//        {
//            oSaveStatus = oTeacherDML.SaveSubjectMaxMarks(arrSubjectMaxMarks);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult DisplayMarksheetInputs()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Teacher, Value: (int)SubModule.DisplayMarksheet);
//            return PartialView("_CommonInputs", oInputs);
//            //return PartialView("_CommonInputs", SubModule.DisplayMarksheet);
//        }
//        public ActionResult LoadMarksheetInputs()
//        {
//            return PartialView("_DisplayMarksheet");
//        }
//        public ActionResult DisplayMarksheet(int iStdId)
//        {
//            List<IdValuePair> lstData = new List<IdValuePair>();
//            lstData.Add(new IdValuePair(Id: 1, Value: oTeacherDML.LoadSubjects()));
//            lstData.Add(new IdValuePair(Id: 1, Value: oTeacherDML.LoadMarks(iStdId)));
//            //IdNamePair[] arrSubjects = oTeacherDML.LoadSubjects();
//            return PartialView("_LoadMarksheet", lstData);
//        }
        
//        #endregion
        

//        //Before it was in admin menu
//        public ActionResult StudentsIdCards()
//        {
//            //Image, TName,TId,TFaculty
//            //Make Id card only for thoses who is Active
//            StudentCurrentInfo[] lstStudents = oTeacherDML.LoadActiveInActiveStudents(true);
//            return PartialView("_IDCardOfStudents", lstStudents);
//        }

//        #region Registration
//        public ActionResult StudentRegistration()
//        {
//            DoubleIntValue oData = null;
//            oData = CommonMethod.LoadRegUser((int)Modules.Teacher, (int)SubModule.Registration);
//            return PartialView("_Registration", oData);
//        }
//        #endregion

//        #region LoadSemesterMarksheet
//        public ActionResult LoadSemesterMarksheetInputs()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Teacher, Value: (int)SubModule.LoadSemesterMarksheet);
//            return PartialView("_CommonInputs", oInputs);
//        }
//        //public ActionResult LoadSemesterWiseMarksheet(int iClass, int iYear, int iExamType)
//        //{
//        //    Marks[] lstStudentMarks = null;
//        //    lstStudentMarks = oTeacherDML.LoadSemesterMarksheet(iClass, iYear, iExamType);
//        //    return PartialView("_SemesterWiseMarksheet");
//        //}
//        public ActionResult LoadSemesterWiseMarksheet(int iClass, int iYear, int iExamType)
//        {
//            Marks[] lstStudent = oTeacherDML.LoadCreateDMarksheet(iClass, iYear);
//            //FillMarks
//            lstStudent = oTeacherDML.FillMarks(lstStudent.ToList(), iExamType);
//            return PartialView("_CreatedMarksheet", lstStudent);
//        }
//        #endregion
//    }
//}