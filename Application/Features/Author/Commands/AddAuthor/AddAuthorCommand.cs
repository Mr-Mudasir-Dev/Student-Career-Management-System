using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Commands.AddAuthor
{
    public class AddAuthorCommand : IRequest<Result>
    {
        public string Name { get; set; } = string.Empty;
        public string? Bio {  get; set; }
    }
}
