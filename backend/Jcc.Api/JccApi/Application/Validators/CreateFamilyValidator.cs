using FluentValidation;
using JccApi.Models;

namespace JccApi.Application.Validators
{
    public class CreateFamilyValidator : AbstractValidator<CreateFamilyRequest>
    {
        public CreateFamilyValidator()
        {
            RuleFor(fam => fam.Code)
                .NotEmpty().WithMessage("Código é obrigatório")
                .MaximumLength(10).WithMessage("Código deve conter no máximo 10 caracteres.");

            RuleFor(fam => fam.ContactNumber)
                .NotEmpty().WithMessage("Número para contato é obrigatório")
                .MaximumLength(11).WithMessage("Número para contato deve conter no máximo 11 caracteres.");

            RuleFor(fam => fam.Address)
                .NotEmpty().WithMessage("Endereço é obrigatório")
                .MaximumLength(500).WithMessage("Endereço deve conter no máximo 500 caracteres.");

            RuleForEach(fam => fam.Members).ChildRules(member =>
            {
                member.RuleFor(mem => mem.Name)
                    .NotEmpty().WithMessage("Nome do membro é obrigatório")
                    .MaximumLength(250).WithMessage("Nome do membro deve conter no máximo 250 caracteres.");

                member.RuleFor(mem => mem.Type)
                    .IsInEnum().WithMessage("Tipo de responsável inválido.");
            });
        }
    }
}