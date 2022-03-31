using System;
using System.Collections.Generic;

#nullable disable

namespace researchOnlineWebsite.Models
{
    public partial class UserResearch
    {
        public int? UserId { get; set; }
        public int? ResearchId { get; set; }

        public virtual Research Research { get; set; }
        public virtual User User { get; set; }
    }
}
