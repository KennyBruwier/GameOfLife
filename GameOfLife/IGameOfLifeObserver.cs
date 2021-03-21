using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public interface IGameOfLifeObserver
    {
        //void OnNextGeneration(bool[,] aNewRaster );
        void OnNextGeneration(GOLcellObject[,] aNewRaster);
        void OnGameOfLifeLoadedComplete(List<GOLgameObject> aLstOfGeneratedGameObjects, double aFps);
        //void OnGameSpeedChanged(double aFps);

        void OnGameCountChanged(double aFps, bool calibrate = false);
    }
}
