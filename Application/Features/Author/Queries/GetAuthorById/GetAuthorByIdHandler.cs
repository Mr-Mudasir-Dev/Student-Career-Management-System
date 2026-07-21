using Application.Common;
using Application.Features.Author.Queries.DTOs;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.GetAuthorById
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, Result<AuthorDto>>
    {
        private readonly IUnitOfWork _uow;
        public GetAuthorByIdHandler(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _uow.AuthorRepository.GetByIdAsync(request.Id, cancellationToken);

            if(author == null)
                throw new NotFoundException("author", request.Id);

            var dto = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                CreatedAt = author.CreatedAt
            };

            return Result<AuthorDto>.Success(dto, "Author fetched successfully!");
        }
    }
}
