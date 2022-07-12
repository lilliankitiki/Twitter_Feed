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
            try
            {
                string[] userlines = File.ReadAllLines(@"C:\Lillian\Twitter_Feed\user.txt");
                Console.WriteLine("Users");
                Console.WriteLine("\n");

                foreach (var user in userlines)
                {
                    var results = user.Split(' ');
                    Array.ForEach(results, Console.WriteLine);
                    Array.Sort(results);
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }

            try
            {
                Console.WriteLine("\n");
                Console.WriteLine("Tweets");
                Console.WriteLine("\n");
                string[] tweetlines = File.ReadAllLines(@"C:\Lillian\Twitter_Feed\tweet.txt");
                //string user = File.ReadLine(@"C:\Lillian\Twitter_Feed\user.txt");

                 foreach (string line in tweetlines)
                 {
                    //Format the tweets
                    string newline = line.Replace('>', ':');
                    Console.WriteLine("\t" + "@" + newline);
                    Array.Sort(tweetlines);
                 }
            
                // Keep the console window open in debug mode.
                Console.WriteLine("Press any key to exit the console app.");
                System.Console.ReadKey();
            }
            catch (Exception e)
            {
                throw e;
            }
        }            
      }
    }

