using Bss.Optimization.Appetizers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Appetizers.Naive
{
    public static class Extensions
    {
        public static int MaxCount(this MenuItem item, double totalPrice)
        {
            return Convert.ToInt32(System.Math.Floor(totalPrice / item.Price));
        }

        public static double TotalCost(this IEnumerable<MenuItem> items, int[] itemCounts)
        {
            return items.Sum(i => i.Cost * itemCounts[i.Id]);
        }

        public static double TotalPrice(this IEnumerable<MenuItem> items, int[] itemCounts)
        {
            return Math.Round(items.Sum(i => i.Price * itemCounts[i.Id]), 2);
        }

        public static OptimizationResults GetLowestObjectiveValue(this IEnumerable<OptimizationResults> results, IEnumerable<MenuItem> items)
        {
            var bestObjectiveValue = results.Min(r => r.ObjectiveValue);
            return results.First(or => or.ObjectiveValue == bestObjectiveValue);
        }

        public static string ToDisplayString(this int[] values)
        {
            string result = string.Empty;
            for (int i = 0; i < values.Count(); i++)
                result += $"{values[i]} ";
            return result;
        }

        public static int[] GetCurrentQuantities(this int i, int n, int nMax)
        {
            var result = new int[n];
            for (int j = 0; j < n; j++)
            {
                int e = (j % (nMax + 1));
                int p = Convert.ToInt32(Math.Pow((nMax + 1), e));
                result[j] = (i / p) % (nMax + 1);
            }
            return result;
        }
    }
}
