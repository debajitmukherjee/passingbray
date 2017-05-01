using PassingBray.Model;
using PassingBray.Model.Application;
using PassingBray.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassingBray.Business
{
    public static class DeckService
    {

        public static List<Card> Shuffle()
        {
            var cards = PopulateDeck();

            cards.Shuffle();

            return cards;
        }

        public static List<Card> PopulateDeck()
        {
            var cards = new List<Card>();

            cards.AddRange(PopulateCards(CardType.Clubs));
            cards.AddRange(PopulateCards(CardType.Diamonds));
            cards.AddRange(PopulateCards(CardType.Hearts));
            cards.AddRange(PopulateCards(CardType.Spades));

            return cards;
        }

        public static List<Card> PopulateCards(CardType cardType)
        {
            var cards = new List<Card>();

            for (int count = 2; count <= 14; count++)
            {
                cards.Add(new Card
                {
                    Number = count,
                    CardType = cardType,
                    Image = GetImage(count, cardType)
                });

            }

            return cards;
        }

        public static void DistributeCards()
        {
            AppItem.Cards = Shuffle();
            if (AppItem.Users != null && AppItem.Users.Count == 4)
            {
                for (int count = 0; count < AppItem.Cards.Count; count = count + 4)
                {
                    AppItem.Users[0].Cards.Add(AppItem.Cards[count]);
                    AppItem.Users[1].Cards.Add(AppItem.Cards[count + 1]);
                    AppItem.Users[2].Cards.Add(AppItem.Cards[count + 2]);
                    AppItem.Users[3].Cards.Add(AppItem.Cards[count + 3]);
                }
            }

            foreach(var user in AppItem.Users)
            {
                UserService.RearrangeCards(user);
            }

        }

        #region private methods

        private static string GetImage(int number, CardType cardType)
        {
            string image = "_of_" + cardType.ToString().ToLowerInvariant();

            if (number <= 10)
            {
                image = number.ToString() + image;
            }
            else if (number == 11)
            {
                image = "jack" + image + "2";
            }
            else if (number == 12)
            {
                image = "queen" + image + "2";
            }
            else if (number == 13)
            {
                image = "king" + image + "2";
            }
            else if (number == 14)
            {
                image = "ace" + image;
            }

            image += ".png";

            return image;
        }

        #endregion private methods


    }
}
