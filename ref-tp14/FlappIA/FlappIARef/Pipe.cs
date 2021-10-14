using System;
using System.Threading;

namespace tp14Ref
{
    public class Pipe
    {
        private readonly int _pipeWidth;
        
        public long X { get; }
        public int BottomPipeHeight { get; }
        
        public int TopPipeHeight { get; }
        /// <summary>
        ///     Returns the free height between 2 pipes
        /// </summary>
        public int FreeHeight { get; }

        /// <summary>
        ///     Initialize a pipe
        /// </summary>
        /// <param name="x">The x position</param>
        /// <param name="topPipeHeight">The height of the top pipe</param>
        /// <param name="freeHeight">The free height</param>
        /// <param name="bottomPipeHeight">The height of the bottom pipe</param>
        public Pipe(long x, int topPipeHeight, int freeHeight, int bottomPipeHeight)
        {
            X = x;
            TopPipeHeight = topPipeHeight;
            FreeHeight = freeHeight;
            BottomPipeHeight = bottomPipeHeight;
            _pipeWidth = 3;
        }

        /// <summary>
        ///     Check whether a position is within a pipe
        /// </summary>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        /// <returns>True if (x, y) is inside the pipe</returns>
        public bool Collides(long x, int y)
        {
            if (x >= X && x <= X + _pipeWidth)
            {
                if (y < TopPipeHeight)
                    return true;

                if (y >= TopPipeHeight + FreeHeight)
                    return true;
            }

            return false;
        }
    }
}