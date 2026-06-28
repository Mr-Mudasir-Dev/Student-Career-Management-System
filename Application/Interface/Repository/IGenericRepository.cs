using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        // Read
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        // Write
        Task AddAsync (T entity);
        Task UpdateAsync (T entity);
        Task DeleteAsync (T entity);

        // Check
        Task<bool> ExistsAsync (int id);

    }
}
