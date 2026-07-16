using Application.Common;
using Application.Features.Feedback.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetMyFeedbacks
{
    public class GetMyFeedbacksHandler : IRequestHandler<GetMyFeedbacksQuery, Result<List<MyFeedbackDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMyFeedbacksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<MyFeedbackDto>>> Handle(GetMyFeedbacksQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await 
                _unitOfWork.FeedbackRepository
                .GetByUseridAsync(request.UserId, cancellationToken);

            if (!feedbacks.Any())
                return Result<List<MyFeedbackDto>>
                    .Success(new List<MyFeedbackDto>(), "No feedbacks found.");

            var dto = feedbacks.Select(f => new MyFeedbackDto
            {
                Category = f.Category,
                Message = f.Message,
                Rating = f.Rating,
                Status = f.Status,
                AdminReply = f.AdminReply,
                CreatedAt = f.CreatedAt
            }).ToList();

            return Result<List<MyFeedbackDto>>.Success(dto, "All feedbacks fetched!");
        }
    }
}
