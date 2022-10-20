using JccApi.Entities;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class GiftTypeRepository : IGiftTypeRepository
    {
        private readonly DataBaseContext _context;

        public GiftTypeRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GiftType>> GetAll()
        {
            return await _context.GiftTypes.AsNoTracking().ToListAsync();
        }
    }
}
