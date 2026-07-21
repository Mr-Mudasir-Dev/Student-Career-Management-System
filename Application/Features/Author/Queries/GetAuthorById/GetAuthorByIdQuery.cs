using Application.Common;
using Application.Features.Author.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest<Result<AuthorDto>>
    {
        public int Id { get; set; }
    }
}
