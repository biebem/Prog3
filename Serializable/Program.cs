﻿using Microsoft.Azure.WebJobs.Extensions.Files;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        public void writeClass()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"C:\Users\Biebem\Downloads\MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }
        public static FileProcessor readClass()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"C:\Users\Biebem\Downloads\MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileProcessor obj = (FileProcessor)formatter.Deserialize(stream);
            stream.Close();
            return obj;
        }
        //-----------------------------Vrijdag----------------------------------------------
        //public void writeFile() 
        //{
        //    using (StreamWriter sw = new StreamWriter(Path.Combine(Path, "adresInfo.txt"))) 
        //    {
        //        foreach (var x in data.keys)
        //        {
        //            foreach (var y in data[x].keys)
        //            {
        //                foreach (var z in data[x][y])
        //                {
        //                    Console.WriteLine($"{x},{y},{z}");
        //                }

        //            }
        //        }
        //    }
        //}
    }
}
