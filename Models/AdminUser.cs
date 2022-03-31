using System;
using System.Collections.Generic;

#nullable disable

namespace researchOnlineWebsite.Models
{
    public partial class AdminUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
