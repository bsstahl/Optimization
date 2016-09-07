using System;
using System.Collections.Generic;
using System.Linq;
using Google.OrTools.LinearSolver;

namespace Bss.Optimization.Appetizers
{
    public class Model
    {
        Solver _model;
        Variable[] _itemQuantityVariable;
        Variable _itemDiscountVariable;
        Objective _objective;
        IEnumerable<MenuItem> _items;
        double _totalPrice;

        public Model(IEnumerable<MenuItem> items, double totalPrice)
        {
            _items = items;
            _totalPrice = totalPrice;
            _model = Solver.CreateSolver("MipSolver", "CBC_MIXED_INTEGER_PROGRAMMING");
            CreateVariables();
            CreateObjective();
            CreateConstraints(true);
        }

        public OptimizationResults Optimize()
        {
            var results = new OptimizationResults();

            var optimizationStatus = _model.Solve();
            if (optimizationStatus == Solver.OPTIMAL)
            {
                int n = _itemQuantityVariable.Count();
                results.Items = new double[n];
                results.ObjectiveValue = 0.0;

                foreach (var item in _items)
                    results.Items[item.Id] = _itemQuantityVariable[item.Id].SolutionValue();
                results.ObjectiveValue = (_objective.Value() / 100);
                results.Discount = (_itemDiscountVariable.SolutionValue() / 100);
            }

            return results;
        }

        private void CreateVariables()
        {
            _itemDiscountVariable = _model.MakeIntVar(0.0, _totalPrice * 100, $"DiscountInCents");

            _itemQuantityVariable = new Variable[_items.Count()];
            foreach (var item in _items)
                _itemQuantityVariable[item.Id] = _model.MakeIntVar(0.0, _totalPrice, $"{item.Name}_Quantity");
        }

        private void CreateObjective()
        {
            // Maximize the profit on the sale
            _objective = _model.Objective();
            _objective.SetMaximization();
            _objective.AddOffset(_totalPrice * 100);
            _objective.SetCoefficient(_itemDiscountVariable, -1.0);
            foreach (var item in _items)
                _objective.SetCoefficient(_itemQuantityVariable[item.Id], (-item.Cost * 100));
        }

        private void CreateConstraints(bool allowDiscounts)
        {
            Constraint discountConstraint = _model.MakeConstraint("Discount");
            discountConstraint.SetCoefficient(_itemDiscountVariable, 1.0);
            discountConstraint.SetLb(0.0);
            if (allowDiscounts)
                discountConstraint.SetUb(_totalPrice * 100);
            else
                discountConstraint.SetUb(0.0);

            // Total price of items selected (minus discount) must be exactly 15.05
            var profitConstraint = _model.MakeConstraint($"SumOfPrices_Equals_{_totalPrice.ToString()}");
            profitConstraint.SetLb(_totalPrice * 100);
            profitConstraint.SetUb(_totalPrice * 100);
            profitConstraint.SetCoefficient(_itemDiscountVariable, -1.0);
            foreach (var item in _items)
                profitConstraint.SetCoefficient(_itemQuantityVariable[item.Id], (item.Price * 100));
        }

    }
}
