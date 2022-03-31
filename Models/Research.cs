using System;
using System.Collections.Generic;

#nullable disable

namespace researchOnlineWebsite.Models
{
    public partial class Research
    {
        public int? NumberOfPage { get; set; }
        public string Language { get; set; }
        public int ResearchId { get; set; }
        public DateTime? Date { get; set; }
        public string Specialize { get; set; }
        public string Title { get; set; }
        public int? StatId { get; set; }
        public int? UserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string RedirectTo { get; set; }
        public double? Price { get; set; }

        public virtual Status Stat { get; set; }
        public virtual User User { get; set; }
    }
}
