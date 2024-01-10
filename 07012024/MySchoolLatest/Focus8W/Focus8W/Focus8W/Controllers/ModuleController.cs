//using Focus8W.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Focus8W.Controllers
//{
//    public class ModuleController : Controller
//    {
//        // GET: Module
//        //public ActionResult Index()
//        //{
//        //    return View();
//        //}

//        public ActionResult RoleMenu(int iModule)
//        {
//            //CommonModels oCommonDS = new CommonModels();
//            List<IdNameTag> oMenu = new List<IdNameTag>();

//            oMenu = oCommonDS.GetMenuOfModule(iModule);

//            if (iModule == (int)Modules.Admin)
//            {
//                //Admin
//            }
//            if (iModule == (int)Modules.Admin)
//            {
//                //Staff
//            }
//            if (iModule == (int)Modules.Admin)
//            {
//                //Student
//            }else
//            {

//            }
//            return PartialView("_CommonHome", oMenu);
//        }
//    }
//}