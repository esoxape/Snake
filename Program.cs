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
        public static Snake[,] board = new Snake[20, 30];
        public static int direction = 1; //0=north-up, 1=east-right, 2=south-down, 3=west-left
        public static int score = 0;
        public static int highScore = 0;
        public static string playerName = "";
        public static int snakeSize = 0;
        public static int Speed = 2; //speed of snake movement (lower to increase speed)
        public static ConsoleKeyInfo keyPress = new ConsoleKeyInfo();
        public static bool active;
        public enum Snake
        {
            Empty,       // 0
            Wall,        // 1
            Fruit,       // 2
            Body,        // 3
            Head         // 4
        }
        public static class snake_direction //implement directions
        {
            public static int Up = 0;
            public static int Right = 1;
            public static int Down = 2;
            public static int Left = 3;
        }
        public class Position //body implement (imorgon)
        {
            public int i { get; set; }
            public int j { get; set; }
        }
        public static class mySnake //body implement (imorgon)
        {
            public static List<Position> positions = new List<Position>();
            public static void Increase(int i, int j)
            {
            }
        }
        public static class Snake_Head_Position //implement i,j
        {
            public static int i = 0;
            public static int j = 0;
        }
        public static void ResetBoard()
        {
            snakeSize = 0;
            score = 0;
            direction = 6;
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (var j = 0; j < board.GetLength(1); j++)
                    {
                        if (i == 0 || i == board.GetLength(0) - 1 || j == 0 || j == board.GetLength(1) - 1) board[i, j] = Snake.Wall;
                        else if (i == 5 && j == 5)
                        {
                            board[i, j] = Snake.Head;
                            Snake_Head_Position.i = i;
                            Snake_Head_Position.j = j;
                        }
                        else if (i == 10 && j == 10) board[i, j] = Snake.Fruit;
                        else board[i, j] = Snake.Empty;
                    }
                }
            }
        }
        public static void DrawBoard()
        {
            Console.Clear();
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
                {
                    for (var j = 0; j < board.GetLength(1); j++)
                    {
                        if (i == 0 || i == board.GetLength(0) - 1 || j == 0 || j == board.GetLength(1) - 1) board[i, j] = Snake.Wall;
                        else if (i == 5 && j == 5) board[i, j] = Snake.Head;
                        else if (i == 10 && j == 10) board[i, j] = Snake.Fruit;
                        else board[i, j] = Snake.Empty;

                    }
                }
                Console.WriteLine();
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
            else if (keyPress.Key == ConsoleKey.H)
            {
                Help();
            }
            else if (keyPress.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else
            {
                active = true;
            }
            Console.WriteLine();
        }
        static void HighScore()
        {
            Console.WriteLine();
            Console.WriteLine("High Score List:");
            Console.WriteLine();

            Console.WriteLine("Press B for back to main menu");
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
            Console.WriteLine("Press B for back to main menu");
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
        static void Fruit()
        {
            Random random = new Random();
            bool check = true;
            int[,] array = new int[10000, 2];
            int counter = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == Snake.Fruit) check = false;
                    if (board[i, j] == Snake.Empty)
                    {
                        array[counter, 0] = i;
                        array[counter, 1] = j;
                        counter++;
                    }
                }
            }

            if (check == true)
            {
                score += 10;
                int rng = random.Next(counter);
                board[array[rng, 0], array[rng, 1]] = Snake.Fruit;
            }

        }
        static void Main(string[] args)
        {
            StartMenu();
            if (active = true)
            {
                Console.Clear();
                ResetBoard();
                DrawBoard();
            }
        }
    }
}