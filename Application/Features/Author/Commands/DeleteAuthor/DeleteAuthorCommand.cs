using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
