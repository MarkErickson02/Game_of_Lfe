using System;

namespace Game_of_Lfe
{
    class LifeGame
    {
        private int[,] Board;

        public LifeGame(int[,] board)
        {
            this.Board = board;
        }

        public LifeGame(int rows, int cols)
        {
            Board = new int[rows,cols];
        }

        public int FindNeighborCount(LifeGame board,int row, int col)
        {
            int neighbors = 0;
            if (row - 1 >= 0)
            {
                neighbors += board.Board[row - 1, col];
            }
            if (row + 1 < board.Board.GetLength(0))
            {
                neighbors += board.Board[row + 1, col];
            }
            if (col - 1 >= 0)
            {
                neighbors += board.Board[row, col - 1];
            }
            if (col + 1 < board.Board.GetLength(1))
            {
                neighbors += board.Board[row, col + 1];
            }
            if (row - 1 >= 0 && col - 1 >= 0)
            {
                neighbors += board.Board[row - 1, col - 1];
            }
            if (row + 1 < board.Board.GetLength(0) && col - 1 >= 0)
            {
                neighbors += board.Board[row + 1, col - 1];
            }
            if (row - 1 >= 0 && col + 1 < board.Board.GetLength(1))
            {
                neighbors += board.Board[row - 1, col + 1];
            }
            if (row + 1 < board.Board.GetLength(0) && col + 1 < board.Board.GetLength(1))
            {
                neighbors += board.Board[row + 1, col + 1];
            }
            return neighbors;
        }

        public int[,] FindNextGeneration(LifeGame game)
        {
            int[,] nextGen = new int[game.Board.GetLength(0), game.Board.GetLength(1)];
            for (int i=0; i < nextGen.GetLength(0); i++)
            {
                for (int j=0; j < nextGen.GetLength(1); j++)
                {
                    int neighbors = game.FindNeighborCount(game, i, j);
                    if (game.Board[i,j] == 1)
                    {
                        if ( neighbors < 2 || neighbors > 3)
                        {
                            nextGen[i, j] = 0;
                        }
                        else if ( neighbors == 2 || neighbors == 3)
                        {
                            nextGen[i, j] = 1;
                        }
                    }
                    else
                    {
                        if (neighbors == 3)
                        {
                            nextGen[i, j] = 1;
                        }
                    }
                }
            }
            return nextGen;
        }

        public static void Print2DArray(int[,] matrix)
        {
            for (int i=0; i<matrix.GetLength(0); i++)
            {
                for (int j=0; j<matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            
            Random rng = new Random();
            //int row = rng.Next(3, 40);
            //int col = rng.Next(3, 40);
            int row = 40;
            int col = 90;
            int[,] start = new int[row, col];
            for (int i=0; i < row; i++)
            {
                for (int j=0; j<col; j++)
                {
                    start[i, j] = rng.Next(0, 2);
                }
            }
            LifeGame life = new LifeGame(start);

            int[,] nextGen = life.FindNextGeneration(life);
            life.Board = nextGen;

            int gen = rng.Next(1, int.MaxValue);
            Console.WriteLine("Number of generations: " + gen);

            for (int i = 0; i < gen; i++)
            {
                //System.Threading.Thread.Sleep(500);
                Console.WriteLine("Generation Number: " + i);
                int[,] generation = life.FindNextGeneration(life);
                Print2DArray(generation);
                life.Board = generation;
                Console.WriteLine("\n");
            }
        }
    }
}
