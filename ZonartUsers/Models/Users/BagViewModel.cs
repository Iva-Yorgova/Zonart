using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Models.Users
{
    public class BagViewModel
    {
        public int Id { get; set; }

        public Template Template { get; set; }

        public string TemplateName { get; set; } 

        public double TemplatePrice { get; set; }

        public string ImageUrl { get; set; }

    }
}
