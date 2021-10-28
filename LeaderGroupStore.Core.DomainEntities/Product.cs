using System;
using System.Collections.Generic;

#nullable disable

namespace LeaderGroupStore.Core.DomainEntities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string Price { get; set; }

        public virtual Category Category { get; set; }
    }
}
