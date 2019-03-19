using System;

namespace Game_of_Lfe
{
    class LifeGame
    {
        public int[,] Environment { get; set; }
        public int Generations { get; set; }

        public LifeGame(int[,] startingEnvironment)
        {
            this.Environment = startingEnvironment;
            Generations = 0;
        }

        public LifeGame(int rows, int cols)
        {
            Environment = new int[rows,cols];
            Generations = 0;
        }

        public int FindNeighborCount(LifeGame generation, int row, int col)
        {
            int neighbors = 0;

            // Above
            if (row - 1 >= 0)
            {
                neighbors += generation.Environment[row - 1, col];
            }
            // Below
            if (row + 1 < generation.Environment.GetLength(0))
            {
                neighbors += generation.Environment[row + 1, col];
            }
            // Left
            if (col - 1 >= 0)
            {
                neighbors += generation.Environment[row, col - 1];
            }
            // Right
            if (col + 1 < generation.Environment.GetLength(1))
            {
                neighbors += generation.Environment[row, col + 1];
            }
            // Diagonal Above and Left
            if (row - 1 >= 0 && col - 1 >= 0)
            {
                neighbors += generation.Environment[row - 1, col - 1];
            }
            // Diagonal Below and Right
            if (row + 1 < generation.Environment.GetLength(0) && col - 1 >= 0)
            {
                neighbors += generation.Environment[row + 1, col - 1];
            }
            // Diagonal Above and Right
            if (row - 1 >= 0 && col + 1 < generation.Environment.GetLength(1))
            {
                neighbors += generation.Environment[row - 1, col + 1];
            }
            // Diagonal Below and Left
            if (row + 1 < generation.Environment.GetLength(0) && col + 1 < generation.Environment.GetLength(1))
            {
                neighbors += generation.Environment[row + 1, col + 1];
            }
            return neighbors;
        }

        public int[,] FindNextGeneration(LifeGame generation)
        {
            int[,] nextGen = new int[generation.Environment.GetLength(0), generation.Environment.GetLength(1)];
            for (int row=0; row < nextGen.GetLength(0); row++)
            {
                for (int col=0; col < nextGen.GetLength(1); col++)
                {
                    int neighbors = generation.FindNeighborCount(generation, row, col);

                    if (generation.Environment[row, col] == 1)
                    {
                        if ( neighbors < 2 || neighbors > 3)
                        {
                            nextGen[row, col] = 0;
                        }
                        else if ( neighbors == 2 || neighbors == 3)
                        {
                            nextGen[row, col] = 1;
                        }
                    }
                    else
                    {
                        if (neighbors == 3)
                        {
                            nextGen[row, col] = 1;
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

        public static int[,] CreateRandom2dArray(Random rng, int minValue, int maxValue)
        {
            //int row = rng.Next(3, 40);
            //int col = rng.Next(3, 40);
            int row = 40;
            int col = 90;
            int[,] start = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    start[i, j] = rng.Next(minValue, maxValue);
                }
            }
            return start;
        }

        static void Main(string[] args)
        {
            
            Random rng = new Random();
            int[,] startingEnv = CreateRandom2dArray(rng, 0, 2);
            LifeGame life = new LifeGame(startingEnv);
            int gen = rng.Next(1, int.MaxValue);
            Console.WriteLine("Number of generations: " + gen);

            for (int i = 0; i < gen; i++)
            {
                //System.Threading.Thread.Sleep(500);
                Console.WriteLine("Generation Number: " + i);
                int[,] generation = life.FindNextGeneration(life);
                Print2DArray(generation);
                life.Environment = generation;
                life.Generations = life.Generations++;
                Console.WriteLine("\n");
            }
        }
    }
}
