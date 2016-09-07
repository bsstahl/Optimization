using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Appetizers.Entities;

namespace Bss.Optimization.Appetizers
{
    class Program
    {
        // Based on XKCD #287 at http://xkcd.com/287/
        // Uncomment the desired optimizer in the GetOptimizer method below

        static void Main(string[] args)
        {
            var items = new MenuItemCollection();
            var c = GetOptimizer();
            var results = c.GetQuantities(items, 15.05);

            Console.WriteLine("Optimal Solution:");
            Console.WriteLine(results.ToString(items));
        }

        private static Interfaces.IAppetizerOptimizer GetOptimizer()
        {
            return new Bss.Optimization.Appetizers.Naive.Model();
            // return new Bss.Optimization.Appetizers.Gurobi.Model();
            // return new Bss.Optimization.Appetizers.Glop.Model();
        }

    }
}
