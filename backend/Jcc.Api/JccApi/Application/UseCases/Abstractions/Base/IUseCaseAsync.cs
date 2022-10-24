using System.Threading.Tasks;

namespace JccApi.Application.UseCases.Abstractions.Base
{
    public interface IUseCaseAsync<TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request);
    }

    public interface IUseCaseAsync<TResponse>
    {
        Task<TResponse> Execute();
    }

    public interface IUseCaseRequestAsync<TRequest>
    {
        Task Execute(TRequest request);
    }
}