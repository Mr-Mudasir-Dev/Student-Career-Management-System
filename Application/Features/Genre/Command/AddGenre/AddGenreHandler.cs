using Application.Common;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Command.AddGenre
{
    public class AddGenreHandler : IRequestHandler<AddGenreCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public AddGenreHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            var result = await _uow.GenreRepository
                .ExistsByNameAsync(request.Name, cancellationToken);

            if(result)
                throw new ConflictException($"Genre '{request.Name}' already taken");

            var Genre = new Domain.Entities.Genre
            {
                Name = request.Name,
                Description = request.Description,
            };

            await _uow.GenreRepository.AddAsync(Genre, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Genre Added successfuly!");
        }
    }
}
