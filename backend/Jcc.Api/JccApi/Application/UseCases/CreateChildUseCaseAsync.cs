using System;
using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Entities;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class CreateChildUseCaseAsync : ICreateChildUseCaseAsync
    {
        private readonly IChildRepository _childRepository;
        private readonly IValidator<CreateChildRequest> _validator;

        public CreateChildUseCaseAsync(IChildRepository childRepository, IValidator<CreateChildRequest> validator)
        {
            _childRepository = childRepository;
            _validator = validator;
        }

        public async Task<Guid> Execute(CreateChildRequest request)
        {
            _validator.ValidateAndThrow(request);

            var child = new Child(request.Name, request.Age, request.ClotheSize, request.ShoeSize, (int)request.Genre, request.FamilyId);
            await _childRepository.Create(child);
            return child.Id;
        }
    }
}