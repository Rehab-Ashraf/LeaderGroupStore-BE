using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderGroupStore.Models.Products
{
    public class ProductInoutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string Price { get; set; }
        public int CategoryId { get; set; }
    }
}
