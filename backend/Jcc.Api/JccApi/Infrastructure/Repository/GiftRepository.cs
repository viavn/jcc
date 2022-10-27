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

        public async Task<Gift> Find(Guid childId, Guid godParentId, int giftType)
        {
            return await _context.Gifts.FindAsync(childId, godParentId, giftType);
        }

        public async Task<IEnumerable<Gift>> GetAll()
        {
            return await _context.Gifts.AsNoTracking().ToListAsync();
        }

        public async Task<Gift> GetById(Guid childId, Guid godParentId, int giftType)
        {
            return await _context.Gifts.AsNoTracking()
                .FirstOrDefaultAsync(g => g.ChildId == childId && g.GodParentId == godParentId && g.TypeId == giftType);
        }

        public async Task<bool> IsGiftCreated(Guid childId, int giftType)
        {
            return await _context.Gifts.AsNoTracking()
                .AnyAsync(g => g.ChildId == childId && g.TypeId == giftType);
        }

        public async Task Update(Gift updatedGift)
        {
            var gift = new Gift(updatedGift.ChildId, updatedGift.GodParentId);
            _context.Attach(gift);
            _context.Entry(gift).CurrentValues.SetValues(updatedGift);
            await _context.SaveChangesAsync();
        }
    }
}
