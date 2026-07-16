using Application.Common;
using Application.Interface;
using Application.Interface.Service;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly IJwtService _jwtService;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(IUnitOfWork unitOfWork, IJwtService jwtService, ILogger<LoginHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _logger = logger;
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("LoginHandler called with Identifier: {Identifier}", request.Identifier);

            var loginUser = await _unitOfWork.IdentityRepository.Login(request.Identifier, request.Password);

            if (!loginUser.Succeeded)
                return Result<LoginResponse>.Failure(loginUser.Error);

            var roles = await _unitOfWork.IdentityRepository.GetRoles(loginUser.User!.Id!);


            _logger.LogInformation($"User {loginUser.User!.UserName} UserId : {loginUser.User!.Id!} logged in successfully with roles: {string.Join(", ", roles)}");

            var token = _jwtService.GenerateToken(loginUser.User!.Id!, loginUser.User!.UserName!, roles.FirstOrDefault()!);

            return Result<LoginResponse>.Success(new LoginResponse
            {
                Token = token,
                UserName = loginUser.User!.UserName!,
                Email = loginUser.User!.Email!,
                PhoneNumber = loginUser.User!.PhoneNumber!,
                Age = loginUser.User!.Age!
            }, "Login successful");

        }
    }
}
