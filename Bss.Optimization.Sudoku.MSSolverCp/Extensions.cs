using Bss.Optimization.Sudoku.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Sudoku.MSSolverCp
{
    public static class Extensions
    {
        public static void AddHints(this Microsoft.SolverFoundation.Services.Model model, Microsoft.SolverFoundation.Services.Decision[] x, IEnumerable<GridCell> hints)
        {
            // Constrains each cell we know the value of to that specific value
            if (hints != null)
                foreach (var hint in hints)
                {
                    int index = (hint.Y * 9) + hint.X;
                    model.AddConstraint($"Cell_{hint.X}_{hint.Y}_Equals_{hint.Value}", x[index] == hint.Value);
                }
        }

        public static Microsoft.SolverFoundation.Services.Decision[] CreateVariables(this Microsoft.SolverFoundation.Services.Model model, Microsoft.SolverFoundation.Services.Domain digits)
        {
            var grid = new Microsoft.SolverFoundation.Services.Decision[81];
            for (int i = 0; i < 81; i++)
            {
                grid[i] = new Microsoft.SolverFoundation.Services.Decision(digits, $"cell_{i}");
                model.AddDecision(grid[i]);
            }

            return grid;
        }

        public static void CreateConstraints(this Microsoft.SolverFoundation.Services.Model model, Microsoft.SolverFoundation.Services.Decision[] grid)
        {
            for (int group = 0; group < 9; group++)
            {
                // Create row constraints
                var row = new List<Microsoft.SolverFoundation.Services.Decision>();
                for (int i = 0; i < 9; i++)
                {
                    int index = (group * 9) + i;
                    row.Add(grid[index]);
                }
                model.AddConstraint($"Row{group}_AllDifferent", Microsoft.SolverFoundation.Services.Model.AllDifferent(row.ToArray()));

                // Create column constraints
                var col = new List<Microsoft.SolverFoundation.Services.Decision>();
                for (int i = 0; i < 9; i++)
                {
                    int index = (i * 9) + group;
                    col.Add(grid[index]);
                }
                model.AddConstraint($"Col{group}_AllDifferent", Microsoft.SolverFoundation.Services.Model.AllDifferent(col.ToArray()));

                // Create region constraints
                int regionStartX = (group % 3) * 3;
                int regionStartY = (group / 3) * 3;
                var region = new List<Microsoft.SolverFoundation.Services.Decision>();
                for (int i = 0; i < 9; i++)
                {
                    int deltaX = (i % 3);
                    int deltaY = (i / 3);
                    int xLoc = regionStartX + deltaX;
                    int yLoc = regionStartY + deltaY;
                    int index = (yLoc * 9) + xLoc;
                    region.Add(grid[index]);
                }
                model.AddConstraint($"Region{group}_AllDifferent", Microsoft.SolverFoundation.Services.Model.AllDifferent(region.ToArray()));
            }
        }

    }
}
