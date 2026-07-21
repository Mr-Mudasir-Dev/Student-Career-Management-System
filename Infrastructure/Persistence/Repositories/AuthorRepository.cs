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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Authors
                .AnyAsync(a => a.Name.ToLower() == name.ToLower(), cancellationToken);

        }

        public async Task<IEnumerable<Author>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Authors
                .Where(a => a.Name.Contains(name))
                .OrderBy(a => a.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
