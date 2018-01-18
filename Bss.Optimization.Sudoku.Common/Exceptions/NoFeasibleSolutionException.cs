using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Sudoku.Exceptions
{
    public class NoFeasibleSolutionException : Exception
    {
        public NoFeasibleSolutionException() : base("Solution not found")
        { }
    }
}
