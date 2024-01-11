using DomLibrary;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            TEST();

            string sError = "";
            string sFilePath = "";
            do
            {
                sError = "";

                Console.Write("\nEnter FileFolder path : ");
                Console.WriteLine("Default path : {0}", sFilePath);
                sFilePath = Console.ReadLine();
                if(string.IsNullOrEmpty(sFilePath) == true)
                {
                    sFilePath = @"C:\Users\Mohd Sheebu\Desktop\FileFactory";
                }
                ConnectionHelper.SetConString(ref sError, 1, sFilePath);

                if (string.IsNullOrEmpty(sError) == false)
                {
                    Console.WriteLine("Error : " + sError + "\n");
                    Console.WriteLine("Do you want to continue?");
                    Console.Write("Press : Y/N\t:");
                }
            } while (string.IsNullOrEmpty(sError) != true && UNIQUE.Options.Contains(Console.ReadLine()));


            if (string.IsNullOrEmpty(sError) == true)
                MenuAction.MenuList();

            Console.ReadLine();
        }

        public static void TEST()
        {
            Console.WriteLine("This is test method.");
        }
    }
}
