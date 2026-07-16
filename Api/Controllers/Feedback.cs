using Application.Common;
using Application.Features.Feedback.Commands.DeleteFeedback;
using Application.Features.Feedback.Commands.SubmitFeedback;
using Application.Features.Feedback.Commands.UpdateFeedbackStatus;
using Application.Features.Feedback.Queries.GetAllFeedback;
using Application.Features.Feedback.Queries.GetFeedbackByCategory;
using Application.Features.Feedback.Queries.GetFeedbackById;
using Application.Features.Feedback.Queries.GetFeedbacksByStatus;
using Application.Features.Feedback.Queries.GetMyFeedbacks;
using Domain.Enums.Feedback;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, DeleteFeedbackCommand cmd)
        {
            var command = new DeleteFeedbackCommand
            {
                Id = id
            };
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(
            null!, result.Message ?? "Feedback deleted successfully!"));

        }

        [HttpGet("my/{userid}")]
        public async Task<IActionResult> GetMyFeedback(string userid)
        {
            var query = new GetMyFeedbacksQuery
            {
                UserId = userid
            };

            var result = await _mediator .Send(query);


            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data,result.Message));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllFeedback()
        {
            var result = await _mediator.Send(new GetAllFeedbackQuery());

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetByStatus(FeedbackStatus status)
        {
            var query = new GetFeedbacksByStatusQuery
            {
                Status = status
            };

            var result = await _mediator .Send(query);

            if (!result.IsSuccess)
                return BadRequest
                    (ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetFeedbackByCategory(FeedbackCategory cat)
        {
            var qurey = new GetFeedbackByCategoryQuery
            {
                Category = cat
            };

            var resulte = await _mediator.Send(qurey);

            if (!resulte.IsSuccess)
                return BadRequest(ApiResponse<object>
                    .FailResponse(resulte.Message));

            return Ok(ApiResponse<object>
                .SuccessResponse(resulte.Data, resulte.Message));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackbyId(int id)
        {
            var resulte = await _mediator.Send(new GetFeedbackByIdQurey
            {
                Id = id
            });

            if(!resulte.IsSuccess)
                return BadRequest (ApiResponse<object>.FailResponse(resulte.Message));

            return Ok(ApiResponse<object>
                .SuccessResponse(resulte.Data, resulte.Message));
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateFeedbackStatusCommand cmd)
        {
            cmd.Id = id;
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest
                    (ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(null ,result.Message));
        }
    }
}
