using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteHangman.Core.Services;

namespace QuoteHangman
{
    public class Program
    {
        static int lives = 9;
        static string QuoteAnswer = " ";
        static string DisplayWord = " ";

        static void Main(string[] args)
        {
            
            Console.WriteLine("Hi!  Welcome to Hangman!");
            Console.WriteLine("This game uses currently uses one of two options to choose a quote to guess!  Would you like the quote to be from:");
            Console.WriteLine("1. Movies");
            Console.WriteLine("2. Someone famous");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    /// Movies game
                    Console.WriteLine("You've chosen a quote from a movie!  One moment, please!");
                    QuoteAnswer = QuoteRetriever.GetMovieQuote().quote;
                    break;
                case "2":
                    /// Famous
                    Console.WriteLine("You've chosen someone famous!  One moment, please!");
                    QuoteAnswer = QuoteRetriever.GetFamousQuote().quote;
                    break;
                default:
                    Console.WriteLine("Not a valid choice.");
                    break;
            }



            while (lives > 0)
            {
                bool gameOver = PlayRound();
                if (gameOver == true)
                {
                    break;
                }
            }
            if (lives > 0)
            {
                Console.WriteLine("Congratulations, you win!");
            }
            else
            {
                Console.WriteLine("Game over!");
                Console.WriteLine("The answer was {0}", QuoteAnswer);
            }
            }

        public static bool PlayRound()
        {
            Console.WriteLine("Enter a letter");
            string letter = Console.ReadLine();

            if (IsThereA(letter))
            {
                if (QuoteAnswer.ToLower() == DisplayWord.ToLower())
                {
                    return true;
                }
            }
            else
            {
                lives--;
            }

            return false;
        }

        static string maskString(string input)
        {
            string[] words = QuoteAnswer.Split(' ');

            string DisplayWord = "";

            for (int i = 0; i < words.Length; i++)
            {
                string currentWord = words[i];

                for (int j = 0; j < currentWord.Length; j++)
                {
                    DisplayWord += "-";
                }

                DisplayWord += " ";
            }

            return DisplayWord;
        }
    



    public static bool IsThereA(string guessLetter) // Credit to DRapp for this
        {
            bool anyMatch = false;
            for (int i = 0; i < QuoteAnswer.Length; i++)
            {
                if (QuoteAnswer.Substring(i, 1).Equals(guessLetter))
                {
                    anyMatch = true;
                    DisplayWord = DisplayWord.Substring(0, i) + guessLetter + DisplayWord.Substring(i + 1);
                }
                if (anyMatch == false)
                {
                    lives--;
                }
            }
            return anyMatch;
        }


    }
}
