using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bss.Optimization.Pottery.Interfaces;
using System.Linq;

namespace BssOptimization.Pottery.Test
{
    [TestClass]
    public class Optimizer_GetFeasibleTargets_Should
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ReturnAllFeasibleTargets()
        {
            var claySupply = 24;
            var glazeSupply = 16;

            IPotteryOptimizer engine = (null as IPotteryOptimizer).Create();
            var results = engine.GetFeasibleTargets(claySupply, glazeSupply);

            TestContext.WriteLine($"Number of solutions: {results.Count()}");
            TestContext.WriteLine("Small, Large");
            foreach (var targetSet in results)
                TestContext.WriteLine($"{targetSet.Small},{targetSet.Large}");

            Assert.AreEqual(71, results.Count());
        }

    }
}
