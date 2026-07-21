using Application.Common;
using Application.Features.Genre.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Queries.GetGenreBySeach
{
    public class GetGenreBySearchHandler : IRequestHandler<GetGenreBySearchQuery, Result<List<GenreDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetGenreBySearchHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<GenreDto>>> Handle(GetGenreBySearchQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.GenreRepository
                .SearchByNameAsync(request.Search, cancellationToken);

            if(!result.Any())
                return Result<List<GenreDto>>
                    .Success(new List<GenreDto>(), "Genre not found!");

            var dto = result.Select(x => new GenreDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
            }).ToList();

            return Result<List<GenreDto>>
                    .Success(dto, "Genre fetched successfully!");
        }
    }
}
