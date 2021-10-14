using System;

namespace tp14Ref
{
    public class Drawer
    {
        /// <summary>
        ///     Initialize a new console drawer
        /// </summary>
        /// <param name="width">The width of the console</param>
        /// <param name="height">The height of the console</param>
        public Drawer(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public int Height { get; }
        public int Width { get; }

        /// <summary>
        ///     Draw a bird
        /// </summary>
        /// <param name="bird">The bird to draw</param>
        public static void Draw(Bird bird)
        {
            if (bird.Dead)
                return;

            Console.SetCursorPosition(1, bird.Y);
            Console.ForegroundColor = bird.Color;
            Console.Write(bird.VerticalSpeed < 0 ? '^' : 'v');
            Console.ResetColor();
        }

        /// <summary>
        ///     Draw a pipe
        /// </summary>
        /// <param name="pipe">The pipe to draw</param>
        /// <param name="x">The current x position</param>
        public void Draw(Pipe pipe, long x)
        {
            var bottom = pipe.TopPipeHeight + pipe.FreeHeight;
            var drawPos = (int) (pipe.X - x + 1);

            if (0 <= drawPos && drawPos < Console.WindowWidth)
            {
                if (pipe.TopPipeHeight > 0)
                {
                    Console.SetCursorPosition(drawPos, 0);
                    for (var i = 1; i < pipe.TopPipeHeight; ++i)
                    {
                        Console.Write('│');
                        Console.SetCursorPosition(drawPos, i);
                    }

                    Console.Write('└');
                }

                if (bottom < Height)
                {
                    Console.SetCursorPosition(drawPos, bottom);
                    Console.Write('┌');
                    for (var i = 1; i < pipe.BottomPipeHeight; ++i)
                    {
                        Console.SetCursorPosition(drawPos, bottom + i);
                        Console.Write('│');
                    }
                }
            }

            drawPos += 1;
            if (0 <= drawPos && drawPos < Console.WindowWidth)
            {
                if (pipe.TopPipeHeight > 0)
                {
                    Console.SetCursorPosition(drawPos, pipe.TopPipeHeight - 1);
                    Console.Write('─');
                }

                if (bottom < Height)
                {
                    Console.SetCursorPosition(drawPos, bottom);
                    Console.Write('─');
                }
            }

            drawPos += 1;
            if (0 <= drawPos && drawPos < Console.WindowWidth)
            {
                if (pipe.TopPipeHeight > 0)
                {
                    Console.SetCursorPosition(drawPos, 0);
                    for (var i = 1; i < pipe.TopPipeHeight; ++i)
                    {
                        Console.Write('│');
                        Console.SetCursorPosition(drawPos, i);
                    }

                    Console.Write('┘');
                }

                if (bottom < Height)
                {
                    Console.SetCursorPosition(drawPos, bottom);
                    Console.Write('┐');
                    for (var i = 1; i < pipe.BottomPipeHeight; ++i)
                    {
                        Console.SetCursorPosition(drawPos, bottom + i);
                        Console.Write('│');
                    }
                }
            }
        }

        /// <summary>
        ///     Clear the console
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }
    }
}