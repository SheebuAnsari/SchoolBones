//using Focus8W.BL;
//using Focus8W.Models;
//using SchoolBones;
//using SchoolBones.Account;
//using SchoolBones.DataStructs;
//using SchoolBones.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Focus8W.Controllers.Accounts
//{
//    public class AccountsController : Controller
//    {
//        TestingDML oTestingDML = new TestingDML();

//        AccountsDML oDML = new AccountsDML();
//        SaveStatus oSaveStatus = new SaveStatus();

//        AccountsDML oAccountsDML = new AccountsDML();

       


//        public ActionResult FeeSubmission()
//        {
//            //return PartialView("_CommonInputs", SubModule.FeeSubmission);
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.FeeSubmission);
//            return PartialView("_CommonInputs", oInputs);
//        }

//        public PartialViewResult LoadFeeDetails(int iEnrollmentno, int iRollno)
//        {
//            FeeDetails objFeeDetails = oTestingDML.LoadFeeDetails(iEnrollmentno, iRollno);
//            return PartialView("_LoadFeeDetails", objFeeDetails);
//        }
       
//        public ActionResult SaveFee(FeeInfo oFeeInfo)
//        {
//            oSaveStatus = oTestingDML.SaveFeeDetails(oFeeInfo);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult OpenFeeCard(FeeDetails oFeeDetails)
//        {
//            List<FeeDetails> lstFeeDetails = new List<FeeDetails>();
//            lstFeeDetails.Add(oFeeDetails);
//            return View("_FeeCard", lstFeeDetails);
//        }

//        public ActionResult ClasswiseStudents()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.ClasswiseStudents);
//            return PartialView("_CommonInputs", oInputs);
//            //return PartialView("_CommonInputs", SubModule.ClasswiseStudents);
//        }
//        public ActionResult LoadClasswiseStudents(int iClass,int iYear)
//        {
//            StudentDetails[] lstStudents = oDML.LoadClasswiseStudents(iClass, iYear);
//            return PartialView("_LoadClasswiseStudents", lstStudents);
//        }

//        #region DefineMonthlySalary
//        public ActionResult MonthlySalary()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.DefineMontlySalary);
//            return PartialView("_CommonInputs", oInputs);
//        }
//        public ActionResult LoadMonthlySalary(int iYear)
//        {
//            TeacherSalary[] arrTeacherSalary = oAccountsDML.LoadSalary(iYear);
//            return PartialView("_DefineSalary", arrTeacherSalary);
//        }
      
//        public ActionResult SaveSalary(TeacherSalary[] arrTeacherSalary)
//        {
//            oSaveStatus = oAccountsDML.SaveSalary(arrTeacherSalary);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
//        #endregion

//        #region DefineFee
//        public ActionResult FeeStructure()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.FeesStructure);
//            return PartialView("_CommonInputs", oInputs);
//            //return PartialView("_CommonInputs", SubModule.FeesStructure);
//        }
//        public ActionResult LoadFeesStructure(int iYear)
//        {
//            FeesStructure[] arrFeesStructure = oAccountsDML.LoadFeesStructure(iYear);
//            return PartialView("_FeesStructure", arrFeesStructure);
//        }
//        public ActionResult DeleteFeesStructure(int iFeeStructureId)
//        {
//            if (iFeeStructureId > 0)
//            {
//                oSaveStatus = oAccountsDML.DeleteFeesStructure(iFeeStructureId);
//                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//            }
//            else
//            {
//                oSaveStatus.iStatus = -1;
//                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult SaveUpdateFeesStructure(FeesStructure oFeesStructure)
//        {
//            oSaveStatus = oAccountsDML.SaveFeesStructure(oFeesStructure);
//            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
//        }
//        #endregion

//        public ActionResult LoadTeachersSalaryInput()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.LoadSalary);
//            return PartialView("_CommonInputs", oInputs);
//        }
//        public ActionResult LoadTeachersSalary(int iMonth, int iYear)
//        {
//            TeacherSalary[] arrTeacherSalary = oAccountsDML.LoadSalaryDetails(iMonth, iYear);
//            return PartialView("_LoadTeachersSalary", arrTeacherSalary);
//        }

//        #region FeeCollectionData
//        public ActionResult FeeCollection()
//        {
//            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.LoadFeeCollection);
//            return PartialView("_CommonInputs", oInputs);
//        }
//        public ActionResult LoadFeeCollection(int iClass, int iMonth, int iYear)
//        {
//            ReportData oReportData = oAccountsDML.LoadFeeCollection(iClass, iMonth, iYear);
//            return PartialView("_FeeCollection", oReportData);
//        }
        
//        #endregion
//    }
//}