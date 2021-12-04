using JccApi.Entities;
using System;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByLogin(string login);
        Task Create(User user);
        Task Update(User user);
    }
}
