
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ZonartUsers.Data.Models
{
    using static GlobalConstants;

    public class Bag
    {
        [Required]
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
