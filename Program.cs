using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

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
        public static int Speed = 10; //speed of snake movement (lower to increase speed)
        public static ConsoleKeyInfo keyPress = new ConsoleKeyInfo();
        public static bool active = true;
        public static bool activePlay = true;
        public static int lastDirection = 0;
        public static bool shoot = false;
        public static Timer timer;
        public static int moveCounter = 0;
        public static List<Bullet> activeBullets = new List<Bullet>();
        public static int wallCheck = 0;
        public static Boss boss = new Boss();
        public enum Snake
        {
            Empty,                   // 0
            Wall,                 // 1
            Fruit,                 // 2
            Body,                 // 3
            Head,                // 4
            Monster,             // 5
            Shott,               // 6
            Explosion1,         // 7
            Explosion2,         // 8
            WallDestroyable,
            Boss
        }
        public class Boss
        {
            public int[,] location = { {18, 14},{18,15}, { 17, 14 }, { 17, 15 } };
            public int hp = 0;
            public void Move()
            {
                board[location[0, 0], location[0, 1]] = Snake.Empty;
                board[location[1, 0], location[1, 1]] = Snake.Empty;
                board[location[2, 0], location[2, 1]] = Snake.Empty;
                board[location[3, 0], location[3, 1]] = Snake.Empty;
                if (Snake_Head_Position.i - location[0,0]<0)
                {
                    for(int i=0; i<4; i++)
                    {
                        location[i,0]=location[i,0]-1; 
                    }

                }
                else if (Snake_Head_Position.i - location[0, 0]> 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        location[i, 0] = location[i, 0] + 1;
                    }

                }
                else if (Snake_Head_Position.j - location[1, 1] > 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        location[i, 1] = location[i, 1] + 1;
                    }
                }
                else if (Snake_Head_Position.j - location[1, 1] < 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        location[i, 1] = location[i, 1] - 1;
                    }
                }
                board[location[0, 0], location[0, 1]] = Snake.Boss;
                board[location[1, 0], location[1, 1]] = Snake.Boss;
                board[location[2, 0], location[2, 1]] = Snake.Boss;
                board[location[3, 0], location[3, 1]] = Snake.Boss;
            }
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
            score = 0;
            direction = 2;
            boss.hp = 0;
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
                        else board[i, j] = Snake.Empty;
                        if (i == board.GetLength(0) - 1 && j == 15) board[i, j] = Snake.WallDestroyable;
                    }
                }
            }
        }

        public static void LevelTwo()
        {
            boss.hp = 20;            
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (var j = 0; j < board.GetLength(1); j++)
                    {
                        if (i == 0 || i == board.GetLength(0) - 1 || j == 0 || j == board.GetLength(1) - 1) board[i, j] = Snake.Wall;
                        else if (i == 1 && j == 15)
                        {
                            board[i, j] = Snake.Head;
                            Snake_Head_Position.i = i;
                            Snake_Head_Position.j = j;
                        }
                        else if (boss.location[0, 0] == i && boss.location[0, 1] == j) board[i,j] = Snake.Boss;
                        else if (boss.location[1, 0] == i && boss.location[1, 1] == j) board[i, j] = Snake.Boss;
                        else if (boss.location[2, 0] == i && boss.location[2, 1] == j) board[i, j] = Snake.Boss;
                        else if (boss.location[3, 0] == i && boss.location[3, 1] == j) board[i, j] = Snake.Boss;
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
                    else if (board[i, j] == Snake.Monster)
                    {
                        Console.Write("M", Console.ForegroundColor = ConsoleColor.Red);
                    }
                    else if (board[i, j] == Snake.Shott)
                    {
                        Console.Write("¤", Console.ForegroundColor = ConsoleColor.Yellow);
                    }
                    else if (board[i, j] == Snake.Boss)
                    {
                        Console.Write("B", Console.ForegroundColor = ConsoleColor.DarkRed);
                    }
                    else if (board[i,j] == Snake.WallDestroyable)
                    {
                        if(wallCheck==0) Console.Write("@", Console.ForegroundColor = ConsoleColor.White);
                        if (wallCheck == 1) Console.Write("@", Console.ForegroundColor = ConsoleColor.Green);
                        if (wallCheck == 2) Console.Write("@", Console.ForegroundColor = ConsoleColor.DarkBlue);
                        if (wallCheck == 3) Console.Write("@", Console.ForegroundColor = ConsoleColor.Red);
                        if (wallCheck == 4) Console.Write("@", Console.ForegroundColor = ConsoleColor.Yellow);
                    }
                }
                Console.WriteLine();
            }
            if (boss.hp > 0)
            {
                Console.Write("Boss HP: ", Console.ForegroundColor = ConsoleColor.DarkRed);
                for (int i = 0; i < boss.hp; i++) Console.Write("|");
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
                String line = highScoreName + ";" + highScore;
                using StreamWriter file = new("scores.txt");
                file.WriteLine(line);
                file.Close();
            }
            ResetBoard();
            mySnake.positions.Clear();
            Console.WriteLine();
            Console.WriteLine("Du dog!!!! Tryck på valfri knapp för att komma vidare");
        }
        public static void Shoot()
        {
            Bullet M = new Bullet(direction, Snake_Head_Position.i, Snake_Head_Position.j);
            activeBullets.Add(M);
        }
        public static void Explosion(int remove)
        {
            boardBoom[activeBullets[remove].i, activeBullets[remove].j] = Snake.Explosion1;

            if (board[activeBullets[remove].i, activeBullets[remove].j] == Snake.Monster)
            {                
                board[activeBullets[remove].i, activeBullets[remove].j] = Snake.Empty;
                score = score + 1;
            }

            if (board[activeBullets[remove].i, activeBullets[remove].j] == Snake.WallDestroyable && wallCheck >3)
            {
                board[activeBullets[remove].i, activeBullets[remove].j] = Snake.Empty;
                wallCheck = 0;
            }
            else if (board[activeBullets[remove].i, activeBullets[remove].j] == Snake.WallDestroyable) wallCheck = wallCheck + 1;

            activeBullets.RemoveAt(remove);
        }
        public static void BulletMove()
        {
            for (int i = 0; i < activeBullets.Count; i++)
            {
                if (activeBullets[i].direction == 0)
                {
                    if (board[activeBullets[i].i, activeBullets[i].j] == Snake.Shott) board[activeBullets[i].i, activeBullets[i].j] = Snake.Empty;
                    if (activeBullets[i].i == 0)
                    {
                        activeBullets.RemoveAt(i);
                        break;
                    }
                    else activeBullets[i].i = activeBullets[i].i - 1;
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
                    if (activeBullets[i].j == board.GetLength(1) - 1)
                    {
                        activeBullets.RemoveAt(i);
                        break;
                    }
                    else activeBullets[i].j = activeBullets[i].j + 1;
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
                    if (activeBullets[i].i == board.GetLength(0) - 1)
                    {
                        activeBullets.RemoveAt(i);
                        break;
                    }
                    else activeBullets[i].i = activeBullets[i].i + 1;
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
                    if (activeBullets[i].j == 0)
                    {
                        activeBullets.RemoveAt(i);
                        break;
                    }
                    else activeBullets[i].j = activeBullets[i].j - 1;
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
            int bossmovement = 0;
            while (true)
            {
                if (bossmovement > 3 && boss.hp>0)
                {
                    boss.Move();
                    bossmovement = 0;
                }

                if (Snake_Head_Position.i == board.GetLength(0) - 1 && Snake_Head_Position.j == 15) LevelTwo();
                moveCounter = moveCounter + 1;
                lastDirection = lastDirection - 1;
                if (shoot == true)
                {
                    shoot = false;
                    Shoot();
                }
                if (direction == snake_direction.Up && moveCounter == 3)
                {
                    bossmovement = bossmovement+1;
                    moveCounter = 0;
                    // goto UP
                    if (board[Snake_Head_Position.i-1, Snake_Head_Position.j] == Snake.Wall)
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
                                board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Monster ||
                                board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Boss)
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
                    bossmovement = bossmovement + 1;
                    moveCounter = 0;
                    // goto right
                    if (board[Snake_Head_Position.i, Snake_Head_Position.j+1] == Snake.Wall)
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
                                board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Monster ||
                                board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Boss)
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
                    bossmovement = bossmovement + 1;
                    moveCounter = 0;
                    // goto down
                    if (board[Snake_Head_Position.i + 1, Snake_Head_Position.j] == Snake.Wall)
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
                                board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Monster ||
                                board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Boss)
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
                    bossmovement = bossmovement + 1;
                    moveCounter = 0;
                    // goto left
                    if (board[Snake_Head_Position.i, Snake_Head_Position.j-1] == Snake.Wall)
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
                                 board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Monster ||
                                 board[Snake_Head_Position.i, Snake_Head_Position.j] == Snake.Boss)
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
            if (File.Exists("scores.txt"))
                {
                String[] lines = System.IO.File.ReadAllLines("scores.txt");
                if (lines.Length > 0)
                {
                    String data = lines[0].ToString();
                    String[] temp = data.Split(";");
                    highScoreName = temp[0];
                    highScore = Convert.ToInt32(temp[1]);
                }
            }
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
                Console.ResetColor();
                string prompt = "\r\n███████╗███╗   ██╗ █████╗ ██╗  ██╗███████╗     ██████╗  █████╗ ███╗   ███╗███████╗\r\n██╔════╝████╗  ██║██╔══██╗██║ ██╔╝██╔════╝    ██╔════╝ ██╔══██╗████╗ ████║██╔════╝\r\n███████╗██╔██╗ ██║███████║█████╔╝ █████╗      ██║  ███╗███████║██╔████╔██║█████╗  \r\n╚════██║██║╚██╗██║██╔══██║██╔═██╗ ██╔══╝      ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  \r\n███████║██║ ╚████║██║  ██║██║  ██╗███████╗    ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗\r\n╚══════╝╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝\r\n                                                                                  \r\n";
                string[] options = { "Play", "Highscore", "Help", "Change speed", "Exit" };
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
                        ChangeSpeed();
                        break;
                    case 4:
                        ExitGame();
                        break;
                }
            }
            private void ChangeSpeed()
            {
                string prompt = "Choose speed:";
                string[] options = { "Fast", "Medium", "Slow" };
                Menu speed = new Menu(prompt, options);
                int selectedIndex = speed.Run();

                switch (selectedIndex)
                {
                    case 0:
                        Speed = 5;
                        break;
                    case 1:
                        Speed = 30;
                        break;
                    case 2:
                        Speed = 60;
                        break;
                }
                RunMainMenu();
            }
            private void HighScore()
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("High Score List:");
                Console.WriteLine($"{highScoreName} {highScore}");
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("<< * Back to main menu >>", Console.BackgroundColor);
                Console.ReadKey(true);
                if (true)
                {
                    RunMainMenu();
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
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("<< * Back to main menu >>", Console.BackgroundColor);
                Console.ReadKey(true);
                if (true)
                {
                    RunMainMenu();
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
                Console.WriteLine();
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