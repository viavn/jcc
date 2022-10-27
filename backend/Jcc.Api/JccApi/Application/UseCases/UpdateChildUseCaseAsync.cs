using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class UpdateChildUseCaseAsync : IUpdateChildUseCaseAsync
    {
        private readonly IChildRepository _childRepository;
        private readonly IValidator<UpdateChildRequest> _validator;

        public UpdateChildUseCaseAsync(IChildRepository childRepository, IValidator<UpdateChildRequest> validator)
        {
            _childRepository = childRepository;
            _validator = validator;
        }

        public async Task Execute(UpdateChildRequest request)
        {
            _validator.ValidateAndThrow(request);

            var child = await _childRepository.Find(request.Id);
            if (child is null)
            {
                throw new JccException("Criança inválida");
            }

            child.Update(request.Name, request.Age, request.ClotheSize, request.ShoeSize, (int)request.Genre);
            await _childRepository.Update(child);
        }
    }
}