using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.DeleteFeedback
{
    public class DeleteFeedbackCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
