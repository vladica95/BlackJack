using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Player
    {
        private int valueOfCards = 0;
        private List<Card> hand;
        private String name;
        private int score=0;
        private bool status = true;
        

        public Player()
        {
            Console.WriteLine("Enter name of player: ");
            Name= Console.ReadLine();
            hand = new List<Card>();
        }
        
        public Player(String name,int score)
        {
            Name = name;
            Score = score;
        }

        public bool Decision()
        {
            int decision = -1; bool tmpRet = false;
            while (decision < 0)
            {
                Console.WriteLine("Take card? 1 - yes or 0 - no: ");
                decision = Int32.Parse(Console.ReadLine());
                if (decision > 1)
                {
                    decision = -1;
                }
                if (decision == 1)
                {
                    tmpRet = true;
                }
                else if (decision == 0)
                {
                    Status = false;
                    tmpRet = false;
                }
            }
            return tmpRet;
        }
        public void SaveScore(TextWriter textWriter)
        {
            textWriter.WriteLine(Name+ " " + Score);
 
        }

        public void TakeCard(Card card)
        {
            hand.Add(card);            
            Console.Write("Player " + Name + " took card: ");
            card.PrintCard();
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
            foreach(var v in hand)
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
            
            foreach (var v in hand)
            {
                v.PrintCard();
            }
            Console.WriteLine("Value of hand: " + ValueOfHand());
        }
        
        #region Properties

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int ValueOfCards
        {
            get { return this.valueOfCards; }
            set { this.valueOfCards = value; }
        }

        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        public bool Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        #endregion

    }
}
