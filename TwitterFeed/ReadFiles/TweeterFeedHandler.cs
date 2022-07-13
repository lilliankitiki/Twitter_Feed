using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFiles
{
    public class TweeterFeedHandler
    {
        #region Public Methods

        /// <summary>
        /// Return all users loaded from the file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<User> GetTweeterUsersFromFile(string path)
        {
            List<User> users = new List<User>();
            List<User> followers = new List<User>();
            try
            {
                string[] userLines = File.ReadAllLines(path);
                var fileData = userLines.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                foreach (string line in fileData)
                {
                    string[] data = line.Replace(" ", "").Replace("follows", " ").Split(' ');
                    User user = new User() { Username = data[0] };

                    List<User> usersFollowing = GetTweeterUserFollowing(data[1].Split(',').ToList());
                    foreach (var item in usersFollowing)
                    {
                        AddTweeterUser(ref followers, item);
                        //AddTweeterUser(ref users, item);
                        AddUserFollowing(ref user, item);
                    }

                    AddTweeterUser(ref users, user);
                    //users.Add(user);



                }

                var values = users.Concat(followers).ToList();
                return users;
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace);
                return users;
            }
        }

        /// <summary>
        /// Return all tweets that were tweeted by users
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<Tweet> GetUserTweets(string path)
        {
            List<Tweet> tweets = new List<Tweet>();
            try
            {
                string[] tweetLines = File.ReadAllLines(path);
                var fileData = tweetLines.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                foreach (var tweet in fileData)
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

                    tweets.Add(post);
                }

                return tweets;
            }
            catch (Exception ex)
            {
                ErrorHandling.LogError(ex.StackTrace);
                return tweets;
            }
        }
        #endregion

        #region Private Methods

        private void AddUserFollowing(ref User follower, User item)
        {
            int index = follower.Following.IndexOf(item);
            if (index == -1)
            {
                follower.Following.Add(item);
            }
        }

        private bool UserExists(List<User> users, User user)
        {
            int index = users.FindIndex(u => u.Username == user.Username);
            return index > 0;
        }

        private void AddTweeterUser(ref List<User> users, User user)
        {
            int index = users.FindIndex(u => u.Username == user.Username);

            if (index == -1)
            {
                users.Add(user);
            }
            else
            {
                users[index] = user;
            }
        }

        /// <summary>
        /// Returns all the user followed
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private List<User> GetTweeterUserFollowing(List<string> values)
        {
            List<User> tweeterUsers = new List<User>();
            try
            {
                //Loop through users being followed
                foreach (var username in values)
                {
                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        //Create user followed by tweeter user
                        User following = new User();
                        following.Username = username;
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

        #endregion
    }
}
