using System;

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
                    }
                }
                Console.WriteLine();
            }
        }
        public static void start_thread() //auto movement implement using thread method
        {
            while (true)
            {
                if (direction == snake_direction.Up)
                {
                    // goto UP
                    if (Snake_Head_Position.i == 1)
                    {                        
                        break;
                    }
                    else
                    {
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Empty;
                        Snake_Head_Position.i--;
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        DrawBoard();
                    }
                }
                else if (direction == snake_direction.Right)
                {
                    // goto right
                    if (Snake_Head_Position.j == board.GetLength(1)-2)
                    {                        
                        break;
                    }
                    else
                    {
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Empty;
                        Snake_Head_Position.j++;
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        DrawBoard();
                    }
                }
                else if (direction == snake_direction.Down)
                {
                    // goto down
                    if (Snake_Head_Position.i == board.GetLength(0)-2)
                    {                        
                        break;
                    }
                    else
                    {
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Empty;
                        Snake_Head_Position.i++;
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        DrawBoard();
                    }
                }
                else if (direction == snake_direction.Left)
                {
                    // goto left
                    if (Snake_Head_Position.j == 1)
                    {                        
                        break;
                    }
                    else
                    {
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Empty;
                        Snake_Head_Position.j--;
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        DrawBoard();
                    }
                }
                Thread.Sleep(Speed * 250); //apply speed
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
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ResetBoard();
            DrawBoard();           
            Thread th = new Thread(new ThreadStart(start_thread)); //implement thread
            th.Start();
            while (true)
            {
                var key = Console.ReadKey().Key; // Read Key From Console
                // Getting,Implementing arrow movements to work UP,DOWN,LEFT,RIGHT + stop from going oppisite way !=
                if (key == ConsoleKey.UpArrow)
                {                    
                    if (direction != snake_direction.Down)
                    {
                        direction = snake_direction.Up;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {                    
                    if (direction != snake_direction.Up)
                    {
                        direction = snake_direction.Down;
                    }
                }
                else if (key == ConsoleKey.LeftArrow)
                {                    
                    if (direction != snake_direction.Right)
                    {
                        direction = snake_direction.Left;
                    }
                }
                else if (key == ConsoleKey.RightArrow)
                {                    
                    if (direction != snake_direction.Left)
                    {
                        direction = snake_direction.Right;
                    }
                }
                else
                {
                }
            }
            Console.WriteLine("Thanks For Playing");
            Console.ReadLine();
        }
    }
}