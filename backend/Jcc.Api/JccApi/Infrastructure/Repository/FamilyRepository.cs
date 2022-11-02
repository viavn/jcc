using JccApi.Entities;
using JccApi.Entities.Dtos;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Families.AsNoTracking().AnyAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Family>> GetAll()
        {
            return await _context.Families.AsNoTracking().ToListAsync();
        }

        public async Task<Family> GetById(Guid id)
        {
            return await _context.Families.AsNoTracking()
                .Include(f => f.Members)
                .ThenInclude(m => m.LegalPersonType)
                .Include(f => f.Children)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Family> Find(Guid id)
        {
            return await _context.Families.FindAsync(id);
        }

        public async Task<IEnumerable<FamilyWithMember>> GetFamiliesWithSingleMember()
        {
            return await _context.Families.Include(f => f.Members).AsNoTracking()
                .Select(f => new FamilyWithMember(f.Id, f.Code, f.Address, f.Members.FirstOrDefault().Name))
                .ToListAsync();
        }

        public async Task Update(Family updatedFamily)
        {
            var family = new Family(updatedFamily.Id);
            _context.Attach(family);
            _context.Entry(family).CurrentValues.SetValues(updatedFamily);
            await _context.SaveChangesAsync();
        }

        public async Task<int> MembersQuantity(Guid id)
        {
            return await _context.Families.AsNoTracking().Where(fam => fam.Id == id).SelectMany(f => f.Members).CountAsync();
        }
    }
}
