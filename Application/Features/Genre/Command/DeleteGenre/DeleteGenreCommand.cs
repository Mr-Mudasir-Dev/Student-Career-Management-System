using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Command.DeleteGenre
{
    public class DeleteGenreCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
