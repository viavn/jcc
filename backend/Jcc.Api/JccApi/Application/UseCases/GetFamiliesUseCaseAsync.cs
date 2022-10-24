using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class GetFamiliesUseCaseAsync : IGetFamiliesUseCaseAsync
    {
        private readonly IFamilyRepository _familyRepository;

        public GetFamiliesUseCaseAsync(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        public async Task<IEnumerable<FamilyResponse>> Execute()
        {
            var families = await _familyRepository.GetFamiliesWithSingleMember();
            return families.Select(f => new FamilyResponse(f.Id, f.Code, f.Address, f.MemberName));
        }
    }
}