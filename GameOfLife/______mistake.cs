using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class ______mistake
    {
        //========================================================================================================
        //Kenny, dit is zwaar overheaded.... je test kolommen maal rijen keer 
        //op deleteAll... hier kan je veel beter 2 forlussen voor maken,
        // is goed bedoeld he ;-)
        //========================================================================================================
        //private Status[,] VolgendeGeneratie(Status[,] huidigeGrid, bool deleteAll = false)
        //{
        //    Status[,] volgendeGeneratie = new Status[huidigeGrid.GetLength(0), huidigeGrid.GetLength(1)];

        //    for (int rij = 0; rij < huidigeGrid.GetLength(0); rij++)
        //    {
        //        for (int kolom = 0; kolom < huidigeGrid.GetLength(1); kolom++)
        //        {
        //            int burenInLeven = 0;

        //            if (!deleteAll)  //  !!!!!!!!!!!!!!!!!!!! dit bedoel ik !!!!!!!!!!!!!!!!!!!!
        //            {
        //                int buurRij;
        //                for (int i = -1; i <= 1; i++)
        //                {
        //                    buurRij = (rij + i) < 1 ?
        //                                huidigeGrid.GetLength(0) - 3 - i :
        //                                (rij + i) > huidigeGrid.GetLength(0) - 2 ?
        //                                    0 + i : rij + i;
        //                    for (int j = -1; j <= 1; j++)
        //                    {
        //                        // kan beter: ifs eruit halen
        //                        burenInLeven += huidigeGrid[
        //                            buurRij,
        //                            (kolom + j) < 1 ?
        //                                huidigeGrid.GetLength(1) - 3 - j :
        //                                (kolom + j) > huidigeGrid.GetLength(1) - 2 ?
        //                                    0 + j : kolom + j] == Status.Alive ? 1 : 0;
        //                    }
        //                }

        //                Status huidigeCel = huidigeGrid[rij, kolom];
        //                burenInLeven -= huidigeCel == Status.Alive ? 1 : 0;

        //                if (huidigeCel == Status.Alive && burenInLeven < 2)
        //                    volgendeGeneratie[rij, kolom] = Status.Dead;
        //                else if (huidigeCel == Status.Alive && burenInLeven > 3)
        //                    volgendeGeneratie[rij, kolom] = Status.Dead;
        //                else if (huidigeCel == Status.Dead && burenInLeven == 3)
        //                    volgendeGeneratie[rij, kolom] = Status.Alive;
        //                else volgendeGeneratie[rij, kolom] = huidigeCel;
        //            }
        //            else volgendeGeneratie[rij, kolom] = Status.Dead;

        //        }
        //    }
        //    return volgendeGeneratie;
        //}



    }
}
