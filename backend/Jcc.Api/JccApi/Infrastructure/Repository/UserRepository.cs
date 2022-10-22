using JccApi.Entities;
using JccApi.Infrastructure.Context;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
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

        public async Task Update(User updatedUser)
        {
            var user = new User(updatedUser.Id);
            _context.Attach(user);
            _context.Entry(user).CurrentValues.SetValues(updatedUser);
            await _context.SaveChangesAsync();
        }
    }
}
