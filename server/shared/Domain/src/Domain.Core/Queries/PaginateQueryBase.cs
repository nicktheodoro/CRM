using FluentValidation.Results;
using MediatR;

namespace MyApp.SharedDomain.Queries
{
    public abstract class PaginateQueryBase<TResponse> : IRequest<PaginateQueryResponseBase<TResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public virtual bool Valid(out ValidationResult validationResult)
        {
            validationResult = new PaginateQueryBaseValidator<TResponse>().Validate(this);
            return validationResult.IsValid;
        }
    }
}
