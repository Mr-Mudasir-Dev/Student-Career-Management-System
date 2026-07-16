using Application.Common;
using Application.Features.Feedback.Queries.DTOs;
using Domain.Enums.Feedback;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetFeedbacksByStatus
{
    public class GetFeedbacksByStatusQuery : IRequest<Result<List<FeedbackDto>>>
    {
        public FeedbackStatus Status { get; set; }
    }
}
