using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Templates
{
    public class AllTemplatesModel
    {  
        public const int TemplatesPerPage = 10;

        public string Category { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalTemplates { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<TemplateListingViewModel> Templates { get; set; }
    }
}
