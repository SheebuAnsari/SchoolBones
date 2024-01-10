using Focus8W.Models;
using SchoolBones;
//using Focus8W.GlobalFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Focus8W.Models.GLOBAL;
//using static Focus8W.GlobalFactory.GLOBAL;

namespace Focus8W.App_Start
{
    public class MyFilter: ActionFilterAttribute
    {
        public MyFilter()
        {

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //SchoolDetails oDetails = new SchoolDetails();
            //GLOBAL obj = new GLOBAL();
            //oDetails = obj.LoadSchoolDetails();
            //System.Web.HttpContext.Current.Session[SessionKey.SchoolId] = oDetails.SchoolId;
            //base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //base.OnActionExecuted(filterContext);
        }
        //GLOBAL oGLOBAL = new GLOBAL();

        //public GLOBAL OGLOBAL
        //{
        //    get
        //    {
        //        return oGLOBAL;
        //    }

        //    set
        //    {
        //        oGLOBAL = value;
        //    }
        //}

        //oGLOBAL.g
        //oGLOBAL. .GetGlobalData();
    }
}