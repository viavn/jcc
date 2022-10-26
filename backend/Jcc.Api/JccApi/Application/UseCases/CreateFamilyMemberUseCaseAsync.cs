using System;
using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Entities;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class CreateFamilyMemberUseCaseAsync : ICreateFamilyMemberUseCaseAsync
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IValidator<MemberRequest> _validator;

        public CreateFamilyMemberUseCaseAsync(IFamilyMemberRepository familyMemberRepository, IValidator<MemberRequest> validator)
        {
            _familyMemberRepository = familyMemberRepository;
            _validator = validator;
        }

        public async Task<Guid> Execute(MemberRequest request)
        {
            _validator.ValidateAndThrow(request);

            var member = new FamilyMember(request.Name, (int)request.Type, request.FamilyId);
            await _familyMemberRepository.Create(member);
            return member.Id;
        }
    }
}