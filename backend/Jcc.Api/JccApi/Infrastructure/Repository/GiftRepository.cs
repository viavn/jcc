using JccApi.Entities;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class GiftRepository : IGiftRepository
    {
        private readonly DataBaseContext _context;

        public GiftRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task Create(Gift gift)
        {
            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Gift gift)
        {
            _context.Gifts.Remove(gift);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Gift>> GetAll()
        {
            return await _context.Gifts.AsNoTracking().ToListAsync();
        }

        public async Task<Gift> GetById(Guid childId, Guid godParentId)
        {
            return await _context.Gifts.AsNoTracking()
                .FirstOrDefaultAsync(g => g.ChildId == childId && g.GodParentId == godParentId);
        }

        public async Task Update(Gift gift)
        {
            _context.Gifts.Update(gift);
            await _context.SaveChangesAsync();
        }
    }
}
