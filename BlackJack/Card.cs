using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Card
    {
        private int cardValue;
        private String number, color;

        public Card(String num, String col)
        {

            Number = num;
            Color = col;
            if (num == "A")
            {
                Value = 11;
            }
            else if (num == "J" || num == "Q" || num == "K")
            {
                Value = 10;
            }
            else
            {
                Value = Int32.Parse(number);
            }

        }
        #region Properties
        public String Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        public String Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public int Value
        {
            get { return this.cardValue; }
            set { this.cardValue = value; }
        }

        #endregion


        public void PrintCard()
        {
            Console.Write(Number + " " + Color + "  ");
        }
    }
}
