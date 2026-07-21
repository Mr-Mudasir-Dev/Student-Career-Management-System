using Application.Common;
using Application.Features.Book.Command.AddBook;
using Application.Features.Book.Command.DeleteBook;
using Application.Features.Book.Queries.GetAllBook;
using Application.Features.Book.Queries.GetBestSellers;
using Application.Features.Book.Queries.GetBookById;
using Application.Features.Book.Queries.GetByAuthorId;
using Application.Features.Book.Queries.GetByGenreBook;
using Application.Features.Book.Queries.GetBySearchBook;
using Application.Features.Book.Queries.GetNewArrivals;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Book : ControllerBase
    {
        private readonly IMediator _mediator;
        public Book(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook(AddBookCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return StatusCode(201, ApiResponse<object>
                .CreatedResponse(null!, result.Message ?? "Created Successfully"));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBook()
        {
            var result = await _mediator.Send(new GetAllBookQuery());

            if(!result.IsSuccess)
               return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("best/seller")]
        public async Task<IActionResult> GetBestSellerBook()
        {
            var result = await _mediator.Send(new GetBestSellersQuery());

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery
            {
                Id = id
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("new/arrivals")]
        public async Task<IActionResult> GetNewArrivalBook()
        {
            var result = await _mediator.Send(new GetNewArrivalsQuery());

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("author/{id}")]
        public async Task<IActionResult> GetByAuthorBook(int id)
        {
            var result = await _mediator.Send(new GetByAuthorIdBookQuery
            {
                Id= id
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("genre/{id}")]
        public async Task<IActionResult> GetByGenreBook(int id)
        {
            var result = await _mediator.Send(new GetByGenreBookQuery
            {
                Id = id
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand
            {
                Id = id
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(null, result.Message));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBook(string search)
        {
            var result = await _mediator.Send(new GetBySearchBookQuery
            {
                SearchTerm = search
            });

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }
    }
}
