using JccApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IGodParentRepository
    {
        Task<IEnumerable<GodParent>> GetAll();
        Task<GodParent> GetById(Guid id);
        Task Create(GodParent godParent);
        Task Update(GodParent godParent);
        Task Delete(GodParent godParent);
    }
}
