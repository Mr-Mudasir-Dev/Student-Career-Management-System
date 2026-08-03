using Application.Common;
using Application.Features.Authentication.Command.Login;
using Application.Features.Authentication.Command.Register;
using Application.Features.Authentication.Command.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<Authentication> _logger;

        public Authentication(IMediator mediator,ILogger<Authentication> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));

            return Ok(ApiResponse<Object?>.SuccessResponse(null, result.Message));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.NotFoundResponse("Invalid credentials"));


            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }
        [HttpGet("verify-email")]

        public async Task<IActionResult> VerifyEmail([FromQuery] string token, [FromQuery] string email)
        {
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
    }
}
