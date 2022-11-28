using System.Runtime.InteropServices;

namespace Snake
{
    internal class Program
    {
        public static Snake[,] board = new Snake[20, 30];
        public static int direction = 0; //0=north, 1=east, 2=south, 3=west
        public static int score = 0;
        public static int highScore = 0;
        public static string playerName="";
        public static int snakeSize = 0;

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
            snakeSize= 0;
            score = 0;
            direction = 0;
            {
                for(int i = 0; i < board.GetLength(0); i++)
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
                    if(board [i, j] == Snake.Empty)
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

        static void Main(string[] args)
        {
            ResetBoard();
            DrawBoard();
            string choice="";

            do
            {

            } while (choice!="quit");
        }
    }
}