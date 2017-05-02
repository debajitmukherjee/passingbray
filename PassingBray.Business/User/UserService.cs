using PassingBray.Model;
using PassingBray.Model.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace PassingBray.Business
{
    public static class UserService
    {
        public static bool IsAuthenticated(string userName, string password)
        {
            bool isAuthenticated = false;

            string json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Users.txt"));
            var users = new JavaScriptSerializer().Deserialize<List<User>>(json);

            isAuthenticated = users.Any(u => u.UserName == userName && u.Password == password);

            return isAuthenticated;
        }

        public static User GetUser(string userName)
        {
            string json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Users.txt"));
            var users = new JavaScriptSerializer().Deserialize<List<User>>(json);

            return users.FirstOrDefault(u => u.UserName == userName);
        }

        public static bool IsUserAlreadyConnected(User user)
        {
            if(PassingBrayContext.Users == null)
            {
                PassingBrayContext.Users = new List<User>();
            }

            return PassingBrayContext.Users.Any(u => u.UserName == user.UserName);
        }

        public static void RemoveUserFromContext(User user)
        {
            if(PassingBrayContext.Users != null)
            {
                var currentUser = PassingBrayContext.Users.FirstOrDefault(u => u.UserName == user.UserName);
                if(currentUser != null)
                {
                    PassingBrayContext.Users.Remove(currentUser);
                }
            }
        }
       
        public static void RearrangeCards(User user)
        {
            user.Cards = user.Cards.OrderBy(c => c.CardType.ToString()).ThenBy(c => c.Number).ToList();
        }
    }
}
