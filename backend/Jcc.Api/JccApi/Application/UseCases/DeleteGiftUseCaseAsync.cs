using System.Threading.Tasks;
using FluentValidation;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class DeleteGiftUseCaseAsync : IDeleteGiftUseCaseAsync
    {
        private readonly IGiftRepository _giftRepository;
        private readonly IChildRepository _childRepository;
        private readonly IGodParentRepository _godParentRepository;
        private readonly IValidator<UpdateGiftRequest> _validator;

        public DeleteGiftUseCaseAsync(IGiftRepository giftRepository, IChildRepository childRepository, IGodParentRepository godParentRepository, IValidator<UpdateGiftRequest> validator)
        {
            _giftRepository = giftRepository;
            _childRepository = childRepository;
            _godParentRepository = godParentRepository;
            _validator = validator;
        }

        public async Task Execute(UpdateGiftRequest request)
        {
            _validator.ValidateAndThrow(request);

            if (!await _childRepository.Exists(request.ChildId))
            {
                throw new JccException("Criança inválida");
            }

            if (!await _godParentRepository.Exists(request.GodParentId))
            {
                throw new JccException("Madrinha/Padrinho inválido");
            }

            var gift = await _giftRepository.Find(request.ChildId, request.GodParentId, (int)request.GiftType);
            if (gift is null)
            {
                throw new JccException("Presente inválido");
            }

            if (gift.IsDelivered)
            {
                throw new JccException("Presente já entregue");
            }

            await _giftRepository.Delete(gift);
        }
    }
}