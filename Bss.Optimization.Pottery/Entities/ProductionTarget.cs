using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Pottery.Entities
{
    public class ProductionTarget
    {
        public int Small { get; set; }

        public int Large { get; set; }

        public double ExpectedRevenue()
        {
            return (3 * this.Small) + (9 * this.Large);
        }
    }
}
