using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bss.Optimization.Appetizers.Entities;
using Google.OrTools.LinearSolver;

namespace Bss.Optimization.Appetizers.Glop
{
    public class Model: Interfaces.IAppetizerOptimizer
    {
        Solver _model;
        Variable[] _itemQuantityVariable;
        Objective _objective;
        IEnumerable<MenuItem> _items;
        double _totalPrice;


        public OptimizationResults GetQuantities(IEnumerable<MenuItem> items, double totalPrice)
        {
            _items = items;
            _totalPrice = totalPrice;
            _model = Solver.CreateSolver("MipSolver", "CBC_MIXED_INTEGER_PROGRAMMING");

            CreateVariables();
            CreateObjective();
            CreateConstraints();

            var results = new OptimizationResults();

            var optimizationStatus = _model.Solve();
            if (optimizationStatus == Solver.OPTIMAL)
            {
                int n = _itemQuantityVariable.Count();
                results.Items = new int[n];
                results.ObjectiveValue = 0.0;

                foreach (var item in _items)
                    results.Items[item.Id] = Convert.ToInt32(_itemQuantityVariable[item.Id].SolutionValue());
                results.ObjectiveValue = _objective.Value();
            }

            return results;
        }

        private void CreateVariables()
        {
            _itemQuantityVariable = new Variable[_items.Count()];
            foreach (var item in _items)
                _itemQuantityVariable[item.Id] = _model.MakeIntVar(0.0, 8.0, $"{item.Name}_Quantity");
        }

        private void CreateObjective()
        {
            // Minimize the total cost of items selected
            _objective = _model.Objective();
            _objective.SetMinimization();
            foreach (var item in _items)
                _objective.SetCoefficient(_itemQuantityVariable[item.Id], item.Cost);
        }

        private void CreateConstraints()
        {
            // Total price of items selected must be exactly equal to the price specified
            var profitConstraint = _model.MakeConstraint($"SumOfPrices_Equals_{_totalPrice.ToString()}");
            profitConstraint.SetLb(_totalPrice);
            profitConstraint.SetUb(_totalPrice);
            foreach (var item in _items)
                profitConstraint.SetCoefficient(_itemQuantityVariable[item.Id], item.Price);
        }

    }
}
