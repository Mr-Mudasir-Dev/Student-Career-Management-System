using Application.Common;
using Application.Features.Feedback.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetFeedbackByCategory
{
    public class GetFeedbackByCategoryHandler : IRequestHandler<GetFeedbackByCategoryQuery, Result<List<FeedbackDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetFeedbackByCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<FeedbackDto>>> Handle(GetFeedbackByCategoryQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await 
                _unitOfWork.FeedbackRepository
                .GetByCategoryAync(request.Category);

            if (!feedbacks.Any())
                return Result<List<FeedbackDto>>
                    .Success(new List<FeedbackDto>(), "feedback not found");

            var dto = feedbacks.Select(f => new FeedbackDto
            {
                Id = f.Id,
                UserId = f.IsAnonymous ? "Anonymous" : f.UserId,
                Category = f.Category,
                Message = f.Message,
                Rating = f.Rating,
                IsAnonymous = f.IsAnonymous,
                Status = f.Status,
                AdminReply = f.AdminReply,
                CreatedAt = f.CreatedAt
            }).ToList();

            return Result<List<FeedbackDto>>.Success(dto, "feedback feetch successfully!");

        }
    }
}
