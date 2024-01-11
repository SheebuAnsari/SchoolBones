using DomLibrary;
using Focus8W.BL;
using SchoolBones;
using SchoolBones.Common;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Focus8W.Areas.DeveloperArea.Controllers
{
    public class DeveloperController : Controller
    {
        // GET: DevArea/Developer1
        Menu[] arrMenu = null;
        SaveStatus oSaveStatus = new SaveStatus();
        CoreDML oCoreDML = new CoreDML();


        public ActionResult AddMenu()
        {
            arrMenu = oCoreDML.LoadMenu(Convert.ToInt32(Session["usertype"]));
            return PartialView("AddMenu", arrMenu);
        }
        public ActionResult TestIndex()
        {
            return View();
        }
        public ActionResult SaveMenu(Menu oMenu)
        {
            oCoreDML.WriteXMLMenuFile();
            oSaveStatus = oCoreDML.SaveMenu(oMenu);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MenuCRUD(DomLibrary.CRUD eAction, IdValuePair oMenu)
        {
            //1 : Delete, 2 : Update

            if (eAction == CRUD.Delete)
            {
                oSaveStatus = oCoreDML.DeleteMenus(oMenu);
            }
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TestIcons()
        {
            return PartialView("TestIcons");
        }
        public ActionResult ToExcel()
        {
            ExportData oExport = new ExportData();
            oExport.ToEXCEL("", null);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FilterMenus(Modules eModule)
        {
            arrMenu = oCoreDML.LoadMenu((int)eModule);
            //return PartialView("~/Views/Admin/_LoadMenus.cshtml", arrMenu);
            return PartialView("~/Areas/DeveloperArea/Views/Shared/_LoadMenu.cshtml", arrMenu);

        }
        public ActionResult CalculatenoofPeriods()
        {
            return PartialView("Time");
        }
        public ActionResult PrintReciept()
        {
            return PartialView("PrintReciept");
        }


        public ActionResult ReceiptUsingServer(int iEnrollmentNo, int iStdId, int iMonthlyFeeId)
        {
            List<string> oData = new List<string>(); ;
            oData.Add("Test.html");
            oData.Add(GenerateHtmlBuilder());
            return Json(oData, JsonRequestBehavior.AllowGet);
        }
        public static string GenerateHtmlBuilder()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html>");
            sb.AppendLine("<style>");
            sb.AppendLine("a:link {color:#0000FF; text-decoration:none}");
            sb.AppendLine("a:visited {color:#00FF00;}");
            sb.AppendLine("a:hover {color:#FFF000; text-decoration:underLine}");
            sb.AppendLine("a:active {color:#6600FF;}");
            sb.AppendLine("</style>");
            sb.AppendLine("<head></head>");
            sb.AppendLine("<body>");

            sb.AppendLine("<table align=Center>");
            sb.AppendFormat("<tr> <td align=Center> <font style=text-transform: capitalize;> <b> {0} </b> </font> </td> </tr>", "CompanyName : Focus");
            sb.AppendFormat("<tr> <td align=Center> <font style=text-transform: capitalize;> <b> {0} </b> </font> </td> ", "ReportName : Test Report");
            sb.AppendLine("<tr> <td align=Center>&nbsp;</td> ");
            sb.AppendLine("</table>");

            sb.AppendLine("<table border=\"1\" align=Center style=\"border-collapse: collapse\">");
            sb.AppendLine("<tr>");
            sb.AppendFormat("<td width={0} align={1}> <font style=font-weight:{2}; face={3} color={4}> {5} </font> </td>", 120, "Center", "Bold", "Verdana", "Black", "Image Name");
            sb.AppendFormat("<td width={0} align={1}> <font style=font-weight:{2}; face={3} color={4}> {5} </font> </td>", 120, "Center", "Bold", "Verdana", "Black", "Image");
            sb.AppendLine("</tr>");

            sb.AppendLine("<tr>");
            sb.AppendFormat("<td width={0} align={1}> <font style=font-weight:{2}; face={3} color={4}> {5} </font> </td>", 120, "Center", "Bold", "Verdana", "Black", "Problem");
            sb.AppendLine("<td>");
            sb.AppendLine("<div style=\"font-style: {0}; font-weight: {1}; color:" + "Black" + ";\">");
            sb.AppendFormat("{0}", 0);
            sb.AppendLine("</div>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");

            sb.AppendLine("<tr>");
            sb.AppendFormat("<td width={0} align={1}> <font style=font-weight:{2}; face={3} color={4}> {5} </font> </td>", 120, "Center", "Bold", "Verdana", "Black", "Solution");
            sb.AppendLine("<td>");
            sb.AppendFormat("<div style=\"font-style: {0}; font-weight: {1}; color:" + "Black" + ";\">", "Italic", "Bold");
            sb.AppendLine(string.Format("<img src={0} width={1} height={2}/>", 0, 150, 100));
            sb.AppendLine("</div>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");

            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }
    }
}