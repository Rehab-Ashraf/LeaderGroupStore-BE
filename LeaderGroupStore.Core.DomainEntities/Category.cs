using System;
using System.Collections.Generic;

#nullable disable

namespace LeaderGroupStore.Core.DomainEntities
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
