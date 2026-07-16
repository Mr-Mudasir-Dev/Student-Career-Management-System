using Domain.Enums.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.DTOs
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public FeedbackCategory Category { get; set; }
        public string? Message { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public bool IsAnonymous { get; set; }
        public FeedbackStatus Status { get; set; }
        public string? AdminReply { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
