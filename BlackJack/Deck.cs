using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Deck
    {
        private List<Card> cards;
        private String[] colArray = { "pik", "tref", "karo", "herc" };
        private String[] valArray = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        public Deck()
        {
            cards = new List<Card>();
            for (int i = 0; i < valArray.Length; i++)
            {
                for (int j = 0; j < colArray.Length; j++)
                {
                    cards.Add(new Card(valArray[i], colArray[j]));
                }
            }

        }
        private int Luck()
        {
            return ((DateTime.Now.Millisecond * DateTime.Now.Minute * DateTime.Now.Year) % cards.Count);
        }

        public Card GetCard()
        {
            Card retCard = cards[Luck()];
            cards.Remove(retCard);
            return retCard;
        }


        public void Print()
        {
            foreach (var v in cards)
            {
                v.PrintCard();
            }
        }
    }
}
