using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFiles
{
     class Program
    {
            static void Main()
            {
                string[] userlines = File.ReadAllLines(@"C:\Lillian\Twitter_Feed\user.txt");
                Console.WriteLine("Users");
                Console.WriteLine("\n");

            foreach (var user in userlines)
            {
                Console.WriteLine(user);   
                
            }
            Array.Sort(userlines);

            Console.WriteLine("\n");
                Console.WriteLine("Tweets");
                Console.WriteLine("\n");
                string[] tweetlines = File.ReadAllLines(@"C:\Lillian\Twitter_Feed\tweet.txt");

                foreach (string line in tweetlines)
                {
                    // Use a tab to indent each line of the file.
               
                        string newline = line.Replace('>', ':');
                        Console.WriteLine("\t" + "@" + newline);
                    Array.Sort(tweetlines);
                }

                    // Keep the console window open in debug mode.
                    Console.WriteLine("Press any key to exit the console app.");
                    System.Console.ReadKey();
            }
        }
    }

