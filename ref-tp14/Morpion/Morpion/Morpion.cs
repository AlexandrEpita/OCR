using System;
using System.Data;
using System.Threading;

namespace Morpion
{
    public class Game
    {
        public char[] board;
        // 0 1 2
        // 3 4 5
        // 6 7 8
        private uint depth_;
        public Game(uint prof)
        {
            board = new []{ '_', '_', '_', '_', '_', '_', '_', '_', '_' };
            depth_ = prof;
        }


        public static Game load_game(String board, uint prof)
        {
            Game game = new Game(prof);
            for(int i = 0; i < 9; i++)
            {
                game.board[i] = board[i];
            }

            return game;
        }

        // returns the number of moves played on the board
        public int nb_move()
        {
            int c = 0;
            for (int i = 0; i < 9; i++)
                if (board[i] != '_')
                    c++;
            return c;
        }

        // Go through the horizontal lines,
        // and return the difference between n pawns aligned by the ia
        // and n pawns aligned by the player
        public int horizontal(int n)
        {
            int nb_two_ia = 0;
            int nb_two_j = 0;

            for (int i = 0; i < 3; i++)
            {
                int cmp_ia = 0;
                int cmp_j = 0;

                for (int j = 0; j < 3; j++)
                {
                    if (board[i * 3 + j] == 'x')
                    {
                        cmp_ia++;
                        cmp_j = 0;
                        if (cmp_ia == n)
                            nb_two_ia++;
                    }
                    else if (board[i * 3 + j] == 'o')
                    {
                        cmp_j++;
                        cmp_ia = 0;
                        if (cmp_j == n)
                            nb_two_j++;
                    }
                }
            }
            return nb_two_ia - nb_two_j;
        }
        
        // Go through the vertical lines,
        // and return the difference between n pawns aligned by the ia
        // and n pawns aligned by the player
        public int vertical(int n)
        {
            int nb_two_ia = 0;
            int nb_two_j = 0;

            for (int i = 0; i < 3; i++)
            {
                int cmp_ia = 0;
                int cmp_j = 0;

                for (int j = 0; j < 3; j++)
                {
                    if (board[j * 3 + i] == 'x')
                    {
                        cmp_ia++;
                        cmp_j = 0;
                        if (cmp_ia == n)
                            nb_two_ia++;
                    }
                    else if (board[j * 3 + i] == 'o')
                    {
                        cmp_j++;
                        cmp_ia = 0;
                        if (cmp_j == n)
                        {
                            nb_two_j++;
                        }
                    }
                }
            }
            return nb_two_ia - nb_two_j;
        }

        // Go through the diagonals,
        // and return the difference between n pawns aligned by the ia
        // and n pawns aligned by the player
        public int diagonal(int[] ligne, int n)
        {
            int nb_two_ia = 0;
            int nb_two_j = 0;
            
            int cmp_ia = 0;
            int cmp_j = 0;
            
            foreach (int i in ligne)
            {
                if (board[i] == 'x')
                {
                    cmp_ia++;
                    cmp_j = 0;
                    if (cmp_ia == n)
                        nb_two_ia++;
                }
                else if (board[i] == 'o')
                {
                    cmp_j++;
                    cmp_ia = 0;
                    if (cmp_j == n)
                        nb_two_j++;
                }
            }

            return nb_two_ia - nb_two_j;
        }
        
        // return the state of the board,
        // if there is a winner, draw or if the game is not over
        // 2 -> IA / 1 -> Player / 3 -> Draw / 0 -> Not finish
        public int stop()
        {
            int score = 0;
            score += diagonal( new[] {0, 4, 8}, 3);
            score += diagonal( new[] {2, 4, 6}, 3);
            score += horizontal( 3);
            score += vertical( 3);
            if (score == 1)
                return 2;
            if (score == -1)
                return 1;
            int count = nb_move();
            if (count == 9)
                return 3;
            return 0;
        }

        // max part of algo minmax
        long val_max(uint depth)
        {
            long maximum = -10000;
            if (depth == 0 || stop() > 0)
                return eval();

            for (int i = 0; i < 9; i++)
            {
                if (board[i] == '_')
                {
                    board[i] = 'x';
                    long tmp = val_min(depth - 1);
                    if (tmp > maximum)
                    {
                        maximum = tmp;
                    }
                    board[i] = '_';
                }
            }
            return maximum;
        }

        // min part of algo minmax
        long val_min(uint depth)
        {
            long minimum = 10000;
            if (depth == 0 || stop() > 0)
                return eval();
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == '_')
                {
                    board[i] = 'o';
                    long tmp = val_max( depth - 1);
                    if (tmp < minimum)
                    {
                        minimum = tmp;
                    }
                    board[i] = '_';
                }
            }
            return minimum;
        }

        // returns a number allowing to know the "weight of the box",
        // depending on whether there is a winner, or if a player is close to winning
        // It is this number which will make it possible to know on which square played
        public long eval()
        {
            int result = stop();
            if (result == 2)
                return 1000 - nb_move();

            if (result == 1)
                return -1000 + nb_move();

            if (result == 3)
                return 0;
            

            long score = 0;
            score += diagonal( new[] {0, 4, 8}, 2);
            score += diagonal( new[] {2, 4, 6}, 2);
            score += horizontal( 2);
            score += vertical( 2);

            return score;
        }

        // play the ai
        void play_ia()
        {
            long maximum = -10000;
            int pos = -1;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == '_')
                {
                    board[i] = 'x';
                    long tmp = val_min(depth_ - 1);
                    if (tmp > maximum)
                    {
                        maximum = tmp;
                        pos = i;
                    }
                    board[i] = '_';
                }
            }
            if (pos != -1)
                board[pos] = 'x';
        }

        // play the player
        void play_player()
        {
            string pos;
            do
            {
                Console.WriteLine("Where do you want to play ?");
                pos = Console.ReadLine();
                if (pos.Length > 1 || pos[0] > '8' || pos[0] < '0')
                    Console.Error.WriteLine("You must play a number between 0 and 8 inclusive.");
                else if (board[pos[0] - '0'] != '_')
                    Console.Error.WriteLine("The box is already taken.");
            } while (pos[0] > '8' || pos[0] < '0' || board[pos[0] - '0'] != '_');
            board[pos[0] - '0'] = 'o';
        }

        // display of the current game as a grid
        void prettyprint()
        {
            Console.WriteLine(" ___________");
            Console.WriteLine("| {0} | {1} | {2} |", board[0], board[1], board[2]);
            Console.WriteLine("|___________|");
            Console.WriteLine("| {0} | {1} | {2} |", board[3], board[4], board[5]);
            Console.WriteLine("|___________|");
            Console.WriteLine("| {0} | {1} | {2} |", board[6], board[7], board[8]);
            Console.WriteLine("|___________|");
        }
        
        // display of the game with the numbers corresponding to the boxes
        void positions()
        {
            if (!Console.IsOutputRedirected)
            {
                int y = 0;
                Console.SetCursorPosition(20, y++);
                Console.WriteLine(" ___________");
                for (int i = 0; i < 3; i++)
                {
                    Console.SetCursorPosition(20, y++);
                    Console.WriteLine("| {0} | {1} | {2} |", i * 3, i * 3 + 1, i * 3 + 2);
                    Console.SetCursorPosition(20, y++);
                    Console.WriteLine("|___________|");
                }
            }
        }

        // start a game turn
        public void play()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            prettyprint(); 
            positions();
            
            play_player();
            if (stop() == 0)
                play_ia();
        }

        public string state()
        {
            string res = "";
            for (int i = 0; i < 9; i++)
                res += board[i];
            return res;
        }
    }
} 