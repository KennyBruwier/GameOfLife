using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using TDSconsoleMuisEvents;
using System.IO;
using System.Runtime.InteropServices;

namespace GameOfLife
{

    enum Status
    {
        Alive,
        Dead
    }
    enum Objects
    {
        Glider,
        Lwss,
        Mwss,
        Hwss,
        Pulsar,
        _119P4H1V0,
        Glidergun
    }
    class Program
    {
        static int snelheid = 50;
        static bool invoer = false;
        static bool muisClick = false;
        static bool runSimulator = true;
        static ConsoleKeyInfo ckiNow;
        static int muisX = 0;
        static int muisY = 0;
        static int aanAfTeller = 0;
        static Objects myObjects = Objects.Glider;

        static void Main(string[] args)
        {
            // -- Merci Tom Dilen!
            TDSconsoleMuis muis = new TDSconsoleMuis();
            TDSwindow tDSwindow = new TDSwindow();

            int h = 63;
            int b = 240;
            TDSwindow.SetFixedWindow(h>Console.LargestWindowWidth? Console.LargestWindowWidth : h, b > Console.LargestWindowHeight?Console.LargestWindowHeight:b);
            muis.OnMuisKlik += Muis_OnMuisKlik;

            Thread gameOfLife1 = new Thread(()=>GameOfLife(50,100,0,0));
            gameOfLife1.Name = "game1";
            gameOfLife1.Start();
            //Thread gameOfLife2 = new Thread(() => GameOfLife(16, 16, 30, 50));
            //gameOfLife2.Name = "game2";
            //gameOfLife2.Start();

            Thread tInvoer = new Thread(Invoer);
            tInvoer.Name = "invoer";
            tInvoer.Start();
        }

        private static void Muis_OnMuisKlik(object sender, TDSmuisKlikEventArgs e)
        {
            
            muisClick = false;
            muisX = e.Muis.Xpositie;
            muisY = e.Muis.Ypositie;
            muisClick = true;
            invoer = true;
        }

        static void GameOfLife
            (
            int Rijen = 50, 
            int Kolomen = 100, 
            int cursX = 0, 
            int cursY = 0, 
            int leftBorder = 4, 
            int topBorder = 2
            )
        {
            Status[,] grid = new Status[Rijen, Kolomen];
            Console.OutputEncoding = Encoding.Unicode;
            bool clear = false;
            bool[,] glider = { { false, true, false }, { false, false, true }, { true, true, true } };
            bool[,] lWSS = { { false, true, true, true, true }, { true, false, false, false, true }, { false, false, false, false, true }, { true, false, false, true, false } };
            bool[,] mWSS = { { false, true, true, true, true, true }, { true, false, false, false, false, true }, { false, false, false, false, false, true }, { true, false, false, false, true, false }, { false, false, true, false, false, false } };
            bool[,] hWSS = { { false, false, true, true, false, false, false }, { true, false, false, false, false, true, false }, { false, false, false, false, false, false, true }, { true, false, false, false, false, false, true }, { false, true, true, true, true, true, true } };
            bool[,] pulsar = {  { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false },
                                { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false},
                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
                                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                                { true, true, true, false, false, true, true, false, true, true, false, false, true, true, true},
                                { false, false, true, false, true, false, true, false, true, false, true, false, true, false, false},
                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
                                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
                                { false, false, true, false, true, false, true, false, true, false, true, false, true, false, false},
                                { true, true, true, false, false, true, true, false, true, true, false, false, true, true, true},
                                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
                                { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false},
                                { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false }
            };
            bool [,] _119P4H1V0 = { {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false },
                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true },
                                    {false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,false,false,false },
                                    {false,false,false,false,false,false,true,false,false,false,false,true,false,false,false,false,true,false,true,true,true,true,true,true,false,false,false,false,true,true,false,false,false,false,false },
                                    {false,false,false,false,false,false,true,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,true,false,false,true,false,true,true,true,false },
                                    {false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,true,true,true,false,false,false,false,true,true,true,false },
                                    {false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,true,false,false,false,false,false,false,false },
                                    {false,true,false,false,true,true,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false },
                                    {false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                                    {true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                                    {false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                                    {false,true,false,false,true,true,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false },
                                    {false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,true,false,false,false,false,false,false,false },
                                    {false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,true,true,true,false,false,false,false,true,true,true,false },
                                    {false,false,false,false,false,false,true,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,true,false,false,true,false,true,true,true,false },
                                    {false,false,false,false,false,false,true,false,false,false,false,true,false,false,false,false,true,false,true,true,true,true,true,true,false,false,false,false,true,true,false,false,false,false,false },
                                    {false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,false,false,false },
                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true },
                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false }};
            bool[,] gliderGun = {   {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false },
                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false },
                                    {false,false,false,false,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,true,true },
                                    {false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,true,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,true,true },
                                    {true,true,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                                    {true,true,false,false,false,false,false,false,false,false,true,false,false,false,true,false,true,true,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false },
                                    {false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false },
                                    {false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,true,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false },
                                    {false,false,false,false,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false }};

            string[] keyBindsMenu = { "G A M E  of  L I F E", "L I F E  of  G A M E", "1: Glider", "2: Light-weight spaceship", "3: Middle-weight spaceship", "4: Heavy-weight spaceship", "5: Pulsar", "6: 119P4H1V0", "7: Glider Gun" };

            string[] title = { "G A M E  of  L I F E", "L I F E  of  G A M E" };
            
            Console.WindowWidth = 150;
            Console.WindowHeight = 61;

            for (int rij = 0; rij < Rijen; rij++)
            {
                for (int kolom = 0; kolom < Kolomen; kolom++)
                {
                    grid[rij, kolom] = Status.Dead;
                }
            }

            while (runSimulator)
            {
                if (invoer)
                {
                    if (!muisClick)
                    {
                        switch (ckiNow.Key)
                        {
                            case ConsoleKey.Enter:
                                {
                                    //drawSquar = true;
                                }
                                break;
                            case ConsoleKey.UpArrow:
                                {
                                    snelheid += (snelheid - 10) > 0 ? -10 : 0;
                                }
                                break;
                            case ConsoleKey.DownArrow:
                                {
                                    snelheid += (snelheid + 10) < 1000 ? +10 : 0;
                                }
                                break;
                            case ConsoleKey.Delete:
                                {
                                    clear = true;
                                }
                                break;
                            case ConsoleKey.NumPad1:
                            case ConsoleKey.D1:
                                {
                                    myObjects = Objects.Glider;
                                }
                                break;
                            case ConsoleKey.NumPad2:
                            case ConsoleKey.D2:
                                {
                                    myObjects = Objects.Lwss;
                                }
                                break;
                            case ConsoleKey.NumPad3:
                            case ConsoleKey.D3:
                                {
                                    myObjects = Objects.Mwss;
                                }
                                break;
                            case ConsoleKey.NumPad4:
                            case ConsoleKey.D4:
                                {
                                    myObjects = Objects.Hwss;
                                }
                                break;
                            case ConsoleKey.NumPad5:
                            case ConsoleKey.D5:
                                {
                                    myObjects = Objects.Pulsar;
                                }
                                break;
                            case ConsoleKey.NumPad6:
                            case ConsoleKey.D6:
                                {
                                    myObjects = Objects._119P4H1V0;
                                }
                                break;
                            case ConsoleKey.NumPad7:
                            case ConsoleKey.D7:
                                {
                                    myObjects = Objects.Glidergun;
                                }
                                break;
                        }
                    }
                    
                    invoer = false;

                    if (muisClick)
                    {
                        muisClick = false;
                        if ((muisX < Kolomen+leftBorder && muisY < Rijen+topBorder) && (muisX > leftBorder && muisY > topBorder))
                        {
                            switch (myObjects)
                            {
                                case Objects.Glider:
                                    grid = DrawObjectInGrid(grid, glider, muisY-topBorder, muisX-leftBorder);
                                    break;
                                case Objects.Lwss:
                                    grid = DrawObjectInGrid(grid, lWSS, muisY - topBorder, muisX - leftBorder);
                                    break;
                                case Objects.Mwss:
                                    grid = DrawObjectInGrid(grid, mWSS, muisY - topBorder, muisX - leftBorder);
                                    break;
                                case Objects.Hwss:
                                    grid = DrawObjectInGrid(grid, hWSS, muisY - topBorder, muisX - leftBorder);
                                    break;
                                case Objects.Pulsar:
                                    grid = DrawObjectInGrid(grid, pulsar, muisY - topBorder, muisX - leftBorder);
                                    break;
                                case Objects._119P4H1V0:
                                    grid = DrawObjectInGrid(grid, _119P4H1V0, muisY - topBorder, muisX - leftBorder);
                                    break;
                                case Objects.Glidergun:
                                    grid = DrawObjectInGrid(grid, gliderGun, muisY - topBorder, muisX - leftBorder);
                                    break;
                            }
                        }
                    }
                }
                PrintGrid(grid, keyBindsMenu, snelheid, cursX, cursY,leftBorder:leftBorder,topBorder:topBorder);
                Console.SetCursorPosition(leftBorder, grid.GetLength(0) + 4);
                Console.Write("Left mouse click to create");
                Console.Write(string.Format("{0,74}","<UP ARROW> increase speed"));
               
                Console.SetCursorPosition(leftBorder, grid.GetLength(0) + 5);
                Console.Write($"huidige snelheid: {snelheid}");
                Console.Write("<DOWN ARROW> decrease speed".PadLeft(82-snelheid.ToString().Length));
                if (clear)
                {
                    grid = VolgendeGeneratie(grid, true);
                    clear = false;
                }
                else
                {
                    grid = VolgendeGeneratie(grid, false);
                }
            }
        }

        static void Invoer()
        {
            while (runSimulator)
            {
                //invoer = false;
                ckiNow = Console.ReadKey(true);
                invoer = true;
            }
        }
        static Status[,] VolgendeGeneratie(Status[,] huidigeGrid, bool deleteAll = false)
        {
            Status[,] volgendeGeneratie = new Status[huidigeGrid.GetLength(0), huidigeGrid.GetLength(1)];

            for (int rij = 0; rij < huidigeGrid.GetLength(0) ; rij++)
            {
                for (int kolom = 0; kolom < huidigeGrid.GetLength(1) ; kolom++)
                {
                    
                    int burenInLeven = 0;

                    if (!deleteAll)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                burenInLeven += huidigeGrid[
                                    (rij + i) < 1 ?
                                        huidigeGrid.GetLength(0) - 3 - i :
                                        (rij + i) > huidigeGrid.GetLength(0) - 2 ?
                                            0 + i : rij + i,
                                    (kolom + j) < 1 ?
                                        huidigeGrid.GetLength(1) - 3 - j :
                                        (kolom + j) > huidigeGrid.GetLength(1) - 2 ?
                                            0 + j : kolom + j] == Status.Alive ? 1 : 0;
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
                    else volgendeGeneratie[rij, kolom] = Status.Dead;

                }
            }
            return volgendeGeneratie;
        }

        static void PrintGrid(Status[,] toekomst, string[] msg, int timeout = 200, int cursX = 0, int cursY = 0, bool showIndex = false, int leftBorder = 4, int topBorder = 2, char oN = '☻', char oFf = ' ')
        {
            StringBuilder stringBuilder = new StringBuilder();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            
            if (showIndex)
            {
                for (int kolom = 0; kolom < toekomst.GetLength(1); kolom++)
                {
                    for (int rij = 0; rij < 1; rij++)
                    {
                        if (kolom % 2 != 0)
                            stringBuilder.Append(kolom + 1);
                        else if (kolom < 10)
                            stringBuilder.Append(' ');
                    }
                }
                Console.SetCursorPosition(cursY + 4, cursX);
                Console.Write(stringBuilder.ToString());
                stringBuilder.Clear();
                for (int rij = 0; rij < toekomst.GetLength(0); rij++)
                {
                    for (int kolom = 0; kolom < 1; kolom++)
                    {
                        Console.SetCursorPosition(cursY + kolom, cursX + 2 + rij);
                        Console.Write(rij + 1);
                    }
                }
            }
            
            for (int rij = 0; rij < toekomst.GetLength(0); rij++)
            {
                stringBuilder.Clear();
                for (int kolom = 0; kolom < toekomst.GetLength(1); kolom++)
                {
                    stringBuilder.Append(toekomst[rij, kolom] == Status.Alive ? oN : oFf);
                }
                Console.SetCursorPosition(cursY + leftBorder, cursX + topBorder + rij);
                Console.Write(stringBuilder.ToString());
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < msg.GetLength(0); i++)
            {
                aanAfTeller = aanAfTeller - 1 < 0 ? 100 : --aanAfTeller;

               
                if (i==0)
                {
                    if (aanAfTeller > 100/6)
                    {
                        Console.SetCursorPosition(cursY + 4 + toekomst.GetLength(1) + 4, cursX + 2);
                        Console.Write(msg[i]);
                    }
                }
                else if(i==1)
                {
                    if (aanAfTeller < 100 / 6)
                    {
                        Console.SetCursorPosition(cursY + 4 + toekomst.GetLength(1) + 4, cursX + 2);
                        Console.Write(msg[i]);
                    }
                }
                else
                {
                    switch (myObjects)
                    {
                        case Objects.Glider:
                            {
                                if (i==2)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }break;
                        case Objects.Lwss:
                            {
                                if (i == 3)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            break;
                        case Objects.Mwss:
                            {
                                if (i == 4)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            break;
                        case Objects.Hwss:
                            {
                                if (i == 5)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            break;
                        case Objects.Pulsar:
                            {
                                if (i == 6)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            break;
                        case Objects._119P4H1V0:
                            {
                                if (i == 7)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            break;
                        case Objects.Glidergun:
                            {
                                if (i == 8)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            break;
                        default:
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                            }break;
                    }
                    Console.SetCursorPosition(cursY + 4 + toekomst.GetLength(1) + 4, cursX + 2 + (i * 4));
                    Console.Write(msg[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                    

            }
            Console.CursorVisible = false;
            Thread.Sleep(timeout);
        }
        
        static bool[,] ArrayFlipDim(bool[,] toReverse)
        {
            bool[,] newArray = new bool[toReverse.GetLength(1), toReverse.GetLength(0)];
            for (int i = 0; i < toReverse.GetLength(0); i++)
            {
                for (int j = 0; j < toReverse.GetLength(1); j++)
                {
                    newArray[j, i] = toReverse[i, j];
                }
            }
            return newArray;
        }
        static bool[,] ArrayReverseDim(bool[,] toReverse, bool reverseDim1 = true)
        {
            bool[,] newArray = new bool[toReverse.GetLength(0), toReverse.GetLength(1)];
            for (int i = 0; i < toReverse.GetLength(0); i++)
                if (reverseDim1)
                    for (int j = 0; j < toReverse.GetLength(1); j++)
                        newArray[i, newArray.GetLength(1) - 1 - j] = toReverse[i, j];
                else
                    for (int j = 0; j < toReverse.GetLength(1); j++)
                        newArray[newArray.GetLength(0) - 1 - i,  j] = toReverse[i, j];
            return newArray;
        }

        static Status[,] DrawObjectInGrid(Status[,] huidigeGrid, bool[,] toPrint, int oRij = 0, int oKol = 0, bool flipHorizontal = false, bool flipVertical = false)
        {
            Status[,] newGrid = huidigeGrid;
            bool[,] newToPrint = toPrint;

            if (flipHorizontal)
                newToPrint = ArrayReverseDim(newToPrint);
            if (flipVertical)
                newToPrint = ArrayFlipDim(newToPrint);


            int maxKol = ((oKol + newToPrint.GetLength(1)) < huidigeGrid.GetLength(1)) ? (oKol + newToPrint.GetLength(1)) : huidigeGrid.GetLength(1) - 1;
            int maxRij = oRij + newToPrint.GetLength(0);
            int startKol = (oKol + newToPrint.GetLength(1)) > huidigeGrid.GetLength(1) ? huidigeGrid.GetLength(1) - newToPrint.GetLength(1) - 1 : oKol;
            int startRij = (oRij + newToPrint.GetLength(0)) > huidigeGrid.GetLength(0) ? huidigeGrid.GetLength(0) - newToPrint.GetLength(0) - 1 : oRij;
            int printRij = 0;



            for (int rij = startRij  ; rij < maxRij ; rij++)
            {
                int printKol = 0;
                for (int kolom = startKol ; kolom < maxKol; kolom++)
                {
                    if (newToPrint[printRij, (printKol>newToPrint.GetLength(1)-1)?newToPrint.GetLength(1)-1:printKol++]) newGrid[rij, kolom] = Status.Alive;
                    else newGrid[rij, kolom] = Status.Dead;
                }
                printRij++;
            }
            return newGrid;
        }
        static bool DeleteRecordInFile
            (
                string bestandsnaam,
                string recordKey,
                char seperator = '#'
            )
        {
            string searchMsg = SearchDataInRecord(bestandsnaam, recordKey);
            switch (searchMsg)
            {
                case "0": // file found but not record
                case null: return false; // file not found
                default:
                    {
                        string[] accReader = File.ReadAllLines(bestandsnaam);
                        string[] newFile = new string[accReader.GetUpperBound(0)];
                        int count = 0;
                        foreach (string accReaderLine in accReader)
                        {
                            string[] recGegevens = accReaderLine.Split(seperator);
                            if (recGegevens[0] != recordKey)
                                newFile[count++] = accReaderLine;
                        }
                        File.Delete(bestandsnaam);
                        File.WriteAllLines(bestandsnaam, newFile);
                        return true;
                    }
            }
        }
        static bool WriteDataInRecord
            (
                string bestandsnaam,
                string recordKey,
                char seperator = '#',
                bool createIfFileNotFound = false,
                params string[] dataToAdd
            )
        {
            string recordToAdd = recordKey;
            for (int i = 0; i < dataToAdd.Length; i++)
                recordToAdd += seperator + dataToAdd[i];

            if (File.Exists(bestandsnaam))
            {
                FileStream appendFile = File.Open(bestandsnaam, FileMode.Append);
                StreamWriter writer = new StreamWriter(appendFile);
                writer.WriteLine(recordToAdd);
                writer.Close();
                return true;
            }
            else
            {
                if (createIfFileNotFound)
                {
                    using (StreamWriter writer = new StreamWriter(bestandsnaam))
                        writer.WriteLine(recordToAdd);
                    return true;
                }
                else
                    return false;
            }
        }
        static string SearchDataInRecord
            (
                string bestandsnaam,
                string recordKey,
                int cellToReturn = 0,
                char seperator = '#'
            )   /* returns: 
                 *  null when file not found or 
                 *  "0" when record not found
                 */
        {
            if (File.Exists(bestandsnaam))
            {
                string[] accReader = File.ReadAllLines(bestandsnaam);

                foreach (string accReaderLine in accReader)
                {
                    string[] recGegevens = accReaderLine.Split(seperator);
                    if (recGegevens[0] == recordKey)
                        if (cellToReturn <= recGegevens.GetUpperBound(0))
                            return recGegevens[cellToReturn];
                        else
                            return "-1";
                }
            }
            else
                return null;
            return "0";
        }
    }
}
