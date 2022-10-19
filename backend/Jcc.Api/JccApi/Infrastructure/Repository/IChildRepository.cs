using JccApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public interface IChildRepository
    {
        Task<IEnumerable<Child_Old>> GetAll();
        Task<IEnumerable<Child_Old>> GetAllWithGodParents();
        Task<Child_Old> GetById(Guid id);
        Task Create(Child_Old child);
    }
}
