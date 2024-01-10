using DomLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolConfiguration;

namespace UI
{
    class MenuAction
    {
        public static List<Option> options;

        public static void MenuList()
        {
            options = new List<Option>
            {
                new Option("Do you want to Create Database?", () => DoAction("Create")),
                new Option("Load list of database.", () =>  DoAction("Load")),
                new Option("Delete created database", () =>  DoAction("Delete")),
                new Option("Create supportive dbConfig.", () =>  DoAction("CreateDb")),
                new Option("Delete supportive dbConfig.", () =>  DoAction("DeleteDbConfig")),
                new Option("Create supportive Tables.", () =>  DoAction("CreateTables")),
                new Option("Create Menu.", () =>  DoAction("CreateMenu")),
                new Option("Delete duplicate Menu.", () =>  DoAction("DeleteDuplicateMenu")),
                new Option("Exit", () => Environment.Exit(0)),
            };
            WriteMenu(options[0]);
            PerformAction();
        }

        static void WriteMenu(Option selectedOption)
        {
            Console.Clear();

            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
                if (option.Name == "Delete created database")
                {
                    Console.WriteLine("-----------------------------------");
                }
            }
        }

        static void DoAction(string sAction)
        {
            Console.Clear();
            Console.WriteLine(sAction);
            if (sAction == "Create")
            {
                SchoolCRUD.SchoolRegistration();
            }
            else if (sAction == "Load")
            {
                SchoolCRUD.LoadDatabase();
            }
            else if (sAction == "Delete")
            {
                SchoolCRUD.DeleteDatabase();
            }
            else if (sAction == "CreateDb")
            {
                SchoolCRUD.CreateDBConfig();
            }
            else if (sAction == "DeleteDbConfig")
            {
                SchoolCRUD.DeleteDbConfig();
            }
            else if (sAction == "CreateTables")
            {
                SchoolCRUD.CreateTables();
            }
            else if (sAction == "CreateMenu")
            {
                SchoolCRUD.CreateMenu();
            }
            else if (sAction == "DeleteDuplicateMenu")
            {
                SchoolCRUD.DeleteDuplicateMenu();
            }

            Console.WriteLine("\nBack to Home");
            Console.ReadKey();

            WriteMenu(options.First());
        }

        static void PerformAction()
        {
            int index = 0;

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        //WriteMenu(options, options[index]);
                        WriteMenu(options[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(options[index]);
                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);
        }
    }
}
