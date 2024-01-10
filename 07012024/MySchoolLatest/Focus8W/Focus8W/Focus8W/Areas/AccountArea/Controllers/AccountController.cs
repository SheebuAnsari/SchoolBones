using DomLibrary;
using Focus8W.BL;
using SchoolBones.Account;
using SchoolBones.Enums;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Focus8W.Areas.AccountArea.Controllers
{
    public class AccountController : Controller
    {
        TestingDML oTestingDML = new TestingDML();

        AccountsDML oDML = new AccountsDML();
        SaveStatus oSaveStatus = new SaveStatus();

        AccountsDML oAccountsDML = new AccountsDML();
        static StringBuilder sb = new StringBuilder();



        public ActionResult FeeSubmission()
        {
            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.FeeSubmission);
            return PartialView("~/Views/Shared/_CommonInputs.cshtml", oInputs);
        }

        public ActionResult LoadFeeDetails(int iRegistrationId, int iRollno)
        {
            FeeDetails objFeeDetails = oTestingDML.LoadFeeDetails(iRegistrationId, iRollno);
            return PartialView("LoadFeeDetails", objFeeDetails);
        }

        public ActionResult SaveFee(FeeInfo oFeeInfo)
        {
            oSaveStatus = oTestingDML.SaveFeeDetails(oFeeInfo);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult OpenFeeCard(FeeDetails oFeeDetails)
        //{
        //    List<FeeDetails> lstFeeDetails = new List<FeeDetails>();
        //    lstFeeDetails.Add(oFeeDetails);
        //    return View("FeeCard", lstFeeDetails);
        //}

        public ActionResult ClasswiseStudents()
        {
            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.ClasswiseStudents);
            return PartialView("~/Views/Shared/_CommonInputs.cshtml", oInputs);
        }
        public ActionResult LoadClasswiseStudents(int iClass, int iYear)
        {
            StudentDetails[] lstStudents = oDML.LoadClasswiseStudents(iClass, iYear);
            return PartialView("LoadClasswiseStudents", lstStudents);
        }

        #region DefineMonthlySalary
        public ActionResult MonthlySalary()
        {
            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.DefineMontlySalary);
            return PartialView("~/Views/Shared/_CommonInputs.cshtml", oInputs);
        }
        public ActionResult LoadMonthlySalary(int iYear)
        {
            TeacherSalary[] arrTeacherSalary = oAccountsDML.LoadSalary(iYear);
            return PartialView("DefineSalary", arrTeacherSalary);
        }

        public ActionResult SaveSalary(TeacherSalary[] arrTeacherSalary)
        {
            oSaveStatus = oAccountsDML.SaveSalary(arrTeacherSalary);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DefineFee
        public ActionResult FeeStructure()
        {
            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.FeesStructure);
            return PartialView("~/Views/Shared/_CommonInputs.cshtml", oInputs);
        }
        public ActionResult LoadFeesStructure(int iYear)
        {
            FeesStructure[] arrFeesStructure = oAccountsDML.LoadFeesStructure(iYear);
            return PartialView("FeesStructure", arrFeesStructure);
        }
        public ActionResult DeleteFeesStructure(int iFeeStructureId)
        {
            if (iFeeStructureId > 0)
            {
                oSaveStatus = oAccountsDML.DeleteFeesStructure(iFeeStructureId);
                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
            }
            else
            {
                oSaveStatus.iStatus = -1;
                return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateFeesStructure(FeesStructure oFeesStructure)
        {
            oSaveStatus = oAccountsDML.SaveFeesStructure(oFeesStructure);
            return Json(oSaveStatus, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult LoadTeachersSalaryInput()
        {
            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.LoadSalary);
            return PartialView("~/Views/Shared/_CommonInputs.cshtml", oInputs);
        }
        public ActionResult LoadTeachersSalary(int iMonth, int iYear)
        {
            TeacherSalary[] arrTeacherSalary = oAccountsDML.LoadSalaryDetails(iMonth, iYear);
            return PartialView("LoadTeachersSalary", arrTeacherSalary);
        }

        #region FeeCollectionData
        public ActionResult FeeCollection()
        {
            DoubleInt oInputs = new DoubleInt(Id: (int)Modules.Account, Value: (int)SubModule.LoadFeeCollection);
            return PartialView("~/Views/Shared/_CommonInputs.cshtml", oInputs);
        }
        public ActionResult LoadFeeCollection(int iClass, int iMonth, int iYear)
        {
            ReportData oReportData = oAccountsDML.LoadFeeCollection(iClass, iMonth, iYear);
            return PartialView("FeeCollection", oReportData);
        }

        public ActionResult ReceiptUsingServer(int iEnrollmentNo, int iStdId, int iMonthlyFeeId)
        {
            PrintInvoice oPrintInvoice = null;
            oPrintInvoice = oTestingDML.GetPrintObject(iEnrollmentNo, iStdId, iMonthlyFeeId);




            List<string> oData = new List<string>();
            oData.Add("Test.html");
            oData.Add(GenerateHtmlBuilder(oPrintInvoice));
            return Json(oData, JsonRequestBehavior.AllowGet);
        }


        private static void Header()
        {
            sb.AppendLine("<html>");
            sb.AppendLine("<style>");
            sb.AppendLine("a:link {color:#0000FF; text-decoration:none}");
            sb.AppendLine("a:visited {color:#00FF00;}");
            sb.AppendLine("a:hover {color:#FFF000; text-decoration:underLine}");
            sb.AppendLine("a:active {color:#6600FF;}");
            sb.AppendLine("</style>");
            sb.AppendLine("<head></head>");
            sb.Append("<div>Header</div>");
        }
        private static void Body(PrintInvoice oPrintInvoice)
        {
            sb.Append("<div>body</div>");
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
        }
        private static void Footer()
        {
            sb.Append("<div>Footer</div>");
            sb.AppendLine("</html>");
        }
        public static string GenerateHtmlBuilder(PrintInvoice oPrintInvoice)
        {
            Header();
            Body(oPrintInvoice);
            Footer();
            return sb.ToString();
        }
        #endregion
    }
}