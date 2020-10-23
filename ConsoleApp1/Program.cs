using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Utilities;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopSiteDB db = new ShopSiteDB();

            Console.WriteLine("enter the pass");
            var pass = Console.ReadLine();
            var us = db.Users.SingleOrDefault(u => u.UserName == "martini");
            var userpass = us.Password;


            if (AccountsUtilities.VerifyHashPassword(userpass,pass))
            {
                Console.WriteLine("success");
            }
            else
            {
                Console.WriteLine("failed");
            }

            Console.ReadKey();
        }
    }
}
