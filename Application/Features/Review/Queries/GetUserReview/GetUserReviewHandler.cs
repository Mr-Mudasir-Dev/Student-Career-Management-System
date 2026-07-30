using Application.Common;
using Application.Features.Review.Queries.DTOs;
using Application.Interface;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Queries.GetUserReview
{
    public class GetUserReviewHandler : IRequestHandler<GetUserReviewQuery, Result<List<ReviewDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetUserReviewHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<ReviewDto>>> Handle(GetUserReviewQuery request, CancellationToken cancellationToken)
        {
            var userexists = await _uow.UserRepository
                .ExistsByUserIdAsync(request.UserId);

            if(!userexists)
                throw new NotFoundException("User", request.UserId);

            var review = await _uow.ReviewRepository
                .GetByUserIdAsync(request.UserId);

            if (review.Any())
                return Result<List<ReviewDto>>
                    .Success(new List<ReviewDto>(), "Review not found");

            var dto = review.Select(r => new ReviewDto
            {
                Id = r.Id,
                BookId = r.BookId,
                BookTitle = r.Book.Title,
                Rating = r.Ratting,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();

            return Result<List<ReviewDto>>
            .Success(dto, $"{dto.Count} reviews found!");
        }
    }
}
