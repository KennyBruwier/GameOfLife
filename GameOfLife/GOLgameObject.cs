using System;
using System.IO;

namespace GameOfLife
{
    public class GOLgameObject
    {
        public enum PatternType
        {
            Unknow,
            Stable,
            Oscillators,
            Spaceships
        }
        
        public PatternType PatterType { get; private set; }
        public string Name { get; private set; }
        internal bool[,] Pattern { get; private set; }


        //================================================================================
        internal GOLgameObject(string aFileName)
        {
            //--------------------------------------------Name en PatternType aan object geven
            string[] splittedFilename = 
                Path.GetFileNameWithoutExtension(aFileName).Split('_');
            Name = splittedFilename[2];
            int iTmp = Convert.ToInt32(splittedFilename[1]);

            if (Enum.IsDefined(typeof(PatternType), iTmp))
                PatterType = (PatternType)iTmp;
            else
                PatterType = PatternType.Unknow;
            //--------------------------------------------------------------------------------

            string[] lines = File.ReadAllLines(aFileName);
            int lengteEersteString = lines[0].Length;

            Pattern = new bool[lines.Length, lengteEersteString];

            for (int rij = 0; rij < lines.Length; rij++)
            {
                //chekken dat alle strings lijnen in de file even lang zijn
                if (lengteEersteString != lines[rij].Length)
                    throw new IOException("error in .gol file: there are lines with different length detected in: " + aFileName);

                for (int kol = 0; kol < lines[rij].Length; kol++)
                {
                    if (lines[rij][kol] == '1')
                        Pattern[rij, kol] = true;
                    else if (lines[rij][kol] == '0')
                        Pattern[rij, kol] = false;
                    else
                        throw new IOException( "error in .gol file: there are characters found that are not 0 or 1: " + aFileName);
                }
            }
        }
        public override string ToString() //for testing
        {
            return $"GOLgameObject, Name: {Name} , PatternType: {PatterType}";
        }
    }
}
