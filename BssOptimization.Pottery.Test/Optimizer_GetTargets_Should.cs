using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bss.Optimization.Pottery.Interfaces;

namespace BssOptimization.Pottery.Test
{
    [TestClass]
    public class Optimizer_GetTargets_Should
    {
        public TestContext TestContext { get; set; }


        [TestMethod]
        public void ReturnTheOptimumNumberOfSmallVases()
        {
            var claySupply = 24;
            var glazeSupply = 16;

            IPotteryOptimizer engine = (null as IPotteryOptimizer).Create();
            var result = engine.GetTargets(claySupply, glazeSupply);
            Assert.AreEqual(8, result.Small);
        }

        [TestMethod]
        public void ReturnTheOptimumNumberOfLargeVases()
        {
            var claySupply = 24;
            var glazeSupply = 16;

            IPotteryOptimizer engine = (null as IPotteryOptimizer).Create();
            var result = engine.GetTargets(claySupply, glazeSupply);
            Assert.AreEqual(4, result.Large);
        }

        [TestMethod]
        public void ReturnTheOptimumNumberOfSmallVasesIfNonStandardConstraintsAreSupplied()
        {
            var claySupply = 37;
            var glazeSupply = 31;

            IPotteryOptimizer engine = (null as IPotteryOptimizer).Create();
            var result = engine.GetTargets(claySupply, glazeSupply);
            Assert.AreEqual(25, result.Small);
        }

        [TestMethod]
        public void ReturnTheOptimumNumberOfLargeVasesIfNonStandardConstraintsAreSupplied()
        {
            var claySupply = 37;
            var glazeSupply = 31;

            IPotteryOptimizer engine = (null as IPotteryOptimizer).Create();
            var result = engine.GetTargets(claySupply, glazeSupply);
            Assert.AreEqual(3, result.Large);
        }

        [TestMethod, Timeout(10000)]
        public void ReturnQuicklyEvenAtScale()
        {
            // This test is expected to fail for any optimizer that 
            // cannot handle a large solution set, such as 
            // the Naive optimizer in this solution.

            var claySupply = Int32.MaxValue;  // Huge value
            var glazeSupply = Int32.MaxValue - 394;  // Huge value picked at random

            IPotteryOptimizer engine = (null as IPotteryOptimizer).Create();
            var result = engine.GetTargets(claySupply, glazeSupply);
        }

    }
}
