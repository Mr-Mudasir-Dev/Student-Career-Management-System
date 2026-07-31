using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Queries.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
