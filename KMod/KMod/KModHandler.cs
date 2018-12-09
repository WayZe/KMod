using System;
using System.Collections.Generic;
using System.Collections;
namespace KMod
{
    public static class KModHandler
    {
        public static int XAMount { get; set; }

        public static List<List<int>> X { get; set; }

        public static List<List<int>> centroids = new List<List<int>>();

        public static List<List<int>> clusters = new List<List<int>>();

        private static int modalInterval = 10;

        private struct Cluster
        {
            public int distance;
            public int number;
        }

        public static List<List<int>> GenerateStartCentroids(int k)
        {
            //Random rnd = new Random();
            //for (int i = 0; i < k; i++)
            //{
            //    List<int> centroid = new List<int>();
            //    for (int j = 0; j < XAMount; j++)
            //    {
            //        centroid.Add(rnd.Next(30, 100));
            //    }
            //    centroids.Add(centroid);
            //}

            //for (int i = 0; i < k; i++)
            //{
            //    List<int> centroid = new List<int>();
            //    for (int j = 0; j < X[0].Count; j++)
            //    {
            //        if (i == j)
            //        {
            //            centroid.Add(100);
            //        }
            //        else
            //        {
            //            centroid.Add(0);
            //        }
            //    }
            //    centroids.Add(centroid);
            //}

            int step = (int)Math.Round((double)X.Count/k-1, MidpointRounding.AwayFromZero) + 1;
            int clusterPointNumber = 0;
            for (int i = 0; i < k-1; i++)
            {
                centroids.Add(X[clusterPointNumber]);
                Console.WriteLine(clusterPointNumber);
                clusterPointNumber += step;
            }

            centroids.Add(X[X.Count-1]);

            return centroids;
        }

        public static List<List<int>> GenerateStartClusters()
        {
            clusters.Clear();
            for (int i = 0; i < centroids.Count; i++)
            {
                clusters.Add(new List<int>());
            }

            for (int i = 0; i < X.Count; i++)
            {
                Cluster minCluster;
                minCluster.distance = 0;
                minCluster.number = 0;

                for (int k = 0; k < X[0].Count; k++)
                {
                    minCluster.distance += (int)Math.Pow(centroids[0][k] - X[i][k], 2);
                }

                for (int j = 1; j < centroids.Count; j++)
                {
                    int distance = 0;
                    for (int k = 0; k < X[0].Count; k++)
                    {
                        distance += (int)Math.Pow(centroids[j][k] - X[i][k], 2);
                    }

                    if (distance < minCluster.distance)
                    {
                        minCluster.distance = distance;
                        minCluster.number = j;
                    }
                }

                clusters[minCluster.number].Add(i);
            }

            return clusters;
        }

        public static List<List<int>> RecalculateCentroids()
        {
            // По кластерам
            for (int i = 0; i < clusters.Count; i++)
            {
                // По факторам
                for (int k = 0; k < X[0].Count; k++)
                {
                    List<int> oneFactorValues = new List<int>(); // значение фактора - номер наблюдения
                    // По точкам в кластере при закрепленном факторе
                    for (int j = 0; j < clusters[i].Count; j++)
                    {
                        oneFactorValues.Add(X[clusters[i][j]][k]);
                    }

                    Dictionary<int, int> intervalFrequencies = new Dictionary<int, int>();
                    int range = 100/modalInterval;
                    /* Начальная инициализация словаря <номер интервала/количество значений на интервале> */
                    for (int j = 0; j < range; j++)
                    {
                        intervalFrequencies[j] = 0;
                    }

                    /* Распределение значений фактора по интервалам */
                    foreach (int value in oneFactorValues)
                    {
                        int interval = value/modalInterval;
                        intervalFrequencies[interval]++;
                    }

                    /* Нахождение модального интервала */
                    int maxFrequencyInterval = 0;
                    for (int j = 1; j < range; j++)
                    {
                        if (intervalFrequencies[maxFrequencyInterval] < intervalFrequencies[j])
                        {
                            maxFrequencyInterval = j;
                        }
                    }

                    /* Расчет моды для фактора, запись ее значение в соответствующий фактор соответсвующего центроида */
                    if (maxFrequencyInterval != 0 && maxFrequencyInterval != intervalFrequencies.Count - 1)
                    {
                        //Console.WriteLine(intervalFrequencies.Count + " " + maxFrequencyInterval + " " + i + " " + k + " " + centroids.Count);
                        centroids[i][k] = (int)(10 * maxFrequencyInterval + modalInterval * (double)(intervalFrequencies[maxFrequencyInterval] - intervalFrequencies[maxFrequencyInterval - 1])
                        / (2 * intervalFrequencies[maxFrequencyInterval] - intervalFrequencies[maxFrequencyInterval - 1]
                                                                         - intervalFrequencies[maxFrequencyInterval + 1]));
                    }
                    else if (maxFrequencyInterval == 0)
                    {
                        centroids[i][k] = (int)(10 * maxFrequencyInterval + modalInterval * (double)(intervalFrequencies[maxFrequencyInterval])
                        / (2 * intervalFrequencies[maxFrequencyInterval] - intervalFrequencies[maxFrequencyInterval + 1]));
                    }
                    else
                    {
                        centroids[i][k] = (int)(10 * maxFrequencyInterval + modalInterval * (double)(intervalFrequencies[maxFrequencyInterval] - intervalFrequencies[maxFrequencyInterval - 1])
                        / (2 * intervalFrequencies[maxFrequencyInterval] - intervalFrequencies[maxFrequencyInterval - 1]));
                    }
                }
            }

            return centroids;
        }
    }
}
