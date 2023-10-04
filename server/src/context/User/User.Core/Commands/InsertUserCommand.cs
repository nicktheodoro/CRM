using FluentValidation.Results;
using MyApp.SharedDomain.Commands;

namespace MyApp.Core.Users.Commands
{
    public class InsertUserCommand : InsertCommandBase
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new InsertUserCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
