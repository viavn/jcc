using JccApi.Entities;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly DataBaseContext _context;

        public UserTypeRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserType>> GetAll()
        {
            return await _context.UserTypes.AsNoTracking().ToListAsync();
        }
    }
}
