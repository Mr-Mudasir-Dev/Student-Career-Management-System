
using Application.Common;
using Application.Features.Feedback.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetMyFeedbacks
{
    public class GetMyFeedbacksQuery : IRequest<Result<List<MyFeedbackDto>>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
