using FluentValidation;
using JccApi.Models;

namespace JccApi.Application.Validators
{
    public class FamilyMemberRequestValidator : AbstractValidator<MemberRequest>
    {
        public FamilyMemberRequestValidator()
        {
            RuleFor(mem => mem.Name)
                .NotEmpty().WithMessage("Nome do membro é obrigatório")
                .MaximumLength(250).WithMessage("Nome deve conter no máximo 250 caracteres.");

            RuleFor(mem => mem.Type)
                .IsInEnum().WithMessage("Tipo de responsável inválido.");

            RuleFor(mem => mem.FamilyId)
                .NotEmpty().WithMessage("Identificador da Família é obrigatório");


            RuleFor(mem => mem.Id)
                .NotEmpty().WithMessage("Identificador do Membro é obrigatório")
                .When(mem => !mem.IsCreating);
        }
    }
}