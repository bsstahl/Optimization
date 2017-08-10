using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Appetizers.Entities;

namespace Bss.Optimization.Appetizers.Glop
{
    public static class Extensions
    {
        public static int ToCents(this double price)
        {
            return Convert.ToInt32(Math.Round(price * 100));
        }

        public static double CalculateObjective(this OptimizationResults result, IEnumerable<MenuItem> items)
        {
            double objectiveValue = 0.0;
            foreach (var item in items)
                objectiveValue += item.Price * result.Items[item.Id];
            return objectiveValue;
        }

        public static int DistinctItemCount(this OptimizationResults result)
        {
            return result.Items.Count(i => i > 0);
        }

        public static int MaxSingleItemCount(this OptimizationResults result)
        {
            return result.Items.Max(i => i);
        }

        public static int PlateCount(this OptimizationResults result)
        {
            return result.Items.Sum(i => i);
        }
    }
}
