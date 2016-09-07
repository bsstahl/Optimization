using Gurobi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Appetizers.Entities;

namespace Bss.Optimization.Appetizers.Gurobi
{
    public class Model: Interfaces.IAppetizerOptimizer
    {
        GRBEnv _env;
        GRBModel _model;
        GRBVar[] _v;

        public Model()
        {
            _env = new GRBEnv("Optimization.log");
            _model = new GRBModel(_env);
        }

        public OptimizationResults GetQuantities(IEnumerable<MenuItem> items, double totalPrice)
        {
            CreateVariables(items);
            CreateConstraints(totalPrice, items);
            CreateObjective(items);

            _model.Optimize();

            var results = new OptimizationResults();
            results.Items = _model.Get(GRB.DoubleAttr.X, _v).ToIntArray();
            results.ObjectiveValue = _model.Get(GRB.DoubleAttr.ObjVal);
            return results;
        }

        private void CreateVariables(IEnumerable<MenuItem> items)
        {
            int n = items.Count();
            var itemArray = items.ToArray();

            _v = new GRBVar[n];

            for (int i = 0; i < n; i++)
            {
                _v[i] = _model.AddVar(0.0, 8.0, 0.0, GRB.INTEGER, $"x[{i}]");
                Console.WriteLine($"x[{i}] - {itemArray[i].Name}");
            }

            _model.Update();
        }

        private void CreateObjective(IEnumerable<MenuItem> items)
        {
            // Minimize the cost of the items that have been selected
            GRBLinExpr expr = 0.0;
            foreach (var item in items)
                expr.AddTerm(item.Cost, _v[item.Id]);

            _model.SetObjective(expr, GRB.MINIMIZE);
        }

        private void CreateConstraints(double totalPrice, IEnumerable<MenuItem> items)
        {
            // Total price of items selected, must be exactly 15.05
            GRBLinExpr expr = 0.0;
            foreach (var item in items)
                expr.AddTerm(item.Price, _v[item.Id]);

            _model.AddConstr(expr == totalPrice, $"Sum_Prices_Equal_{totalPrice}");
            Console.WriteLine($"Sum_Prices_Equals_{totalPrice}");

            _model.Update();
        }

    }
}
