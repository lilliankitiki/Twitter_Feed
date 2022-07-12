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
                List<User> tweeterUsers = GetAllTweeterUsers();
                List<Tweet> userTweets = new List<Tweet>();

                //Get tweeter users tweets
                foreach (var user in tweeterUsers)
                {
                    List<Tweet> tweets = GetUserTweets(user.Username);
                    userTweets.AddRange(tweets);
                    user.Tweets = tweets;
                }

                foreach (var tweeter in userTweets)
                {

                }

                var sortedUsers = tweeterUsers.OrderBy(x => x.Username).ToList();

                //var display = from user in tweeterUsers
                //              join tweeter in userTweets on user.Username equals tweeter.Tweeter.Username
                //              select new 
                //              {
                //                  user.Username,
                //                  user.Tweets,

                //              };
                DisplayUserTweets(sortedUsers);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void DisplayUserTweets(List<User> tweeterUsers)
        {
            try
            {
                foreach (var user in tweeterUsers)
                {
                    Console.WriteLine(user.Username);
                    foreach (var userTweet in user.Tweets)
                    {
                        Console.WriteLine($"@{user.Username}: {userTweet.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static List<User> GetAllTweeterUsers()
        {
            try
            {
                //Create table 'tweeterUsers'
                List<User> tweeterUsers = new List<User>();

                //1. Read Users from the file
                //x = Ward follows Alan
                string[] userLines = File.ReadAllLines(@"user.txt");

                //2. Create users from file data
                //var users = userLines
                //    .Where(x => !string.IsNullOrWhiteSpace(x))
                //    .Select(x => new User() { Username = })
                //    .ToList();
                foreach (var line in userLines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        /*
                         Ward follows Martin, Alan
                         */
                        string[] names = line.Replace(" ", "").Replace("follows", " ").Split(' ');
                        //Create 
                        User tweeterUser = new User();
                        tweeterUser.Username = names[2];//Ward
                        int index = CheckTweeterUserDuplicate(tweeterUsers, tweeterUser);

                        //3. Add user into table 'tweeterUsers'
                        if (index == -1)
                        {
                            tweeterUser.Following = GetTweeterUserFollowing(names[1].Split(',').ToList());
                            tweeterUsers.Add(tweeterUser);
                        }
                        else
                        {
                            //Update users following
                            var following = GetTweeterUserFollowing(names[1].Split(',').ToList());
                            foreach (var item in following)
                            {
                                if (tweeterUsers[index].Following.FindIndex(x => x.Username == item.Username) == -1)
                                {
                                    tweeterUsers[index].Following.Add(item);
                                }
                            }
                        }
                    }
                }

                return tweeterUsers;
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.Message);
                throw;
            }
        }

        private static int CheckTweeterUserDuplicate(List<User> tweeterUsers, User newTweeterUser)
        {
            try
            {
                return tweeterUsers.FindIndex(x => x.Username == newTweeterUser.Username);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static List<User> GetTweeterUserFollowing(List<string> values)
        {
            List<User> tweeterUsers = new List<User>();
            try
            {
                //Loop through users being followed
                foreach (var item in values)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        //Create user followed by tweeter user
                        User following = new User();
                        following.Username = item;//Alan 
                        tweeterUsers.Add(following);
                    }
                }

                return tweeterUsers;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        static List<Tweet> GetUserTweets(string username)
        {
            List<Tweet> userTweets = new List<Tweet>();
            try
            {
                string[] tweetLines = File.ReadAllLines(@"tweet.txt");
                var tweets = tweetLines.Where(x => !string.IsNullOrWhiteSpace(x) && x.Contains(username)).ToList();

                foreach (var tweet in tweets)
                {
                    //Alan> If you have a procedure with 10 parameters, you probably missed some.
                    Tweet post = new Tweet()
                    {
                        Message = tweet.Substring(tweet.IndexOf('>') + 1).Trim(),
                        Tweeter = new User()
                        {
                            Username = tweet.Substring(0, tweet.IndexOf('>')).Trim()
                        }
                    };

                    userTweets.Add(post);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return userTweets;
        }
    }
}

