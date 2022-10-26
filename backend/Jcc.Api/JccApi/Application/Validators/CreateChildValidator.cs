using FluentValidation;
using JccApi.Models;

namespace JccApi.Application.Validators
{
    public class CreateChildValidator : AbstractValidator<CreateChildRequest>
    {
        public CreateChildValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(250).WithMessage("Nome deve conter no máximo 250 caracteres.");

            RuleFor(c => c.Age)
                .NotEmpty().WithMessage("Idade é obrigatório")
                .MaximumLength(250).WithMessage("Idade deve conter no máximo 250 caracteres.");

            RuleFor(c => c.ClotheSize)
                .NotEmpty().WithMessage("Tamanho roupa é obrigatório")
                .MaximumLength(250).WithMessage("Tamanho roupa deve conter no máximo 250 caracteres.");

            RuleFor(c => c.ShoeSize)
                .NotEmpty().WithMessage("Tamanho calçado é obrigatório")
                .MaximumLength(250).WithMessage("Tamanho calçado deve conter no máximo 250 caracteres.");

            RuleFor(c => c.Genre)
                .IsInEnum().WithMessage("Sexo inválido.");

            RuleFor(mem => mem.FamilyId)
                .NotEmpty().WithMessage("Identificador da Família é obrigatório");
        }
    }
}