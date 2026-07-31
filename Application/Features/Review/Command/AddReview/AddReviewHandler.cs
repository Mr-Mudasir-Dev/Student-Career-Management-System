using Application.Common;
using Application.Interface;
using Domain.Entities;
using Domain.Enums.Order;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Command.AddReview
{
    public class AddReviewHandler : IRequestHandler<AddReviewCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        public AddReviewHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var book = await _uow.BookRepository
                .ExistsAsync(request.BookId, cancellationToken);

            if(!book)
                throw new NotFoundException("Book", request.BookId);

            var order = await _uow.OrderRepository
                .GetByIdWithItemsAsync(request.OrderId, cancellationToken);

            if (order == null)
                throw new NotFoundException("Order", request.OrderId);

            if (order.UserId != request.UserId)
                throw new ForbiddenException("This is not your order.");

            if (order.Status != OrderStatus.Delivered)
                return Result.Failure("You can only review delivered orders.");

            var bookInOrder = order.OrderItems
                .Any(oi => oi.BookId == request.BookId);

            if(!bookInOrder)
                return Result.Failure("This book is not in your order.");

            var exists = await _uow.ReviewRepository
            .ExistsAsync(request.UserId, request.BookId, request.OrderId, cancellationToken);

            if (exists)
                throw new ConflictException("You have already reviewed this book.");

            var review = new Domain.Entities.Review
            {
                UserId = request.UserId,
                OrderId = request.OrderId,
                BookId = request.BookId,
                Comment = request.Comment,
                Ratting = request.Rating
            };

            await _uow.ReviewRepository.AddAsync(review, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success("Review place successfully!");
        }
    }
}
