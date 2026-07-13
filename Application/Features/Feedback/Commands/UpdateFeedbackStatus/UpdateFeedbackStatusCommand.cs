using Application.Common;
using Domain.Enums.Feedback;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.UpdateFeedbackStatus
{
    public class UpdateFeedbackStatusCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public FeedbackStatus Status { get; set; }
    }
}
