using Application.Common;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Command.DeleteGenre
{
    public class DeleteGenrehandler : IRequestHandler<DeleteGenreCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public DeleteGenrehandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _uow.GenreRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if(genre == null)
                throw new NotFoundException("Genre", request.Id);

            _uow.GenreRepository.DeleteAsync(genre);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Deleted genre Successfully!");
        }
    }
}
