using JccApi.Entities;
using JccApi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IChildRepository
    {
        Task<IEnumerable<Child>> GetAll();
        Task<IEnumerable<ChildGiftDto>> GetAllWithDeliveredInformation();
        Task<Child> GetById(Guid id);
        Task<Child> Find(Guid id);
        Task<bool> Exists(Guid id);
        Task Create(Child child);
        Task Update(Child updatedChild);
        Task Delete(Child child);
    }
}
