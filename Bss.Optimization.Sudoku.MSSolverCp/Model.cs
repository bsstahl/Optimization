using Bss.Optimization.Sudoku.Entities;
using Bss.Optimization.Sudoku.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Microsoft.SolverFoundation.Services;

namespace Bss.Optimization.Sudoku.MSSolverCp
{
    public class Model : ISudokuSolver
    {
        const int MAX_SOLUTIONS = 100000;

        public IEnumerable<GridCell[]> Solve(IEnumerable<GridCell> hints)
        {
            var context = Microsoft.SolverFoundation.Services.SolverContext.GetContext();
            var model = context.CreateModel();
            model.Name = "Sudoku";

            var digits = Microsoft.SolverFoundation.Services.Domain.IntegerRange(1, 9);
            var grid = model.CreateVariables(digits);
            model.CreateConstraints(grid);
            model.AddHints(model.Decisions.ToArray(), hints);

            var solution = context.Solve(new Microsoft.SolverFoundation.Services.ConstraintProgrammingDirective());
            if (solution.Quality != Microsoft.SolverFoundation.Services.SolverQuality.Feasible)
                throw new InvalidOperationException("Solution not found");

            // TODO: Iterate solutions
            var results = new List<GridCell[]>();

            var solutionDetails = new GridCell[81];
            for (int i = 0; i < 81; i++)
            {
                byte xLoc = Convert.ToByte(i % 9);
                byte yLoc = Convert.ToByte(i / 9);
                var value = Convert.ToByte(solution.Decisions.ToArray()[i].GetValues().Single().Single());
                solutionDetails[i] = GridCell.Create(xLoc, yLoc, value);
            }
            results.Add(solutionDetails);

            return results;

        }

    }
}
