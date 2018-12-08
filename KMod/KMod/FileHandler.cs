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

            return formatList;
        }
    }
}
