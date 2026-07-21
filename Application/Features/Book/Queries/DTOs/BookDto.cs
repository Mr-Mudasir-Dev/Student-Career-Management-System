using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CoverImage { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public bool IsBestseller { get; set; }
        public bool IsNewArrival { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }

        
        public List<string> Authors { get; set; } = new();
        public List<string> Genres { get; set; } = new();
    }
}
