using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Sudoku.Exceptions
{
    public class TooManySolutionsException : Exception
    {
        public TooManySolutionsException() : base("Maximum number of solutions exceeded")
        { }

        public TooManySolutionsException(int maxSolutionCount) : base($"Maximum number of solutions ({maxSolutionCount}) exceeded")
        { }
    }
}
