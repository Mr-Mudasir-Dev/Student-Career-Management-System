using Application.Common;
using Application.Features.Book.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.GetByAuthorId
{
    public class GetByAuthorIdBookQuery : IRequest<Result<List<BookDto>>>
    {
        public int Id { get; set; }
    }
}
