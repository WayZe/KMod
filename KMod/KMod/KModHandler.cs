using System;
using System.Collections.Generic;
namespace KMod
{
    public static class KModHandler
    {
        public static int XAMount { get; set; }

        public static List<List<int>> X { get; set; }

        private static List<List<int>> centroids = new List<List<int>>();

        private static List<List<int>> clusters = new List<List<int>>();

        public static List<List<int>> GenerateStartCentroids(int k)
        {
            Random rnd = new Random();
            for (int i = 0; i < k; i++)
            {
                List<int> centroid = new List<int>();
                for (int j = 0; j < XAMount; j++)
                {
                    centroid.Add(rnd.Next(30, 100));
                }
                centroids.Add(centroid);
            }

            return centroids;
        }

        public static List<List<int>> GenerateStartClusters()
        {
            for (int i = 0; i < X.Count; i++)
            {
                for (int j = 0; j < centroids.Count; j++)
                {
                    // TO_DO
                }
            }

            return clusters;
        }
    }
}
