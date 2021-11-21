using JccApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public interface IGodParentRepository
    {
        Task DeleteOldThenCreateNewGodParents(IEnumerable<GodParent> godParentsToBeDeleted, IEnumerable<GodParent> newGodParents);
        Task Delete(GodParent godParent);
    }
}
