using JccApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IGiftRepository
    {
        Task<IEnumerable<Gift>> GetAll();
        Task<Gift> GetById(Guid childId, Guid godParentId);
        Task Create(Gift gift);
        Task Update(Gift gift);
        Task Delete(Gift gift);
    }
}
