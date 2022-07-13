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

            TweeterFeedHandler tweeterFeedHandler = new TweeterFeedHandler();

            List<User> users = tweeterFeedHandler.GetTweeterUsersFromFile(@"user.txt").OrderBy(o => o.Username).ToList();
            List<Tweet> tweets = tweeterFeedHandler.GetUserTweets(@"tweet.txt").ToList();

            List<User> usersWithTweets = new List<User>();
            List<Tweet> userTweets = new List<Tweet>();

            foreach (var user in users)
            {
                var followingTweets = tweets.Where(x => user.Following.Any(f => f.Username == x.Tweeter.Username));
                userTweets = tweets.Where(x => x.Tweeter.Username == user.Username)
                    .Concat(followingTweets).OrderBy(o => o.DateCreated).ToList();

                Console.WriteLine(user.Username);
                userTweets.ForEach((tweet) => Console.WriteLine($"@{tweet.Tweeter.Username}: {tweet.Message}"));
            }
            Console.ReadKey();
        }
    }
}

