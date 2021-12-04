using JccApi.Entities;
using JccApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task Create(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
