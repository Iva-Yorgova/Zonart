using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZonartUsers.Services.Statistics
{
    public class StatisticsServiceModel
    {
        public int TotalTemplates { get; set; }

        public int TotalUsers { get; set; }

        public int TotalOrders { get; set; }

        public int TotalContacts { get; set; }
    }
}
