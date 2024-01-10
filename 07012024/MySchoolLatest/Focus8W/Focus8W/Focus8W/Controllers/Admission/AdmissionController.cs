//using SchoolBones;
//using SchoolBones.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Focus8W.Controllers.Admission
//{
//    public class AdmissionController : Controller
//    {
//        // GET: Admission
//        //public ActionResult Index()
//        //{
//        //    return View();
//        //}
//        public ActionResult Registration()
//        {
//            DoubleIntValue oData = null;
//            oData = CommonMethod.LoadRegUser((int)Modules.Teacher, (int)SubModule.Registration);
//            return PartialView("_Registration", oData);
//        }
//    }
//}