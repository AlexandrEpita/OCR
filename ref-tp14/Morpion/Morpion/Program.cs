using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Morpion
{
    public class Program
    {
        static void Main(string[] args)
        {
            Play();
        }

        // profondeur
        // 1 -> Easy
        // 2 -> Medium
        // 5 -> Hard (see unbeatable)
        
        // start the game
        static public void Play()
        {
            
            Console.WriteLine("Hi ! Welcome to the game Tic Tac Toe.");
            string replay;
            do
            {
                Console.WriteLine("What difficulty do you want?\n1 : Easy  2 : Medium  3 : Hard");
                int difficulte = Int32.Parse(Console.ReadLine());
                uint prof = 2; // Moyen par defaut
                if (difficulte == 1)
                    prof = 1;
                else if (difficulte == 3)
                    prof = 5;
            
                Game game = new Game(prof);
                int res = game.stop();
                while (res == 0)
                {
                    game.play();
                    res = game.stop();
                }
                if (res == 2)
                    Console.WriteLine("Sorry the IA win!");
                else if (res == 1)
                    Console.WriteLine("Congratulation, you win!");
                else
                    Console.WriteLine("Draw");
                
                Console.WriteLine("Do you want to replay ? yes / no");
                replay = Console.ReadLine();
            } while (replay == "yes");
            Console.WriteLine("Thanks for playing!");
        }

    }
}