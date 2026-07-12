using Application.Common;
using Application.Interface;
using Application.Interface.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Cammand.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Age = request.Age
            };

            var result = await _unitOfWork.IdentityRepository.Register(newUser, request.Password);
            if (!result.Succeeded)
                return Result.Failure(result.Errors);

            return Result.Success("User registered successfully");
        }
    }

}

