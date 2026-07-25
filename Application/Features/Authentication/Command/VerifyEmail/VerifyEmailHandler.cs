using Application.Common;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Command.VerifyEmail
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyEmailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.IdentityRepository.VerifyEmail(request.Token, request.Email);
            if (!user.Succeeded) return Result.Failure(user.Errors);

            return Result.Success("Email verified successfully.");

        }
    }
}
