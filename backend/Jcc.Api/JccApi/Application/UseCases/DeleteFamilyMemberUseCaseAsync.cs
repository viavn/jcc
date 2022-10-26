using System;
using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class DeleteFamilyMemberUseCaseAsync : IDeleteFamilyMemberUseCaseAsync
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IValidator<DeleteMemberRequest> _validator;

        public DeleteFamilyMemberUseCaseAsync(
            IFamilyMemberRepository familyMemberRepository,
            IFamilyRepository familyRepository,
            IValidator<DeleteMemberRequest> validator)
        {
            _familyMemberRepository = familyMemberRepository;
            _familyRepository = familyRepository;
            _validator = validator;
        }

        public async Task Execute(DeleteMemberRequest request)
        {
            _validator.ValidateAndThrow(request);

            var member = await _familyMemberRepository.Find(request.MemberId);
            if (member is null)
            {
                throw new JccException("Membro inválido");
            }

            var quantity = await _familyRepository.MembersQuantity(request.FamilyId);
            if (quantity < 2)
            {
                throw new InvalidOperationException("Família não pode ficar sem responsável");
            }

            await _familyMemberRepository.Delete(member);
        }
    }
}