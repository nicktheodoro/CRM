using FluentValidation.Results;
using MyApp.SharedDomain.Commands;

namespace MyApp.Core.Users.Commands
{
    public class UpdateUserPassword : CommandBase
    {
        public required string ActualPassword { get; set; }
        public required string NewPassword { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new UpdateUserPasswordCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
