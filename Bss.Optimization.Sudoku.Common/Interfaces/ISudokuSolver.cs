using Bss.Optimization.Sudoku.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Sudoku.Interfaces
{
    public interface ISudokuSolver
    {
        IEnumerable<Grid> Solve(IEnumerable<Hint> hints);
    }
}
