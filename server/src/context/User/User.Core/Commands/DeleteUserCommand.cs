using FluentValidation.Results;
using MyApp.SharedDomain.Commands;

namespace MyApp.Core.Users.Commands
{
    public class DeleteUserCommand : CommandBase
    {
        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new DeleteUserCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
