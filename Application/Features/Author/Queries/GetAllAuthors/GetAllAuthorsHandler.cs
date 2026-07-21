using Application.Common;
using Application.Features.Author.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.GetAllAuthors
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, Result<List<AuthorDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllAuthorsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetAllAsync(cancellationToken);

            if (!author.Any())
                return Result<List<AuthorDto>>.Success(new List<AuthorDto>(), "No author found");

            var dto = author.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                CreatedAt = a.CreatedAt
            }).ToList();

            return Result<List<AuthorDto>>.Success(dto, "Authors fetched successfully!");
        }
    }
}
