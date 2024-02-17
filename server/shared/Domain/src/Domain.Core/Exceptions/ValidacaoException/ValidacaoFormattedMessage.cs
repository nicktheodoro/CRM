namespace MyApp.SharedDomain.Exceptions.ValidacaoException
{
    public class ValidacaoFormattedMessage(string message, IEnumerable<ValidacaoMessage> errors)
    {
        public string Message { get; } = message;
        public IEnumerable<ValidacaoMessage> Errors { get; } = errors;
    }
}
