using System;
using JccApi.Application.UseCases.Abstractions.Base;
using JccApi.Models;

namespace JccApi.Application.Abstractions.UseCases
{
    public interface ICreateFamilyUseCaseAsync : IUseCaseAsync<CreateFamilyRequest, Guid> { }
}