using Application.Common;
using Application.Features.Author.Commands.AddAuthor;
using Application.Features.Author.Commands.DeleteAuthor;
using Application.Features.Author.Queries.GetAllAuthors;
using Application.Features.Author.Queries.GetAuthorById;
using Application.Features.Author.Queries.SearchAuthors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Author : ControllerBase
    {
        public readonly IMediator _mediator;
        public Author(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAuthor(AddAuthorCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message ?? "Something went wrong"));

            return StatusCode(201, ApiResponse<object>.CreatedResponse(null!, result.Message ?? "Author submitted successfully!"));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAuthor()
        {
            var result = await _mediator.Send(new GetAllAuthorsQuery());

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var cmd = new GetAuthorByIdQuery
            {
                Id = id
            };

            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorById(int id)
        {
            var cmd = new DeleteAuthorCommand
            {
                Id = id
            };

            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(null, result.Message));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string search)
        {
            var cmd = new SearchAuthorsQuery
            {
                SearchTerm = search
            };

            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>
                    .FailResponse(result.Message));

            return Ok(ApiResponse<object>
                .SuccessResponse(result.Data!, result.Message));
        }
    }
}
