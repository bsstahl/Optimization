using Gurobi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Appetizers
{
    class Program
    {
        // Based on XKCD #287 at http://xkcd.com/287/

        static void Main(string[] args)
        {
            var env = new GRBEnv("Menu.log");
            var items = new MenuItemCollection();
            var c = new Model(env, items, 15.05);
            var results = c.Optimize();
            DisplayResults(results, items);
        }

        private static void DisplayResults(OptimizationResults results, IEnumerable<MenuItem> items)
        {
            Console.WriteLine($"Objective Value: {results.ObjectiveValue}");
            var n = results.Items.GetLength(0);
            for (int j = 0; j < n; j++)
            {
                var item = items.Single(i => i.Id == j);
                Console.WriteLine($"{item.Name}: {results.Items[j]}");
            }

            Console.WriteLine(string.Empty);
        }

    }
}
