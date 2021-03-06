﻿using System.Collections.Generic;
using System.Linq;

namespace FILIO_Oef_Straat
{
    class Parsers
    {
        public static Dictionary<int, string> StraatParser(string fileToReadPath)//straatnamen
        {
            Dictionary<int, string> straat = new Dictionary<int, string>();
            List<List<string>> lines = BackEndClasses.FileSplitter(fileToReadPath, ';');
            foreach (List<string> line in lines)
            {
                int.TryParse(line[0], out int key);
                if (key > 0)
                    straat.Add(key, line[1]);
            }
            straat = straat.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return straat;
        }
        public static Dictionary<int, List<int>> StratenInGemeentenParser(string fileToReadPath) //straatnaamID_gemeenteID
        {
            Dictionary<int, List<int>> stratenInGemeenten = new Dictionary<int, List<int>>();
            List<List<string>> lines = BackEndClasses.FileSplitter(fileToReadPath, ';');
            List<int> values = new List<int>();
            //foreach (string[] line in lines)
            //{
            //    int key;
            //    int value;
            //    int.TryParse(line[0], out key);
            //    int.TryParse(line[1], out value);
            //    if(key > 0)
            //    stratenInGemeenten.Add(key, value);
            //}//moet niet gesorteerd worden = soort van lookup table

            //---------------tester------------------------
            int currentKey = 1;
            foreach (List<string> line in lines)
            {
                bool keyJuist = int.TryParse(line[1], out int key);
                int.TryParse(line[0], out int value);
                if (keyJuist)
                {
                    if (key == currentKey)
                        values.Add(value);
                    else
                    {
                        currentKey++;
                        stratenInGemeenten.Add(key, values);
                        values.Clear();
                    }
                }
            }
            return stratenInGemeenten;
        }
        public static Dictionary<int, string> GemeenteEnProvincieParser(string fileToReadPath)  //provincieInfo
        {
            Dictionary<int, string> gemeente = new Dictionary<int, string>();
            List<List<string>> lines = BackEndClasses.FileSplitter(fileToReadPath, ';');
            foreach (List<string> line in lines)
            {
                if (line[2] == "nl")
                {
                    int.TryParse(line[1], out int key);
                    if (!gemeente.ContainsKey(key))
                        gemeente.Add(key, line[3]);
                }
            }
            return gemeente.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
        public static List<int> ProvincieParser(string fileToReadPath)
        {
            List<int> ProvincieIDs = new List<int>();
            List<List<string>> lines = BackEndClasses.FileSplitter(fileToReadPath, ',');
            foreach (List<string> line in lines)
            {
                int.TryParse(line[0], out int id);
                ProvincieIDs.Add(id);
            }
            return ProvincieIDs;
        }
        public static Dictionary<int, List<int>> GemeentenInProvincieParser(string fileToReadPath) //koppeling tussen ProvincieID en gemeenteID => prov = key , gem = value ==>> kan anders ?
        {
            Dictionary<int, List<int>> gemeentenInProvincies = new Dictionary<int, List<int>>();
            List<List<string>> lines = BackEndClasses.FileSplitter(fileToReadPath, ';');
            lines.RemoveAt(0);//eerste lijn wegkrijgen.
            List<int> values = new List<int>();
            //-----------------------test met provincieID als key------------------------- List wordt niet ingevuld keys zijn wel juist 
            int currentKey = 1;
            while (currentKey != -1)
            {
                foreach (List<string> line in lines)
                {
                    if (line[2] == "nl")  //per provincieID moet een lijst van gemeenteIDs opgeslagen worden (problemen op het einde van het bestand provincieinfo.csv)
                    {
                        int.TryParse(line[0], out int key); //gemeenteID     
                        int.TryParse(line[1], out int value); //provincieID
                        if (key == currentKey)
                            values.Add(value);
                    }
                }
                if (values.Count > 0)
                {
                    gemeentenInProvincies.Add(currentKey, values);
                    currentKey++;
                }
                else
                    currentKey = -1;
            }
            return gemeentenInProvincies;
        }
    }
}
