using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
    internal class SlackResponse
    {
        public class User
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        public class Message
        {
            public string text { get; set; }
            public string user { get; set; }
            public string ts { get; set; }
        }

        public User[] users { get; set; }
        public Message[] messages { get; set; }
    }
}
