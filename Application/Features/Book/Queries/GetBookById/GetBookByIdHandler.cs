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

namespace Application.Features.Book.Queries.GetBookById
{
    internal class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Result<BookDto>>
    {
        private readonly IUnitOfWork _uow;
        public GetBookByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _uow.BookRepository
                .GetByIdWithDetailsAsync(request.Id, cancellationToken);

            if (book == null)
                throw new NotFoundException("Book", request.Id);

            var dto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                CoverImage = book.CoverImage,
                Price = book.Price,
                PublishedDate = book.PublishedDate,
                Language = book.Language.ToString(),
                IsBestseller = book.IsBestseller,
                IsNewArrival = book.IsNewArrival,
                IsAvailable = book.IsAvailable,
                CreatedAt = book.CreatedAt,
                Authors = book.BookAuthors
                 .Select(ba => ba.Author.Name)
                 .ToList(),
                Genres = book.BookGenres
                 .Select(bg => bg.Genre.Name)
                 .ToList()
            };

            return Result<BookDto>.Success(dto, "Book fetched successfully!");
        }
    }
}
