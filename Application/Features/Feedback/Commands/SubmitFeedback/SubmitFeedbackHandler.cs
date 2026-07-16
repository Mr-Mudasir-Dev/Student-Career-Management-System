using Application.Common;
using Application.Interface;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.SubmitFeedback
{
    public class SubmitFeedbackHandler : IRequestHandler<SubmitFeedbackCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public SubmitFeedbackHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(SubmitFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = new Domain.Entities.Feedback
            {
                UserId = request.UserId,
                Category = request.Category,
                Message = request.Message,
                Rating = request.Rating,
                IsAnonymous = request.IsAnonymous,
            };

            await _uow.FeedbackRepository.AddAsync(feedback, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Feedback Submitted Successfully");
        }
    }
}
