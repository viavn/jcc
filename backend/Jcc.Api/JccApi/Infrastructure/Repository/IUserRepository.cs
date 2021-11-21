using JccApi.Entities;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task<User> GetUserByLogin(string login);
    }
}
