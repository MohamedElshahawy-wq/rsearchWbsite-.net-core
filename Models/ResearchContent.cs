using System;
using System.Collections.Generic;

#nullable disable

namespace researchOnlineWebsite.Models
{
    public partial class ResearchContent
    {
        public string ResearchPdf { get; set; }
        public string ResearchPdfUrl { get; set; }
        public int ResearchId { get; set; }
        public string Specialize { get; set; }
        public int? StatId { get; set; }
    }
}
