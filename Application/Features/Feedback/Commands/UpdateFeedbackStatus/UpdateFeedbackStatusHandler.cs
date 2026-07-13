using Application.Common;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.UpdateFeedbackStatus
{
    public class UpdateFeedbackStatusHandler : IRequestHandler<UpdateFeedbackStatusCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateFeedbackStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result> Handle(UpdateFeedbackStatusCommand request, CancellationToken cancellationToken)
        {
            var feedback = await 
                _unitOfWork.FeedbackRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (feedback == null)
                throw new NotFoundException("Feedback", request.Id);

            feedback.Status = request.Status;
            feedback.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.FeedbackRepository.UpdateAsync(feedback);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Status updated successfully!");
        }
    }
}
