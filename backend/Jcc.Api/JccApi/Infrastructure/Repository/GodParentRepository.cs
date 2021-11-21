using JccApi.Entities;
using JccApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task BatchCreate(IEnumerable<GodParent> godParents)
        {
            _context.GodParents.AddRange(godParents);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(GodParent godParent)
        {
            _context.Entry(godParent).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOldThenCreateNewGodParents(IEnumerable<GodParent> godParentsToBeDeleted, IEnumerable<GodParent> newGodParents)
        {
            _context.GodParents.RemoveRange(godParentsToBeDeleted);
            _context.GodParents.AddRange(newGodParents);
            await _context.SaveChangesAsync();
        }
    }
}
