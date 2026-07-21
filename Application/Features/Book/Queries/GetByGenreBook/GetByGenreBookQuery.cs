using Application.Common;
using Application.Features.Book.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.GetByGenreBook
{
    public class GetByGenreBookQuery : IRequest<Result<List<BookDto>>>
    {
        public int Id { get; set; }
    }
}
