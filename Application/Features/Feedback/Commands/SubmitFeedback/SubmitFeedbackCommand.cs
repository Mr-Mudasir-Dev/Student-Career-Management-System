using Application.Common;
using Domain.Enums.Feedback;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.SubmitFeedback
{
    public class SubmitFeedbackCommand : IRequest<Result>
    {
        public string UserId { get; set; } = string.Empty;
        public FeedbackCategory Category { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
