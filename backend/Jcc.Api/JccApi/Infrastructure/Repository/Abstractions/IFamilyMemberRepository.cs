using JccApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface IFamilyMemberRepository
    {
        Task<IEnumerable<FamilyMember>> GetAll();
        Task<FamilyMember> GetById(Guid id);
        Task Create(FamilyMember member);
        Task Update(FamilyMember updatedMember);
        Task Delete(FamilyMember member);
    }
}
