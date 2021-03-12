using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using TDSconsoleMuisEvents;

namespace GameOfLife
{

    enum Status
    {
        Alive,
        Dead
    }
    class Program
    {
        const int Rijen = Kolomen/2;
        const int Kolomen = 100;
        static int snelheid = 50;
        static bool invoer = false;
        static bool eersteX = false;
        static int aantalInvoer = 0;
        static bool runSimulator = true;
        static ConsoleKeyInfo cki;
        static int muisX = 0;
        static int muisY = 0;
        static void Main(string[] args)
        {
            TDSconsoleMuis muis = new TDSconsoleMuis();

            muis.OnMuisKlik += Muis_OnMuisKlik;

            Thread gameOfLife = new Thread(GameOfLife);
            gameOfLife.Name = "game";

            Thread invoer = new Thread(Invoer);
            invoer.Name = "invoer";

            gameOfLife.Start();

            invoer.Start();
        }

        private static void Muis_OnMuisKlik(object sender, TDSmuisKlikEventArgs e)
        {
            muisX = e.Muis.Xpositie;
            muisY = e.Muis.Ypositie;
            eersteX = true;
            invoer = true;
        }

        static void GameOfLife()
        {
            Status[,] grid = new Status[Rijen, Kolomen];
            Random rnd = new Random();
            Console.OutputEncoding = Encoding.Unicode;
            int verschil = 0;

            Console.CancelKeyPress += (sender, arg) =>
            {
                runSimulator = false;
                Console.WriteLine("\nSimulatie gestopt");
                Thread.Sleep(500);
            };
       
            Console.Clear();
            Console.WindowWidth = 150;
            Console.WindowHeight = 61;
            bool aanAf = true;

            for (int rij = 0; rij < Rijen; rij++)
            {
                for (int kolom = 0; kolom < Kolomen; kolom++)
                {
                    if (aanAf)
                    {
                        aanAf = true;
                        grid[rij, kolom] = Status.Dead;
                    }else
                    {
                        aanAf = false;
                        grid[rij, kolom] = Status.Alive;
                    }
                }
            }

            while (runSimulator)
            {
                if (invoer)
                {
                    switch (cki.Key)
                    {
                        case ConsoleKey.Enter:
                            {               
                                eersteX = true;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            {
                                snelheid += (snelheid - 10) > 0 ? -10 : 0;
                            }break;
                        case ConsoleKey.DownArrow:
                            {
                                snelheid += (snelheid + 10) < 1000 ? +10 : 0;
                            }
                            break;
                    }
                    invoer = false;
                }
                if (eersteX)
                {
                    aantalInvoer = 15;
                    verschil = 15;
                    eersteX = false;
                }
                if  (aantalInvoer > 0)
                {
                    verschil -= verschil == 0 ? 0 : verschil / aantalInvoer;
                    int lengteVierkant = 10;
                    int oRij, bRij, oKol, bKol;

                    if (muisX < Kolomen && muisY < Rijen)
                    {
                        oKol = muisX - (lengteVierkant/2);
                        bKol = muisX + (lengteVierkant/2);
                        oRij = muisY - (lengteVierkant/2);
                        bRij = muisY + (lengteVierkant/2);

                    }else
                    {
                        oRij = (Rijen / 2) - lengteVierkant / 2 - verschil;
                        bRij = (Rijen / 2) + lengteVierkant / 2 + verschil;
                        oKol = (Kolomen / 2) - lengteVierkant / 2 - verschil;
                        bKol = (Kolomen / 2) + lengteVierkant / 2 + verschil;
                    }
                    grid = DrawInGrid(grid, oRij, bRij, oKol, bKol);
                    aantalInvoer -= aantalInvoer > 0 ? 1 : 0;
                    if (aantalInvoer == 0) invoer = false;
                    eersteX = false;
                    //aantalInvoer--;
                }

                snelheid = (snelheid < 10) ? 10 : snelheid;
                Print(grid, snelheid);
                Console.WriteLine("Click in the pool to throw a rock <UP ARROW> increase speed <DOWN ARROW> decrease speed");
                Console.WriteLine($"huidige snelheid: {snelheid}");
                grid = VolgendeGeneratie(grid);
            }
        }

        static void Invoer()
        {
            ConsoleKeyInfo vorigeKey;
            while (runSimulator)
            {
                cki = Console.ReadKey(true);
                invoer = true;
            }
        }
        static Status[,] VolgendeGeneratie(Status[,] huidigeGrid)
        {
            Status[,] volgendeGeneratie = new Status[huidigeGrid.GetLength(0), huidigeGrid.GetLength(1)];

            for (int rij = 1; rij < huidigeGrid.GetLength(0)-1 ; rij++)
            {
                for (int kolom = 1; kolom < huidigeGrid.GetLength(1)-1 ; kolom++)
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

        static void Print(Status[,] toekomst, int timeout = 200)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int rij = 0; rij < Rijen; rij++)
            {
                for (int kolom = 0; kolom < Kolomen; kolom++)
                {
                    stringBuilder.Append(toekomst[rij, kolom] == Status.Alive ? "☻" : " ");
                }
                stringBuilder.Append("\n");
            }

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
            Thread.Sleep(timeout);
        }
        static Status[,] DrawInGrid(Status[,] huidigeGrid, int oRij = 0, int bRij = 0, int oKol = 0, int bKol = 0, Status verander = Status.Alive, bool aanEnAf = true)
        {
            Status[,] newGrid = huidigeGrid;
            Random rnd = new Random();
            bool aanAf = true; ;

            for (int rij = oRij; ((rij < huidigeGrid.GetLength(0))&&(rij <= bRij)); rij++)
            {
                for (int kolom = oKol; ((kolom < huidigeGrid.GetLength(1))&&(kolom <= bKol)); kolom++)
                {
                    if (!aanEnAf)
                        newGrid[rij, kolom] = verander;
                    else
                    {
                        if (aanAf)
                        {
                            newGrid[rij, kolom] = Status.Alive;
                            aanAf = false;
                        }
                        else 
                        {
                            newGrid[rij, kolom] = Status.Dead;
                            aanAf = true;
                        }
                    }
                }
            }
            return newGrid;
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
        static ConsoleKeyInfo InputKey(string tekst = "Keuze: ")
        {
            Console.Write(tekst);
            ConsoleKeyInfo keyStrike = Console.ReadKey(true);
            return keyStrike;

        }
    }

}
