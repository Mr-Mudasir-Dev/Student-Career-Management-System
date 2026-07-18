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
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, Result<List<AuthorByUserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllAuthorsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<AuthorByUserDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetAllAsync(cancellationToken);

            if (!author.Any())
                return Result<List<AuthorByUserDto>>.Success(new List<AuthorByUserDto>(), "No author found");

            var dto = author.Select(a => new AuthorByUserDto
            {
                Name = a.Name,
                Bio = a.Bio,
                CreatedAt = a.CreatedAt
            }).ToList();

            return Result<List<AuthorByUserDto>>.Success(dto, "Authors fetched successfully!");
        }
    }
}
