using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GameOfLife
{
    public class GameOfLife
    {
        //========================================================================================================
        const double MAX_FPS = 1000; //niet hoger dan 1000zetten (haal je toch nooit)
        const double MIN_FPS = 0.5;
        //--------------------------------------------------------------------------------------only private
        private bool[,] _grid; //vergelijkingen gaan sneller met booleans
        private List<GOLgameObject> _lstGameObjects;
        private bool _runSimulator = true;
        private bool _clear = false;
        private IGameOfLifeObserver _iGameOfLifeObserver = null;
        private int _delay; //altijd deze 2via 
        private double _fps; //propertie FPS instellen
        private bool _isLoadingComplete = false;
        private double _cAlive;
        public double _CAlive {
            get { return _cAlive; }
            set {
                _cAlive = value;

                if (_isLoadingComplete)
                    _iGameOfLifeObserver.OnGameCountChanged(_fps,_cAlive);
            }
        }
        //----------------------------------------------------------------------------- read and write props
        public double FPS {
            get { return _fps; }
            set {
                value = Math.Min(MAX_FPS, value);
                value = Math.Max(MIN_FPS, value);
                _fps = value;
                _delay = (int)(1000 / _fps);
                //enkel afvuren als niet door constructor geset is
                if (_isLoadingComplete)
                    _iGameOfLifeObserver.OnGameSpeedChanged(_fps);
            }
        }
        public bool IsPause { get; set; }
        public int CurrentSelectedGameObject { get;  set; } = 0;
        //====================================================================================================ctor
        public GameOfLife(IGameOfLifeObserver aGameOfLifeObserver, int aRowCount, int aColCount, double aFPS,string aGameObjectDirectory)
        {
            //init interface
            //---------------
            _iGameOfLifeObserver = aGameOfLifeObserver;

            //init fps
            //--------
            FPS = aFPS;

            //grid aanmaken en initialisatie alles Dead van de grid 
            //-----------------------------------------------------
            _grid = new bool[aRowCount, aColCount];
            for (int rij = 0; rij < _grid.GetLength(0); rij++)
                for (int kolom = 0; kolom < _grid.GetLength(1); kolom++)
                    _grid[rij, kolom] = false;

            //list gameObjects vullen
            //-----------------------
            _lstGameObjects = new List<GOLgameObject>();

            //aanmaken gameobjects (constructor verwacht filename)
            //----------------------------------------------------
            string[] fileNames = Directory.GetFiles(aGameObjectDirectory, "*.gol");
            Array.Sort(fileNames); //denk niet dat die sort echt nodig is nu?
            foreach (var item in fileNames)
                _lstGameObjects.Add(new GOLgameObject(@item));

            new Thread(completeSender).Start();
        }
        //========================================================================================================
        //is nodig omdat instantie in observer anders nog naar null verwijst in dit object
        private void completeSender()
        {
            Thread.Sleep(100);

            //afvuren naar observer
            //---------------------
            _iGameOfLifeObserver.OnGameOfLifeLoadedComplete(_lstGameObjects, FPS, _cAlive);
            _isLoadingComplete = true;
        }
        //========================================================================================================
        public void Run()
        {
            //threadmethode starten
            //---------------------
            new Thread(GameOfLifeThreadDelegate).Start();
        }
        //========================================================================================================
        public void ClearRaster()
        {
            _clear = true; //wordt afgehandeld in  de threadmethode
        }
        //=========================================================================================================
        public void InsertSelectedGameObject(int x, int y)
        {
            DrawObjectInGrid(_grid, _lstGameObjects[CurrentSelectedGameObject].Pattern, y, x);
        }
        //========================================================================================================
        private void GameOfLifeThreadDelegate()
        {
            while (_runSimulator)
            {
                if (! IsPause) { 
                    //ipv van hier te tekenen sturen we het nu naar de observer 
                    _iGameOfLifeObserver.OnNextGeneration(_grid);
                    _iGameOfLifeObserver.OnGameCountChanged(_fps,_cAlive);
                    if (_clear)
                    {
                        KillAllCells();
                        _clear = false;
                    }
                    else
                    {
                        VolgendeGeneratie();
                    }
                    Thread.Sleep(_delay);
                }
                else
                {
                    Thread.Sleep(200);
                }

            }
        } 
        //======================================================================================================================
        private void KillAllCells(/*bool[,] huidigeGrid*/)
        {
            //kill ze allemaal
            for (int rij = 0; rij < _grid.GetLength(0); rij++)
                for (int kolom = 0; kolom < _grid.GetLength(1); kolom++)
                    _grid[rij, kolom] = false;
        }
        //=======================================================================================================================
        
        private void VolgendeGeneratie()
        {
            bool[,] volgendeGeneratie = new bool[_grid.GetLength(0), _grid.GetLength(1)];

            _cAlive = 0;
            for (int rij = 0; rij < _grid.GetLength(0); rij++)
            {
                for (int kolom = 0; kolom < _grid.GetLength(1); kolom++)
                {
                    int burenInLeven = 0;

                    int buurRij;
                    for (int i = -1; i <= 1; i++)
                    {
                        buurRij = (rij + i) < 1 ?
                                    _grid.GetLength(0) - 3 - i :
                                    (rij + i) > _grid.GetLength(0) - 2 ?
                                        0 + i : rij + i;
                        for (int j = -1; j <= 1; j++)
                        {
                            // kan beter: ifs eruit halen
                            burenInLeven += _grid[
                                buurRij,
                                (kolom + j) < 1 ?
                                    _grid.GetLength(1) - 3 - j :
                                    (kolom + j) > _grid.GetLength(1) - 2 ?
                                        0 + j : kolom + j]  ? 1 : 0;
                        }
                    }
                    
                    //if (huidigeGrid[rij - 1 ,   kolom-1] == Status.Alive) burenInLeven++;
                    //if (huidigeGrid[rij     ,   kolom-1] == Status.Alive) burenInLeven++;
                    //if (huidigeGrid[rij + 1 ,   kolom-1] == Status.Alive) burenInLeven++;
                    //if (huidigeGrid[rij - 1 ,   kolom] == Status.Alive) burenInLeven++;
                    //if (huidigeGrid[rij + 1 ,   kolom] == Status.Alive) burenInLeven++;
                    //if (huidigeGrid[rij - 1 ,   kolom + 1] == Status.Alive) burenInLeven++;
                    //if (huidigeGrid[rij     ,   kolom + 1] == Status.Alive) burenInLeven++;
                    //if (huidigeGrid[rij + 1 ,   kolom + 1] == Status.Alive) burenInLeven++;

                    bool huidigeCel = _grid[rij, kolom];
                    burenInLeven -= huidigeCel  ? 1 : 0;

                    if (huidigeCel && burenInLeven < 2)
                        volgendeGeneratie[rij, kolom] = false;
                    else if (huidigeCel && burenInLeven > 3)
                        volgendeGeneratie[rij, kolom] = false;
                    else if ( ! huidigeCel && burenInLeven == 3)
                        volgendeGeneratie[rij, kolom] = true;
                    else volgendeGeneratie[rij, kolom] = huidigeCel;
                    _cAlive += volgendeGeneratie[rij, kolom] ? 1 : 0;
                    if (_cAlive > 0)
                    {
                        string temp = "";
                    }
                }
            }
            _grid = volgendeGeneratie;
        }
        //============================================================================================================================
        private void DrawObjectInGrid(bool[,] huidigeGrid, bool[,] toPrint, int oRij = 0, int oKol = 0)
        {
            bool[,] newGrid = huidigeGrid;
            bool[,] newToPrint = toPrint;

            int maxKol = (oKol + newToPrint.GetLength(1)) < huidigeGrid.GetLength(1) ? oKol + newToPrint.GetLength(1) : huidigeGrid.GetLength(1) - 1;
            int maxRij = (oRij + newToPrint.GetLength(0)) < huidigeGrid.GetLength(0) ? oRij + newToPrint.GetLength(0) : huidigeGrid.GetLength(0) - 1;
            int startKol = (oKol + newToPrint.GetLength(1)) > huidigeGrid.GetLength(1) ? huidigeGrid.GetLength(1) - newToPrint.GetLength(1) - 1 : oKol;
            int startRij = (oRij + newToPrint.GetLength(0)) > huidigeGrid.GetLength(0) ? huidigeGrid.GetLength(0) - newToPrint.GetLength(0) - 1 : oRij;
            int printRij = 0;

            for (int rij = startRij; rij < maxRij; rij++)
            {
                int printKol = 0;
                for (int kolom = startKol; kolom < maxKol; kolom++)
                    if (newToPrint[printRij, (printKol > newToPrint.GetLength(1) - 1) ? newToPrint.GetLength(1) - 1 : printKol++]) newGrid[rij, kolom] = true;
                    else newGrid[rij, kolom] = false;
                printRij++;
            }
        }
    }
}
