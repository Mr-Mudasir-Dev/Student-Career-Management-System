using Application.Interface.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly AppDbContext _context;
        public GenreRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Genres
                .AnyAsync(g => g.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        public async Task<IEnumerable<Genre>> SearchByNameAsync(string search, CancellationToken cancellationToken = default)
        {
            return await _context.Genres
                .Where(g => g.Name.Contains(search))
                .OrderBy(g => g.Name)
                .ToListAsync();
        }
    }
}
