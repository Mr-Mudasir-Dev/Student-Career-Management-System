using Application.Common;
using Application.Interface;
using Application.Interface.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Command.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService jwtService;

        public LoginHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            this.jwtService = jwtService;
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.FindUserRepository.FindByEmailOrUsernameAsync(request.Identifier!);
            if (user == null)
                return Result<LoginResponse>.Failure("Invalid credentials");


            var loginUser = await _unitOfWork.IdentityRepository.Login(user, request.Password!);

            if (!loginUser.Succeeded)
                return Result<LoginResponse>.Failure("Something went wrong during login.");

            var roles = await _unitOfWork.IdentityRepository.GetRoles(user.Id!);

            var token = jwtService.GenerateToken(user.Id!, user.UserName!, user.Email!, roles.FirstOrDefault()!);

            return Result<LoginResponse>.Success(new LoginResponse
            {
                Token = token,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age
            }, "Login successful");


            throw new NotImplementedException();
        }
    }
}
