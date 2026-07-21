using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Command.DeleteBook
{
    public class DeleteBookCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
