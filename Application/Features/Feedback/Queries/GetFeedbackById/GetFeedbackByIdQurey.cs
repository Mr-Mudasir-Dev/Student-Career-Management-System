using Application.Common;
using Application.Features.Feedback.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetFeedbackById
{
    public class GetFeedbackByIdQurey : IRequest<Result<FeedbackDto>>
    {
        public int Id { get; set; }
    }
}
