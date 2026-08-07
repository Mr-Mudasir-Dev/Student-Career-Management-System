using Application.Common;
using Application.Features.Authentication.Command.ChangePassword;
using Application.Features.Authentication.Command.Login;
using Application.Features.Authentication.Command.Register;
using Application.Features.Authentication.Command.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<Authentication> _logger;

        public Authentication(IMediator mediator, ILogger<Authentication> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //Register EndPoint

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));
            return Ok(ApiResponse<Object?>.SuccessResponse(null, result.Message));
        }


        //Login EndPoint


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.NotFoundResponse(result.Message!));
            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        //VerifyEmail EndPoint

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token, [FromQuery] string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
                return BadRequest(ApiResponse<object>.FailResponse("Something Wen't Wrong"));
            var command = new VerifyEmailCommand
            {
                Token = token,
                Email = email
            };
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.NotFoundResponse(result.Message!));
            return Ok(ApiResponse<object>.SuccessResponse(null, result.Message));
        }

        // ChangePassword EndPoint
        [Authorize]
        [HttpPost("change-password")]

        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation("UserId from claims: {UserId}", userId);


            if (userId == null)
                return BadRequest(ApiResponse<object>.FailResponse("signin required"));
            var command = new ChangePasswordCommand
            {
                UserId = userId,
                OldPassword = changePasswordDTO.OldPassword,
                NewPassword = changePasswordDTO.Password,
                ConfirmPassword = changePasswordDTO.ConfirmPassword
            };
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));
            return Ok(ApiResponse<object>.SuccessResponse(null, result.Message));
        }
    }
}
