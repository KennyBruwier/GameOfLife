using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using TDSconsoleMuisEnToetsenbordEvents;

//==============================================================================================

// -- in samenwerking met Tom Dilen! (merci!)
// -- presenteren we:
// 

/// <summary>
/// --------------
/// what is new:
/// --------------
/// GameOfLife is nu een business-object, maw... hierin wordt niets getekend, dit gebeurd nu allemaal
/// via het observer-pattern dat de resultaten naar de myUi classe stuurt.
/// 
/// gameObjecten worden geladen uit bestanden, voeg je een bestand toe, dan past het hele prog zich 
/// aan naar gelang de files, alles gebeurd automatisch, zoals het hoort
/// 
/// </summary>
/// 
/// -- Parking 
/// 
//static bool[,] ArrayFlipDim(bool[,] toReverse)
//{
//    bool[,] newArray = new bool[toReverse.GetLength(1), toReverse.GetLength(0)];
//    for (int i = 0; i < toReverse.GetLength(0); i++)
//    {
//        for (int j = 0; j < toReverse.GetLength(1); j++)
//        {
//            newArray[j, i] = toReverse[i, j];
//        }
//    }
//    return newArray;
//}
//static bool[,] ArrayReverseDim(bool[,] toReverse, bool reverseDim1 = true)
//{
//    bool[,] newArray = new bool[toReverse.GetLength(0), toReverse.GetLength(1)];
//    for (int i = 0; i < toReverse.GetLength(0); i++)
//        if (reverseDim1)
//            for (int j = 0; j < toReverse.GetLength(1); j++)
//                newArray[i, newArray.GetLength(1) - 1 - j] = toReverse[i, j];
//        else
//            for (int j = 0; j < toReverse.GetLength(1); j++)
//                newArray[newArray.GetLength(0) - 1 - i, j] = toReverse[i, j];
//    return newArray;
//}
namespace GameOfLife
{
    public enum ShowStyle
    {
        Xsmall =48,
        Small = 64,
        Medium = 128,
        Large = 200,
        Xlarge = 400,
        XXlarge = 800,
        XXXlarge = 1600,
    }
    //===============================================================================================
    class Program 
    {
        //deze static class Program deed moeilijk over het implementeren van de interface
        //IGameOfLifeObserver, daarom maar ff via omweggeske.

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;


            ////eerst hier wat show verkopen
            //Console.WriteLine("Kenny");
            ////Thread.Sleep(500);
            //Console.WriteLine("En");
            ////Thread.Sleep(500);
            //Console.WriteLine("Tom");
            ////Thread.Sleep(500);
            //Console.WriteLine("Presents....");
            ////Thread.Sleep(500);
            //Console.WriteLine("The Game of Life");

            ////hier menuke kiezen

            //Console.ReadKey();

            ShowStyle style = ShowStyle.Small;

            //hier nog menuke welke showstyle

            new myUi(style);
        }
    }
    //===============================================================================================
    public class myUi : IGameOfLifeObserver
    {
        //int snelheid = 50;
        //bool runSimulator = true;
        //int aanAfTeller = 0;
        private const string APP_TITTLE = "The Great \"Game of life\" By Tom Dilen and Kenny Bruwier   ";
        private const char _alive = '☻';
        private const char _dead = ' ';
        private const int BORDER_TOP = 2;
        private const int BORDER_BOTTOM = 2;
        private const int BORDER_RIGHT = 25;
        private const int BORDER_LEFT = 4;

        private GameOfLife myGameOfLife;
        private object myTekenLocker = new object();
        private List<GOLgameObject> myLstOfGameObjects;
        private double countAlive;
        private double countGem;
        const int AvgLength = 100;
        private double[] countAvg = new double[AvgLength];
        private int iAvg = 0;
        private int iFrames;
        private int _benchmarkTeller = 0;
        private const int BENCHMARK_PART = 60;
        Stopwatch watch = new Stopwatch();


        private StringBuilder[] tekenBuffer;

        //================================================================================ ctor
        public myUi(ShowStyle showStyle)
        {

            int KOLOMMEN = 900;
            int RIJEN = 900;


            //TDSwindow.SetCurrentFont("Consolas", 12);
            Console.Title = APP_TITTLE;

            switch (showStyle)
            {
                case ShowStyle.Xsmall:
                    TDSwindow.SetCurrentFont("Consolas", 18);
                    break;
                case ShowStyle.Small:
                    TDSwindow.SetCurrentFont("Consolas", 16);
                    break;
                case ShowStyle.Medium:
                    TDSwindow.SetCurrentFont("Consolas", 14);
                    break;
                case ShowStyle.Large:
                    TDSwindow.SetCurrentFont("Consolas", 12);
                    break;
                case ShowStyle.Xlarge:
                    TDSwindow.SetCurrentFont("Consolas", 10);
                    break;
                case ShowStyle.XXlarge:
                    TDSwindow.SetCurrentFont("Consolas", 8);
                    break;
                case ShowStyle.XXXlarge:
                    TDSwindow.SetCurrentFont("Consolas", 6);
                    break;
                default:
                    break;
            }
            RIJEN = (int)showStyle/2;
            KOLOMMEN = (int)showStyle;







            #region-------------------------------------------------screen and array calculation
            //hoogte en breedte van het scherm aanpassen, 
            //indien nodig de kolommen en rijen inkrimpen
            //----------------------------------------------------------------------------------
            int tmpBreedte = KOLOMMEN + BORDER_RIGHT + BORDER_LEFT;
            int tmpHoogte = RIJEN  + BORDER_TOP + BORDER_BOTTOM;

            if(tmpBreedte > Console.LargestWindowWidth)
            {
                KOLOMMEN = Console.LargestWindowWidth - BORDER_RIGHT - BORDER_LEFT;
                tmpBreedte = Console.LargestWindowWidth;
            }
            if(tmpHoogte > Console.LargestWindowHeight)
            {
                RIJEN = Console.LargestWindowHeight - BORDER_TOP - BORDER_BOTTOM;
                tmpHoogte = Console.LargestWindowHeight;
            }
            TDSwindow.SetFixedWindow(tmpBreedte, tmpHoogte);
            #endregion--------------------------------------------------------------------------


            var consoleWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            TDSwindow.SetWindowPos(consoleWnd, 0, 0, 0, 0, 0, TDSwindow.SWP_NOSIZE | TDSwindow.SWP_NOZORDER);

            //----------------------------------------------------------------------------
            //tekenbuffer initialisatie
            //----------------------------------------------------------------------------
            tekenBuffer = new StringBuilder[RIJEN]; //
            for (int rij = 0; rij < RIJEN; rij++)
            {
                tekenBuffer[rij] = new StringBuilder(new String(' ', KOLOMMEN));
            }
            //----------------------------------------------------------------------------

            //instantie maken van de geweldige GameOfLife classe
            myGameOfLife = new GameOfLife(this, RIJEN, KOLOMMEN, 20 , @"GameObjects");

        }



        #region=========================================================================== IGameOfLifeObserver methods
        public void OnGameOfLifeLoadedComplete(List<GOLgameObject> aLstOfGeneratedGameObjects, double aFps)
        {
            TDSconsoleMuisEnToetsenbord.StartEvents();
            TDSconsoleMuisEnToetsenbord.GetInstance().OnMuisKlik += Program_OnMuisKlik;
            TDSconsoleMuisEnToetsenbord.GetInstance().OnToetsIngedrukt += Program_OnToetsIngedrukt;
            TDSconsoleMuisEnToetsenbord.GetInstance().OnMuisScroll += Program_OnMuisScroll;

            //Thread.Sleep(1000);//nog wat show verkopen :-)

            Console.CursorVisible = false;

            //Console.Title = APP_TITTLE + string.Format("{0,10}: {1,-10} {2,-10}","Fps",aFps);
            this.myLstOfGameObjects = aLstOfGeneratedGameObjects;

            //myGameOfLife.CurrentSelectedGameObject = 4;
            TekenMenu();
            watch.Start();

            //en runnen lek zot die handel :-)
            myGameOfLife.Run();
        }
        public void OnNextGeneration(GOLcellObject[,] aNewRaster)
        {
            countAlive = 0;
            //stringbuffers opnieuw initialiseren
            for (int rij = 0; rij < aNewRaster.GetLength(0); rij++)
            {
                for (int kolom = 0; kolom < aNewRaster.GetLength(1); kolom++)
                {
                    tekenBuffer[rij][kolom] = aNewRaster[rij, kolom].CurrentAlive ? aNewRaster[rij, kolom].Alive : aNewRaster[rij, kolom].Dead;
                    if (aNewRaster[rij, kolom].CurrentAlive) countAlive++;
                }
            }

            //locken en tekenen die handel, nog niet 100% content, 
            //bij het gebruik van het menu met de pijltjes flikkert
            //het geheel heel af en toe, UPDATE: toch opgelost, die console.bacgroundcolor
            //en foregroundcolor stond buiten de lock, djeeeeezuuuuus
            lock (myTekenLocker)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                for (int rij = 0; rij < aNewRaster.GetLength(0); rij++)
                {
                    Console.SetCursorPosition(BORDER_LEFT, BORDER_TOP + rij);
                    Console.Write(tekenBuffer[rij].ToString());
                }
                Console.ResetColor();
            }
            _benchmarkTeller++;
            if (_benchmarkTeller > BENCHMARK_PART)
            {
                watch.Stop();
                Console.Title = "fps: " + (1000 / (watch.ElapsedMilliseconds / BENCHMARK_PART) + " # rijen: " + RIJEN + " # kolommen: " + KOLOMMEN); ;
                _benchmarkTeller = 0;
                watch.Reset();

                watch.Start();
            }
        }

        public void OnNextGeneration(bool[,] aNewRaster)
        {
            countAlive = 0;
            //stringbuffers opnieuw initialiseren
            for (int rij = 0; rij < aNewRaster.GetLength(0); rij++)
            {
                for (int kolom = 0; kolom < aNewRaster.GetLength(1); kolom++)
                {
                    tekenBuffer[rij][kolom] = aNewRaster[rij, kolom] ? _alive : _dead;
                    if (aNewRaster[rij, kolom]) countAlive++;
                }
            }
            
            //locken en tekenen die handel, nog niet 100% content, 
            //bij het gebruik van het menu met de pijltjes flikkert
            //het geheel heel af en toe, UPDATE: toch opgelost, die console.bacgroundcolor
            //en foregroundcolor stond buiten de lock, djeeeeezuuuuus
            lock (myTekenLocker)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                for (int rij = 0; rij < aNewRaster.GetLength(0); rij++)
                {
                    Console.SetCursorPosition(BORDER_LEFT, BORDER_TOP + rij);
                    Console.Write(tekenBuffer[rij].ToString());
                }
                Console.ResetColor();
            }
            _benchmarkTeller++;
            if (_benchmarkTeller > BENCHMARK_PART)
            {
                watch.Stop();
                Console.Title = "fps: " + (1000 / (watch.ElapsedMilliseconds / BENCHMARK_PART) + " # rijen: " + RIJEN + " # kolommen: " + KOLOMMEN); ;
                _benchmarkTeller = 0;
                watch.Reset();

                watch.Start();
            }
        }


        public void OnGameCountChanged(double aFps, bool calibrate = false)
        {
            const int iFramesReset = 500; // ingame "cycles"

            int iGameSec = Convert.ToInt32(iFrames / aFps);
            iFrames = (iFrames > aFps * iFramesReset) ? 0 : iFrames + 1;
            countAvg[iAvg] = countAlive;
            iAvg = (iAvg > AvgLength - 2) ? 0 : iAvg + 1;
            if (iFrames % Math.Round(aFps,0) == 0)
                countGem = countAvg.Average();
            string title = string.Format(   "Fps: {0,-5:0.0} " +
                                            "frame: {1,-7} " +
                                            "game sec: {2,-5}", aFps, iFrames, iGameSec);
            title+= string.Format(  "{2,2}: {0,-7}" +
                                    "gem {2,2}: {1,-9:0.0}", countAlive, countGem, _alive) + APP_TITTLE;


            Console.Title = title;

        }

        public void OnGameSpeedChanged(double aFps)
        {
            //Console.Title = APP_TITTLE + string.Format("{0,10}: {1,-10:0.0} alive: {2,-10}", "Fps", aFps, countAlive);
            //Debug.WriteLine("new Fps: " + aFps);
        }

        #endregion====================================================================================================




        #region=============================================================================muis en toetsenbord events
        private void Program_OnMuisScroll(object sender, TDSmiddenMuisScrollEventArgs e)
        {
            if (e.MiddenMuisscrollRichting == MiddenMuisScroll.Boven)
                myGameOfLife.FPS *=1.2;
            else
                myGameOfLife.FPS *=0.8;
        }
        private void Program_OnToetsIngedrukt(object sender, TDStoetsenbordEventArgs e)
        {
            switch (e.ConsoleKey)
            {
                case ConsoleKey.Enter:
                    {
                        //drawSquar = true;
                    }
                    break;
                case ConsoleKey.UpArrow: 
                    {
                        //snelheid += (snelheid - 10) > 0 ? -10 : 0;
                        //myGameOfLife.Speed = snelheid;
                        //UpdateSpeedText();
                        if (myGameOfLife.CurrentSelectedGameObject > 0)
                        {
                            myGameOfLife.CurrentSelectedGameObject--;
                            TekenMenu();
                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    {
                        //snelheid += (snelheid + 10) < 1000 ? +10 : 0;
                        //myGameOfLife.Speed = snelheid;
                        //UpdateSpeedText();
                        if (myGameOfLife.CurrentSelectedGameObject < myLstOfGameObjects.Count-1)
                        {
                            myGameOfLife.CurrentSelectedGameObject++;
                            TekenMenu();
                        }
                    }
                    break;
                case ConsoleKey.Delete:
                    {
                        myGameOfLife.ClearRaster();
                    }
                    break;
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    {
                        //myGameOfLife.myObjects = Objects2.Glider;
                    }
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    {
                        //myGameOfLife.myObjects = Objects2.Lwss;
                    }
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    {
                        //myGameOfLife.myObjects = Objects2.Mwss;
                    }
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    {
                        //myGameOfLife.myObjects = Objects2.Hwss;
                    }
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    {
                        //myGameOfLife.myObjects = Objects2.Pulsar;
                    }
                    break;
                case ConsoleKey.NumPad6:
                case ConsoleKey.D6:
                    {
                        //myGameOfLife.myObjects = Objects2._119P4H1V0;
                    }
                    break;
                case ConsoleKey.NumPad7:
                case ConsoleKey.D7:
                    {
                        //myGameOfLife.myObjects = Objects2.Glidergun;
                    }
                    break;
                case ConsoleKey.P:
                    {
                        myGameOfLife.IsPause = !myGameOfLife.IsPause;
                    }
                    break;
            }
        }
        private  void Program_OnMuisKlik(object sender, TDSmuisKlikEventArgs e)
        {
            myGameOfLife.InsertSelectedGameObject(e.Muis.Xpositie, e.Muis.Ypositie);
        }

        #endregion=========================================================================================================


        private void TekenMenu()
        {
            lock (myTekenLocker)
            {
                for (int i = 0; i < myLstOfGameObjects.Count; i++)
                {
                    Console.ResetColor();
                    if (i == myGameOfLife.CurrentSelectedGameObject)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.SetCursorPosition(Console.WindowWidth - 18, 2 + (i * 2));
                    Console.WriteLine(myLstOfGameObjects[i].Name);
                }
                Console.ResetColor();
            }
        }


        //    int muisX = x;
        //    int muisY = y;


        //        if ((muisX<Kolomen + leftBorder && muisY<Rijen + topBorder) && (muisX > leftBorder && muisY > topBorder))
        //        {
        //            DrawObjectInGrid(grid, lstGameObjects[CurrentSelectedGameObject].Pattern, muisY - topBorder, muisX - leftBorder);
        //          }


        //private int aanAfTeller = 0;
        //string[] keyBindsMenu = { "G A M E  of  L I F E", "L I F E  of  G A M E", "1: Glider", "2: Light-weight spaceship", "3: Middle-weight spaceship", "4: Heavy-weight spaceship", "5: Pulsar", "6: 119P4H1V0", "7: Glider Gun" };

        //string[] title = { "G A M E  of  L I F E", "L I F E  of  G A M E" };



    }
}

