using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassingBray.Model
{
    public class User
    {
        public User()
        {
            Cards = new List<Card>();
        }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public List<Card> Cards { get; set; }

        public List<Card> ReceivedCards { get; set; }

        public bool IsAdmin { get; set; }

        public int Position { get; set; }
    }
}
