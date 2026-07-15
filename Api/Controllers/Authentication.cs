using Application.Common;
using Application.Features.Authentication.Register;
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
        private readonly ILogger<Authentication> logger;

        public Authentication(IMediator mediator,ILogger<Authentication> logger)
        {
            _mediator = mediator;
            this.logger = logger;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));

            return Ok(ApiResponse<Object?>.SuccessResponse(null, result.Message));
        }
<<<<<<< Updated upstream
=======


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginCommand command)
        {
            logger.LogInformation("Login attempt for user: {Identifier}", command.Identifier);
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));

            //return Ok(ApiResponse<Object?>.SuccessResponse(null, result.Message));
            return Ok(ApiResponse<LoginResponse>.SuccessResponse(result.Data!, result.Message));
        }
>>>>>>> Stashed changes
    }
}
