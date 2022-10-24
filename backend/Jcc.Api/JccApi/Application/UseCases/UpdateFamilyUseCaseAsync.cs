using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Entities;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class UpdateFamilyUseCaseAsync : IUpdateFamilyUseCaseAsync
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IValidator<UpdateFamilyRequest> _validator;

        public UpdateFamilyUseCaseAsync(IFamilyRepository familyRepository, IValidator<UpdateFamilyRequest> validator)
        {
            _familyRepository = familyRepository;
            _validator = validator;
        }

        public async Task Execute(UpdateFamilyRequest request)
        {
            if (!await _familyRepository.Exists(request.Id))
            {
                throw new JccException("Família inválida");
            }

            var family = new Family(request.Id, request.Code, request.ContactNumber, request.Address, request.Comment);
            await _familyRepository.Update(family);
        }
    }
}