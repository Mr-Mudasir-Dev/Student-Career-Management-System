using Application.Common;
using Application.Features.Author.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.SearchAuthors
{
    public class SearchAuthorsHandler : IRequestHandler<SearchAuthorsQuery, Result<List<AuthorDto>>>
    {
        private readonly IUnitOfWork _uow;
        public SearchAuthorsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<AuthorDto>>> Handle(SearchAuthorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.AuthorRepository
                .SearchByNameAsync(request.SearchTerm, cancellationToken);

            if (!result.Any())
                return Result<List<AuthorDto>>
                    .Success(new List<AuthorDto>(), "No author found");

            var dto = result.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                CreatedAt = a.CreatedAt
            }).ToList();

            return Result<List<AuthorDto>>.Success(dto, $"{dto.Count} author found");
        }
    }
}
