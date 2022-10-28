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
        Task<bool> Exists(Guid id);
        Task<GodParent> Find(Guid id);
        Task Create(GodParent godParent);
        Task Update(GodParent updatedGodParent);
        Task Delete(GodParent godParent);
    }
}
