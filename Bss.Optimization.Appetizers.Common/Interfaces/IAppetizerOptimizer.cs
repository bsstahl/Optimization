using Bss.Optimization.Appetizers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Appetizers.Interfaces
{
    public interface IAppetizerOptimizer
    {
        OptimizationResults GetQuantities(IEnumerable<MenuItem> items, double totalPrice);
    }
}
