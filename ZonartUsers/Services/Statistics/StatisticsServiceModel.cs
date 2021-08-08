using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZonartUsers.Services.Statistics
{
    public class StatisticsServiceModel
    {
        public int TotalTemplates { get; init; }

        public int TotalUsers { get; init; }

        public int TotalOrders { get; init; }
    }
}
