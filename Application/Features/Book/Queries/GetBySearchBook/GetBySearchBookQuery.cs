using Application.Common;
using Application.Features.Book.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.GetBySearchBook
{
    public class GetBySearchBookQuery : IRequest<Result<List<BookDto>>>
    {
        public string SearchTerm { get; set; } = string.Empty;
    }
}
