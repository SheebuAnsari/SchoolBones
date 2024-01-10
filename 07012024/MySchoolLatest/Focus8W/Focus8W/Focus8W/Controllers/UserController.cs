using SchoolBones.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Focus8W.Controllers
{
    public class UserController : Controller
    {
        public string GetFileFactoryPath(ref string sError)
        {
            string sPath = "";

            try
            {
                sPath = Server.MapPath("~/");
                if (System.IO.Directory.Exists(sPath))
                {
                    sPath += "FileFactory\\";
                    if (System.IO.Directory.Exists(sPath))
                    {
                        return sPath;
                    }
                }
                return sError = "!FileFactory is missing";
            }
            catch(Exception ex)
            {
                return sError;
            }
        }
    }
}