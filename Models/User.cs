using System;
using System.Collections.Generic;

#nullable disable

namespace researchOnlineWebsite.Models
{
    public partial class User
    {
        public User()
        {
            Researches = new HashSet<Research>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
        public int? RoleId { get; set; }
        public int? Telephone { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Research> Researches { get; set; }
    }
}
