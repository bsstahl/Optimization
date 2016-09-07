using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Appetizers.Entities;

namespace Bss.Optimization.Appetizers.Naive
{
    public class Model : Interfaces.IAppetizerOptimizer
    {
        public OptimizationResults GetQuantities(IEnumerable<MenuItem> items, double totalPrice)
        {
            var options = GetFeasibleQuantities(items, totalPrice);

            Console.WriteLine($"There are {options.Count()} feasible solutions");
            foreach (var option in options)
                Console.WriteLine(option.ToString(items));

            return options.GetLowestObjectiveValue(items);
        }

        private IEnumerable<OptimizationResults> GetFeasibleQuantities(IEnumerable<MenuItem> items, double totalPrice)
        {
            var results = new List<OptimizationResults>();

            var n = items.Count();
            var nMax = items.Max(i => i.MaxCount(totalPrice));
            var optionCount = Math.Pow(nMax + 1, n);
            int[] currentQuantities;

            Console.WriteLine($"Testing {optionCount} possible orders");
            for (int i = 0; i < optionCount; i++)
            {
                currentQuantities = i.GetCurrentQuantities(n, nMax);
                // Console.WriteLine(currentQuantities.ToDisplayString());

                var selectedItemsPrice = items.TotalPrice(currentQuantities);
                if (selectedItemsPrice == totalPrice)
                    results.Add(new OptimizationResults()
                    {
                        Items = currentQuantities,
                        ObjectiveValue = items.TotalCost(currentQuantities)
                    });
            }

            return results;
        }

    }
}
