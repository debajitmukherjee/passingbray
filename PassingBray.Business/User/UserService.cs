using PassingBray.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassingBray.Business
{
    public static class UserService
    {
        public static bool Authenticate(string userName, string password)
        {
            bool isAuthenticated = false;


            return isAuthenticated;
        }

        public static void RearrangeCards(User user)
        {
            user.Cards = user.Cards.OrderBy(c => c.CardType.ToString()).ThenBy(c => c.Number).ToList();
        }
    }
}
