﻿using System;
using System.Collections.Generic;

#nullable disable

namespace LeaderGroupStore.Core.DomainEntities
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
