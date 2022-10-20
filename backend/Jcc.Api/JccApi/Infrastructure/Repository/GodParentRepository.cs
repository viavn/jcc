using JccApi.Entities;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class GodParentRepository : IGodParentRepository
    {
        private readonly DataBaseContext _context;

        public GodParentRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task Create(GodParent godParent)
        {
            _context.GodParents.Add(godParent);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(GodParent godParent)
        {
            _context.GodParents.Remove(godParent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GodParent>> GetAll()
        {
            return await _context.GodParents.AsNoTracking().ToListAsync();
        }

        public async Task<GodParent> GetById(Guid id)
        {
            return await _context.GodParents.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(GodParent godParent)
        {
            _context.GodParents.Update(godParent);
            await _context.SaveChangesAsync();
        }

        // public async Task BatchCreate(IEnumerable<GodParent_Old> godParents)
        // {
        //     _context.GodParents.AddRange(godParents);
        //     await _context.SaveChangesAsync();
        // }

        // public async Task Delete(GodParent_Old godParent)
        // {
        //     _context.Entry(godParent).State = EntityState.Deleted;
        //     await _context.SaveChangesAsync();
        // }

        // public async Task DeleteOldThenCreateNewGodParents(IEnumerable<GodParent_Old> godParentsToBeDeleted, IEnumerable<GodParent_Old> newGodParents)
        // {
        //     _context.GodParents.RemoveRange(godParentsToBeDeleted);
        //     _context.GodParents.AddRange(newGodParents);
        //     await _context.SaveChangesAsync();
        // }
    }
}
