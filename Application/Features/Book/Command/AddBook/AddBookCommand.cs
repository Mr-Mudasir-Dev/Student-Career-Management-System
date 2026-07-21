using Application.Common;
using Domain.Enums.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Command.AddBook
{
    public class AddBookCommand : IRequest<Result>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CoverImage { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public BookLanguage Language { get; set; }
        public bool IsBestseller { get; set; }
        public bool IsNewArrival { get; set; }

        // ✅ Author or Genre Ids
        public List<int> AuthorIds { get; set; } = new();
        public List<int> GenreIds { get; set; } = new();
    }
}
