using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Sudoku.Entities;
using Bss.Optimization.Sudoku.Interfaces;
using Google.OrTools.ConstraintSolver;

namespace Bss.Optimization.Sudoku.GoogleCp
{
    public class Model : ISudokuSolver
    {

        public IEnumerable<GridCell[]> Solve(IEnumerable<GridCell> hints)
        {
            var model = new Solver("CPSolver");

            // Create variables
            var x = new IntVar[81];
            for (int i = 0; i < 81; i++)
                x[i] = model.MakeIntVar(1, 9, $"x[{i}]");

            // Create constraints
            for (int group = 0; group < 9; group++)
            {
                // Create row constraints
                IntVarVector row = new IntVarVector();
                for (int i = 0; i < 9; i++)
                {
                    int index = (group * 9) + i;
                    row.Add(x[index]);
                }
                var rowConstraint = model.MakeAllDifferent(row);
                model.Add(rowConstraint);

                // Create column constraints
                IntVarVector col = new IntVarVector();
                for (int i = 0; i < 9; i++)
                {
                    int index = (i * 9) + group;
                    col.Add(x[index]);
                }
                var colConstraint = model.MakeAllDifferent(col);
                model.Add(colConstraint);

                // Create region constraints
                int regionStartX = (group % 3) * 3;
                int regionStartY = (group / 3) * 3;
                IntVarVector region = new IntVarVector();
                for (int i = 0; i < 9; i++)
                {
                    int deltaX = (i % 3);
                    int deltaY = (i / 3);
                    int xLoc = regionStartX + deltaX;
                    int yLoc = regionStartY + deltaY;
                    int index = (yLoc * 9) + xLoc;
                    region.Add(x[index]);
                }
                var regionConstraint = model.MakeAllDifferent(region);
                model.Add(regionConstraint);
            }

            // Add hints
            foreach (var hint in hints)
            {
                int index = (hint.Y * 9) + hint.X;
                model.Add(x[index] == hint.Value);
            }

            DecisionBuilder decisionBuilder = model.MakePhase(x, Solver.INT_VAR_DEFAULT, Solver.INT_VALUE_DEFAULT);
            var optimizationStatus = model.Solve(decisionBuilder);

            if (!optimizationStatus)
                throw new InvalidOperationException("Solution not found");

            var results = new List<GridCell[]>();
            while (model.NextSolution())
            {
                var solution = new GridCell[81];

                for (int i = 0; i < 81; i++)
                {
                    byte xLoc = Convert.ToByte(i % 9);
                    byte yLoc = Convert.ToByte(i / 9);
                    solution[i] = GridCell.Create(xLoc, yLoc, Convert.ToByte(x[i].Value()));
                }

                results.Add(solution);
            }

            return results;
        }


        // Takes a collection of MenuItem
        // Returns an OptimizationResult (collection of grid solutions?)
        public Object GetQuantities(IEnumerable<Object> items, double totalPrice)
        {
            var model = new Solver("CPSolver");
            IntVar[] itemQuantityVariable = CreateVariables(model, items);
            CreateConstraints(model, itemQuantityVariable, items, totalPrice);

            DecisionBuilder decisionBuilder = model.MakePhase(itemQuantityVariable, Solver.INT_VAR_DEFAULT, Solver.INT_VALUE_DEFAULT);
            var optimizationStatus = model.Solve(decisionBuilder);

            if (!optimizationStatus)
                throw new InvalidOperationException("Solution not found");

            // Returns an OptimizationResult (collection of grid solutions?)
            var results = new List<object>();
            Console.WriteLine("Feasible Solutions:");
            while (model.NextSolution())
            {
                var solution = new object(); // OptimizationResult

                int n = itemQuantityVariable.Count();
                // solution.Items = new int[n];

                //foreach (var item in items)
                //    solution.Items[item.Id] = Convert.ToInt32(itemQuantityVariable[item.Id].Value());
                //solution.ObjectiveValue = solution.CalculateObjective(items);

                Console.WriteLine(solution.ToString());

                results.Add(solution);
            }

            // returns the solution with the greatest # of different items (most diverse assortment)
            // return results.Find(r => r.DistinctItemCount() == results.Max(s => s.DistinctItemCount()));

            // returns the solution with the fewest # of any single item (reduce redundancy)
            // return results.Find(r => r.MaxSingleItemCount() == results.Min(s => s.MaxSingleItemCount()));

            // returns the solution with the fewest # of plates to carry (least work for the waiter)
            // return results.Find(r => r.PlateCount() == results.Min(s => s.PlateCount()));

            // returns the solution with the fewest # of different items (least work for the kitchen)
            // return results.Find(r => r.DistinctItemCount() == results.Min(s => s.DistinctItemCount()));

            throw new NotImplementedException();
        }

        private static IntVar[] CreateVariables(Solver model, IEnumerable<object> items)
        {
            var variables = new IntVar[items.Count()];
            //foreach (var item in items)
            //    variables[item.Id] = model.MakeIntVar(0, 8, $"{item.Name}_Quantity");
            return variables;
        }

        private static void CreateConstraints(Solver model, IntVar[] decisionVariables, IEnumerable<object> items, double price)
        {
            // Total price of items selected must be exactly equal to the price specified
            // IntExpr exp = decisionVariables[0] * 0; // TODO: Is there a better way to construct an empty expression?
            //foreach (var item in items)
            //    exp += decisionVariables[item.Id] * item.Price.ToCents();
            // model.Add(exp == price);
        }

    }
}
