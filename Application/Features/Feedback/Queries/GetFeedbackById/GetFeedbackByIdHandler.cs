using Application.Common;
using Application.Features.Feedback.Queries.DTOs;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetFeedbackById
{
    public class GetFeedbackByIdHandler : IRequestHandler<GetFeedbackByIdQurey, Result<FeedbackDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetFeedbackByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<FeedbackDto>> Handle(GetFeedbackByIdQurey request, CancellationToken cancellationToken)
        {
            var feedback = await _unitOfWork
                .FeedbackRepository.GetByIdAsync(request.Id, cancellationToken);

            if(feedback == null)
                throw new NotFoundException("Feedback", request.Id);

            var dto = new FeedbackDto
            {
                Id = feedback.Id,
                UserId = feedback.IsAnonymous ? "Anonymous" : feedback.UserId,
                Category = feedback.Category,
                Message = feedback.Message,
                Rating = feedback.Rating,
                IsAnonymous = feedback.IsAnonymous,
                Status = feedback.Status,
                AdminReply = feedback.AdminReply,
                CreatedAt = feedback.CreatedAt
            };

            return Result<FeedbackDto>.Success(dto, "Feedback fetched successfully!");
        }
    }
}
