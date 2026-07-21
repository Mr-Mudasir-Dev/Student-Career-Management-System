using Application.Common;
using Application.Features.Genre.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Queries.GetGenreById
{
    public class GetGenreByIdQuery : IRequest<Result<GenreDto>>
    {
        public int Id { get; set; }
    }
}
