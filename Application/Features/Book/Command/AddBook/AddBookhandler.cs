using Application.Common;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Command.AddBook
{
    public class AddBookhandler : IRequestHandler<AddBookCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public AddBookhandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var exists = await _uow.BookRepository
                .ExistsByTitleAsync(request.Title, cancellationToken);

            if (exists)
                throw new ConflictException($"Book '{request.Title}' already exists.");

            foreach(var authorId in request.AuthorIds)
            {
                var authorExist = await _uow.AuthorRepository
                    .ExistsAsync(authorId, cancellationToken);

                if (!authorExist)
                    throw new NotFoundException("Author", authorId);
            }

            foreach(var genreId in request.GenreIds)
            {
                var genreExists = await _uow.GenreRepository
                    .ExistsAsync(genreId, cancellationToken);

                if(!genreExists)
                    throw new NotFoundException("Genre", genreId);
            }

            var book = new Domain.Entities.Book
            {
                Title = request.Title,
                Description = request.Description,
                CoverImage = request.CoverImage,
                Price = request.Price,
                PublishedDate = request.PublishedDate,
                Language = request.Language,
                IsBestseller = request.IsBestseller,
                IsNewArrival = request.IsNewArrival,

                BookAuthors = request.AuthorIds.Select(authorid => new Domain.Entities.BookAuthor
                {
                    AuthorId = authorid
                }).ToList(),

                BookGenres = request.GenreIds.Select(genreId => new Domain.Entities.BookGenre
                {
                    GenreId = genreId
                }).ToList()
            };

            await _uow.BookRepository.AddAsync(book, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Book added successfully!");
        }
    }
}
