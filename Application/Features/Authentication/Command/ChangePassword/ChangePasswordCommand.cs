using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Command.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public string UserId { get; set; }  = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

        public string ConfirmPassword { get; set;} = string.Empty;
    }
}
