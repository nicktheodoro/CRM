using FluentValidation.Results;
using MyApp.SharedDomain.Commands;

namespace MyApp.Core.Users.Commands
{
    public class InactiveUserCommand : CommandBase
    {
        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new InactiveUserCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
