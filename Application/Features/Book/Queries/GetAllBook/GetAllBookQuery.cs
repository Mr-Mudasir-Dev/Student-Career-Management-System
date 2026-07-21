using Application.Common;
using Application.Features.Book.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Queries.GetAllBook
{
    public class GetAllBookQuery : IRequest<Result<List<BookDto>>>
    {
    }
}
