using JccApi.Entities;
using JccApi.Entities.Dtos;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;
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

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Children.AsNoTracking().AnyAsync(f => f.Id == id);
        }

        public async Task<Child> Find(Guid id)
        {
            return await _context.Children.FindAsync(id);
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
                        orderby child.Family.Code, child.Name
                        select new ChildGiftDto(
                            child.Id,
                            child.Name,
                            child.Family.Code,
                            giftsDelivered,
                            remainingToBeInserted,
                            child.Age,
                            child.ShoeSize,
                            child.ClotheSize
                        );

            return await query.ToListAsync();
        }

        public async Task<GetChildrenByIdResponse> GetById(Guid id)
        {
            return await _context.Children.AsNoTracking()
                .Select(c => new GetChildrenByIdResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Age = c.Age,
                    ClotheSize = c.ClotheSize,
                    ShoeSize = c.ShoeSize,
                    Family = new FamilyChildResponse
                    {
                        Id = c.FamilyId,
                        Address = c.Family.Address,
                        Code = c.Family.Code,
                        ContactNumber = c.Family.ContactNumber,
                        Member = c.Family.Members
                            .Select(m => $"{m.LegalPersonType.Description} - {m.Name}")
                            .FirstOrDefault()
                    },
                    Genre = new TypeResponse(c.GenreType.Id, c.GenreType.Description),
                    Gifts = c.Gifts.Select(g => new GiftResponse
                    {
                        IsDelivered = g.IsDelivered,
                        GiftType = new TypeResponse(g.Type.Id, g.Type.Description),
                        GodParent = new GodParentResponse
                        {
                            Id = g.GodParentId,
                            Name = g.GodParent.Name,
                            Address = g.GodParent.Address,
                            ContactNumber = g.GodParent.ContactNumber
                        }
                    }),
                })
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Child updatedChild)
        {
            _context.Entry(updatedChild).CurrentValues.SetValues(updatedChild);
            await _context.SaveChangesAsync();
        }
    }
}
