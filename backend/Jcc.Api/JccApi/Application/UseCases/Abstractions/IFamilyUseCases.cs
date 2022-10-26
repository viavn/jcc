using System;
using System.Collections.Generic;
using JccApi.Application.UseCases.Abstractions.Base;
using JccApi.Models;

namespace JccApi.Application.Abstractions.UseCases
{
    public interface ICreateFamilyUseCaseAsync : IUseCaseAsync<CreateFamilyRequest, Guid> { }
    public interface IUpdateFamilyUseCaseAsync : IUseCaseRequestAsync<UpdateFamilyRequest> { }
    public interface IDeleteFamilyUseCaseAsync : IUseCaseRequestAsync<Guid> { }
    public interface IGetFamiliesUseCaseAsync : IUseCaseAsync<IEnumerable<FamilyResponse>> { }
    public interface IGetFamilyUseCaseAsync : IUseCaseAsync<Guid, FamilyByIdResponse> { }
}