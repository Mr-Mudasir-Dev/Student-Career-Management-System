using Domain.Entities;
using Domain.Enums.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        // basic mathod genric-Repo de raha h

        Task<IEnumerable<Feedback>> GetByUseridAsync(string id);
        Task<IEnumerable<Feedback>> GetByStatusAsync(FeedbackStatus status);
        Task<IEnumerable<Feedback>> GetByCategoryAync(FeedbackCategory category);
    }
}
