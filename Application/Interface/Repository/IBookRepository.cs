using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetBestsellersAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetNewArrivalsAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
        Task<Book?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetSearchByTitleAsync(string serachTerm, CancellationToken cancellationToken= default);
    }
}
