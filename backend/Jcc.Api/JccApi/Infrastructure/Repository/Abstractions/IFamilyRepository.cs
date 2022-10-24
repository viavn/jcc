using JccApi.Entities;
using JccApi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IFamilyRepository
    {
        Task<IEnumerable<Family>> GetAll();
        Task<IEnumerable<FamilyWithMember>> GetFamiliesWithSingleMember();
        Task<Family> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task Create(Family family);
        Task Update(Family updatedFamily);
        Task Delete(Family family);
    }
}
