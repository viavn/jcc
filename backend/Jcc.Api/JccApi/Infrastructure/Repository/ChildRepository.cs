using JccApi.Entities;
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

        public async Task<IEnumerable<Child>> GetAll()
        {
            return await _context.Children.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Child>> GetAllWithInformation()
        {
            var giftTypes = new List<int> {
                (int)JccApi.Enums.GiftType.Clothe,
                (int)JccApi.Enums.GiftType.Shoe,
                (int)JccApi.Enums.GiftType.Toy,
            };
            var query = from child in _context.Children.AsNoTracking()
                        select new
                        {
                            child.Id,
                            child.Name,
                            child.ClotheSize,
                            child.ShoeSize,
                            child.GenreType,
                            child.Family.Code,
                            GiftsDelivered = child.Gifts.Where(g => g.IsDelivered).Count(),
                            RemainingToBeInserted = giftTypes.Except(child.Gifts.Select(g => g.TypeId)).Count()
                        };
            var result = await query.ToListAsync();

            return await _context.Children.AsNoTracking().ToListAsync();
        }

        public async Task<Child> GetById(Guid id)
        {
            return await _context.Children.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Child updatedChild)
        {
            var child = new Child(updatedChild.Id);
            _context.Attach(child);
            _context.Entry(child).CurrentValues.SetValues(updatedChild);
            await _context.SaveChangesAsync();
        }
    }
}
