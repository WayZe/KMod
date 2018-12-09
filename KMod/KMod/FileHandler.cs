#define __DEBUG__
//#undef __DEBUG__

using System;
using System.Collections.Generic;
using System.IO;

namespace KMod
{
    public static class FileHandler
    {
        private static String inputFilePath = "/Users/andreymakarov/Downloads/AiOD/variations.txt";
        private static String outputFilePath = "/Users/andreymakarov/Downloads/AiOD/output.txt";

        public static List<List<int>> ReadFile()
        {
            StreamReader streamReader = new StreamReader(inputFilePath);
            List<List<int>> formatList = new List<List<int>>();
            String fileString = streamReader.ReadLine();

            while (!streamReader.EndOfStream)
            {
                fileString = streamReader.ReadLine();
                String[] arrayString = fileString.Split(' ', '\t');
                List<int> listString = new List<int>();
                for (int i = 1; i < arrayString.Length; i++)
                {
                    if (arrayString[i] != "")
                    {
                        listString.Add(Convert.ToInt32(arrayString[i]));
                    }
                }
                formatList.Add(listString);
            }
            streamReader.Close();

            return formatList;
        }

        public static void PrintOutputFile()
        {
            StreamWriter streamWriter = new StreamWriter(outputFilePath);
            for (int i = 0; i < KModHandler.clusters.Count; i++)
            {
                streamWriter.WriteLine("Кластер №" + (i+1));
                streamWriter.WriteLine("\t  Скорость\t  Дриблинг\t\t  Удар\t\tЗащита\t\tПередачи\tФизика");
                for (int j = 0; j < KModHandler.clusters[i].Count; j++)
                {
                    String outString = "";
                    for (int k = 0; k < KModHandler.X[0].Count; k++)
                    {
                        outString += "\t\t\t" + KModHandler.X[KModHandler.clusters[i][j]][k];
                    }
                    Console.WriteLine(outString);
                    streamWriter.WriteLine(outString);
                    streamWriter.Flush();
                }
            }
            streamWriter.Close();
        }
    }
}
