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
        public DateTime CreatedAt { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Product CreateAt()
        {

            CreatedAt = DateTime.UtcNow;

            return this;
        }
        public Product UpdateCategory(Category category)
        {
            Category = category;
            return this;
        }
    }
}
