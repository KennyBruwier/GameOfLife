using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class __ZZZZ____deletedCode
    {
        //static bool DeleteRecordInFile
        //    (
        //        string bestandsnaam,
        //        string recordKey,
        //        char seperator = '#'
        //    )
        //{
        //    string searchMsg = SearchDataInRecord(bestandsnaam, recordKey);
        //    switch (searchMsg)
        //    {
        //        case "0": // file found but not record
        //        case null: return false; // file not found
        //        default:
        //            {
        //                string[] accReader = File.ReadAllLines(bestandsnaam);
        //                string[] newFile = new string[accReader.GetUpperBound(0)];
        //                int count = 0;
        //                foreach (string accReaderLine in accReader)
        //                {
        //                    string[] recGegevens = accReaderLine.Split(seperator);
        //                    if (recGegevens[0] != recordKey)
        //                        newFile[count++] = accReaderLine;
        //                }
        //                File.Delete(bestandsnaam);
        //                File.WriteAllLines(bestandsnaam, newFile);
        //                return true;
        //            }
        //    }
        //}
        //static bool WriteDataInRecord
        //    (
        //        string bestandsnaam,
        //        string recordKey,
        //        char seperator = '#',
        //        bool createIfFileNotFound = false,
        //        params string[] dataToAdd
        //    )
        //{
        //    string recordToAdd = recordKey;
        //    for (int i = 0; i < dataToAdd.Length; i++)
        //        recordToAdd += seperator + dataToAdd[i];

        //    if (File.Exists(bestandsnaam))
        //    {
        //        FileStream appendFile = File.Open(bestandsnaam, FileMode.Append);
        //        StreamWriter writer = new StreamWriter(appendFile);
        //        writer.WriteLine(recordToAdd);
        //        writer.Close();
        //        return true;
        //    }
        //    else
        //    {
        //        if (createIfFileNotFound)
        //        {
        //            using (StreamWriter writer = new StreamWriter(bestandsnaam))
        //                writer.WriteLine(recordToAdd);
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //}
        //    static string SearchDataInRecord
        //        (
        //            string bestandsnaam,
        //            string recordKey,
        //            int cellToReturn = 0,
        //            char seperator = '#'
        //        )   /* returns: 
        //             *  null when file not found or 
        //             *  "0" when record not found
        //             */
        //    {
        //        if (File.Exists(bestandsnaam))
        //        {
        //            string[] accReader = File.ReadAllLines(bestandsnaam);

        //            foreach (string accReaderLine in accReader)
        //            {
        //                string[] recGegevens = accReaderLine.Split(seperator);
        //                if (recGegevens[0] == recordKey)
        //                    if (cellToReturn <= recGegevens.GetUpperBound(0))
        //                        return recGegevens[cellToReturn];
        //                    else
        //                        return "-1";
        //            }
        //        }
        //        else
        //            return null;
        //        return "0";
        //    }

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




        //                   if (showIndex)
        //            {
        //                for (int kolom = 0; kolom<toekomst.GetLength(1); kolom++)
        //                {
        //                    for (int rij = 0; rij< 1; rij++)
        //                    {
        //                        if (kolom % 2 != 0)
        //                            stringBuilder.Append(kolom + 1);
        //                        else if (kolom< 10)
        //                            stringBuilder.Append(' ');
        //                    }
        //}
        //Console.SetCursorPosition(cursY + 4, cursX);
        //Console.Write(stringBuilder.ToString());
        //stringBuilder.Clear();
        //for (int rij = 0; rij < toekomst.GetLength(0); rij++)
        //{
        //    for (int kolom = 0; kolom < 1; kolom++)
        //    {
        //        Console.SetCursorPosition(cursY + kolom, cursX + 2 + rij);
        //        Console.Write(rij + 1);
        //    }
        //}
        //            }




        //#region ==============================================================================================plusars etc
        ////hier zou ik persoonlijk objecten van maken en in lijst stoempen

        ////al gebeurd :) ... nog even en ik kan de file ook terug inlezen
        ////en we zijn van die wall of true of false af + tekenen in excel...

        ////static bool clear = false;
        //static bool[,] glider = { { false, true, false }, { false, false, true }, { true, true, true } };
        //static bool[,] lWSS = { { false, true, true, true, true }, { true, false, false, false, true }, { false, false, false, false, true }, { true, false, false, true, false } };
        //static bool[,] mWSS = { { false, true, true, true, true, true }, { true, false, false, false, false, true }, { false, false, false, false, false, true }, { true, false, false, false, true, false }, { false, false, true, false, false, false } };
        //static bool[,] hWSS = { { false, false, true, true, false, false, false }, { true, false, false, false, false, true, false }, { false, false, false, false, false, false, true }, { true, false, false, false, false, false, true }, { false, true, true, true, true, true, true } };
        //static bool[,] pulsar = {  { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false },
        //                        { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false},
        //                        { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                        { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //                        { true, true, true, false, false, true, true, false, true, true, false, false, true, true, true},
        //                        { false, false, true, false, true, false, true, false, true, false, true, false, true, false, false},
        //                        { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                        { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //                        { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                        { false, false, true, false, true, false, true, false, true, false, true, false, true, false, false},
        //                        { true, true, true, false, false, true, true, false, true, true, false, false, true, true, true},
        //                        { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //                        { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                        { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false},
        //                        { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false }
        //    };
        //static bool[,] _119P4H1V0 = { {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false },
        //                            {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true },
        //                            {false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,false,false,false },
        //                            {false,false,false,false,false,false,true,false,false,false,false,true,false,false,false,false,true,false,true,true,true,true,true,true,false,false,false,false,true,true,false,false,false,false,false },
        //                            {false,false,false,false,false,false,true,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,true,false,false,true,false,true,true,true,false },
        //                            {false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,true,true,true,false,false,false,false,true,true,true,false },
        //                            {false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,true,false,false,false,false,false,false,false },
        //                            {false,true,false,false,true,true,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false },
        //                            {false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                            {true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                            {false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                            {false,true,false,false,true,true,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false },
        //                            {false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,true,false,false,false,false,false,false,false },
        //                            {false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,true,true,true,false,false,false,false,true,true,true,false },
        //                            {false,false,false,false,false,false,true,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,true,false,false,true,false,true,true,true,false },
        //                            {false,false,false,false,false,false,true,false,false,false,false,true,false,false,false,false,true,false,true,true,true,true,true,true,false,false,false,false,true,true,false,false,false,false,false },
        //                            {false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,false,false,false },
        //                            {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true },
        //                            {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false }};
        //static bool[,] gliderGun = {   {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                            {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                            {false,false,false,false,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,true,true },
        //                            {false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,true,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,true,true },
        //                            {true,true,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                            {true,true,false,false,false,false,false,false,false,false,true,false,false,false,true,false,true,true,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                            {false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                            {false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,true,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false },
        //                            {false,false,false,false,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false }};

        //#endregion ================================================================================================

        //
        // vanuit de methode DrawObjectInGrid, denk niet dat er een referentie naar was
        //

        //if (flipHorizontal)
        //    newToPrint = ArrayReverseDim(newToPrint);
        //if (flipVertical)
        //    newToPrint = ArrayFlipDim(newToPrint);





        //switch (myObjects)
        //{
        //    case Objects2.Glider:
        //        DrawObjectInGrid(grid, glider, muisY - topBorder, muisX - leftBorder);
        //        break;
        //    case Objects2.Lwss:
        //        DrawObjectInGrid(grid, lWSS, muisY - topBorder, muisX - leftBorder);
        //        break;
        //    case Objects2.Mwss:
        //        DrawObjectInGrid(grid, mWSS, muisY - topBorder, muisX - leftBorder);
        //        break;
        //    case Objects2.Hwss:
        //        DrawObjectInGrid(grid, hWSS, muisY - topBorder, muisX - leftBorder);
        //        break;
        //    case Objects2.Pulsar:
        //        DrawObjectInGrid(grid, pulsar, muisY - topBorder, muisX - leftBorder);
        //        break;
        //    case Objects2._119P4H1V0:
        //        DrawObjectInGrid(grid, _119P4H1V0, muisY - topBorder, muisX - leftBorder);
        //        break;
        //    case Objects2.Glidergun:
        //        DrawObjectInGrid(grid, gliderGun, muisY - topBorder, muisX - leftBorder);
        //        break;
        //}






        //Console.BackgroundColor = ConsoleColor.Black;
        //Console.ForegroundColor = ConsoleColor.White;
        //for (int i = 0; i < msg.GetLength(0); i++)
        //{
        //    aanAfTeller = aanAfTeller - 1 < 0 ? 100 : --aanAfTeller;


        //    if (i == 0)
        //    {
        //        if (aanAfTeller > 100 / 6)
        //        {
        //            Console.SetCursorPosition(cursY + 4 + toekomst.GetLength(1) + 4, cursX + 2);
        //            Console.Write(msg[i]);
        //        }
        //    }
        //    else if (i == 1)
        //    {
        //        if (aanAfTeller < 100 / 6)
        //        {
        //            Console.SetCursorPosition(cursY + 4 + toekomst.GetLength(1) + 4, cursX + 2);
        //            Console.Write(msg[i]);
        //        }
        //    }
        //    else
        //    {
        //        switch (myObjects)
        //        {
        //            case Objects2.Glider:
        //                {
        //                    if (i == 2)
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.White;
        //                        Console.ForegroundColor = ConsoleColor.Black;
        //                    }
        //                    else
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.Black;
        //                        Console.ForegroundColor = ConsoleColor.White;
        //                    }
        //                }
        //                break;
        //            case Objects2.Lwss:
        //                {
        //                    if (i == 3)
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.White;
        //                        Console.ForegroundColor = ConsoleColor.Black;
        //                    }
        //                    else
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.Black;
        //                        Console.ForegroundColor = ConsoleColor.White;
        //                    }
        //                }
        //                break;
        //            case Objects2.Mwss:
        //                {
        //                    if (i == 4)
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.White;
        //                        Console.ForegroundColor = ConsoleColor.Black;
        //                    }
        //                    else
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.Black;
        //                        Console.ForegroundColor = ConsoleColor.White;
        //                    }
        //                }
        //                break;
        //            case Objects2.Hwss:
        //                {
        //                    if (i == 5)
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.White;
        //                        Console.ForegroundColor = ConsoleColor.Black;
        //                    }
        //                    else
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.Black;
        //                        Console.ForegroundColor = ConsoleColor.White;
        //                    }
        //                }
        //                break;
        //            case Objects2.Pulsar:
        //                {
        //                    if (i == 6)
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.White;
        //                        Console.ForegroundColor = ConsoleColor.Black;
        //                    }
        //                    else
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.Black;
        //                        Console.ForegroundColor = ConsoleColor.White;
        //                    }
        //                }
        //                break;
        //            case Objects2._119P4H1V0:
        //                {
        //                    if (i == 7)
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.White;
        //                        Console.ForegroundColor = ConsoleColor.Black;
        //                    }
        //                    else
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.Black;
        //                        Console.ForegroundColor = ConsoleColor.White;
        //                    }
        //                }
        //                break;
        //            case Objects2.Glidergun:
        //                {
        //                    if (i == 8)
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.White;
        //                        Console.ForegroundColor = ConsoleColor.Black;
        //                    }
        //                    else
        //                    {
        //                        Console.BackgroundColor = ConsoleColor.Black;
        //                        Console.ForegroundColor = ConsoleColor.White;
        //                    }
        //                }
        //                break;
        //            default:
        //                {
        //                    Console.BackgroundColor = ConsoleColor.Black;
        //                    Console.ForegroundColor = ConsoleColor.White;
        //                }
        //                break;
        //        }
        //        Console.SetCursorPosition(cursY + 4 + toekomst.GetLength(1) + 4, cursX + 2 + (i * 4));
        //        Console.Write(msg[i]);
        //        Console.BackgroundColor = ConsoleColor.Black;
        //        Console.ForegroundColor = ConsoleColor.White;
        //    }


        //}

        //Thread.Sleep(timeout);









        ////TODO die char moet nog een static worden
        //private void PrintGrid(bool[,] toekomst, int cursX = 0, int cursY = 0, bool showIndex = false, int leftBorder = 4, int topBorder = 2 /*, char oN = '☻', char oFf = ' '*/)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    Console.BackgroundColor = ConsoleColor.White;
        //    Console.ForegroundColor = ConsoleColor.Black;



        //    for (int rij = 0; rij < toekomst.GetLength(0); rij++)
        //    {
        //        stringBuilder.Clear();
        //        for (int kolom = 0; kolom < toekomst.GetLength(1); kolom++)
        //        {
        //            //stringBuilder.Append(toekomst[rij, kolom] == StatusAlive ? oN : oFf);
        //            stringBuilder.Append(toekomst[rij, kolom] ? '☻' : ' ');
        //        }
        //        Console.SetCursorPosition(cursY + leftBorder, cursX + topBorder + rij);
        //        Console.Write(stringBuilder.ToString());
        //    }
        //    Console.CursorVisible = false;

        //} //einde tekenen





        //    return;
        //}


        //switch (aFileName)
        //{
        //    case "119p4h1v0":
        //        this.PatterType = PatternType.Spaceships;
        //        this.Pattern = _119P4H1V0();
        //        break;
        //    case "glider":
        //        this.PatterType = PatternType.Spaceships;
        //        this.Pattern = Glitter();
        //        break;
        //    case "glidergun":
        //        this.PatterType = PatternType.Spaceships;
        //        this.Pattern = gliderGun();
        //        break;
        //    case "hwss":
        //        this.PatterType = PatternType.Spaceships;
        //        this.Pattern = hWSS();
        //        break;
        //    case "lwss":
        //        this.PatterType = PatternType.Spaceships;
        //        this.Pattern = lWSS();
        //        break;
        //    case "mwss":
        //        this.PatterType = PatternType.Spaceships;
        //        this.Pattern = mWSS();
        //        break;
        //    case "pulsar":
        //        this.PatterType = PatternType.Oscillators;
        //        this.Pattern = pulsar();
        //        break;
        //    default:
        //        //voorlopig exception
        //        throw new Exception("pattern niet gevonden");
        //        break;
        //}



        //static bool[,] glider = { { false, true, false }, { false, false, true }, { true, true, true } };
        //static bool[,] lWSS = { { false, true, true, true, true }, { true, false, false, false, true }, { false, false, false, false, true }, { true, false, false, true, false } };
        //static bool[,] mWSS = { { false, true, true, true, true, true }, { true, false, false, false, false, true }, { false, false, false, false, false, true }, { true, false, false, false, true, false }, { false, false, true, false, false, false } };
        //static bool[,] hWSS = { { false, false, true, true, false, false, false }, { true, false, false, false, false, true, false }, { false, false, false, false, false, false, true }, { true, false, false, false, false, false, true }, { false, true, true, true, true, true, true } };
        //static bool[,] pulsar = {  { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false },
        //                                { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false},
        //                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //                                { true, true, true, false, false, true, true, false, true, true, false, false, true, true, true},
        //                                { false, false, true, false, true, false, true, false, true, false, true, false, true, false, false},
        //                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                                { false, false, true, false, true, false, true, false, true, false, true, false, true, false, false},
        //                                { true, true, true, false, false, true, true, false, true, true, false, false, true, true, true},
        //                                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                                { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false},
        //                                { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false }
        //            };
        //static bool[,] _119P4H1V0 = { {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true },
        //                                    {false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,false,false,false },
        //                                    {false,false,false,false,false,false,true,false,false,false,false,true,false,false,false,false,true,false,true,true,true,true,true,true,false,false,false,false,true,true,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,true,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,true,false,false,true,false,true,true,true,false },
        //                                    {false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,true,true,true,false,false,false,false,true,true,true,false },
        //                                    {false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,true,false,false,false,false,false,false,false },
        //                                    {false,true,false,false,true,true,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,true,false,false,true,true,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,true,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,true,true,true,false,false,false,false,true,true,true,false },
        //                                    {false,false,false,false,false,false,true,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,true,false,false,true,false,true,true,true,false },
        //                                    {false,false,false,false,false,false,true,false,false,false,false,true,false,false,false,false,true,false,true,true,true,true,true,true,false,false,false,false,true,true,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false }};
        //static bool[,] gliderGun = {   {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,true,true },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,true,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,true,true },
        //                                    {true,true,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {true,true,false,false,false,false,false,false,false,false,true,false,false,false,true,false,true,true,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,true,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false }};


        //public bool[,] Glitter()
        //{
        //    return new bool[,] { { false, true, false }, { false, false, true }, { true, true, true } };
        //}
        //public bool[,] lWSS()
        //{
        //    return new bool[,] { { false, true, true, true, true }, { true, false, false, false, true }, { false, false, false, false, true }, { true, false, false, true, false } };
        //}
        //public bool[,] mWSS()
        //{
        //    return new bool[,] { { false, true, true, true, true, true }, { true, false, false, false, false, true }, { false, false, false, false, false, true }, { true, false, false, false, true, false }, { false, false, true, false, false, false } };
        //}
        //public bool[,] hWSS()
        //{
        //    return new bool[,] { { false, false, true, true, false, false, false }, { true, false, false, false, false, true, false }, { false, false, false, false, false, false, true }, { true, false, false, false, false, false, true }, { false, true, true, true, true, true, true } };
        //}
        //public bool[,] pulsar()
        //{
        //    return new bool[,] {  { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false },
        //                                { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false},
        //                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //                                { true, true, true, false, false, true, true, false, true, true, false, false, true, true, true},
        //                                { false, false, true, false, true, false, true, false, true, false, true, false, true, false, false},
        //                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                                { false, false, true, false, true, false, true, false, true, false, true, false, true, false, false},
        //                                { true, true, true, false, false, true, true, false, true, true, false, false, true, true, true},
        //                                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //                                { false, false, false, false, true, true, false, false, false, true, true, false, false, false, false},
        //                                { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false},
        //                                { false, false, false, false, true, false, false, false, false, false, true, false, false, false, false }
        //            };
        //}
        //public bool[,] _119P4H1V0()
        //{
        //    return new bool[,] { {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true },
        //                                    {false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,false,false,false },
        //                                    {false,false,false,false,false,false,true,false,false,false,false,true,false,false,false,false,true,false,true,true,true,true,true,true,false,false,false,false,true,true,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,true,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,true,false,false,true,false,true,true,true,false },
        //                                    {false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,true,true,true,false,false,false,false,true,true,true,false },
        //                                    {false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,true,false,false,false,false,false,false,false },
        //                                    {false,true,false,false,true,true,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,true,false,false,true,true,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,true,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,true,true,true,false,false,false,false,true,true,true,false },
        //                                    {false,false,false,false,false,false,true,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,true,false,false,true,false,true,true,true,false },
        //                                    {false,false,false,false,false,false,true,false,false,false,false,true,false,false,false,false,true,false,true,true,true,true,true,true,false,false,false,false,true,true,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,true,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false }};
        //}
        //public bool[,] gliderGun()
        //{
        //    return new bool[,] {   {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,true,true },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,true,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,true,true },
        //                                    {true,true,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {true,true,false,false,false,false,false,false,false,false,true,false,false,false,true,false,true,true,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,true,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false },
        //                                    {false,false,false,false,false,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false }};
        //}

    }
}
