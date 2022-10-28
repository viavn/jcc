using System;
using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Entities;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class CreateGiftUseCaseAsync : ICreateGiftUseCaseAsync
    {
        private readonly IGiftRepository _giftRepository;
        private readonly IChildRepository _childRepository;
        private readonly IGodParentRepository _godParentRepository;
        private readonly IValidator<CreateGiftRequest> _validator;

        public CreateGiftUseCaseAsync(IGiftRepository giftRepository, IChildRepository childRepository, IGodParentRepository godParentRepository, IValidator<CreateGiftRequest> validator)
        {
            _giftRepository = giftRepository;
            _childRepository = childRepository;
            _godParentRepository = godParentRepository;
            _validator = validator;
        }

        public async Task<Guid> Execute(CreateGiftRequest request)
        {
            _validator.ValidateAndThrow(request);
            if (!await _childRepository.Exists(request.ChildId))
            {
                throw new JccException("Criança inválida");
            }

            if (await _giftRepository.IsGiftCreated(request.ChildId, (int)request.Type))
            {
                throw new JccException("Este tipo de presente já foi cadastrado");
            }

            var godParent = new GodParent(request.GodParent.Name, request.GodParent.ContactNumber, request.GodParent.Address);
            var gift = new Gift(request.ChildId, godParent.Id, (int)request.Type, DateTime.Now, DateTime.Now, request.UserId);

            await _godParentRepository.Create(godParent);
            await _giftRepository.Create(gift);

            return godParent.Id;
        }
    }
}