using FIAP.TECH.CORE.DOMAIN.Entities;
using FluentValidation;

namespace FIAP.TECH.CORE.DOMAIN.Validation;

public class ContactUpdateValidation : AbstractValidator<Contact>
{
    public ContactUpdateValidation()
    {
        #region Attributes
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

        RuleFor(c => c.Email)
            .EmailAddress()
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido corretamente.");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .Matches("^(?:((?:9\\d|[2-9])\\d{3})\\-?(\\d{4}))$").WithMessage("O campo {PropertyName} precisa ser valido.");

        RuleFor(c => c.DDD)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .Matches("^(?:\\(?([1-9][0-9])\\)?\\s?)$").WithMessage("DDD inválido."); ;
        #endregion
    }
}