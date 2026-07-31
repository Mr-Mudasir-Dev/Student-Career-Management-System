using Application.Common;
using Application.Features.Order.Command.CancelOrder;
using Application.Features.Order.Command.CreateOrder;
using Application.Features.Order.Command.UpdateOrderStatus;
using Application.Features.Order.Queries.GetAllOrders;
using Application.Features.Order.Queries.GetMyOrder;
using Application.Features.Order.Queries.GetOrderById;
using Application.Features.Order.Queries.GetOrderByStatus;
using Domain.Enums.Order;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order : ControllerBase
    {
        private readonly IMediator _mediator;
        public Order(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Place")]
        public async Task<IActionResult> PlaceOrder(CreateOrderCommand cmd)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized(ApiResponse<object>.UnauthorizedResponse());

            cmd.UserId = UserId;

            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Message ?? "Something went wrong"));

            return StatusCode(201, ApiResponse<object>.CreatedResponse(null!, result.Message ?? "Order Place successfully!"));
        }

        [HttpPatch("{id}/Cancel")]
        public async Task<IActionResult> OrderCancel(int id)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized(ApiResponse<object>.UnauthorizedResponse());

            var result = await _mediator.Send(new CancelOrderCommand
            {
                OrderId = id,
                UserId = UserId
            });

            if (!result.IsSuccess)
                return BadRequest
                    (ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(null, result.Message));
        }

        [HttpPatch("{id}/Status")]
        public async Task<IActionResult> StatusUpdate(int id, OrderStatus status)
        {
            var result = await _mediator.Send(new UpdateOrderStatusCommand
            {
                OrderId = id,
                Status = status
            });

            if (!result.IsSuccess)
                return BadRequest
                    (ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(null, result.Message));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());

            if (!result.IsSuccess)
                return BadRequest
                    (ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetAlMylOrder()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized(ApiResponse<object>.UnauthorizedResponse());

            var result = await _mediator.Send(new GetMyOrderQuery
            {
                UserId = UserId,
            });

            if (!result.IsSuccess)
                return BadRequest
                    (ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery
            {
                Id = id
            });

            if (!result.IsSuccess)
                return BadRequest
                    (ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetOrderByStatus(OrderStatus status)
        {
            var result = await _mediator.Send(new GetOrderByStatusQuery
            {
                Status = status
            });

            if (!result.IsSuccess)
                return BadRequest
                    (ApiResponse<object>.FailResponse(result.Message));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

    }
}
