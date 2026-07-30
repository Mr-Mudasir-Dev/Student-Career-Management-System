using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Ratting { get; set; }
        public string Comment { get; set; } = string.Empty;


        public Book Book { get; set; } = null!;
        public Order Order { get; set; } = null!;
    }
}
