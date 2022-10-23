using FluentValidation;
using JccApi.Models;

namespace JccApi.Application.Validators
{
    public class CreateFamilyValidator : AbstractValidator<CreateFamilyRequest>
    {
        public CreateFamilyValidator()
        {
            RuleFor(fam => fam.Code).NotEmpty().MaximumLength(10);
            RuleFor(fam => fam.ContactNumber).NotEmpty().MaximumLength(11);
            RuleFor(fam => fam.Address).NotEmpty().MaximumLength(500);
            RuleForEach(fam => fam.Members).ChildRules(member =>
            {
                member.RuleFor(mem => mem.Name).NotEmpty().MaximumLength(250);
            });
        }
    }
}