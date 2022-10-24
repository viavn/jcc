using System;
using System.Linq;
using System.Threading.Tasks;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class GetFamilyUseCaseAsync : IGetFamilyUseCaseAsync
    {
        private readonly IFamilyRepository _familyRepository;

        public GetFamilyUseCaseAsync(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        public async Task<FamilyByIdResponse> Execute(Guid request)
        {
            var family = await _familyRepository.GetById(request);
            if (family is null)
            {
                return null;
            }

            return new FamilyByIdResponse
            {
                Id = family.Id,
                Address = family.Address,
                Code = family.Code,
                Comment = family.Comment,
                ContactNumber = family.ContactNumber,
                Members = family.Members.Select(m => new FamilyMemberResponse
                {
                    Id = m.Id,
                    Name = m.Name,
                    LegalPerson = new TypeResponse(m.LegalPersonTypeId, m.LegalPersonType.Description),
                }).OrderBy(m => m.LegalPerson.Id),
                Children = family.Children.Select(c => new FamilyByIdResponse.ChildResponse
                {
                    Id = c.Id,
                    Age = c.Age,
                    Name = c.Name,
                }).OrderBy(c => c.Age),
            };
        }
    }
}