using DomLibrary;
using Focus8W.Controllers;
using SchoolBones.Admin;
using SchoolBones.Common;

using SchoolBones.Teacher;
using System.Web.Mvc;

namespace Focus8W.Areas.AdmissionArea.Controllers
{
    public class AdmissionController : Controller
    {
        SaveStatus oSaveStatus = new SaveStatus();
        AdminDML oAdminDML = new AdminDML();        //Can control Teacher //Changes are required for BL it should be Admission.BL
        TeacherDML oTeacherDML = new TeacherDML();  //Can control Teacher //Changes are required for BL it should be Admission.BL
        CommonDML oCommonDML = new CommonDML();  //Can control Teacher //Changes are required for BL it should be Admission.BL



        public ActionResult TeacherRegistration()
        {
            return LoadRegistration((int)Modules.Teacher);
        }
        public ActionResult StudentRegistration()
        {
            return LoadRegistration((int)Modules.Student);
        }
        
        public ActionResult LoadRegistration(int iModuleId)
        {
            DoubleIntValue oData = null;
            oData = CommonMethod.LoadRegUser(iModuleId, (int)SubModule.Registration);
            return PartialView("Registration", oData);
        }
        public ActionResult SaveRegistration(Registration oRegistration)
        {
            int iRegistrationNo = oTeacherDML.SaveRegistration(oRegistration);
            return Json(iRegistrationNo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRegistration(int iRegId)
        {
            if (iRegId > 0)
            {
                oSaveStatus = oAdminDML.DeleteRegistration(iRegId);
                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Something wrong", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddUserDetails(int iRegId)
        {
            Registration oRegistration = null;
            oRegistration = oCommonDML.LoadRegisteredUser(iRegId);
            if (oRegistration == null)
            {
                return Json("Alert : No record available.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView("AddUserDetails", oRegistration);
            }
        }
        public ActionResult SaveStudentDetails(AdmissionDetails oAdmissionDetails = null)
        {
            oSaveStatus = oTeacherDML.StudentAdmission(oAdmissionDetails);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveTeacherDetails(TeacherDetails oTeacherDetails)
        {
            oSaveStatus = oAdminDML.SaveTeacherDetails(oTeacherDetails);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
    }
}