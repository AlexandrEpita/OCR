using System;

namespace tp14Ref
{
    public class Bird
    {
        /// <summary>
        /// Parameters of the Bird's movements
        /// </summary>
        private const double Gravity = 0.04;
        private const double Jump = -0.02;
        private const double MaximalUp = 1 * Jump;
        private const double MaximalDown = 4 * Gravity;
        
        public bool Dead { get; set; }
        public double VerticalSpeed { get; private set; }
        /// <summary>
        /// Color to differentiate different Birds
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// NeuralNetwork is literally the brain of the bird
        /// </summary>
        private NeuralNetwork NeuralNetwork { get; }
        /// <summary>
        /// The score represent how well the bird is doing, in our case it's the distance it has travelled so far
        /// </summary>
        public long Score { get; private set; }
        
        /// <summary>
        /// Y position of the bird in the console, the console height is required
        /// to initialize the bird Y position, so its default value is -1 and the
        /// Drawer object initializes it later.
        ///
        /// </summary>
        private int _y;

        /// The X position is relative to the Drawer, therefore it does never change and there
        /// is no point in storing it.
        /// 
        private void BirdInitialState()
        {
            _y = -1;
            Score = 0;
            Color = (ConsoleColor) FlappIA.Rnd.Next(16);
        }
        
        public Bird()
        {
            BirdInitialState();
            
            /*
             *  Model of the neural network :
             *
             * The first value is the input layer dimension, the 4 values are :
             *  - The y position of the bird
             *  - The vertical speed of the bird
             *  - The y position of the top pipe
             *  - The x distance to the next pipe
             *
             * The middle values are the hidden layer dimension. There is none in this model because it's quite an easy
             * IA, but you can try to add some and see what happens.
             * 
             * The last value is the output layer dimension, you can choose to have 2 output :
             *  - One for "should jump"
             *  - One for "should not jump"
             *
             * You will just have to check which one got the higher value.
             *
             * You can change those values as you want, try some stuff and check if it works !
            */
            int[] networkDimension = {4, 1};
            NeuralNetwork = new NeuralNetwork(networkDimension);
        }

        public Bird(Bird bird, bool mutate = true)
        {
            BirdInitialState();
            NeuralNetwork = new NeuralNetwork(bird.NeuralNetwork, mutate);
        }

        /// <summary>
        /// Initialize a bird from a file
        /// </summary>
        /// <param name="path"></param>
        public Bird(string path)
        {
            BirdInitialState();
            NeuralNetwork = NeuralNetwork.Restore(path);
        }

        /// <summary>
        /// Simple setter and getter, we can set the Y value only if it has not already been set
        /// </summary>
        public int Y
        {
            get => _y;
            set
            {
                if (_y == -1)
                    _y = value;
            }
        }

        /// <summary>
        /// Kill the bird and set its score
        /// </summary>
        /// <param name="score"></param>
        public void Kill(long score)
        {
            Score = score;
            Dead = true;
        }

        /// <summary>
        /// Mutate the bird
        /// </summary>
        public void Mutate()
        {
            NeuralNetwork.Mutate();
        }

        /// <summary>
        /// Crossover function
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        public Bird Crossover(Bird partner)
        {
            var res = new Bird(this, false);
            res.NeuralNetwork.Crossover(partner.NeuralNetwork);
            return res;
        }

        /// <summary>
        /// Update the bird position, it use its brain to jump or not.
        /// </summary>
        /// <param name="drawer"></param>
        /// <param name="x"></param>
        /// <param name="pipes"></param>
        public void Update(Drawer drawer, long x, Queue<Pipe> pipes)
        {
            // Get the maximal y value
            var max = drawer.Height - 1;
            // Call the controller and either jump or fall accordingly
            var pipe = pipes.PeekFront();
            if (pipe.X <= x)
                pipe = pipes.PeekSecond();
                
            double[] input = {_y, VerticalSpeed, pipe.TopPipeHeight, pipe.X - x};

            NeuralNetwork.Feed(input);
            NeuralNetwork.FrontProp();

            if (NeuralNetwork.ShouldJump())
                VerticalSpeed += Jump;
            else
                VerticalSpeed += Gravity;

            // Bounds checking of the speed
            if (VerticalSpeed < MaximalUp)
                VerticalSpeed = MaximalUp;
            if (VerticalSpeed > MaximalDown)
                VerticalSpeed = MaximalDown;

            // Place the bird within the bounds of the drawer
            _y = (int) (_y + VerticalSpeed * drawer.Height);
            if (_y > max)
            {
                VerticalSpeed = 0;
                _y = max;
            }

            if (_y < 0)
            {
                VerticalSpeed = 0;
                _y = 0;
            }
        }

        /// <summary>
        /// Save the bird's brain
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            NeuralNetwork.Save(path);
        }
    }
}