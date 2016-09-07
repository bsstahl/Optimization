using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bss.Optimization.Appetizers.Entities
{
    public class MenuItemCollection : List<MenuItem>
    {
        public MenuItemCollection()
        {
            this.Add(new MenuItem() { Id = 0, Name = "Mixed Fruit", Price = 2.15, Cost = 2.00 });
            this.Add(new MenuItem() { Id = 1, Name = "French Fries", Price = 2.75, Cost = 0.75 });
                         
            this.Add(new MenuItem() { Id = 2, Name = "Side Salad", Price = 3.35, Cost = 1.75 });
            // this.Add(new MenuItem() { Id = 2, Name = "Side Salad", Price = 3.01, Cost = 1.75 });
                         
            this.Add(new MenuItem() { Id = 3, Name = "Hot Wings", Price = 3.55, Cost = 2.20 });
            this.Add(new MenuItem() { Id = 4, Name = "Cheese Sticks", Price = 4.20, Cost = 3.07 });
            this.Add(new MenuItem() { Id = 5, Name = "Sampler Plate", Price = 5.80, Cost = 3.60 });
        }
    }
}
