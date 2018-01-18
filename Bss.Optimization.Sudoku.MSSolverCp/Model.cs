using Bss.Optimization.Sudoku.Entities;
using Bss.Optimization.Sudoku.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Sudoku.MSSolverCp
{
    public class Model : ISudokuSolver
    {
        public IEnumerable<GridCell[]> Solve(IEnumerable<GridCell> hints)
        {
            throw new NotImplementedException();
        }
    }
}
