using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Dealer
    {
        private int valueOfCards = 0;
        private List<Card> hand;

        public Dealer()
        {
            hand = new List<Card>();
        }

        public void TakeCard(Card card)
        {
            hand.Add(card);
            Console.Write("Dealer took card: ");
            card.PrintCard();
            ValueOfHand();
            Console.WriteLine();
        }

        public void ClearHand()
        {
            hand.Clear();
        }

        private bool ACheck()
        {
            foreach (var v in hand)
            {
                if (v.Number == "A")
                    return true;
            }
            return false;
        }

        private int ValueOfHand()
        {
            ValueOfCards = 0;
            foreach (var v in hand)
            {
                ValueOfCards = ValueOfCards + v.Value;
            }
            if (ValueOfCards > 21 && ACheck())
            {
                ValueOfCards = ValueOfCards - 10;
            }
            return ValueOfCards;
        }
        public void PrintHand()
        {
            Console.WriteLine();
            foreach (var v in hand)
            {
                v.PrintCard();
            }
            Console.WriteLine("Dealer's value of cards: " + ValueOfCards);
            Console.WriteLine();
        }
        public int ValueOfCards
        {
            get { return this.valueOfCards; }
            set { this.valueOfCards = value; }
        }
    }
}
