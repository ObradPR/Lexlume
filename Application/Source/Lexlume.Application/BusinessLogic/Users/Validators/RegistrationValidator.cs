using FluentValidation;
using Lexlume.Application.BusinessLogic.Users.Commands;
using Lexlume.Application.Extensions;
using Lexlume.Domain.Entities.Application;

namespace Lexlume.Application.BusinessLogic.Users.Validators;

public class RegistrationValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationValidator()
    {
        RuleFor(_ => _.User.FullName)
            .Required()
            .MaximumLengthAuto(User.NAME_MAX_LENGTH);

        RuleFor(_ => _.User.Username)
            .Required()
            .MaximumLengthAuto(User.NAME_MAX_LENGTH);

        RuleFor(_ => _.User.Email)
            .Required()
            .EmailAddressAuto()
            .MaximumLengthAuto(User.EMAIL_MAX_LENGTH);

        RuleFor(_ => _.User.Password)
            .Required()
            .MatchesPassword();

        RuleFor(_ => _.User.ConfirmPassword)
             .Required()
             .EqualAuto(_ => _.User.Password);
    }
}