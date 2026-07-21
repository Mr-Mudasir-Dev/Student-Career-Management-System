using Application.Common;
using Application.Features.Book.Queries.DTOs;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.GetByAuthorId
{
    public class GetByAuthorIdBookHandler : IRequestHandler<GetByAuthorIdBookQuery, Result<List<BookDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetByAuthorIdBookHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<BookDto>>> Handle(GetByAuthorIdBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _uow.BookRepository
                .GetByAuthorIdAsync(request.Id, cancellationToken);


            if (!book.Any())
                throw new NotFoundException("Book.Author", request.Id);

            var dto = book.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                CoverImage = b.CoverImage,
                Price = b.Price,
                PublishedDate = b.PublishedDate,
                Language = b.Language.ToString(),
                IsBestseller = b.IsBestseller,
                IsNewArrival = b.IsNewArrival,
                IsAvailable = b.IsAvailable,
                CreatedAt = b.CreatedAt,

                Authors = b.BookAuthors
                .Select(ba => ba.Author.Name)
                .ToList(),

                Genres = b.BookGenres
                .Select(bg => bg.Genre.Name)
                .ToList()
            }).ToList();

            return Result<List<BookDto>>.Success(dto, "Book fetched successfully");
        }
    }
}
