using Application.Common;
using Application.Features.Author.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Queries.SearchAuthors
{
    public class SearchAuthorsQuery : IRequest<Result<List<AuthorDto>>>
    {
        public string SearchTerm { get; set; } = string.Empty;
    }
}
