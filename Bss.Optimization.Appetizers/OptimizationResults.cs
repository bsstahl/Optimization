using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Appetizers
{
    public class OptimizationResults
    {
        public double ObjectiveValue { get; set; }
        public double[] Items { get; set; }
        public double Discount { get; set; }
    }
}
