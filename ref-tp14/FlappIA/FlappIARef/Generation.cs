using System;
using System.Linq;
using System.Threading;

namespace tp14Ref
{
    public class Generation
    {
        public Bird[] Birds { get; private set; }

        /// <summary>
        /// Initialize a new Generation of size Bird.
        /// They are the birds you must train.
        /// </summary>
        /// <param name="size"></param>
        public Generation(int size)
        {
            Birds = new Bird[size];
            for (var i = 0; i < size; ++i)
                Birds[i] = new Bird();
        }

        /// <summary>
        /// Create a generation with one Bird.
        /// It's use by PlayFromPath() to play with an already trained Bird.
        /// </summary>
        /// <param name="bird"></param>
        public Generation(Bird bird)
        {
            Birds = new[] {bird};
        }

        /// <summary>
        /// Sort function
        /// </summary>
        private void Sort()
        {
            Array.Sort(Birds, (b1, b2) => b1.Score.CompareTo(b2.Score));
        }

        /// <summary>
        /// Select a bird using fitness proportionate selection
        /// </summary>
        /// <param name="fitnessSum"> Sum of all the score of all birds </param>
        /// <returns> Bird chosen </returns>
        /// <exception cref="Exception"> If no bird are found, it should never happen </exception>
        private Bird SelectBird(double fitnessSum)
        {
            double x = FlappIA.Rnd.NextDouble();
            foreach (var bird in Birds)
            {
                double birdOdd = bird.Score / fitnessSum; 
                if (x <= birdOdd)
                    return bird;
                x -= birdOdd;
            }

            //It should never happen
            throw new Exception("ERROR: No bird found in the selection pool");
        }

        /// <summary>
        /// Create new generation of bird
        /// </summary>
        public void NewGen()
        {
            Sort();
            
            var newGeneration = new Bird[Birds.Length];
            double fitnessSum = Birds.Sum(bird => bird.Score);

            int i = 0;
            // Random crossover
            for (; i < Birds.Length / 2; i++)
            {
                var selectedBird1 = SelectBird(fitnessSum);
                var selectedBird2 = SelectBird(fitnessSum);
                newGeneration[i] = selectedBird1.Crossover(selectedBird2);
                newGeneration[i].Mutate();
            }

            // Best birds crossover
            for (; i < 3 * Birds.Length / 4; i++)
            {
                var bestBird1 = Birds[Birds.Length/4 + i - 1];
                var bestBird2 = Birds[Birds.Length/4 + i];
                newGeneration[i] = bestBird1.Crossover(bestBird2);
                newGeneration[i].Mutate();
            }
           
            // Save old best birds and mutating them (it also works if without mutation)
            for (; i < Birds.Length; i++)
                newGeneration[i] = new Bird(Birds[i]);

            Birds = newGeneration;
        }

        public void PrintBirdsScore()
        {
            Sort();
            foreach (var bird in Birds)
                Console.Write(bird.Score + " "); // Do not mind the trailing space...
            Console.WriteLine();
            Console.WriteLine("BEST SCORE: " + Birds[^1].Score);
        }

        /// <summary>
        /// Compute average score of all birds
        /// </summary>
        /// <returns></returns>
        public void PrintAvg()
        {
            Console.WriteLine("Average: " + Birds.Sum(bird => bird.Score) / Birds.Length);
        }

        /// <summary>
        /// Select the best bird in the current generation
        /// </summary>
        /// <returns></returns>
        public Bird GetBestBird()
        {
            Sort();
            return Birds[^1];
        }
    }
}