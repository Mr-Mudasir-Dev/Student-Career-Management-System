using Application.Common;
using Application.Features.Genre.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Queries.GetAllGenre
{
    public class GetAllGenreHandler : IRequestHandler<GetAllGenreQuery, Result<List<GenreDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllGenreHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<GenreDto>>> Handle(GetAllGenreQuery request, CancellationToken cancellationToken)
        {
            var genre = await _uow.GenreRepository.GetAllAsync(cancellationToken);

            if(!genre.Any())
                return Result<List<GenreDto>>.Success(new List<GenreDto>(), "Genre not found");

            var dto = genre.Select(x => new GenreDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
            }).ToList();

            return Result<List<GenreDto>>.Success(dto, "Genre fetched successfuly!");
        }
    }
}
