using JccApi.Entities;
using JccApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class ChildRepository : IChildRepository
    {
        private readonly DataBaseContext _context;

        public ChildRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Child_Old>> GetAll()
        {
            return await _context.Children
                .AsNoTracking()
                .OrderBy(c => c.FamilyAcronym)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Child_Old>> GetAllWithGodParents()
        {
            return await _context.Children
                .AsNoTracking()
                .Include(c => c.GodParents)
                .OrderBy(c => c.FamilyAcronym)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Child_Old> GetById(Guid id)
        {
            return await _context.Children.AsNoTracking().Include(c => c.GodParents)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Create(Child_Old child)
        {
            _context.Children.Add(child);
            await _context.SaveChangesAsync();
        }
    }
}
