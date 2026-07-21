using Application.Common;
using Application.Features.Author.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.GetAllAuthors
{
    public class GetAllAuthorsQuery : IRequest<Result<List<AuthorDto>>>
    {
    }
}
