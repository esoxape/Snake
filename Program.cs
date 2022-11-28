namespace Snake
{
    internal class Program
    {
        public static Snake[,] board = new Snake[50, 50];
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
            ///loop to reset the board to starting position
            for(int i = 0; i < 51; i++)
            {
                for(var j = 0; j < 51; j++)
                {
                    if (i == 0 || i == 50 || j==0 || j ==50) board[i, j] = Snake.Wall;
                    else if (i==25 && j==25) board[i, j] = Snake.Head;
                    else board[i, j] = Snake.Empty;                    
                }
            }
        }
        static void Main(string[] args)
        {
            ResetBoard();
            string choice="";
            do
            {

            } while (choice!="quit");
        }
    }
}