using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Appetizers.Gurobi
{
    public static class Extensions
    {
        public static int[] ToIntArray(this double[] source)
        {
            var result = new int[source.Count()];
            for (int i = 0; i < source.Count(); i++)
                result[i] = Convert.ToInt32(source[i]);
            return result;
        }
    }
}
