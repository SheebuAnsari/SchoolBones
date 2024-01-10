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
            string sError = "";
            string sFilePath = "";
            do
            {
                sError = "";
                sFilePath = @"C:\Users\Venkateshwar\Desktop\CoreFiles";

                Console.Write("\nEnter FileFolder path : ");
                Console.WriteLine("Default path : {0}", sFilePath);
                Console.ReadLine();

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
    }
}
