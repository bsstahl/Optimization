using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Pottery.Entities;
using Google.OrTools.LinearSolver;

namespace Bss.Optimization.Pottery.Glop
{
    public class Optimizer : Interfaces.IPotteryOptimizer
    {
        // If the solution space is very large, this method will not return.
        // The most likely result is that it will exceed memory capacity
        // and then fail.  As a result, we shouldn't use this method in
        // any production optimization.
        public IEnumerable<ProductionTarget> GetFeasibleTargets(double claySupply, double glazeSupply)
        {
            //List<ProductionTarget> targets = new List<ProductionTarget>();

            //// Setup the Model
            //var solver = CreateLinearProgrammingSolver();

            //// Setup the decision variables
            //var xS = CreateSmallVasesVariable(solver, claySupply, glazeSupply);
            //var xL = CreateLargeVasesVariable(solver, claySupply, glazeSupply);

            //// Create Constraints
            //var c0 = CreateClayConstraint(solver, xS, xL, claySupply);
            //var c1 = CreateGlazeConstraint(solver, xS, xL, glazeSupply);

            //// Find the greatest number of small vases we can make
            //var maxSmall = System.Math.Min(claySupply, glazeSupply);

            //// Find the greatest number of large vases we can make
            //var maxLarge = System.Math.Min(claySupply / 4.0, glazeSupply / 2.0);

            //// Find all feasible combinations of small and large vases
            //// Note: There are probably several better ways of doing this
            //// that are more efficient and organic.  For example, we could make
            //// a tree that represents all of the possible decisions and let the
            //// optimizer find the solutions from within that tree.
            //var results = new List<ProductionTarget>();
            //for (int nSmall = 0; nSmall <= maxSmall; nSmall++)
            //    for (int nLarge = 0; nLarge <= maxLarge; nLarge++)
            //    {
            //        // Force the solution to the target set of values
            //        var c2 = solver.MakeConstraint(nSmall, nSmall, $"xS_Equals_{nSmall}");
            //        c2.SetCoefficient(xS, 1);

            //        var c3 = solver.MakeConstraint(nLarge, nLarge, $"xL_Equals_{nLarge}");
            //        c3.SetCoefficient(xL, 1);

            //        // See if the solution is feasible with those values
            //        var result = solver.Solve();
            //        if (result == Solver.OPTIMAL)
            //            results.Add(new ProductionTarget() { Small = nSmall, Large = nLarge });

            //        solver.Remove(c2);
            //    }

            //return results;

            // TODO: Check out using a DecisionBuilder
            // https://developers.google.com/optimization/mip/integer_opt_cp

            throw new NotImplementedException();
        }

        // This method will work irrespective of the size of the solution space
        public ProductionTarget GetTargets(double claySupply, double glazeSupply)
        {
            var solver = CreateLinearProgrammingSolver();
            var xS = CreateSmallVasesVariable(solver, claySupply, glazeSupply);
            var xL = CreateLargeVasesVariable(solver, claySupply, glazeSupply);

            var c0 = CreateClayConstraint(solver, xS, xL, claySupply);
            var c1 = CreateGlazeConstraint(solver, xS, xL, glazeSupply);

            // Objective: maximize 3xS + 9xL
            Objective objective = solver.Objective();
            objective.SetCoefficient(xS, 3);
            objective.SetCoefficient(xL, 9);
            objective.SetMaximization();

            int resultStatus = solver.Solve();

            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.OPTIMAL)
            {
                string message = "The problem does not have an optimal solution!";
                throw new InvalidOperationException(message);
            }

            return new Entities.ProductionTarget()
            {
                Small = Convert.ToInt32(xS.SolutionValue()),
                Large = Convert.ToInt32(xL.SolutionValue())
            };
        }

        private static Constraint CreateClayConstraint(Solver solver, Variable xS, Variable xL, double claySupply)
        {
            // Constraint: xS + 4xL <= claySupply
            var c = solver.MakeConstraint(0.0, claySupply);
            c.SetCoefficient(xS, 1);
            c.SetCoefficient(xL, 4);
            return c;
        }

        private static Constraint CreateGlazeConstraint(Solver solver, Variable xS, Variable xL, double glazeSupply)
        {
            // Constraint: xS + 2xL <= glazeSupply
            var c = solver.MakeConstraint(0.0, glazeSupply);
            c.SetCoefficient(xS, 1);
            c.SetCoefficient(xL, 2);
            return c;
        }

        private static Variable CreateSmallVasesVariable(Solver solver, double claySupply, double glazeSupply)
        {
            var maxSmall = Math.Min(claySupply, glazeSupply);
            return solver.MakeIntVar(0.0, maxSmall, "xS");
        }

        private static Variable CreateLargeVasesVariable(Solver solver, double claySupply, double glazeSupply)
        {
            var maxLarge = Math.Min(claySupply / 4, glazeSupply / 2);
            return solver.MakeIntVar(0.0, maxLarge, "xL");
        }

        private static Solver CreateLinearProgrammingSolver()
        {
            Solver solver = Solver.CreateSolver("LinearProgramming", "GLOP_LINEAR_PROGRAMMING");
            if (solver == null)
                throw new InvalidOperationException("Could not create solver");
            return solver;
        }
    }
}
