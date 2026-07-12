using Application.Common;
using Application.Features.Feedback.Commands.SubmitFeedback;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Feedback : ControllerBase
    {
        private readonly IMediator _mediator;
        public Feedback(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] SubmitFeedbackCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message ?? "Something went wrong"));

            return StatusCode(201, ApiResponse<object>.CreatedResponse(null!, result.Message ?? "Feedback submitted successfully!"));
        }
    }
}
