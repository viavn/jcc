using System;
using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Entities;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class CreateFamilyUseCaseAsync : ICreateFamilyUseCaseAsync
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IValidator<CreateFamilyRequest> _validator;

        public CreateFamilyUseCaseAsync(IFamilyRepository familyRepository, IValidator<CreateFamilyRequest> validator)
        {
            _familyRepository = familyRepository;
            _validator = validator;
        }

        public async Task<Guid> Execute(CreateFamilyRequest request)
        {
            _validator.ValidateAndThrow(request);

            var family = new Family(request.Code, request.ContactNumber, request.Address, request.Comment);
            request.Members.ForEach(m =>
            {
                family.AddMember(new FamilyMember(m.Name, (int)m.Type, family.Id));
            });

            await _familyRepository.Create(family);
            return family.Id;
        }
    }
}