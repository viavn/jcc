using JccApi.Entities;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class LegalPersonTypeRepository : ILegalPersonTypeRepository
    {
        private readonly DataBaseContext _context;

        public LegalPersonTypeRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LegalPersonType>> GetAll()
        {
            return await _context.LegalPersonTypes.AsNoTracking().ToListAsync();
        }
    }
}
