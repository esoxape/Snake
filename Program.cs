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
        public static Snake[,] boardBoom = new Snake[20, 30];
        public static int direction = 1; //0=north-up, 1=east-right, 2=south-down, 3=west-left
        public static int score = 0;
        public static int highScore = 0;
        public static string highScoreName = "";
        public static string playerName = "";
        public static int snakeSize = 0;
        public static int Speed = 10; //speed of snake movement (lower to increase speed)
        public static ConsoleKeyInfo keyPress = new ConsoleKeyInfo();
        public static bool active = true;
        public static bool activePlay = true;
        public static int lastDirection = 0;
        public static bool shoot = false;
        public static Timer timer;
        public static int moveCounter = 0;
        public static List<Bullet> activeBullets = new List<Bullet>();
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
        public class Bullet
        {
            public int direction;
            public int i;
            public int j;
            public Bullet(int direction1, int i1, int j1)
            {
                direction = direction1;
                i = i1;
                j = j1;
            }
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
            direction = 2;
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
                    if (boardBoom[i, j] == Snake.Explosion1)
                    {
                        Console.Write("#", Console.ForegroundColor = ConsoleColor.Red);
                        boardBoom[i, j] = Snake.Explosion2;
                        if (i + 1 < board.GetLength(0))
                        {
                            boardBoom[i + 1, j] = Snake.Explosion2;
                        }
                        if (i + 1 < board.GetLength(0) && j + 1 < board.GetLength(1))
                        {
                            boardBoom[i + 1, j + 1] = Snake.Explosion2;
                        }
                        if (j + 1 < board.GetLength(1))
                        {
                            boardBoom[i, j + 1] = Snake.Explosion2;
                        }
                        if (j - 1 > -1)
                        {
                            boardBoom[i, j - 1] = Snake.Explosion2;
                        }
                        if (i - 1 > -1)
                        {
                            boardBoom[i - 1, j] = Snake.Explosion2;
                        }

                    }
                    else if (boardBoom[i, j] == Snake.Explosion2)
                    {
                        Console.Write("#", Console.ForegroundColor = ConsoleColor.Yellow);
                        boardBoom[i, j] = Snake.Empty;
                    }
                    else if (board[i, j] == Snake.Empty)
                    {
                        Console.Write(' ');
                    }
                    else if (board[i, j] == Snake.Wall)
                    {
                        Console.Write("@", Console.ForegroundColor = ConsoleColor.Magenta);
                    }
                    else if (board[i, j] == Snake.Head)
                    {
                        Console.Write("O", Console.ForegroundColor = ConsoleColor.Green);
                    }
                    else if (board[i, j] == Snake.Body)
                    {
                        Console.Write("o", Console.ForegroundColor = ConsoleColor.DarkGreen);
                    }
                    else if (board[i, j] == Snake.Fruit)
                    {
                        Console.Write("*", Console.ForegroundColor = ConsoleColor.Blue);
                    }
                    if (board[i, j] == Snake.Monster)
                    {
                        Console.Write("M", Console.ForegroundColor = ConsoleColor.Red);
                    }
                    else if (board[i, j] == Snake.Shott)
                    {
                        Console.Write("¤", Console.ForegroundColor = ConsoleColor.Yellow);
                    }
                }
                Console.WriteLine();
            }
        }
        public static void BodyAdd()
        {
            Position M = new Position(Snake_Head_Position.i, Snake_Head_Position.j);
            mySnake.positions.Add(M);
            if (mySnake.positions.Count == 1) mySnake.positions.Add(M);
            score = score + 10;
        }
        public static void BodyMove()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == Snake.Body) board[i, j] = Snake.Empty;
                }
            }

            for (int i = 0; i < mySnake.positions.Count; i++)
            {
                if (board[mySnake.positions[i].i, mySnake.positions[i].j] != Snake.Head) board[mySnake.positions[i].i, mySnake.positions[i].j] = Snake.Body;
            }

            for (int i = mySnake.positions.Count - 1; i > -1; i--)
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
            Bullet M = new Bullet(direction, Snake_Head_Position.i, Snake_Head_Position.j);
            activeBullets.Add(M);
        }
        public static void Explosion(int remove)
        {
            if (board[activeBullets[remove].i, activeBullets[remove].j] == Snake.Monster)
            {
                boardBoom[activeBullets[remove].i, activeBullets[remove].j] = Snake.Explosion1;
                board[activeBullets[remove].i, activeBullets[remove].j] = Snake.Empty;
                score = score + 1;
            }
            activeBullets.RemoveAt(remove);
        }
        public static void BulletMove()
        {
            for (int i = 0; i < activeBullets.Count; i++)
            {
                if (activeBullets[i].direction == 0)
                {
                    if (board[activeBullets[i].i, activeBullets[i].j] == Snake.Shott) board[activeBullets[i].i, activeBullets[i].j] = Snake.Empty;
                    activeBullets[i].i = activeBullets[i].i - 1;
                    if (board[activeBullets[i].i, activeBullets[i].j] != Snake.Empty && board[activeBullets[i].i, activeBullets[i].j] != Snake.Head)
                    {
                        Explosion(i);
                        break;
                    }
                    else board[activeBullets[i].i, activeBullets[i].j] = Snake.Shott;
                }
                if (activeBullets[i].direction == 1)
                {
                    if (board[activeBullets[i].i, activeBullets[i].j] == Snake.Shott) board[activeBullets[i].i, activeBullets[i].j] = Snake.Empty;
                    activeBullets[i].j = activeBullets[i].j + 1;
                    if (board[activeBullets[i].i, activeBullets[i].j] != Snake.Empty && board[activeBullets[i].i, activeBullets[i].j] != Snake.Head)
                    {
                        Explosion(i);
                        break;
                    }
                    else board[activeBullets[i].i, activeBullets[i].j] = Snake.Shott;
                }
                if (activeBullets[i].direction == 2)
                {
                    if (board[activeBullets[i].i, activeBullets[i].j] == Snake.Shott) board[activeBullets[i].i, activeBullets[i].j] = Snake.Empty;
                    activeBullets[i].i = activeBullets[i].i + 1;
                    if (board[activeBullets[i].i, activeBullets[i].j] != Snake.Empty && board[activeBullets[i].i, activeBullets[i].j] != Snake.Head)
                    {
                        Explosion(i);
                        break;
                    }
                    else board[activeBullets[i].i, activeBullets[i].j] = Snake.Shott;
                }
                if (activeBullets[i].direction == 3)
                {
                    if (board[activeBullets[i].i, activeBullets[i].j] == Snake.Shott) board[activeBullets[i].i, activeBullets[i].j] = Snake.Empty;
                    activeBullets[i].j = activeBullets[i].j - 1;
                    if (board[activeBullets[i].i, activeBullets[i].j] != Snake.Empty && board[activeBullets[i].i, activeBullets[i].j] != Snake.Head)
                    {
                        Explosion(i);
                        break;
                    }
                    else board[activeBullets[i].i, activeBullets[i].j] = Snake.Shott;
                }
            }
        }
        public static void Start_thread() //auto movement implement using thread method
        {
            while (true)
            {                
                moveCounter = moveCounter + 1;
                lastDirection = lastDirection - 1;
                if (shoot == true)
                {
                    shoot = false;
                    Shoot();
                }
                if (direction == snake_direction.Up && moveCounter == 3)
                {
                    moveCounter = 0;
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
                        else if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Body ||
                                board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Monster)
                        {
                            Score();
                            break;
                        }
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        Fruit();
                        BodyMove();
                    }
                }
                else if (direction == snake_direction.Right && moveCounter == 3)
                {
                    moveCounter = 0;
                    // goto right
                    if (Snake_Head_Position.j == board.GetLength(1) - 2)
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
                        else if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Body ||
                                board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Monster)
                        {
                            Score();
                            break;
                        }
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        Fruit();
                        BodyMove();
                    }
                }
                else if (direction == snake_direction.Down && moveCounter == 3)
                {
                    moveCounter = 0;
                    // goto down
                    if (Snake_Head_Position.i == board.GetLength(0) - 2)
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
                        else if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Body ||
                                board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Monster)
                        {
                            Score();
                            break;
                        }
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        Fruit();
                        BodyMove();
                    }
                }
                else if (direction == snake_direction.Left && moveCounter == 3)
                {
                    moveCounter = 0;
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
                        else if (board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Body ||
                                 board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Monster)
                        {
                            Score();
                            break;
                        }
                        board[Snake_Head_Position.i, Snake_Head_Position.j] = Snake.Head;
                        Fruit();
                        BodyMove();
                    }
                }
                BulletMove();
                DrawBoard();
                Thread.Sleep(Speed * 4); //apply speed
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
            Console.WriteLine("Press M to change speed");
            Console.WriteLine("Press Esc to quit");
            if (Speed == 10)
            {
                Console.WriteLine("Speed: Fast");
            }
            else if (Speed == 30)
            {
                Console.WriteLine("Speed: Medium");
            }
            else if (Speed == 50)
            {
                Console.WriteLine("Speed: Slow");
            }
            keyPress = Console.ReadKey(true);

            if (keyPress.Key == ConsoleKey.M)
            {
                ChooseSpeed();
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

        private static void ChooseSpeed()
        {
            if (Speed == 10)
            {
                Speed = 30;
            }
            else if (Speed == 30)
            {
                Speed = 50;
            }
            else if (Speed == 50)
            {
                Speed = 10;
            }
            StartMenu();
        }

        static void PlayerName()
        {
            Console.Clear();
            Console.Write("Your name: ");
            playerName = Console.ReadLine();
            active = true;
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
                int rng = random.Next(counter);
                board[array[rng, 0], array[rng, 1]] = Snake.Fruit;
            }
        }
        static void Monster()
        {
            while (true)
            {
                Random random = new Random();
                int[,] array = new int[10000, 2];
                int counter = 0;
                int countMonster = 0;
                bool check = false;
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (var j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == Snake.Monster)
                        {
                            countMonster++;
                            if (countMonster == 6) check = true;
                        }
                        if (board[i, j] == Snake.Empty)
                        {
                            array[counter, 0] = i;
                            array[counter, 1] = j;
                            counter++;
                        }
                    }
                }
                if (check == false)
                {
                    int rng = random.Next(counter);
                    board[array[rng, 0], array[rng, 1]] = Snake.Monster;
                }
                Thread.Sleep(random.Next(5000, 10000));
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            GameMenu myGame = new GameMenu();
            myGame.Start();
            ResetBoard();
            do
            {
                DrawBoard();
                Thread th = new Thread(new ThreadStart(Start_thread)); //implement thread
                th.Start();
                Thread th2 = new Thread(new ThreadStart(Monster));
                th2.Start();
                while (active == true)
                {              

                    var key = Console.ReadKey().Key; // Read Key From Console
                                                     // Getting,Implementing arrow movements to work UP,DOWN,LEFT,RIGHT + stop from going oppisite way !=
                    if (key == ConsoleKey.UpArrow)
                    {
                        if (direction != snake_direction.Down && lastDirection < 1)
                        {
                            direction = snake_direction.Up;
                            lastDirection = 3;
                        }
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        if (direction != snake_direction.Up && lastDirection < 1)
                        {
                            direction = snake_direction.Down;
                            lastDirection = 3;
                        }
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        if (direction != snake_direction.Right && lastDirection < 1)
                        {
                            direction = snake_direction.Left;
                            lastDirection = 3;
                        }
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        if (direction != snake_direction.Left && lastDirection < 1)
                        {
                            direction = snake_direction.Right;
                            lastDirection = 3;
                        }
                    }
                    else if (key == ConsoleKey.Spacebar)
                    {
                        shoot = true;
                    }
                }
                myGame.Start();
            } while (activePlay == true);
        }
        class GameMenu
        {
            public void Start()
            {
                Console.Title = "Snake Game - Group 3";
                RunMainMenu();

            }
            private void RunMainMenu()
            {
                string prompt = "\r\n███████╗███╗   ██╗ █████╗ ██╗  ██╗███████╗     ██████╗  █████╗ ███╗   ███╗███████╗\r\n██╔════╝████╗  ██║██╔══██╗██║ ██╔╝██╔════╝    ██╔════╝ ██╔══██╗████╗ ████║██╔════╝\r\n███████╗██╔██╗ ██║███████║█████╔╝ █████╗      ██║  ███╗███████║██╔████╔██║█████╗  \r\n╚════██║██║╚██╗██║██╔══██║██╔═██╗ ██╔══╝      ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  \r\n███████║██║ ╚████║██║  ██║██║  ██╗███████╗    ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗\r\n╚══════╝╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝\r\n                                                                                  \r\n";
                string[] options = { "Play", "Highscore", "Help", "Exit" };
                Menu mainMenu = new Menu(prompt, options);
                int selectedIndex = mainMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        PlayerName();
                        break;
                    case 1:
                        HighScore();
                        break;
                    case 2:
                        Help();
                        break;
                    case 3:
                        ExitGame();
                        break;
                }
            }
            private void HighScore()
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
                    Start();
                }
                if (keyPress.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
            private void Help()
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"_______    _______  _______________  _______         ___________\n" +
                                  $"|     |    |     |  |             |  |     |         |   ____   |\n" +
                                  $"|     |____|     |  |    _________|  |     |         |   |__|   |\n" +
                                  $"|                |  |    |_______    |     |         |     _____|\n" +
                                  $"|      ____      |  |    ________|   |     |_______  |     |\n" +
                                  $"|     |    |     |  |    |_________  |            |  |     |\n" +
                                  $"|_____|    |_____|  |_____________|  |____________|  |_____|\n");
                Console.WriteLine($"Controls:");
                Console.WriteLine("Up-Arrow:    Move up");
                Console.WriteLine("Down-Arrow:  Move down");
                Console.WriteLine("Right-Arrow: Move right");
                Console.WriteLine("Left-Arrow:  Move left");
                Console.WriteLine("Spacebar:    Shoot!");
                Console.WriteLine();
                Console.WriteLine("Press B for back to main menu");
                Console.WriteLine("Press Esc to quit");
                keyPress = Console.ReadKey(true);
                if (keyPress.Key == ConsoleKey.B)
                {
                    Start();
                }
                if (keyPress.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
            private void ExitGame()
            {
                Console.WriteLine("Press any key to exit....");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
        }
        class Menu
        {
            private int SelectedIndex;
            private string[] Options;
            private string Prompt;
            public Menu(string prompt, string[] options)
            {
                Prompt = prompt;
                Options = options;
                SelectedIndex = 0;
            }
            private void DisplayOptions()
            {
                Console.WriteLine(Prompt);
                for (int i = 0; i < Options.Length; i++)
                {
                    string currentOption = Options[i];
                    string prefix;

                    if (i == SelectedIndex)
                    {
                        prefix = "*";
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        prefix = " ";
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine($"{prefix} << {currentOption} >>");
                }
                Console.ResetColor();
            }
            public int Run()
            {
                ConsoleKey keyPressed;
                do
                {
                    Console.Clear();
                    DisplayOptions();
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    keyPressed = keyInfo.Key;
                    if (keyPressed == ConsoleKey.UpArrow)
                    {
                        SelectedIndex--;
                        if (SelectedIndex == -1)
                        {
                            SelectedIndex = Options.Length - 1;
                        }
                    }
                    else if (keyPressed == ConsoleKey.DownArrow)
                    {
                        SelectedIndex++;
                        if (SelectedIndex == Options.Length)
                        {
                            SelectedIndex = 0;
                        }
                    }
                } while (keyPressed != ConsoleKey.Enter);

                return SelectedIndex;
            }
        }
    }
}