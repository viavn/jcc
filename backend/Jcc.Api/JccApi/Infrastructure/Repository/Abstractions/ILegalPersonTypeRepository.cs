using JccApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Infrastructure.Repository.Abstractions
{
    public interface ILegalPersonTypeRepository
    {
        Task<IEnumerable<LegalPersonType>> GetAll();
    }
}
