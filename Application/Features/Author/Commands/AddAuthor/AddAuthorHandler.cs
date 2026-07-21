using Application.Common;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Commands.AddAuthor
{
    public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public AddAuthorHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var exists = await _uow.AuthorRepository.ExistsByNameAsync(request.Name, cancellationToken);

            if (exists)
                throw new ConflictException($"author '{request.Name}' already taken");

            var author = new Domain.Entities.Author
            {
                Name = request.Name,
                Bio = request.Bio,
            };

            await _uow.AuthorRepository.AddAsync(author, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Author added successfully!");
        }
    }
}
