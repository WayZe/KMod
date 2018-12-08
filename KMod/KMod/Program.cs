#define __DEBUG__
//#undef __DEBUG__

using System;
using System.Collections.Generic;

namespace KMod
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = 0;
            while (k == 0)
            {
                Console.WriteLine("Введите количество кластеров:");
                try
                {
                    k = Convert.ToInt32(Console.ReadLine());
                    if (k <= 0)
                    {
                        k = 0;
                        Console.WriteLine("Введите цифру больше нуля!");
                    }
                }
                catch
                {
                    Console.WriteLine("Введите цифру!");
                }
            }
            Console.WriteLine();

            KModHandler.X = FileHandler.ReadFile();


            KModHandler.XAMount = KModHandler.X[0].Count;

#if __DEBUG__
            Console.WriteLine("Факторы: ");
            for (int i = 0; i < KModHandler.X.Count; i++)
            {
                for (int j = 0; j < KModHandler.X[i].Count; j++)
                {
                    Console.Write(KModHandler.X[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
#endif

            List<List<int>> centroids = KModHandler.GenerateStartCentroids(k);

#if __DEBUG__
            Console.WriteLine("Начальные центроиды: ");
            for (int i = 0; i < centroids.Count; i++)
            {
                for (int j = 0; j < centroids[i].Count; j++)
                {
                    Console.Write(centroids[i][j] + " ");
                }
                Console.WriteLine();
            }
#endif
        }
    }
}
