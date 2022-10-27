using FluentValidation;
using JccApi.Models;

namespace JccApi.Application.Validators
{
    public class CreateGiftValidator : AbstractValidator<CreateGiftRequest>
    {
        public CreateGiftValidator()
        {
            RuleFor(c => c.ChildId)
                .NotEmpty().WithMessage("Identificador da Criança é obrigatório");

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Identificador do Usuário é obrigatório");

            RuleFor(c => c.Type)
                .IsInEnum().WithMessage("Tipo de presente inválido.");

            RuleFor(c => c.GodParent.Name)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(250).WithMessage("Nome deve conter no máximo 250 caracteres.");

            RuleFor(c => c.GodParent.ContactNumber)
                .NotEmpty().WithMessage("Número para contato é obrigatório")
                .MaximumLength(250).WithMessage("Número para contato deve conter no máximo 250 caracteres.");

            RuleFor(c => c.GodParent.Address)
                .NotEmpty().WithMessage("Endereço é obrigatório")
                .MaximumLength(500).WithMessage("Endereço deve conter no máximo 500 caracteres.");
        }
    }
}