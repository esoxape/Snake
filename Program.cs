using System.Runtime.InteropServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Program
    {
        public static Snake[,] board = new Snake[50, 50];
        public static int direction = 0; //0=north, 1=east, 2=south, 3=west
        public static int score = 0;
        public static int highScore = 0;
        public static string playerName = "";
        public static int snakeSize = 0;
        public static ConsoleKeyInfo keyPress = new ConsoleKeyInfo();

        public enum Snake
        {
            Empty,       // 0
            Wall,        // 1
            Fruit,       // 2
            Body,        // 3
            Head         // 4
        }
        public static void ResetBoard()
        {
            snakeSize = 0;
            score = 0;
            direction = 0;
            ///loop to reset the board to starting position
            for (int i = 0; i < 51; i++)
            {
                for (var j = 0; j < 51; j++)
                {
                    if (i == 0 || i == 50 || j == 0 || j == 50) board[i, j] = Snake.Wall;
                    else if (i == 25 && j == 25) board[i, j] = Snake.Head;
                    else board[i, j] = Snake.Empty;
                }
            }
        }
        static void StartMenu()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("--------------Welcome to snake---------------");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to play");
            Console.WriteLine();
            Console.WriteLine("Press L to high score list");
            Console.WriteLine("Press H to help");
            Console.WriteLine("Press Esc to quit");

            keyPress = Console.ReadKey(true);
            if (keyPress.Key == ConsoleKey.L)
            {
                HighScore();
            }
            if (keyPress.Key == ConsoleKey.H)
            {
                Help();
            }
            if (keyPress.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            Console.WriteLine();
        }
        static void HighScore()
        {
            Console.WriteLine();
            Console.WriteLine("High Score List:");
            Console.WriteLine();

            Console.WriteLine("Press B for back to main manu");
            Console.WriteLine("Press Esc to quit");
            keyPress = Console.ReadKey(true);
            if (keyPress.Key == ConsoleKey.B)
            {
                StartMenu();
            }
            if (keyPress.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
        static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Hepl Page:");
            Console.WriteLine();
            Console.WriteLine("Press B for back to main manu");
            Console.WriteLine("Press Esc to quit");
            keyPress = Console.ReadKey(true);
            if (keyPress.Key == ConsoleKey.B)
            {
                StartMenu();
            }
            if (keyPress.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
        static void Main(string[] args)
        {
            StartMenu();
            ResetBoard();
            string choice = "";
            do
            {

            } while (choice != "quit");
        }
    }
}