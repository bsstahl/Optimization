using Bss.Optimization.Sudoku.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Sudoku.Extensions
{
    public static class DisplayExtensions
    {
        public static string Repeat(this string chars, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < 9; x++)
                sb.Append(chars);
            return sb.ToString();
        }

        public static string GetDisplay(this GridCell[] cells)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("----".Repeat(9));
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    var cell = cells.Single(r => r.X == x && r.Y == y);
                    sb.Append($"| {cell.Value} ");
                }

                sb.AppendLine("|");
                sb.AppendLine("----".Repeat(9));
            }

            return sb.ToString();
        }
    }
}
