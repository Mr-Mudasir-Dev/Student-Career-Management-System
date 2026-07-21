using Application.Common;
using Application.Features.Genre.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Queries.GetGenreBySeach
{
    public class GetGenreBySearchQuery : IRequest<Result<List<GenreDto>>>
    {
        public string Search { get; set; } = string.Empty;
    }
}
