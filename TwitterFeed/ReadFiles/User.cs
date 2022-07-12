using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFiles
{
    public class User
    {
        public string Username { get; set; }

        public List<Tweet> Tweets { get; set; } = new List<Tweet>();

        public List<User> Following { get; set; }

        public List<User> Followers { get; set; }
    }
}
