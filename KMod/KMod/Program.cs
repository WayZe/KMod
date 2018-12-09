#define __DEBUG__
//#undef __DEBUG__

using System;
using System.Collections.Generic;
using System.Linq;

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

            /* Чтение списка факторов из файла */
            KModHandler.X = FileHandler.ReadFile();

            // Получение количества факторов
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
            Console.WriteLine();
#endif

            List<List<int>> clusters = KModHandler.GenerateStartClusters();
            List<int> clusterSums = new List<int>();
            for (int i = 0; i < clusters.Count; i++)
            {
                clusterSums.Add(0);
            }

#if __DEBUG__
            Console.WriteLine("Начальное разбиение по кластерам: ");
            for (int i = 0; i < clusters.Count; i++)
            {
                Console.WriteLine("Кластер №" + (i+1));
                for (int j = 0; j < clusters[i].Count; j++)
                {
                    clusterSums[i] += clusters[i][j];
                    Console.Write(clusters[i][j] + " ");
                }
                Console.WriteLine();
            }
#endif
            bool isFinished = false;
            while (!isFinished)
            {
                List<List<int>> newCentroids = KModHandler.RecalculateCentroids();
                isFinished = true;
#if __DEBUG__
                Console.WriteLine("Новые центроиды: ");
                for (int i = 0; i < centroids.Count; i++)
                {
                    for (int j = 0; j < centroids[i].Count; j++)
                    {
                        Console.Write(centroids[i][j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
#endif
                
                clusters = KModHandler.GenerateStartClusters();

#if __DEBUG__
                Console.WriteLine("Новое разбиение по кластерам: ");
                for (int i = 0; i < clusters.Count; i++)
                {
                    int clusterSum = 0;
                    Console.WriteLine("Кластер №" + (i + 1));
                    for (int j = 0; j < clusters[i].Count; j++)
                    {
                        clusterSum += clusters[i][j];
                        Console.Write(clusters[i][j] + " ");
                    }
                    Console.WriteLine();

                    if (clusterSum != clusterSums[i])
                    {
                        isFinished = false;

                        clusterSums[i] = clusterSum;
                    }
                }
#endif
            }

            FileHandler.PrintOutputFile();
        }
    }
}
