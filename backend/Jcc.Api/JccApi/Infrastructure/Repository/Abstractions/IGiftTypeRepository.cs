using JccApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IGiftTypeRepository
    {
        Task<IEnumerable<GiftType>> GetAll();
    }
}
