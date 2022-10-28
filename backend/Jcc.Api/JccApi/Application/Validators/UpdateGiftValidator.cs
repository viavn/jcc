using FluentValidation;
using JccApi.Models;

namespace JccApi.Application.Validators
{
    public class UpdateGiftValidator : AbstractValidator<UpdateGiftRequest>
    {
        public UpdateGiftValidator()
        {
            RuleFor(c => c.ChildId)
                .NotEmpty().WithMessage("Identificador da Criança é obrigatório");

            RuleFor(c => c.GodParentId)
                .NotEmpty().WithMessage("Identificador da madrinha/padrinho é obrigatório");

            RuleFor(c => c.GiftType)
                .IsInEnum().WithMessage("Tipo de presente inválido.");
        }
    }
}