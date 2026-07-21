using Domain.Common;
using Domain.Enums.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CoverImage { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public BookLanguage Language { get; set; }
        public bool IsBestseller { get; set; } = false;
        public bool IsNewArrival { get; set; } = false;
        public bool IsAvailable { get; set; } = true;

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
