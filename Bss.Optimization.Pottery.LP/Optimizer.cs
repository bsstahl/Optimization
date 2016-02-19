using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Pottery.Entities;
using Gurobi;

namespace Bss.Optimization.Pottery.LP
{
    public class Optimizer : Interfaces.IPotteryOptimizer
    {
        // If the solution space is very large, this method will not return.
        // The most likely result is that it will exceed memory capacity
        // and then fail.  As a result, we shouldn't use this method in
        // any production optimization.
        public IEnumerable<ProductionTarget> GetFeasibleTargets(double claySupply, double glazeSupply)
        {
            List<ProductionTarget> targets = new List<ProductionTarget>();

            // Setup the Gurobi environment & Model
            GRBEnv env = new GRBEnv();
            GRBModel model = new GRBModel(env);

            // Setup the decision variables
            GRBVar xS = CreateSmallVasesVariable(claySupply, model);
            GRBVar xL = CreateLargeVasesVariable(glazeSupply, model);
            model.Update();

            // Create Constraints
            CreateConstraints(model, xS, xL, claySupply, glazeSupply);

            // Find the greatest number of small vases we can make
            var maxSmall = System.Math.Min(claySupply, glazeSupply);

            // Find the greatest number of large vases we can make
            var maxLarge = System.Math.Min(claySupply / 4.0, glazeSupply / 2.0);

            // Find all feasible combinations of small and large vases
            // Note: There are probably several better ways of doing this
            // that are more efficient and organic.  For example, we could make
            // a tree that represents all of the possible decisions and let the
            // optimizer find the solutions from within that tree.
            var results = new List<ProductionTarget>();
            for (int nSmall = 0; nSmall <= maxSmall; nSmall++)
                for (int nLarge = 0; nLarge <= maxLarge; nLarge++)
                {
                    // Force the solution to the target set of values
                    var c1 = model.AddConstr(xS == nSmall, $"xS_Equals_{nSmall}");
                    var c2 = model.AddConstr(xL == nLarge, $"xL_Equals_{nLarge}");
                    model.Update();

                    // See if the solution is feasible with those values
                    model.Optimize();
                    if (model.IsFeasible())
                        results.Add(new ProductionTarget() { Small = nSmall, Large = nLarge });

                    model.Remove(c1);
                    model.Remove(c2);
                    model.Update();
                }

            return results;
        }

        // This method will work irrespective of the size of the solution space
        public ProductionTarget GetTargets(double claySupply, double glazeSupply)
        {
            GRBEnv env = new GRBEnv();
            GRBModel model = new GRBModel(env);
            GRBVar xS = CreateSmallVasesVariable(claySupply, model);
            GRBVar xL = CreateLargeVasesVariable(glazeSupply, model);
            model.Update();

            // Create Constraints
            CreateConstraints(model, xS, xL, claySupply, glazeSupply);

            // Define Objective
            model.SetObjective(3 * xS + 9 * xL, GRB.MAXIMIZE);

            // Find the optimum
            model.Optimize();

            // Load the results
            var results = model.Get(GRB.DoubleAttr.X, new GRBVar[] { xS, xL });

            return new Entities.ProductionTarget() { Small = Convert.ToInt32(results[0]), Large = Convert.ToInt32(results[1]) };
        }

        private static GRBVar CreateLargeVasesVariable(double glazeSupply, GRBModel model)
        {
            return model.AddVar(0.0, glazeSupply, 0.0, GRB.INTEGER, "xL");
        }

        private static GRBVar CreateSmallVasesVariable(double claySupply, GRBModel model)
        {
            return model.AddVar(0.0, claySupply, 0.0, GRB.INTEGER, "xS");
        }

        private static void CreateConstraints(GRBModel model, GRBVar xS, GRBVar xL, double claySupply, double glazeSupply)
        {
            model.AddConstr(xS + 4 * xL <= claySupply, "clay");
            model.AddConstr(xS + 2 * xL <= glazeSupply, "glaze");
            model.Update();
        }
    }
}
