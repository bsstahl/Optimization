using Bss.Optimization.Sudoku.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Sudoku.Extensions
{
    public static class HintExtensions
    {
        public static void AddHint(this IList<GridCell> hints, byte x, byte y, byte value)
        {
            hints.Add(GridCell.Create(x, y, value));
        }
    }
}
