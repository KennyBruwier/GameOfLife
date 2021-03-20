using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public interface IGameOfLifeObserver
    {
        void OnNextGeneration(bool[,] aNewRaster );
        void OnGameOfLifeLoadedComplete(List<GOLgameObject> aLstOfGeneratedGameObjects, double aFps, double cAlive);
        void OnGameSpeedChanged(double aFps);

        void OnGameCountChanged(double aFps, double cAlive, bool calibrate = false);
    }
}
