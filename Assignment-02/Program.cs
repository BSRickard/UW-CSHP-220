using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Course:  UW CSHP 220 C (2018 Winter)
// Project: Assignment 02
// Author:  RICKARD, Brian
// Date:    2018-03-14

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<Models.User>();
            var user  = new Models.User();

            users.Add(new Models.User { Name = "Dave",  Password = "hello" });
            users.Add(new Models.User { Name = "Steve", Password = "steve" });
            users.Add(new Models.User { Name = "Lisa",  Password = "hello" });

            // Use System.Linq for all the requirements. IE. Don't use a for/foreach loop to manipulate the users list.
            // 1.Display to the console, all the passwords that are "hello".Hint: Where

            string badPassword = "hello";
            Console.WriteLine("Searching for all users whose password is {0}...", badPassword);
            IEnumerable<Models.User> query = users.Where(t => t.Password == badPassword);
            if (query == null)
            {
                Console.WriteLine("No password is equal to {0}.", badPassword);
            }
            else
            {
                foreach (var usr in query)
                {
                    // Though not stated, I've assumed we're to display their names, too (else not very meaningful)
                    Console.WriteLine(string.Format("  Found {0}, whose password is {1}", usr.Name, usr.Password));
                }
            }

            // 2.Delete any passwords that are the lower-cased version of their Name.
            // Do not just look for "steve".It has to be data - driven.Hint: Remove or RemoveAll
            Console.WriteLine("\nSearching for all users whose password is their name in lowercase...", badPassword);
            while ((user = users.Find(t => t.Password == t.Name.ToLower())) != null)
            {
                Console.WriteLine(string.Format("  Removing {0}, whose password is {1}.",
                    user.Name, user.Password));
                users.Remove(user);
            }

            // 3.Delete the first User that has the password "hello".Hint: First or FirstOrDefault
            Console.WriteLine("\nSearching for the first user whose password is {0}...", badPassword);
            if ((user = users.Find(t => t.Password == badPassword)) != null)
            {
                Console.WriteLine(string.Format("  Removing {0}, whose password is {1}.",
                    user.Name, user.Password));
                users.Remove(user);
            }

            // 4.Display to the console the remaining users with their Name and Password.
            Console.WriteLine("\nListing all remaining users...");
            foreach (var usr in users)
            {
                Console.WriteLine(string.Format("  Found {0}, whose password is {1}.", usr.Name, usr.Password));
            }
        }
    }
}
