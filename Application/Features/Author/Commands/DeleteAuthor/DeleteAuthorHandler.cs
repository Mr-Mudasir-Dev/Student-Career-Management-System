using Application.Common;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Commands.DeleteAuthor
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public DeleteAuthorHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var result = await _uow.AuthorRepository.GetByIdAsync(request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Author", request.Id);

            _uow.AuthorRepository.DeleteAsync(result);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Deleted author successfully");
        }
    }
}
