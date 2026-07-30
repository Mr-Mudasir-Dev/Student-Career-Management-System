using Application.Common;
using Application.Features.Review.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Queries.GetBookReview
{
    public class GetBookReviewQuery : IRequest<Result<List<ReviewDto>>>
    {
        public int BookId { get; set; }
    }
}
