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
    public class ChildRepository : IChildRepository
    {
        private readonly DataBaseContext _context;

        public ChildRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task Create(Child child)
        {
            _context.Children.Add(child);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Child child)
        {
            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
        }

        public Child Find(Guid id)
        {
            return _context.Children.Find(id);
        }

        public async Task<IEnumerable<Child>> GetAll()
        {
            return await _context.Children.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ChildGiftDto>> GetAllWithDeliveredInformation()
        {
            var giftTypes = new List<int> {
                (int)JccApi.Enums.GiftType.Clothe,
                (int)JccApi.Enums.GiftType.Shoe,
                (int)JccApi.Enums.GiftType.Toy,
            };
            var query = from child in _context.Children.AsNoTracking()
                        let giftsDelivered = child.Gifts.Where(g => g.IsDelivered).Count()
                        let remainingToBeInserted = giftTypes.Except(child.Gifts.Select(g => g.TypeId)).Count()
                        select new ChildGiftDto(child.Id, child.Name, child.Family.Code, giftsDelivered, remainingToBeInserted);

            return await query.ToListAsync();
        }

        public async Task<Child> GetById(Guid id)
        {
            return await _context.Children.AsNoTracking()
                .Include(c => c.GenreType)
                .Include(c => c.Family)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Child updatedChild)
        {
            _context.Entry(updatedChild).CurrentValues.SetValues(updatedChild);
            await _context.SaveChangesAsync();
        }
    }
}
