using System;
using System.Collections.Generic;

#nullable disable

namespace LeaderGroupStore.Core.DomainEntities
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product>  Products { get; set; }
        public static Category CreateWithId(int id) => new Category
        {
            Id = id
        };
    }
}
