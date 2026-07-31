using Application.Common;
using Application.Features.Book.Queries.GetAllBook;
using Application.Features.Review.Command.AddReview;
using Application.Features.Review.Queries.GetAllReview;
using Application.Features.Review.Queries.GetBookReview;
using Application.Features.Review.Queries.GetUserReview;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Review : ControllerBase
    {
        private readonly IMediator _mediator;
        public Review(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReview(AddReviewCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return StatusCode(201, ApiResponse<object>
                .CreatedResponse(null!, result.Message ?? "Created Successfully"));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllReview()
        {
            var result = await _mediator.Send(new GetAllReviewQuery());

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("{id}/book")]
        public async Task<IActionResult> GetAllBookReview(int id)
        {
            var result = await _mediator.Send(new GetBookReviewQuery
            {
                BookId = id
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAllUserReview(string id)
        {
            var result = await _mediator.Send(new GetUserReviewQuery
            {
                UserId = id
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetAllMyReview()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized(ApiResponse<object>.UnauthorizedResponse());

            var result = await _mediator.Send(new GetUserReviewQuery
            {
                UserId = UserId
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }
    }
}
