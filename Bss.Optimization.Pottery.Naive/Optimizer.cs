using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Pottery.Entities;

namespace Bss.Optimization.Pottery.Naive
{
    public class Optimizer : Interfaces.IPotteryOptimizer
    {
        // Clay Constraint: Xsmall + 4*Xlarge <= claySupply
        // Glaze Constraint: Xsmall + 2*Xlarge <= glazeSupply

        public IEnumerable<ProductionTarget> GetFeasibleTargets(double claySupply, double glazeSupply)
        {
            // Find the greatest number of small vases we can make
            var maxSmall = System.Math.Min(claySupply, glazeSupply);

            // Find the greatest number of large vases we can make
            var maxLarge = System.Math.Min(claySupply / 4.0, glazeSupply / 2.0);

            // Find all feasible combinations of small and large vases
            var results = new List<ProductionTarget>();
            for (int nSmall = 0; nSmall <= maxSmall; nSmall++)
                for (int nLarge = 0; nLarge <= maxLarge; nLarge++)
                    if (ClayConstraintIsFeasible(claySupply, nSmall, nLarge) && GlazeConstraintIsFeasible(glazeSupply, nSmall, nLarge))
                        results.Add(new ProductionTarget() { Small = nSmall, Large = nLarge });

            return results;
        }

        // Revenue: 3 * Xsmall + 9 * Xlarge
        public ProductionTarget GetTargets(double claySupply, double glazeSupply)
        {
            ProductionTarget incumbent = null;
            double revenueOfIncumbent = 0.0;

            // Get all of the feasible solutions
            var solutionSet = GetFeasibleTargets(claySupply, glazeSupply);

            // Find the solution that produces the most revenue
            foreach (var solution in solutionSet)
                if (solution.ExpectedRevenue() > revenueOfIncumbent)
                {
                    incumbent = solution;
                    revenueOfIncumbent = solution.ExpectedRevenue();
                }

            return incumbent;
        }

        private bool ClayConstraintIsFeasible(double claySupply, int nSmall, int nLarge)
        {
            return (nSmall + (4 * nLarge)) <= claySupply;
        }

        private bool GlazeConstraintIsFeasible(double glazeSupply, int nSmall, int nLarge)
        {
            return (nSmall + (2 * nLarge)) <= glazeSupply;
        }

    }
}
