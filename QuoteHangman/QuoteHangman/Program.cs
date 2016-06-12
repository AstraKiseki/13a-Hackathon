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
            static bool gameOver = true;
            static List<string> usedLetters = new List<string>();
        // Creating the 'stats' needed.  Note that the gameOver boolean is reversed.  Maybe I should change it to gameOn?

        static void ClearData()
        {
            lives = 9;
            QuoteAnswer = " ";
            DisplayWord = " ";
            gameOver = true;
            usedLetters.Clear();
        }
        // To wipe the slate clean!

        static void Main(string[] args)
        {
            GameStart();

            while (lives >= 0)
            {
                while (gameOver == true)
                {
                    PlayRound();
                    if (DisplayWord.Equals(QuoteAnswer, StringComparison.OrdinalIgnoreCase))
                    {
                        gameOver = false;
                        if (lives > 0)
                        {
                            Console.WriteLine(QuoteAnswer);
                            Console.WriteLine("Congratulations, you win!");
                            Console.WriteLine("Would you like to play again? (Y/N)");
                            string playAgain = Console.ReadLine();
                            playAgain = playAgain.ToUpper();
                            bool playAgainChoice = false;

                            while (playAgainChoice == false)
                            {
                                switch (playAgain)
                                {
                                    case "Y":
                                        playAgainChoice = true;
                                        Console.Clear();
                                        ClearData();
                                        GameStart();
                                        break;
                                    case "N":
                                        playAgainChoice = true;
                                        Console.WriteLine("Cool!  Thanks for playing!");
                                        Console.ReadLine();
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("Sorry, that's not a valid answer.  Please try again.");
                                        playAgain = Console.ReadLine();
                                        playAgain = playAgain.ToUpper();
                                        break;
                                }
                            }
                        }
                    }
                    if (lives == 0)
                    {
                        Console.WriteLine("Oh, you are out of lives!  That's game over!");
                        Console.WriteLine("The answer was {0}", QuoteAnswer);
                        Console.ReadLine();
                        Console.Clear();
                        ClearData();
                        GameStart();
                    }
                }
            }
        }

        public static void GameStart()
        {
            Console.WriteLine("Hi!  Welcome to Hangman!");
            Console.WriteLine("This game uses currently uses one of two options to choose a quote to guess!  Would you like the quote to be from:");
            Console.WriteLine("1. Movies");
            Console.WriteLine("2. Someone famous");

            string choice = Console.ReadLine();
            bool pickedChoice = false;

            while (pickedChoice == false)
            {
                switch (choice)
                {
                    case "1":
                        /// Movies game
                        Console.WriteLine("You've chosen a quote from a movie!  One moment, please!");
                        QuoteAnswer = QuoteRetriever.GetMovieQuote().quote.ToLower();
                        DisplayWord = maskString(QuoteAnswer);
                        pickedChoice = true;
                        break;
                    case "2":
                        /// Famous
                        Console.WriteLine("You've chosen someone famous!  One moment, please!");
                        QuoteAnswer = QuoteRetriever.GetFamousQuote().quote.ToLower();
                        DisplayWord = maskString(QuoteAnswer);
                        pickedChoice = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid choice.  Please select 1 or 2.");
                        choice = Console.ReadLine();
                        break;
                }
            }
            return;
        }

        static string maskString(string input)
        {
            string[] words = QuoteAnswer.Split(' ');
            string DisplayWord = "";

            for (int i = 0; i < words.Length; i++)
            {
                string currentWord = words[i]; // I need to figure out how to auto do the thing for things besides letters.
                for (int j = 0; j < currentWord.Length; j++)
                {
                    DisplayWord += "-";
                }
                DisplayWord += " ";
            }
            DisplayWord = DisplayWord.TrimEnd();  // Or else the two won't look identical at all, due to that one extra space.
            return DisplayWord;
        }

        public static bool PlayRound()
        {
            Console.WriteLine(DisplayWord);
            Console.WriteLine("Enter a letter:");
            string letter = Console.ReadLine();
            string readletter = letter.ToLower();  // Should I use Ordinal here too?

            if (IsThereA(letter))
            {

            }
            else
            {
                lives--;
                Console.WriteLine("Oh no!  You've lost a life.  You have {0} left.", lives);
            }
            return false;
        }

        public static bool IsThereA(string guessLetter) // Credit to DRapp for this
        {
            if (usedLetters.Contains(guessLetter) == false)
            {
                int maxlength = QuoteAnswer.Length;
                bool anyMatch = false;
                for (int i = 0; i < QuoteAnswer.Length; i++)
                {
                    if (QuoteAnswer.Substring(i, 1).Equals(guessLetter))
                    {
                        anyMatch = true;
                        DisplayWord = DisplayWord.Substring(0, i) + guessLetter + DisplayWord.Substring(i + 1);
                    }
                }
                usedLetters.Add(guessLetter);
                return anyMatch;
            }
            if (usedLetters.Contains(guessLetter))
            {
                Console.WriteLine("Oh!  You have already used:");
                usedLetters.ForEach(item => Console.Write(item + " "));
                Console.WriteLine(" ");
            }
            return true;
        }
    }
}
