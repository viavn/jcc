using JccApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IGiftRepository
    {
        Task<IEnumerable<Gift>> GetAll();
        Task<Gift> GetById(Guid childId, Guid godParentId, int giftType);
        Task<Gift> Find(Guid childId, Guid godParentId, int giftType);
        Task<bool> IsGiftCreated(Guid childId, int giftType);
        Task Create(Gift gift);
        Task Update(Gift updatedGift);
        Task Delete(Gift gift);
    }
}
