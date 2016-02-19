using Gurobi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Pottery.LP
{
    public static class Extensions
    {
        public static bool IsFeasible(this GRBModel model)
        {
            var status = model.Get(GRB.IntAttr.Status);
            return (status == GRB.Status.OPTIMAL);
        }
    }
}
