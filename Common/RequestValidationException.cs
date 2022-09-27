namespace Common
{
    public class RequestValidationException : Exception
    {
        public RequestValidationException(string message, ConstraintViolationDto constraintViolationDto, short status) :
            base(message)
        {
        }
    }
}