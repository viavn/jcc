using JccApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAll();
    }
}
