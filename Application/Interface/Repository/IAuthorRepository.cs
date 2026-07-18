using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
