using System;
using System.Collections.Generic;
namespace KMod
{
    public class KModHandler
    {
        //static private int xAmount;
        static public int XAMount { get; set; }

        static public List<List<int>> GenerateStartCentroids(int k)
        {
            List<List<int>> centroids = new List<List<int>>();

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
    }
}
