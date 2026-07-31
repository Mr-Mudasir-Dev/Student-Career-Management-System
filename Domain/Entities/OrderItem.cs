using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
