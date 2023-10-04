using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace MyApp.SharedDomain.Queries
{
    public class QueryBaseValidator<TResponse> : AbstractValidator<QueryBase<TResponse>>
    {
        public QueryBaseValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty());
        }
    }
}
