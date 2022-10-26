using System;
using System.Threading.Tasks;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;

namespace JccApi.Application
{
    public class DeleteFamilyUseCaseAsync : IDeleteFamilyUseCaseAsync
    {
        private readonly IFamilyRepository _familyRepository;

        public DeleteFamilyUseCaseAsync(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        public async Task Execute(Guid familyId)
        {
            var family = await _familyRepository.Find(familyId);
            if (family is null)
            {
                throw new JccException("Família inválida");
            }

            await _familyRepository.Delete(family);
        }
    }
}