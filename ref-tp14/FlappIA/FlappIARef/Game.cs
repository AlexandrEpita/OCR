using System;
using System.Linq;
using System.Threading;

namespace tp14Ref
{
    public class Game
    {
        /// <summary>
        /// To avoid infinite game because of one 200 iq bird 
        /// </summary>
        private const long MaxDistance = 100000;
        /// <summary>
        /// Max distance in Y between 2 successive pipes
        /// It is used to generate pipe with different height.
        /// </summary>
        private readonly int _bound;
        /// <summary>
        /// Drawer object to print the game
        /// </summary>
        private readonly Drawer _drawer;
        /// <summary>
        /// Free Y distance between the top and bottom pipe
        /// </summary>
        private readonly int _free;
        /// <summary>
        /// Generation of Birds
        /// </summary>
        private readonly Generation _generation;
        /// <summary>
        /// Queue of that stores in a FIFO order the generated pipes.
        /// </summary>
        private readonly Queue<Pipe> _pipes;
        /// <summary>
        /// Sleep to avoid epilepsy
        /// </summary>
        private readonly int _sleep;
        /// <summary>
        /// Distance in X between 2 successive pipes
        /// </summary>
        private readonly int _step;
        
        /// <summary>
        /// Current X position of the game
        /// </summary>
        private long _x;

        public Game(Drawer drawer, Generation generation)
        {
            _generation = generation;
            // Reset the current generation
            foreach (var bird in generation.Birds)
                bird.Y = drawer.Height / 2;

            _x = 0;
            _pipes = new Queue<Pipe>();
            _drawer = drawer;
            _sleep = 100;
            _step = 20;
            _free = _drawer.Height / 4;
            _bound = 20;
            long pos = _step;
            
            while (pos < _drawer.Width)
            {
                GeneratePipe(pos);
                pos += _step;
            }

            GeneratePipe(pos);
        }

        /// <summary>
        /// LinQ to detect if there is still one bird alive
        /// </summary>
        public bool Continue => _generation.Birds.Any(bird => !bird.Dead);

        /// <summary>
        /// Generate new pipe
        /// </summary>
        /// <param name="pos"> position of the next pipe </param>
        private void GeneratePipe(long pos)
        {
            Pipe previous;
            if (_pipes.IsEmpty())
                previous = new Pipe(0, (_drawer.Height - _free) / 2, _free,
                    _drawer.Height - _free - (_drawer.Height - _free) / 2);
            else
                previous = _pipes.PeekBack();
            var move = FlappIA.Rnd.Next(-_bound, _bound + 1);

            var free = _free;
            var top = previous.TopPipeHeight + move * _drawer.Height / 100;
            if (top + free > _drawer.Height)
                top = _drawer.Height - free;
            if (top < 0)
                top = 0;
            var bottom = _drawer.Height - top - free;
            var pipe = new Pipe(pos, top, free, bottom);
            _pipes.PushBack(pipe);
        }

        /// <summary>
        /// Update the game :
        ///  - Remove and add new pipe
        ///  - Update the state of all the birds
        /// </summary>
        public void Update()
        {
            // Add pipes while there is enough space to add one
            while (_pipes.PeekBack().X + _step <= _x + _drawer.Width)
                GeneratePipe(_pipes.PeekBack().X + _step);
            
            // Remove pipes that are gone
            while (_pipes.PeekFront().X + 3 < _x)
                _pipes.PopFront();

            // Take the first pipe
            var first = _pipes.PeekFront();
            foreach (var bird in _generation.Birds)
            {
                // Don't check for dead birds
                if (bird.Dead)
                    continue;
                
                // Update the bird
                bird.Update(_drawer, _x, _pipes);
                
                // Kill the bird if it collides with the first pipe
                if (first.Collides(_x + 1, bird.Y) || _x > MaxDistance)
                    bird.Kill(_x);
            }

            _x += 1;
        }

        /// <summary>
        /// Draw the game :
        ///  - Draw all birds
        ///  - Draw all pipes
        /// </summary>
        public void Draw()
        {
            // Clear the output
            Drawer.Clear();
            
            // Draw each alive bird
            var alive = 0;
            foreach (var bird in _generation.Birds)
            {
                if (bird.Dead)
                    continue;
                alive++;
                Drawer.Draw(bird);
            }

            // Draw each pipe
            foreach (Pipe pipe in _pipes) 
                _drawer.Draw(pipe, _x);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("ALIVE: " + alive + " --- SCORE: " + _x);
        }

        /// <summary>
        /// Sleep the thread
        /// </summary>
        public void Sleep()
        {
            Thread.Sleep(_sleep);
        }
    }
}