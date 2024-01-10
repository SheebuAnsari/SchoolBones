using DomLibrary;
using SchoolBones.Common;
using System;

namespace Focus8W.Models
{
    public class SessionKey
    {
        public const string SchoolId = "SchoolId";
        public const string UserId = "UserId";
        public const string RegId = "RegId";
    }
    public class GLOBAL
    {
        public void GetGlobalData()
        {

        }
        public SchoolDetails LoadSchoolDetails()
        {
            SchoolDetails objSchoolDetails = new SchoolDetails();

            //AdminDML oAdminCRUD = new AdminDML();
            CoreDML oCoreDML = new CoreDML();
            objSchoolDetails = oCoreDML.LoadSchoolDetails();
            return objSchoolDetails;
        }

        public int SchoolId
        {
            get
            {
                object oValue = null;
                oValue = System.Web.HttpContext.Current.Session[SessionKey.SchoolId];
                if (oValue == null)
                {
                    return 0;
                }
                return Convert.ToInt32(oValue);
            }
            //set
            //{
            //    value
            //}
        }
        public int UserId
        {
            get
            {
                object oValue = null;
                oValue = System.Web.HttpContext.Current.Session[SessionKey.UserId];
                if (oValue == null)
                {
                    return 0;
                }
                return Convert.ToInt32(oValue);
            }
            set
            {
                System.Web.HttpContext.Current.Session[SessionKey.UserId] = value;
            }
        }
        public int RegId
        {
            get
            {
                object oValue = null;
                oValue = System.Web.HttpContext.Current.Session[SessionKey.RegId];
                if (oValue == null)
                {
                    return 0;
                }
                return Convert.ToInt32(oValue);
            }
            set
            {
                System.Web.HttpContext.Current.Session[SessionKey.RegId] = value;
            }
        }
    }
}