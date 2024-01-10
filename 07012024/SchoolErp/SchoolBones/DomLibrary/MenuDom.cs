using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomLibrary
{
    public class MenuDom
    {
    }
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Caption { get; set; }
        public string ActionMethod { get; set; }
        public int Module { get; set; }
        public int SubModule { get; set; }
        public bool IsGroup { get; set; }
        public bool IsActive { get; set; }
    }
    public class DbInformation
    {
        public string ServerName { get; set; }
        public string DbName { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

    }

    public class Databases
    {
        public int DbId { get; set; }
        public string DbName { get; set; }
    }
    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }

        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
    public class UNIQUE
    {
        public static string Options = "Yes yes Y y";
    }
}
