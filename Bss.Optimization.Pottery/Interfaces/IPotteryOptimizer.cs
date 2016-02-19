using Bss.Optimization.Pottery.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Pottery.Interfaces
{
    public interface IPotteryOptimizer
    {
        IEnumerable<ProductionTarget> GetFeasibleTargets(double claySupply, double glazeSupply);

        ProductionTarget GetTargets(double claySupply, double glazeSupply);
    }
}
