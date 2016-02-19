using Bss.Optimization.Pottery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Pottery.Test
{
    public static class Extensions
    {
        public static IPotteryOptimizer Create(this IPotteryOptimizer ignore)
        {
            // TODO: Set which optimizer you want to use in the tests here

            return new Bss.Optimization.Pottery.Naive.Optimizer();
            // return new Bss.Optimization.Pottery.LP.Optimizer();
        }
    }
}
