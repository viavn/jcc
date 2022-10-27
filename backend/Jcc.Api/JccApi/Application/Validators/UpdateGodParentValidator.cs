using FluentValidation;
using JccApi.Models;

namespace JccApi.Application.Validators
{
    public class UpdateGodParentValidator : AbstractValidator<UpdateGodParentRequest>
    {
        public UpdateGodParentValidator()
        {

            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Identificador da/do madrinha/padrinho é obrigatório");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(250).WithMessage("Nome deve conter no máximo 250 caracteres.");

            RuleFor(c => c.ContactNumber)
                .NotEmpty().WithMessage("Número para contato é obrigatório")
                .MaximumLength(250).WithMessage("Número para contato deve conter no máximo 250 caracteres.");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Endereço é obrigatório")
                .MaximumLength(500).WithMessage("Endereço deve conter no máximo 500 caracteres.");
        }
    }
}