using Application.Common;
using Application.Features.Review.Queries.DTOs;
using Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Queries.GetAllReview
{
    public class GetAllReviewHandler : IRequestHandler<GetAllReviewQuery, Result<List<ReviewDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllReviewHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<ReviewDto>>> Handle(GetAllReviewQuery request, CancellationToken cancellationToken)
        {
            var review = await _uow.ReviewRepository.GetAllWithDetailAsync();

            if (review.Any())
                return Result<List<ReviewDto>>.Success(new List<ReviewDto>(), "Not found review");

            var dto = review.Select(r => new ReviewDto
            {
                Id = r.Id,
                BookId = r.BookId,
                BookTitle = r.Book.Title,
                Rating = r.Ratting,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();

            return Result<List<ReviewDto>>.Success(dto, "Review fetched successfully");
        }
    }
}
