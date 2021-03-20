using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameOfLife
{
    enum StatusObject
    {
        Geldig,
        Geblokkeerd
    }
    public enum PatternType
    {
        Stable,
        Oscillators,
        Spaceships
    }
    public class __Gol_Object
    // -- Game of life object
    {

        private char bluePrintLineSeperator = '#';
        private char lineSeperator = ';';

        private string fileName = "BluePrint.txt";
        private string name = "GiveNameForBluePrint";

        public string Title { get; set; }
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value.Replace(' ', '_').ToLower(); }
        }

        public bool[,] BluePrint { get; set; }
        //public StatusObject Status { get; set; } = StatusObject.Geldig;
        public PatternType Type;

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        public bool WriteBluePrintFile()
        {

            int l = BluePrint.GetLength(0);
            if (BluePrint.GetLength(0) > 1)
            {
                if (name.Length > 0)
                {
                    switch (SearchDataInRecord(fileName, name, seperator: lineSeperator))
                    {
                        case "0": return WriteDataInRecord(fileName, name, lineSeperator, false, CreateBluePrintData());// file found but not record
                        case null: return WriteDataInRecord(fileName, name, lineSeperator, true, CreateBluePrintData()); // file not found
                        default: // file & record found
                            {
                                //DeleteRecordInFile(fileName, name, lineSeperator);
                                //return WriteDataInRecord(fileName, name, lineSeperator, true, CreateBluePrintData());
                                return true;
                            }
                    }
                }

            }
            return false;
        }
        private string CreateBluePrintData()
        {
            string bluePrintData = null;
            if (BluePrint.GetLength(0) > 1)
            {
                for (int i = 0; i < BluePrint.GetLength(0); i++)
                {
                    for (int j = 0; j < BluePrint.GetLength(1); j++)
                    {
                        bluePrintData += BluePrint[i, j] ? 1 : 0;
                    }
                    if (i < BluePrint.GetLength(0) - 1) bluePrintData += bluePrintLineSeperator;
                }
            }
            return bluePrintData;
        }
        private bool DeleteRecordInFile
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
        private bool WriteDataInRecord
            (
                string bestandsnaam,
                string recordKey,
                char seperator = ';',
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
        private string SearchDataInRecord
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


    public class Gol_LoadFromFile
    {
        private string fileName = "BluePrint.txt";
        private char lineSep = '\n';
        private char recSep = ';';
        private char blueprintSep = '#';
        private List<__Gol_Object> gol_Objects;
        public Gol_LoadFromFile()
        {
            Opstart();
        }
        public void Opstart()
        {
            if (File.Exists(fileName))
            {
                string[] txtFileName = File.ReadAllLines(fileName);
                gol_Objects = new List<__Gol_Object>();

                foreach (string txtLine in txtFileName)
                {
                    string[] recGegevens = txtLine.Split(recSep);
                    //gol_Objects.Add(new Gol_Object() { Name = recGegevens[0], BluePrint = recGegevens[1] })
                }
            }
                
        }
        private bool[,] CreateBluePrintFromData(string data)
        {
            string[] txtBluePrint = data.Split(blueprintSep);
            bool[,] createBluePrint = new bool[0,0];

            foreach (string bluePrintPart in data.Split(blueprintSep))
            {

            }

            return createBluePrint;
        }
    }

}
