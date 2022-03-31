using System;
using System.Collections.Generic;

#nullable disable

namespace researchOnlineWebsite.Models
{
    public partial class Status
    {
        public Status()
        {
            Researches = new HashSet<Research>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Research> Researches { get; set; }
    }
}
