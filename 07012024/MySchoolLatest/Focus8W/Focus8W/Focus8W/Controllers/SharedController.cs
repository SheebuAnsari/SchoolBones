using DomLibrary;
using Newtonsoft.Json;
using SchoolBones.Account;
using SchoolBones.Admin;

using SchoolBones.Teacher;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Focus8W.Controllers
{
    public class SharedController : Controller
    {
        AdminDML oAdminDML = new AdminDML();        //Can control Teacher
        TeacherDML oTeacherDML = new TeacherDML();  //Can control Student
        AccountsDML oAccountsDML = new AccountsDML();  //Can control Student




        //There are 3 Cases, 1 : New entry 2: Edited values 3 : Loaded from BL and show in popup
        public ActionResult CallCommonPopup(IdValuePair oPopupData, Inputs oInputs = null)
        {
            if (oPopupData != null)
            {
                switch ((PopupName)oPopupData.Id)
                {
                    case PopupName.None:
                        break;
                    case PopupName.SaveFeeStructure:
                        oPopupData.Value = oPopupData.Value;
                        break;
                    case PopupName.UpdateFeeStructure:
                        FeesStructure oFeesStructure = JsonConvert.DeserializeObject<FeesStructure>(((string[])oPopupData.Value)[0]);
                        oPopupData.Value = oFeesStructure;
                        break;
                    case PopupName.DeleteFeeStructure:
                        int iFeesStructureId = JsonConvert.DeserializeObject<int>(((string[])oPopupData.Value)[0]);
                        oPopupData.Value = iFeesStructureId;
                        break;

                    case PopupName.TakeTeacherAttendance:
                        TeacherDetails[] lstTeacherDetails = null;
                        lstTeacherDetails = oAdminDML.LoadActiveInActiveTeachers(true);
                        oPopupData.Value = lstTeacherDetails;
                        break;
                    case PopupName.TakeStudentAttendance:
                        List<StudentAttendance> lstStudent = oTeacherDML.LoadStudentForAttendance(oInputs.Class);
                        oPopupData.Value = lstStudent;
                        break;
                        //case PopupName.SaveFeeStructure:
                        //    break;
                        //case PopupName.SaveFeeStructure:
                        //case PopupName.SaveFeeStructure:
                        //case PopupName.SaveFeeStructure:
                        //    break;
                        //    break;
                        //    break;
                        //    break;
                        //case PopupName.SaveFeeStructure:
                        //    break;
                }
            }
            //return PartialView("~/Focus8W/Focus8W/Views/Shared/_CommonPopups.cshtml", oPopupData);
            //return PartialView("_Test", oPopupData);
            return PartialView("_CommonPopups", oPopupData);
        }
    }
}