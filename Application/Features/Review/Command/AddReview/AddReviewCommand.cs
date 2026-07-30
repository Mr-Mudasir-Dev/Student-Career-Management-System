using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Review.Command.AddReview
{
    public class AddReviewCommand : IRequest<Result>
    {
        public string UserId { get; set; } = string.Empty;
        public int BookId { get; set; }
        public int OrderId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
