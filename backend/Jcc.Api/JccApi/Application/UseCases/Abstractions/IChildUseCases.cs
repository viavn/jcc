using System;
using JccApi.Application.UseCases.Abstractions.Base;
using JccApi.Models;

namespace JccApi.Application.Abstractions.UseCases
{
    public interface ICreateChildUseCaseAsync : IUseCaseAsync<CreateChildRequest, Guid> { }
    public interface IUpdateChildUseCaseAsync : IUseCaseRequestAsync<UpdateChildRequest> { }
    public interface IDeleteChildUseCaseAsync : IUseCaseRequestAsync<DeleteChildRequest> { }
    public interface ICreateGiftUseCaseAsync : IUseCaseAsync<CreateGiftRequest, Guid> { }
    public interface IUpdateGiftUseCaseAsync : IUseCaseRequestAsync<UpdateGiftRequest> { }
    public interface IDeleteGiftUseCaseAsync : IUseCaseRequestAsync<UpdateGiftRequest> { }
}