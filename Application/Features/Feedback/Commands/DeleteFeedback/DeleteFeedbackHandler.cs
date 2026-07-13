using Application.Common;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.DeleteFeedback
{
    public class DeleteFeedbackHandler : IRequestHandler<DeleteFeedbackCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFeedbackHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _unitOfWork
                .FeedbackRepository.GetByIdAsync(request.Id, cancellationToken);

            if(feedback == null)
                throw new NotFoundException("Feedback", request.Id);

            if (feedback.UserId != request.UserId)
                throw new ForbiddenException("You can only delete your own feedback.");

            if (feedback.Status != Domain.Enums.Feedback.FeedbackStatus.Pending)
                return Result.Failure("Only pending feedback can be deleted.");

            _unitOfWork.FeedbackRepository.DeleteAsync(feedback);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Feedback Deleted Successfully");

        }
    }
}
