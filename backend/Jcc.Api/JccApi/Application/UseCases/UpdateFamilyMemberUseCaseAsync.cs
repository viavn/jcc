using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Entities;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class UpdateFamilyMemberUseCaseAsync : IUpdateFamilyMemberUseCaseAsync
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IValidator<MemberRequest> _validator;

        public UpdateFamilyMemberUseCaseAsync(IFamilyMemberRepository familyMemberRepository, IValidator<MemberRequest> validator)
        {
            _familyMemberRepository = familyMemberRepository;
            _validator = validator;
        }

        public async Task Execute(MemberRequest request)
        {
            _validator.ValidateAndThrow(request);

            if (!await _familyMemberRepository.Exists(request.Id))
            {
                throw new JccException("Membro inválido");
            }

            var member = new FamilyMember(request.Id, request.Name, (int)request.Type, request.FamilyId);
            await _familyMemberRepository.Update(member);
        }
    }
}