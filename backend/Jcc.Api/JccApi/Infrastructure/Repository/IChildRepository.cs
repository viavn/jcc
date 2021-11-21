using JccApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public interface IChildRepository
    {
        Task<IEnumerable<Child>> GetAll();
        Task<Child> GetById(Guid id);
        Task Create(Child child);
    }
}
