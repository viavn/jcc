using JccApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IChildRepository
    {
        Task<IEnumerable<Child>> GetAll();
        Task<Child> GetById(Guid id);
        Task Create(Child child);
        Task Update(Child child);
        Task Delete(Child child);
    }
}
