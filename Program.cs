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
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (var j = 0; j < board.GetLength(1); j++)
                    {
                        if (i == 0 || i == board.GetLength(0) - 1 || j == 0 || j == board.GetLength(1) - 1) board[i, j] = Snake.Wall;
                        else if (i == 5 && j == 5) board[i, j] = Snake.Head;
                        else if (i == 10 && j == 10) board[i, j] = Snake.Fruit;
                        else board[i, j] = Snake.Empty;
                    }
                }
            }
        }
        public static void DrawBoard()
        {
            Console.WriteLine($"Player name: {playerName}");
            Console.WriteLine($"score: {score}");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == Snake.Empty)
                    {
                        Console.Write(' ');
                    }
                    if (board[i, j] == Snake.Wall)
                    {
                        Console.Write("@", Console.ForegroundColor = ConsoleColor.Magenta);
                    }
                    if (board[i, j] == Snake.Head)
                    {
                        Console.Write("O", Console.ForegroundColor = ConsoleColor.Green);
                    }
                    if (board[i, j] == Snake.Body)
                    {
                        Console.Write("o", Console.ForegroundColor = ConsoleColor.DarkGreen);
                    }
                    if (board[i, j] == Snake.Fruit)
                    {
                        Console.Write("*", Console.ForegroundColor = ConsoleColor.Red);
                    }
                }
                Console.WriteLine();
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
        }
    }
}