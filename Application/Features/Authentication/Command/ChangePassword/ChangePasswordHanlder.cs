using Application.Common;
using Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Command.ChangePassword
{
    public class ChangePasswordHanlder : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IIdentityRepository _identityRepository;

        public ChangePasswordHanlder(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }
        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {

            var result = await _identityRepository.ChangePasswordAsync(request.UserId, request.OldPassword, request.NewPassword);
            if(result.Succeeded)
                return Result.Success("Password changed successfully.");

            return Result.Failure(result.Errors);
        }
    }
}
