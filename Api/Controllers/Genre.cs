using Application.Common;
using Application.Features.Genre.Command.AddGenre;
using Application.Features.Genre.Command.DeleteGenre;
using Application.Features.Genre.Queries.GetAllGenre;
using Application.Features.Genre.Queries.GetGenreById;
using Application.Features.Genre.Queries.GetGenreBySeach;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Genre : ControllerBase
    {
        private readonly IMediator _mediator;
        public Genre(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGenre(AddGenreCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return StatusCode(201, ApiResponse<object>.CreatedResponse(null!, result.Message ?? "Genre submitted successfully!"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var result = await _mediator.Send(new DeleteGenreCommand
            {
                Id = id
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(null, result.Message));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGenre()
        {
            var result = await _mediator.Send(new GetAllGenreQuery());

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var result = await _mediator.Send(new GetGenreByIdQuery
            {
                Id = id
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));

        }

        [HttpGet("search")]
        public async Task<IActionResult> GetGenreBySearch(string search)
        {
            var result = await _mediator.Send(new GetGenreBySearchQuery
            {
                Search = search
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

    }
}
