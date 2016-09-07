using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Appetizers.Entities
{
    public class OptimizationResults
    {
        public double ObjectiveValue { get; set; }
        public int[] Items { get; set; }

        public string ToString(IEnumerable<MenuItem> items)
        {
            string result = $"Objective Value: {this.ObjectiveValue}\r\n";
            var n = this.Items.GetLength(0);
            for (int j = 0; j < n; j++)
            {
                var item = items.Single(i => i.Id == j);
                result += $"{item.Name}: {this.Items[j]}\r\n";
            }

            return result;
        }
    }
}
