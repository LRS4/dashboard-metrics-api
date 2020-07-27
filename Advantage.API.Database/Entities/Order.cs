using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advantage.API.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public decimal Total { get; set; }
        public DateTime Placed { get; set; }
        public DateTime? Completed { get; set; }

    }
}
