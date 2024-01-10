using DomLibrary;
using System;
//using SchoolBones;

namespace SchoolConfiguration
{
    public class SchoolCRUD
    {
        static string sDbName = "";


        #region MasterCRUDRelatedOperations
        public static void SchoolRegistration()
        {
            string sError = "";
            CreateDatabase(ref sError);
            if (string.IsNullOrEmpty(sError) == true)
            {

                Console.WriteLine("Database is created successfully.");

                //Create DbConfig either db exist or not
                FileCRUD.CreateDbConfig(sDbName, ref sError);
                Console.WriteLine("CreateDbConfig is Created successfully.");

                //Do you wnat to create default Schema or not?
                if (string.IsNullOrEmpty(sError) == true)
                {
                    //Create Tables
                    //CreateTables(sDbName);
                }
                else
                {
                    Console.WriteLine("Write data in DbDonfig : {0}", sError);
                }
                //Create Tables
                //CreateDbDonfig(sDbName);
                //CreateTables(sDbName);
            }
            else
            {
                Console.WriteLine("Connection Error : {0}", sError);
            }
        }
        public static void CreateDatabase(ref string sError)
        {
            SetInputs(ref sError);

            if (string.IsNullOrEmpty(sError) == true)
            {
                //First Create a new db
                MasterDBOperations.CreateDatabase(sDbName, ref sError);
            }
            else
            {
                Console.WriteLine("Something wrong in inputs.");
            }
        }
        public static void LoadDatabase()
        {
            string sError = "";
            SetInputs(ref sError, "Load");
            if (string.IsNullOrEmpty(sError) == true)
            {
                Databases[] arrDBs = MasterDBOperations.LoadDatabase(ref sError);
                if (string.IsNullOrEmpty(sError) == true)
                {
                    if (arrDBs != null)
                    {
                        Console.WriteLine("Sr No. \t\t Database Id \t\t Database Name");
                        for (int i = 0; i < arrDBs.Length; i++)
                        {
                            Console.WriteLine("\t{2} \t\t {0} \t\t {1}", arrDBs[i].DbId, arrDBs[i].DbName, (i + 1));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Connection error : {0}", sError);
                }
            }
            else
            {
                Console.WriteLine("Something wrong in inputs.");
            }
        }
        public static void DeleteDatabase()
        {
            string sError = "";
            LoadDatabase();
            if (string.IsNullOrEmpty(sError) == true)
            {
                SetInputs(ref sError);
                if (string.IsNullOrEmpty(sError) == true)
                {
                    MasterDBOperations.DeleteDatabase(sDbName, ref sError);
                    if (string.IsNullOrEmpty(sError) == true)
                    {
                        Console.WriteLine("Deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Connection error : {0}", sError);
                    }
                }
                else
                {
                    Console.WriteLine("Something wrong in inputs.");
                }
            }
        }
        #endregion


        #region WelcomeSchool
        public static void CreateTables()
        {
            string sError = "";
            LoadDatabase();
            SetInputs(ref sError);
            if (string.IsNullOrEmpty(sError) == true)
            {
                //Create dbConfig for selected db
                FileCRUD.CreateDbConfig(sDbName, ref sError);
                ConnectionHelper.SetConString(ref sError);
                DefaultTables.CreateTables(sDbName, ref sError);
                if (string.IsNullOrEmpty(sError) == true)
                {
                    Console.WriteLine("Default tables are created successfully.");
                }
                else
                {
                    Console.WriteLine(string.Format("Error duplicate : {0}", sError));
                }
            }
            else
            {
                Console.WriteLine("Something wrong in inputs.");
            }
        }
        public static void CreateMenu()
        {
            string sError = "";
            Menu[] arrMenu = null;
            LoadDatabase();
            SetInputs(ref sError);
            if (string.IsNullOrEmpty(sError) == true)
            {
                arrMenu = FileCRUD.ReadMenuXML(ref sError);
                if (arrMenu != null && arrMenu.Length > 0)
                {
                    ConnectionHelper.SetConString(ref sError);
                    DBOperations.CreateMenuUsingXML(arrMenu, ref sError);
                    if (string.IsNullOrEmpty(sError) == true)
                    {
                        Console.WriteLine("Menu are created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Error : {0}", sError);
                    }
                }
                else
                {
                    Console.WriteLine("Problem in reading menu xml.");
                }
            }
            else
            {
                Console.WriteLine("Something wrong in inputs.");
            }
        }
        public static void DeleteDuplicateMenu()
        {
            string sError = "";
            LoadDatabase();
            SetInputs(ref sError);
            if (string.IsNullOrEmpty(sError) == true)
            {
                DBOperations.DeleteDuplicateMenu(ref sError);
                if (string.IsNullOrEmpty(sError) == false)
                {
                    Console.WriteLine("Duplicate menu deleted successfully.");
                }
                else
                {
                    Console.WriteLine("There is no duplicate menu.");
                }
            }
            else
            {
                Console.WriteLine("Something wrong in inputs.");
            }
        }
        public static void CreateDBConfig()
        {
            string sError = "";
            LoadDatabase();
            SetInputs(ref sError);
            if (string.IsNullOrEmpty(sError) == true)
            {
                FileCRUD.CreateDbConfig(sDbName, ref sError);
                if (string.IsNullOrEmpty(sError) == true)
                {
                    Console.WriteLine("Created successfully");
                }
                else
                {
                    Console.WriteLine("Connection error : {0}", sError);
                }
            }
            else
            {
                Console.WriteLine("Something wrong in inputs.");
            }
        }

        public static void DeleteDbConfig()
        {
            string sError = "";
            FileCRUD.DeleteDbConfig(ref sError);
            if (string.IsNullOrEmpty(sError))
            {
                Console.WriteLine("DbConfig file is deleted successfully.");
                ConnectionHelper.SetConString(ref sError);
                if (string.IsNullOrEmpty(sError))
                {
                    //new connection string formed.
                }
                else
                {
                    //new connection string not formed formed.
                }
            }
            else
            {
                Console.WriteLine("DbConfig file is deleted successfully.");
            }
        }
        #endregion

        public static void SetInputs(ref string sError, string sAction = "")
        {
            if (sAction != "Load")
            {
                Console.Write("Enter db name : ");
                sDbName = Console.ReadLine();

                Console.Write("\nAre you sure to proceed for next Steps? \tPress (Y/N) :\t");

                if (!UNIQUE.Options.Contains(Console.ReadLine()))
                {
                    sError = "Im not sure!!!";
                }
            }
        }
    }
}
