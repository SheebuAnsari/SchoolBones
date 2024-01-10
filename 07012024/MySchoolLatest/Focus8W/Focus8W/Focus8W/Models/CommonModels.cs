//using Focus8W.DataStruct;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Focus8W.Models
//{
//    public class CommonModels
//    {
//        public string LoadCalender()
//        {
//            DateTime dt = DateTime.Now;
//            int iMon = dt.Month;
//            return "";
//        }
//        private static List<IdNameTag> AdminMenu()
//        {
//            List<IdNameTag> lstMenu = new List<IdNameTag>();
//            lstMenu.Add(new IdNameTag(0, "Admin", "Admin"));
//            lstMenu.Add(new IdNameTag(1, "Add Teacher", null));
//            lstMenu.Add(new IdNameTag(2, "All Teachers", null));
//            lstMenu.Add(new IdNameTag(9, "Active Teachers", null));
//            lstMenu.Add(new IdNameTag(10, "In-Active Teachers", null));
//            lstMenu.Add(new IdNameTag(3, "Take Attendance", null));
//            lstMenu.Add(new IdNameTag(4, "Load Attendance", null));
//            //lstMenu.Add(new IdNameTag(5, "Teacher Payroll", null));
//            lstMenu.Add(new IdNameTag(6, "ID Card of Teachers", null));
//            lstMenu.Add(new IdNameTag(7, "Attendance Approval", null));
//            lstMenu.Add(new IdNameTag(8, "ID Card of Students", null));
//            return lstMenu;
//        }
//        private static List<IdNameTag> TeacherMenu()
//        {
//            List<IdNameTag> lstMenu = new List<IdNameTag>();
//            lstMenu.Add(new IdNameTag(0, "Teacher", "Teacher"));
//            lstMenu.Add(new IdNameTag(1, "Add Student", null));
//            lstMenu.Add(new IdNameTag(11, "Load All Students", null));
//            lstMenu.Add(new IdNameTag(12, "Active Students", null));
//            lstMenu.Add(new IdNameTag(13, "In-Active Students", null));
//            lstMenu.Add(new IdNameTag(14, "Delete Students Permanant", null));
//            lstMenu.Add(new IdNameTag(4, "MarkSheet Create", null));
//            lstMenu.Add(new IdNameTag(6, "MarkSheet CreateD", null));
//            lstMenu.Add(new IdNameTag(2, "Add Subjects", null));
//            lstMenu.Add(new IdNameTag(15, "Take Attendance", null));
//            lstMenu.Add(new IdNameTag(16, "Load Attendance", null));
//            return lstMenu;
//        }
//        private void StudentMenu()
//        {
//            List<IdNameTag> lstMenu = new List<IdNameTag>();
//            lstMenu.Add(new IdNameTag(0, "Student", "Student"));
//            lstMenu.Add(new IdNameTag(1, "Fee", null));
//        }
//        private static List<IdNameTag> AccountsMenu()
//        {
//            List<IdNameTag> lstMenu = new List<IdNameTag>();
//            lstMenu.Add(new IdNameTag(0, "Accounts", "Accounts"));
//            lstMenu.Add(new IdNameTag(1, "Fee Submission", null));
//            lstMenu.Add(new IdNameTag(2, "Classwise Students", null));
//            return lstMenu;
//        }
//        public List<IdNameTag> GetMenuOfModule(int iModule)
//        {
//            List<IdNameTag> lstMenu = new List<IdNameTag>(); 
//            //Dictionary<int, string> lstMenu = new Dictionary<int, string>();
//            if (iModule == (int)Modules.Admin)
//            {
//                lstMenu.AddRange(AdminMenu());
//                lstMenu.AddRange(TeacherMenu());
//                lstMenu.AddRange(AccountsMenu());

//                //lstMenu.Add(0, "Admin");
//                //lstMenu.Add(1, "Add Teacher");
//                //lstMenu.Add(2, "Teachers Detail");
//                //lstMenu.Add(3, "Teacher Attendance");
//                //lstMenu.Add(4, "Teacher Payroll");

//                //lstMenu.Add(5, "Teacher");
//                //lstMenu.Add(6, "Add Student");
//                //lstMenu.Add(7, "Delete Student");
//                //lstMenu.Add(8, "Student Attendance");
//                //lstMenu.Add(9, "Student MarkSheet");

//                //lstMenu.Add(10, "Student");
//                //lstMenu.Add(11, "Fee");
//                //lstMenu.Add(12, "Marksheet");

//            }
//            if (iModule == (int)Modules.Teacher)
//            {
//                //lstMenu.Add(0, "Teacher");
//                //lstMenu.Add(1, "Add Student");
//                //lstMenu.Add(2, "Delete Student");
//                //lstMenu.Add(3, "Student Attendance");
//                //lstMenu.Add(4, "Student MarkSheet");

//                //lstMenu.Add(5, "Student");
//                //lstMenu.Add(6, "Fee");
//                //lstMenu.Add(7, "Marksheet");
//            }
//            if (iModule == (int)Modules.Student)
//            {
//                //lstMenu.Add(0, "Student");
//                //lstMenu.Add(1, "Fee");
//                //lstMenu.Add(2, "Marksheet");
//            }
//            return lstMenu;
//        }
        
//    }
//}