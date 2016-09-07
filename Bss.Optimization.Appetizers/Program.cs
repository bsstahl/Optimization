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
            double totalPrice = 15.05;
            var items = new MenuItemCollection();
            var c = new Model(items, totalPrice);
            var results = c.Optimize();
            DisplayResults(results, items, totalPrice);
        }

        private static void DisplayResults(OptimizationResults results, IEnumerable<MenuItem> items, double totalPrice)
        {
            Console.WriteLine($"Total Sale: {totalPrice:0.00}");
            Console.WriteLine($"Profit: {results.ObjectiveValue:0.00}");
            Console.WriteLine($"Discount: {results.Discount:0.00}");

            var n = results.Items.GetLength(0);
            Console.WriteLine("\r\nName, Price, Cost, Qty, Basis, Total");
            for (int j = 0; j < n; j++)
            {
                var item = items.Single(i => i.Id == j);
                Console.WriteLine($"{item.Name}, {item.Price:0.00}, {item.Cost:0.00}, {results.Items[j]}, {(results.Items[j] * item.Cost):0.00}, {(results.Items[j]*item.Price):0.00}");
            }

            Console.WriteLine(string.Empty);
        }

    }
}
