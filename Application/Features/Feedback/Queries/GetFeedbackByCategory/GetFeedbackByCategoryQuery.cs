using Application.Common;
using Application.Features.Feedback.Queries.DTOs;
using Domain.Enums.Feedback;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetFeedbackByCategory
{
    public class GetFeedbackByCategoryQuery : IRequest<Result<List<FeedbackDto>>>
    {
        public FeedbackCategory Category  { get; set; }
    }
}
