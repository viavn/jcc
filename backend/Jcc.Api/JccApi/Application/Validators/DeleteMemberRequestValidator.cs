using FluentValidation;
using JccApi.Models;

namespace JccApi.Application.Validators
{
    public class DeleteMemberRequestValidator : AbstractValidator<DeleteMemberRequest>
    {
        public DeleteMemberRequestValidator()
        {
            RuleFor(mem => mem.FamilyId)
                .NotEmpty().WithMessage("Identificador da Família é obrigatório");

            RuleFor(mem => mem.MemberId)
                .NotEmpty().WithMessage("Identificador do Membro é obrigatório");
        }
    }
}