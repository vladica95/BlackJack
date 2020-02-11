using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            bool GameIsOn = true;
            while (GameIsOn)
            {
                game.PlayTheGame();
                Console.WriteLine("Play again? y/n");
                if (Console.ReadLine() == "n")
                {
                    GameIsOn = false;
                }
            }
        }
    }
}
