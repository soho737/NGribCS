using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGribCS
{
    public static class Extensions
    {
        public static T Min<T>(this T[,] arr) where T : IComparable
        {
            bool minSet = false;
            T min = default(T);
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (!minSet)
                    {
                        minSet = true;
                        min = arr[i, j];
                    }
                    else if (arr[i, j].CompareTo(min) < 0)
                        min = arr[i, j];
            return min;
        }


        public static T Max<T>(this T[,] arr) where T : IComparable
        {
            bool maxSet = false;
            T max = default(T);
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (!maxSet)
                    {
                        maxSet = true;
                        max = arr[i, j];
                    }
                    else if (arr[i, j].CompareTo(max) > 0)
                        max = arr[i, j];
            return max;
        }

        public static T Min<T>(this T[,] arr, T ignore) where T : IComparable
        {
            bool minSet = false;
            T min = default(T);
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j].CompareTo(ignore) != 0)
                        if (!minSet)
                        {
                            minSet = true;
                            min = arr[i, j];
                        }
                        else if (arr[i, j].CompareTo(min) < 0)
                            min = arr[i, j];
            return (minSet) ? min : ignore;
        }
    }
}
