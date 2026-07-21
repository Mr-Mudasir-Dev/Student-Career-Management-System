using Application.Common;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Command.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public DeleteBookHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _uow.BookRepository.GetByIdAsync(request.Id, cancellationToken);

            if (book == null)
                throw new NotFoundException("Book", request.Id);

            _uow.BookRepository.DeleteAsync(book);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Book deleted successfully!");
        }
    }
}
