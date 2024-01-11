using DomLibrary;
using SchoolBones;
using SchoolBones.Common;

using SchoolBones.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Focus8W.Controllers
{
    public class CommonController : Controller
    {
        TeacherDML oTeacherDML = new TeacherDML();
        CommonDML oCommonDML = new CommonDML();

        //public ActionResult FillUserDetails(int iRegId)
        //{
        //    Registration oRegistration = null;
        //    oRegistration = oCommonDML.LoadRegisteredUser(iRegId);
        //    if (oRegistration == null)
        //    {
        //        return Json("Alert : No record available.", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return PartialView("_AddUser", oRegistration);
        //    }

        //}
        public ActionResult LoadRegUserRefresh(int iModuleId)
        {
            DoubleIntValue oData = null;
            oData = CommonMethod.LoadRegUser(iModuleId, (int)SubModule.Registration);
            return PartialView("_Registration", oData);
        }
    }
}