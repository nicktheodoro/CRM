using FluentValidation.Results;
using System.Net;

namespace MyApp.SharedDomain.Exceptions.ValidacaoException
{
    public class ValidationException : ExceptionBase
    {
        private readonly List<ValidacaoMessage> _erros;
        public ValidacaoFormattedMessage FormattedMessage => new(Message, _erros);

        public ValidationException(string message, ValidationResult validationResult) : base(message, HttpStatusCode.BadRequest)
        {
            _erros = [];

            foreach (var error in validationResult.Errors)
            {
                _erros.Add(new ValidacaoMessage(error));
            }
        }
    }
}
