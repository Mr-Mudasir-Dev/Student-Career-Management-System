using Application.Common;
using Application.Features.Feedback.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetAllFeedback
{
    public class GetAllFeedbackhandler : IRequestHandler<GetAllFeedbackQuery, Result<List<FeedbackDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllFeedbackhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<List<FeedbackDto>>> Handle(GetAllFeedbackQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync(cancellationToken);

            if (!feedbacks.Any())
                return Result<List<FeedbackDto>>.Success(new List<FeedbackDto>(), "No feedbacks found.");

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

            return Result<List<FeedbackDto>>.Success(dto, "Fetch all feedbacks");
        }
    }
}
