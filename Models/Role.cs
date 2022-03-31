using System;
using System.Collections.Generic;

#nullable disable

namespace researchOnlineWebsite.Models
{
    public partial class Role
    {
        public Role()
        {
            AdminUsers = new HashSet<AdminUser>();
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<AdminUser> AdminUsers { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
