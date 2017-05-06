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

        public static bool IsUserConnected(User user)
        {
            if(PassingBrayContext.ConnectedUser == null)
            {
                PassingBrayContext.ConnectedUser = new List<User>();
            }

            return PassingBrayContext.ConnectedUser.Any(u => u.UserName == user.UserName);
        }

        public static void RemoveUserFromContext(User user)
        {
            if(PassingBrayContext.ConnectedUser != null)
            {
                var currentUser = PassingBrayContext.ConnectedUser.FirstOrDefault(u => u.UserName == user.UserName);
                if(currentUser != null)
                {
                    PassingBrayContext.ConnectedUser.Remove(currentUser);
                }
            }
        }
       
        public static void RearrangeCards(User user)
        {
            user.Cards = user.Cards.OrderBy(c => c.CardType.ToString()).ThenBy(c => c.Number).ToList();
        }

        public static void PassCards(User user, List<Card> cards)
        {
            if(!IsGameEnabled())
            {
                return;
            }

            //remove the card from the user
            foreach(var card in cards)
            {
                var userCard = user.Cards.FirstOrDefault(c => c.CardType == card.CardType && c.Number == card.Number);
                user.Cards.Remove(userCard);
            }

            int position = user.Position;
            if(position == 4)
            {
                PassingBrayContext.Users[0].ReceivedCards = cards;
                PassingBrayContext.Users[0].Cards.AddRange(cards);
                RearrangeCards(PassingBrayContext.Users[0]);
            }
            else
            {
                PassingBrayContext.Users[position + 1].ReceivedCards = cards;
                PassingBrayContext.Users[position + 1].Cards.AddRange(cards);
                RearrangeCards(PassingBrayContext.Users[position + 1]);
            }
        }

        public static bool IsGameEnabled()
        {
            if(PassingBrayContext.Users == null || PassingBrayContext.Users.Count != 4)
            {
                return false;
            }

            if (PassingBrayContext.ConnectedUser == null || PassingBrayContext.ConnectedUser.Count != 4)
            {
                return false;
            }

            return true;
        }

        public static User GetNextUser(User user)
        {
            int position = user.Position;
            return position == 4 ? PassingBrayContext.Users[0] : PassingBrayContext.Users[position + 1];
        }

        public static void RecieveDeal(Deal deal)
        {
            if(PassingBrayContext.Deals == null)
            {
                PassingBrayContext.Deals = new List<Deal>();
            }

            if(PassingBrayContext.Deals.Count == 4)
            {
                return;
            }

            PassingBrayContext.Deals.Add(deal);

        }

        public static bool IsDealCompleted()
        {
            return PassingBrayContext.Deals.Count == 4;
        }

        public static User CalculatePoint()
        {
            var firstDeal = PassingBrayContext.Deals[0];
            var dealOwner = PassingBrayContext.Deals.Where(d => d.Card.CardType == firstDeal.Card.CardType).OrderByDescending(d => d.Card.Number).FirstOrDefault();

            //Calculate point
            int point = 0;
            foreach(var deal in PassingBrayContext.Deals)
            {
                if(deal.Card.CardType == Model.Enums.CardType.Hearts)
                {
                    point += 1;
                }
                if(deal.Card.CardType == Model.Enums.CardType.Spades && deal.Card.Number == 12)
                {
                    point += 12;
                }
            }


            dealOwner.User.Score += point;
            PassingBrayContext.Deals = new List<Deal>();
            return dealOwner.User;
        }

        public static bool IsGameCompleted()
        {
            return PassingBrayContext.Users.Any(u => u.Score >= 100);
        }
    }
}
