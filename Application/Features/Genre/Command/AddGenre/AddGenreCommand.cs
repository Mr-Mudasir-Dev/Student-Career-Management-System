using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Command.AddGenre
{
    public class AddGenreCommand : IRequest<Result>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
