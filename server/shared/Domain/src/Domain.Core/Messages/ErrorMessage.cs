namespace Domain.Core.Messages
{
    public class ErrorMessage
    {
        private const string UNPROCESSABLE_CONTENT = "We were unable to process your request due to invalid data.";

        public static string UnprocessableContent()
        {
            return UNPROCESSABLE_CONTENT;
        }
    }
}
