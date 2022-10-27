using System.Threading.Tasks;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Exceptions;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;

namespace JccApi.Application
{
    public class DeleteChildUseCaseAsync : IDeleteChildUseCaseAsync
    {
        private readonly IChildRepository _childRepository;

        public DeleteChildUseCaseAsync(IChildRepository childRepository)
        {
            _childRepository = childRepository;
        }

        public async Task Execute(DeleteChildRequest request)
        {
            var child = await _childRepository.Find(request.Id);
            if (child is null)
            {
                throw new JccException("Criança inválida");
            }

            await _childRepository.Delete(child);
        }
    }
}