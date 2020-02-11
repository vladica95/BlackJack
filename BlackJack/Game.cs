using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Game
    {
        private Dealer dealer;
        private Deck deck;
        public Player[] players;
        private List<Player> highScore;
        public int numberOfPlayers = 0;
         
        public Game()
        {
            dealer = new Dealer();
            deck = new Deck();
            FirstSave();
            while (numberOfPlayers < 2)
            {
                Console.WriteLine("Number of player should be between 2 and 6. ");
                Console.WriteLine("Enter number of players: ");
                numberOfPlayers = Int32.Parse(Console.ReadLine());
                if (numberOfPlayers > 6)
                {
                    numberOfPlayers = 0;
                }
            }
            players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i] = new Player();
                if (i > 0)
                {
                    while (!NameCheck(i))
                    {
                        Console.WriteLine("That player name is taken!");
                        players[i] = new Player();
                    }
                }
            }
        }
        private void FirstSave()
        {
            TextWriter textWriter = null;
            try
            {
                FileStream fileStream = new FileStream(@"E:\HighScore.txt", FileMode.Create);
                textWriter = new StreamWriter(fileStream);
                for (int i = 0; i < 10; i++)
                {
                    (new Player ("A",0)).SaveScore(textWriter);
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                textWriter.Close();
            }
        }
        private void Sort(List<Player> array)
        {
            for (int i = 0; i < array.Count - 1; i++)
                for (int j = 0; j < array.Count - i - 1; j++)
                    if (array[j].Score < array[j + 1].Score)
                    {
                        Player temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
        }

        private void Save(String path)
        {
            TextWriter textWriter = null;
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                textWriter = new StreamWriter(fileStream);
                for (int i = 0; i < 10; i++)
                {
                    highScore[i].SaveScore(textWriter);
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                textWriter.Close();
            }
        }

        private void LoadScores(String path)
        {
            TextReader textReader = null;
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                textReader = new StreamReader(fileStream);
                highScore = new List<Player>();
                for (int i = 0; i < numberOfPlayers; i++)
                    highScore.Add(players[i]);
               
                for (int i=0;i<10;i++)
                {
                    String line = textReader.ReadLine();
                    String[] data = line.Split(' ');
                    int num = Int32.Parse(data[1]);
                    int counter = 0;
                    while (counter < numberOfPlayers && highScore[counter].Name != data[0])
                    {
                        counter++;
                    }
                    if (counter< numberOfPlayers)
                    {
                        highScore[counter].Score = highScore[counter].Score + num;
                    }
                     else
                        highScore.Add(new Player(data[0], num));
                    
                }
                Sort(highScore);
            }

            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                textReader.Close();
            }
        }

        private void PrintHighScore()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i + 1 + ". " + highScore[i].Name + " " + highScore[i].Score);
            }
        }

        private bool NameCheck(int num)
        {
            for (int i = 0; i < num; i++)
            {
                if (players[num].Name == players[i].Name)
                {
                    return false;
                }
            }
            return true;
        }

        private void Reset()
        {
            deck = new Deck();
            dealer.ClearHand();
            foreach (var v in players)
            {
                v.Status = true;
                v.ClearHand();
                v.Score = 0;
            }

        }

        private void CheckPoints()
        {
            foreach (var v in players)
            {
                if (v.ValueOfCards == 21)
                    v.Score = 10;
                else if (v.ValueOfCards > 21)
                    v.Score = -3;
            }
            if (dealer.ValueOfCards > 21)
            {
                foreach (var v in players)
                {
                    if (v.ValueOfCards < 21)
                        v.Score = 2;
                }
            }
            else if (dealer.ValueOfCards < 21)
            {
                foreach (var v in players)
                {
                    if (v.ValueOfCards < 21 && v.ValueOfCards > dealer.ValueOfCards)
                        v.Score = 1;
                    else if (v.ValueOfCards < dealer.ValueOfCards)
                        v.Score = -1;
                }
            }
            else
            {
                foreach (var v in players)
                {
                    if (v.ValueOfCards < 21)
                        v.Score = v.Score - 2;
                }
            }
            foreach (var v in players)
            {
                Console.WriteLine(v.Name + " score: " + v.Score);
            }
            Console.WriteLine();
        }

        public void PlayTheGame()
        {
            Reset();
            dealer.TakeCard(deck.GetCard());
            foreach (var v in players)
            {
                v.TakeCard(deck.GetCard());
            }

            foreach (var v in players)
            {
                if (v.Status)
                {
                    Console.WriteLine();
                    Console.WriteLine(v.Name + " turn.");
                    v.PrintHand();
                    while (v.Status && v.Decision())
                    {
                        v.TakeCard(deck.GetCard());
                        v.PrintHand();
                        if (v.ValueOfCards > 20)
                        {
                            v.Status = false;
                        }
                    }
                }
            }
            while (dealer.ValueOfCards < 16)
            {
                dealer.TakeCard(deck.GetCard());
            }
            dealer.PrintHand();
            CheckPoints();
            LoadScores(@"E:\HighScore.txt");
            Save(@"E:\HighScore.txt");
            PrintHighScore();
            PlayTheGame();
        }
    }
}
