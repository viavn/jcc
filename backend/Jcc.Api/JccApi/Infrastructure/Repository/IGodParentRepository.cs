using JccApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository
{
    public interface IGodParentRepository
    {
        Task DeleteOldThenCreateNewGodParents(IEnumerable<GodParent_Old> godParentsToBeDeleted, IEnumerable<GodParent_Old> newGodParents);
        Task Delete(GodParent_Old godParent);
    }
}
