using Domain.Common;
using Domain.Enums.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Feedback : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;

        public FeedbackCategory Category { get; set; }
        public string? Message { get; set; }
        public int? Rating { get; set; }
        public bool IsAnonymous { get; set; } = false;
        public FeedbackStatus Status { get; set; } = FeedbackStatus.Pending;
        public string? AdminReply { get; set; }
        public DateTime? RepliedAt { get; set; }
    }
}
