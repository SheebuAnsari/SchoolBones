using DomLibrary;
using Focus8W.Models;
using SchoolBones.Common;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Focus8W.Controllers
{
    public class HomeController : UserController
    {
        Menu[] arrMenu = null;
        CoreDML oCoreDML = new CoreDML();
        
        List<IdNameTag> oMenu = new List<IdNameTag>();
        SaveStatus oSaveStatus = new SaveStatus();
        
       
        public ActionResult Index()
        {
            string sError = "";
            //1. Check CoreFileFolder
            ServerHall.ServerCoreFilePath = GetFileFactoryPath(ref sError);
            if (string.IsNullOrEmpty(sError))
            {

            }
            else
            {
                //Something wrong when setting server path for files folder
            }


            //string sError = "";
            //int iTodayDate = 0;

            ////1. Check CoreFileFolder
            //SchoolBones.Common.Server.ServerPath = GetFileFactoryPath(ref sError); //Set Server path from UI
            //if (string.IsNullOrEmpty(sError) == false)
            //{
            //    TempData["LicenseError"] = sError;
            //    return RedirectToAction("License", "Home");
            //}

            ////2.  Stable the connection from dbConfig file.
            //ConnectionHelper.GetDBConString(ref sError);
            //if (string.IsNullOrEmpty(sError) == false)
            //{
            //    TempData["LicenseError"] = sError;
            //    return RedirectToAction("License", "Home");
            //}

            //iTodayDate = DateDS.DateToInt(DateTime.Now);
            //sError = oCoreDML.CheckLicense(iTodayDate);

            //if(string.IsNullOrEmpty(sError) == false)
            //{
            //    TempData["LicenseError"] = sError;
            //    return RedirectToAction("License", "Home");
            //}



            if (Session["userid"] != null && Session["UserPassword"] != null)
            {
                arrMenu = oCoreDML.LoadMenu(Convert.ToInt32(Session["usertype"]));
                return View("_Dashboard", arrMenu);
            }
            return View();
        }
        public ActionResult Login(string sUserId = "", string sUserPassword = "", int iUserType = 0)
        {
            GLOBAL oGLOBAL = new GLOBAL();
            CoreDML oCoreDML = new CoreDML();
            SaveStatus oSaveStatus = new SaveStatus();
            int iRegId = oCoreDML.Login(sUserId, sUserPassword, iUserType);
            if (iRegId > 0)
            {
                System.Web.HttpContext.Current.Session[SessionKey.RegId] = iRegId;
                oGLOBAL.UserId = iRegId;
                if (oGLOBAL.RegId == 0)
                {
                    Object oValue = null;
                    oValue = Convert.ToInt32(System.Web.HttpContext.Current.Session[SessionKey.RegId]);
                    if (oValue != null)
                    {
                        oGLOBAL.RegId = Convert.ToInt32(oValue);
                    }
                }
                Session["userid"] = sUserId;
                Session["UserPassword"] = sUserPassword;
                Session["usertype"] = iUserType;
                arrMenu = oCoreDML.LoadMenu(iUserType);
                return PartialView("_Dashboard", arrMenu);
            }
            else
            {
                return Json("Not valid user.", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult License()
        {
            if (string.IsNullOrEmpty(Convert.ToString(TempData["LicenseError"])) == true)
            {
                TempData["LicenseError"] = "Expired";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        ////public ActionResult CreateSchool()
        ////{
        ////    //Check Tool and  MasterDbConfig file is exist or not.  Check this in BL Library
        ////    //If exist then redirect to License exp or Go for newly created School.

        ////    IdNamePair[] arrDbInfo = null;
        ////    arrDbInfo = oCoreDML.LoadCreatedDatabases();
        ////    return View("CreateSchool", arrDbInfo);
        ////}
        ////public ActionResult SaveCreateSchool(ProjectConfiguration oProjectConfiguration)
        ////{
        ////    oSaveStatus = oCoreDML.SchoolRegistration(oProjectConfiguration);
           
        ////    return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        ////}

        ////public ActionResult DeleteCreatedSchool(string sDbName)
        ////{
        ////    oSaveStatus = oCoreDML.DropCreatedDatabases(sDbName);
        ////    if (oSaveStatus.iStatus > 0)
        ////    {
        ////        return RedirectToAction("CreateSchool", "Home");
        ////    }
        ////    else
        ////    {
        ////        return RedirectToAction("License", "Home");
        ////    }
        ////}
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}