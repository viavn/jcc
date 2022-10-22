using JccApi.Entities;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly DataBaseContext _context;

        public FamilyRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task Create(Family family)
        {
            _context.Families.Add(family);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Family family)
        {
            _context.Families.Remove(family);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Family>> GetAll()
        {
            return await _context.Families.AsNoTracking().ToListAsync();
        }

        public async Task<Family> GetById(Guid id)
        {
            return await _context.Families.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Family updatedFamily)
        {
            var family = new Family(updatedFamily.Id);
            _context.Attach(family);
            _context.Entry(family).CurrentValues.SetValues(updatedFamily);
            await _context.SaveChangesAsync();
        }
    }
}
