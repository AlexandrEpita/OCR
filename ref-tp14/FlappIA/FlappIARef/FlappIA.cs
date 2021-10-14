using System;
using System.Threading;

namespace tp14Ref
{
    internal static class FlappIA
    {
        public static readonly Random Rnd = new Random();

        private static void PlayFromPath(string path)
        {
            var bird = new Bird(path);
            var drawer = new Drawer(Console.WindowWidth, Console.WindowHeight);
            var gen = new Generation(bird);
            var game = new Game(drawer, gen);
            game.Draw();
            while (game.Continue)
            {
                game.Update();
                game.Draw();
                game.Sleep();
            }
        }

        /// <summary>
        /// Choose to draw the game or not !
        /// </summary>
        private const bool Draw = true;
        
        /// <summary>
        /// Number of birds for the first generation !
        /// </summary>
        private const int BirdGeneration = 128;

        /// <summary>
        ///     The main function:
        ///     - Register the managers
        ///     - Register the drawers
        ///     - Register the birds
        ///     - Initialize the game
        ///     - Play
        /// </summary>
        public static void Main()
        {
            // Hide the cursor
            Console.CursorVisible = false;
            // Create a new generation of bird
            var gen = new Generation(BirdGeneration);
            // Initialize the console drawer, which will handle the console output
            var drawer = new Drawer(Console.WindowWidth, Console.WindowHeight);

            // Number of generation per game
            var currentGeneration = 0;
            var maxGeneration = 100;


            while (++currentGeneration <= maxGeneration)
            {
                var game = new Game(drawer, gen);
                if (Draw)
                    game.Draw();

                while (game.Continue)
                {
                    game.Update();
                    if (Draw)
                    {
                        game.Draw();
                        game.Sleep();
                    }
                }
                
                Console.Clear();
                gen.PrintBirdsScore();
                gen.PrintAvg();
                Console.WriteLine("Current generation: " + currentGeneration);
                if (Draw)
                    Thread.Sleep(1000);
                gen.NewGen();
            }

            Console.WriteLine("GAME COMPLETE");
        }
    }
}