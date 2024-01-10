using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomLibrary
{
    class CoreDS
    {
    }

    //public class OrganizationRegistration
    //{
    //    public int RegistrationId { get; set; }
    //    public string OrganizationName { get; set; }
    //    public int RegistrationDate { get; set; }
    //    public string DbName { get; set; }

    //}

    //public class DbInformation
    //{
    //    public string ServerName { get; set; }
    //    public string DbName { get; set; }
    //    public bool IntegratedSecurity { get; set; }
    //    public string UserId { get; set; }
    //    public string Password { get; set; }

    //}
    public class ProjectConfiguration
    {
        public LicenseInfo LicenseInfo { get; set; }
        public SchoolInfo SchoolInfo { get; set; }
    }
    public class LicenseInfo
    {
        public int LicenseInfoId { get; set; }
        public string DbName { get; set; }
        public string Provider { get; set; }
        public string Key { get; set; }
        public int Validity { get; set; }
        public int InstallationDate { get; set; }       //ie Current Date
        public int ExpiryDate { get; set; }
    }
    public class SchoolInfo
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Principal { get; set; }
        public string Establish { get; set; }
        public int RegistrationDate { get; set; }
        public string RegistrationNo { get; set; }
        public string Address { get; set; }       //ie Current Date
        public int Contact { get; set; }
    }
}
