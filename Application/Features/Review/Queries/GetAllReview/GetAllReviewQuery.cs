using Application.Common;
using Application.Features.Review.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Queries.GetAllReview
{
    public class GetAllReviewQuery : IRequest<Result<List<ReviewDto>>>
    {
    }
}
