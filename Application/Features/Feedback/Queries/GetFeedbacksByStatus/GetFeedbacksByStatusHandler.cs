using Application.Common;
using Application.Features.Feedback.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetFeedbacksByStatus
{
    public class GetFeedbacksByStatusHandler : IRequestHandler<GetFeedbacksByStatusQuery, Result<List<FeedbackDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetFeedbacksByStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<FeedbackDto>>> Handle(GetFeedbacksByStatusQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await _unitOfWork
                .FeedbackRepository.GetByStatusAsync(request.Status, cancellationToken);

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

            return Result<List<FeedbackDto>>.Success(dto, "Feedbacks fetched successfully!");
        }
    }
}
