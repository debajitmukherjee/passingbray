using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassingBray.Model.Application
{
    public static class PassingBrayContext
    {
        public static List<Card> Cards { get; set; }

        public static List<User> Users { get; set; }

        public static List<User> ConnectedUser { get; set; }

        public static List<Deal> Deals { get; set; }
    }
}
