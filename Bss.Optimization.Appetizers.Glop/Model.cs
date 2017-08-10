using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Appetizers.Entities;
using Google.OrTools.ConstraintSolver;

namespace Bss.Optimization.Appetizers.Glop
{
    public class Model : Interfaces.IAppetizerOptimizer
    {
        public OptimizationResults GetQuantities(IEnumerable<MenuItem> items, double totalPrice)
        {
            var model = new Solver("CPSolver");
            IntVar[] itemQuantityVariable = CreateVariables(model, items);
            CreateConstraints(model, itemQuantityVariable, items, totalPrice.ToCents());

            DecisionBuilder decisionBuilder = model.MakePhase(itemQuantityVariable, Solver.INT_VAR_DEFAULT, Solver.INT_VALUE_DEFAULT);
            var optimizationStatus = model.Solve(decisionBuilder);

            if (!optimizationStatus)
                throw new InvalidOperationException("Solution not found");

            var results = new List<OptimizationResults>();
            Console.WriteLine("Feasible Solutions:");
            while (model.NextSolution())
            {
                var solution = new OptimizationResults();

                int n = itemQuantityVariable.Count();
                solution.Items = new int[n];

                foreach (var item in items)
                    solution.Items[item.Id] = Convert.ToInt32(itemQuantityVariable[item.Id].Value());
                solution.ObjectiveValue = solution.CalculateObjective(items);

                Console.WriteLine(solution.ToString(items));

                results.Add(solution);
            }

            // returns the solution with the greatest # of different items (most diverse assortment)
            // return results.Find(r => r.DistinctItemCount() == results.Max(s => s.DistinctItemCount()));

            // returns the solution with the fewest # of any single item (reduce redundancy)
            // return results.Find(r => r.MaxSingleItemCount() == results.Min(s => s.MaxSingleItemCount()));

            // returns the solution with the fewest # of plates to carry (least work for the waiter)
            // return results.Find(r => r.PlateCount() == results.Min(s => s.PlateCount()));

            // returns the solution with the fewest # of different items (least work for the kitchen)
            return results.Find(r => r.DistinctItemCount() == results.Min(s => s.DistinctItemCount()));
        }

        private static IntVar[] CreateVariables(Solver model, IEnumerable<MenuItem> items)
        {
            var variables = new IntVar[items.Count()];
            foreach (var item in items)
                variables[item.Id] = model.MakeIntVar(0, 8, $"{item.Name}_Quantity");
            return variables;
        }

        private static void CreateConstraints(Solver model, IntVar[] decisionVariables, IEnumerable<MenuItem> items, int priceInCents)
        {
            // Total price of items selected must be exactly equal to the price specified
            IntExpr exp = decisionVariables[0] * 0; // TODO: Is there a better way to construct an empty expression?
            foreach (var item in items)
                exp += decisionVariables[item.Id] * item.Price.ToCents();
            model.Add(exp == priceInCents);
        }

    }
}
