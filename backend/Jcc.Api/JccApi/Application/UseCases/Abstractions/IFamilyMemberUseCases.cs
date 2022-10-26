using System;
using JccApi.Application.UseCases.Abstractions.Base;
using JccApi.Models;

namespace JccApi.Application.Abstractions.UseCases
{
    public interface ICreateFamilyMemberUseCaseAsync : IUseCaseAsync<MemberRequest, Guid> { }
    public interface IUpdateFamilyMemberUseCaseAsync : IUseCaseRequestAsync<MemberRequest> { }
    public interface IDeleteFamilyMemberUseCaseAsync : IUseCaseRequestAsync<DeleteMemberRequest> { }
}