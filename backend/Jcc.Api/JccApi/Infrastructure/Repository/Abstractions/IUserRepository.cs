using JccApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByLogin(string login);
        Task Create(User user);
        Task Update(User user);
    }
}
