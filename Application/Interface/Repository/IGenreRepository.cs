using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Genre>> SearchByNameAsync (string search, CancellationToken cancellationToken = default);
    }
}
