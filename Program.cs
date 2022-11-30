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
        public static string highScoreName = "";
        public static string playerName = "";
        public static int snakeSize = 0;
        public static int Speed = 10; //speed of snake movement (lower to increase speed)
        public static ConsoleKeyInfo keyPress = new ConsoleKeyInfo();
        public static bool active=true;
        public static bool activePlay = true;
        public static int lastDirection = 0;
        public static bool shoot = false;       
        public enum Snake
        {
            Empty,       // 0
            Wall,        // 1
            Fruit,       // 2
            Body,        // 3
            Head,        // 4
            Monster,     // 5
            Shott,       // 6
            Explosion1,  // 7
            Explosion2   // 8
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
            public int i;
            public int j;
            public Position(int i, int j)
            {
                this.i = i;
                this.j = j;
            }
        }
        public static class mySnake //body implement (imorgon)
        {
            public static List<Position> positions = new List<Position>();
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
            direction = 1;
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
            Console.ResetColor();
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
        public static void BodyAdd()
        {
            Position M = new Position(Snake_Head_Position.i, Snake_Head_Position.j);
            mySnake.positions.Add(M);
            if(mySnake.positions.Count == 1) mySnake.positions.Add(M);
            score = score+10;
        }
        public static void BodyMove()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i,j] == Snake.Body)board[i,j] = Snake.Empty;
                }
            }                   

            for (int i = 0; i < mySnake.positions.Count; i++)
            {
                if (board[mySnake.positions[i].i, mySnake.positions[i].j] != Snake.Head) board[mySnake.positions[i].i, mySnake.positions[i].j] = Snake.Body;
            }

            for (int i = mySnake.positions.Count-1; i > -1; i--)
            {
                if (i > 1)
                {
                    mySnake.positions[i].i = mySnake.positions[i - 1].i;
                    mySnake.positions[i].j = mySnake.positions[i - 1].j;
                }
                else
                {
                    mySnake.positions[i].i = Snake_Head_Position.i;
                    mySnake.positions[i].j = Snake_Head_Position.j;
                }
            }
        }
        public static void Score()
        {
            active = false;
            if (score > highScore)
            {
                highScore = score;
                highScoreName = playerName;
            }
            ResetBoard();
            mySnake.positions.Clear();
            Console.WriteLine("Du dog!!!! Tryck på valfri knapp för att komma vidare");
        }
        public static void Shoot()
        {

        }
        public static void Start_thread() //auto movement implement using thread method
        {            
            while (true)
            {
                shootCounter = shootCounter + 1;
                lastDirection = 0;
                if(shoot==true)
                {
                    shoot = false;
                    Shoot();
                }
                if (direction == snake_direction.Up && shootCounter==3)
                {
                    shootCounter = 0;
                    // goto UP
                    if (Snake_Head_Position.i == 1)
                    {
                        Score();
                        break;
                    }
                    else
                    {
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Empty;
                        Snake_Head_Position.i--;
                        if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Fruit)
                        {
                            BodyAdd();
                        }
                        else if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Body)
                        {
                            Score();
                            break;
                        }
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;                        
                        Fruit();
                        BodyMove();                        
                    }
                }
                else if (direction == snake_direction.Right && shootCounter == 3)
                {
                    shootCounter = 0;
                    // goto right
                    if (Snake_Head_Position.j == board.GetLength(1)-2)
                    {
                        Score();
                        break;
                    }
                    else
                    {
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Empty;
                        Snake_Head_Position.j++;
                        if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Fruit)
                        {
                            BodyAdd();
                        }
                        else if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Body)
                        {
                            Score();
                            break;
                        }
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        Fruit();
                        BodyMove();                        
                    }
                }
                else if (direction == snake_direction.Down && shootCounter == 3)
                {
                    shootCounter = 0;
                    // goto down
                    if (Snake_Head_Position.i == board.GetLength(0)-2)
                    {
                        Score();
                        break;
                    }
                    else
                    {
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Empty;
                        Snake_Head_Position.i++;
                        if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Fruit)
                        {
                            BodyAdd();
                        }
                        else if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Body)
                        {
                            Score();
                            break;
                        }
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        Fruit();
                        BodyMove();                        
                    }
                }
                else if (direction == snake_direction.Left && shootCounter == 3)
                {
                    shootCounter = 0;
                    // goto left
                    if (Snake_Head_Position.j == 1)
                    {
                        Score();
                        break;
                    }
                    else
                    {
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Empty;
                        Snake_Head_Position.j--;
                        if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Fruit)
                        {
                            BodyAdd();
                        }
                        else if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Body)
                        {
                            Score();
                            break;
                        }
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        Fruit();
                        BodyMove();                        
                    }
                }
                DrawBoard();
                Thread.Sleep(Speed * 5); //apply speed
            }
        }
        static void StartMenu()
        {
            start:
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("--------------Welcome to snake---------------");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Press P to play");
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
            else if (keyPress.Key == ConsoleKey.P)
            {
                PlayerName();
            }
            else goto start;
            Console.WriteLine();
        }
        static void PlayerName()
        {
            Console.Clear();
            Console.Write("Your name: ");
            playerName = Console.ReadLine();
            active = true;
        }
        static void HighScore()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("High Score List:");
            Console.WriteLine($"{highScoreName} {highScore}");
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
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Help Page:");
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
            int[,] array = new int[10000,2];
            int counter = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i,j]==Snake.Fruit) check = false;
                    if(board[i,j]==Snake.Empty)
                    {
                        array[counter, 0] = i;                        
                        array[counter, 1] = j;
                        counter++;
                    }
                }
            }

            if (check == true)
            {
                int rng = random.Next(counter);
                board[array[rng, 0], array[rng, 1]] = Snake.Fruit;
            }            
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            StartMenu();
            ResetBoard();
            do
            {                
                DrawBoard();
                Thread th = new Thread(new ThreadStart(Start_thread)); //implement thread
                th.Start();                
                while (active==true)
                {
                    var key = Console.ReadKey().Key; // Read Key From Console
                                                     // Getting,Implementing arrow movements to work UP,DOWN,LEFT,RIGHT + stop from going oppisite way !=
                    if (key == ConsoleKey.UpArrow)
                    {
                        if (direction != snake_direction.Down && lastDirection==0)
                        {
                            direction = snake_direction.Up;
                            lastDirection = 1;
                        }
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        if (direction != snake_direction.Up && lastDirection == 0)
                        {
                            direction = snake_direction.Down;
                            lastDirection = 1;
                        }
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        if (direction != snake_direction.Right && lastDirection == 0)
                        {
                            direction = snake_direction.Left;
                            lastDirection = 1;
                        }
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        if (direction != snake_direction.Left && lastDirection == 0)
                        {
                            direction = snake_direction.Right;
                            lastDirection = 1;
                        }
                    }
                    else if (key == ConsoleKey.Spacebar)
                    {
                        shoot = true;
                    }
                }
                StartMenu();
            } while (activePlay == true);
        }
    }
}