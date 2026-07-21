using Application.Common;
using Application.Features.Genre.Queries.DTOs;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Genre.Queries.GetGenreById
{
    public class GetGenreByIdHandler : IRequestHandler<GetGenreByIdQuery, Result<GenreDto>>
    {
        public readonly IUnitOfWork _uow;
        public GetGenreByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<GenreDto>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await _uow.GenreRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if(genre == null)
                throw new NotFoundException("Genre", request.Id);

            var dto = new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description,
                CreatedAt = genre.CreatedAt,
            };

            return Result<GenreDto>.Success(dto, "Genre fetched successfully");
        }
    }
}
