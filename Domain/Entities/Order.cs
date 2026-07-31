using Domain.Common;
using Domain.Enums.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string ShippingAddress { get; set; } = string.Empty;

        // Navigation
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
