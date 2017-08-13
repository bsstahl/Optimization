using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new Bss.Optimization.Sudoku.GoogleCp.Model();
            var results = model.Solve(null);
        }
    }
}
