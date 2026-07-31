using Application.Common;
using Application.Features.Review.Queries.DTOs;
using Application.Interface;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Queries.GetBookReview
{
    public class GetBookReviewHandler : IRequestHandler<GetBookReviewQuery, Result<List<ReviewDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetBookReviewHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<ReviewDto>>> Handle(GetBookReviewQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.BookRepository
                .ExistsAsync(request.BookId, cancellationToken);

            if(!result)
                throw new NotFoundException("Book", request.BookId);

            var review = await _uow.ReviewRepository
                .GetByBookIdAsync(request.BookId, cancellationToken);

            if (!review.Any())
                return Result<List<ReviewDto>>
                    .Success(new List<ReviewDto>(), "Not found review");

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
