using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameOfLife
{
    /*
     * bron: https://khalidabuhakmeh.com/program-the-game-of-life-with-csharp-and-emojis
     */

    enum Status
    {
        Alive,
        Dead
    }
    class Program
    {
        const int Rijen = 70;
        const int Kolomen = 200;

        static bool runSimulator = true;
        static void Main(string[] args)
        {
            Status[,] grid = new Status[Rijen, Kolomen];
            Random rnd = new Random();
            bool pauze = false;

            

            Console.CancelKeyPress += (sender, arg) =>
            {
                runSimulator = false;
                Console.WriteLine("\nSimulatie gestopt");
                Thread.Sleep(500);
            };

            if (!pauze)
            {
                for (int rij = 0; rij < Rijen; rij++)
                {
                    for (int kolom = 0; kolom < Kolomen; kolom++)
                    {
                        grid[rij, kolom] = (Status)rnd.Next(0, 2);
                    }
                }
                Console.Clear();
            }
            

            while(runSimulator)
            {
                Print(grid);
                grid = VolgendeGeneratie(grid);
            }
        }
        static Status[,] VolgendeGeneratie(Status[,] huidigeGrid)
        {
            Status[,] volgendeGeneratie = new Status[Rijen, Kolomen];

            for (int rij = 1; rij < Rijen-1; rij++)
            {
                for (int kolom = 1; kolom < Kolomen-1; kolom++)
                {
                    int burenInLeven = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            burenInLeven += huidigeGrid[rij + i, kolom + j] == Status.Alive ? 1 : 0;
                        }
                    }

                    Status huidigeCel = huidigeGrid[rij, kolom];

                    burenInLeven -= huidigeCel == Status.Alive ? 1 : 0;

                    if (huidigeCel == Status.Alive && burenInLeven < 2)
                        volgendeGeneratie[rij, kolom] = Status.Dead;
                    else if (huidigeCel == Status.Alive && burenInLeven > 3)
                        volgendeGeneratie[rij, kolom] = Status.Dead;
                    else if (huidigeCel == Status.Dead && burenInLeven == 3)
                        volgendeGeneratie[rij, kolom] = Status.Alive;
                    else volgendeGeneratie[rij, kolom] = huidigeCel;
                }
            }
            return volgendeGeneratie;
        }

        static void Print(Status[,] toekomst, int timeout = 500)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string scherm = "";

            for (int rij = 0; rij < Rijen; rij++)
            {
                for (int kolom = 0; kolom < Kolomen; kolom++)
                {
                    //scherm += (toekomst[rij, kolom] == Status.Alive ? "" : "•");
                    stringBuilder.Append(toekomst[rij, kolom] == Status.Alive ? "o" : " ");
                }
                //scherm += "\n";
                stringBuilder.Append("\n");
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
            //Console.Write(scherm);
            Thread.Sleep(timeout);
        }
        static bool InputBool(string tekst = "j/n", bool Cyes = true, bool Cno = false)
        {
            ConsoleKeyInfo keyStrike = new ConsoleKeyInfo();

            Console.WriteLine(tekst);
            keyStrike = Console.ReadKey(true);

            switch (Char.ToLower(keyStrike.KeyChar))
            {
                case 'y':
                case 'j': return Cyes;
                case 'n': return Cno;

            }
            return false;

        }
    }

}
