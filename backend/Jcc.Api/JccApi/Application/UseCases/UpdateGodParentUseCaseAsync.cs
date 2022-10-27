using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class UpdateGodParentUseCaseAsync : IUpdateGodParentUseCaseAsync
    {
        private readonly IGodParentRepository _godParentRepository;
        private readonly IValidator<UpdateGodParentRequest> _validator;

        public UpdateGodParentUseCaseAsync(IGodParentRepository godParentRepository, IValidator<UpdateGodParentRequest> validator)
        {
            _godParentRepository = godParentRepository;
            _validator = validator;
        }

        public async Task Execute(UpdateGodParentRequest request)
        {
            _validator.ValidateAndThrow(request);

            var godParent = await _godParentRepository.Find(request.Id);
            if (godParent is null)
            {
                throw new JccException("Madrinha/Padrino inválido");
            }

            godParent.Update(request.Name, request.ContactNumber, request.Address);
            await _godParentRepository.Update(godParent);
        }
    }
}