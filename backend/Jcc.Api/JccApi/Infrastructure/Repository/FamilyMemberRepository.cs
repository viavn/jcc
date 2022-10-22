using JccApi.Entities;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class FamilyMemberRepository : IFamilyMemberRepository
    {
        private readonly DataBaseContext _context;

        public FamilyMemberRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task Create(FamilyMember member)
        {
            _context.FamilyMembers.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(FamilyMember member)
        {
            _context.FamilyMembers.Remove(member);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FamilyMember>> GetAll()
        {
            return await _context.FamilyMembers.AsNoTracking().ToListAsync();
        }

        public async Task<FamilyMember> GetById(Guid id)
        {
            return await _context.FamilyMembers.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(FamilyMember updatedMember)
        {
            var member = new FamilyMember(updatedMember.Id);
            _context.Attach(member);
            _context.Entry(member).CurrentValues.SetValues(updatedMember);
            await _context.SaveChangesAsync();
        }
    }
}
