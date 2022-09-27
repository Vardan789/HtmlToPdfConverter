namespace Common
{
    public class RequestValidationException : Exception
    {
        public IReadOnlyList<ConstraintViolationDto> Violations { get; private set; }

        public RequestValidationException(string message, List<ConstraintViolationDto> violations)
          : base(message)
        {
            this.Violations = (IReadOnlyList<ConstraintViolationDto>)violations;
        }

        public RequestValidationException(
          string message,
          List<ConstraintViolationDto> violations,short status)
          : base(message)
        {
            this.Violations = (IReadOnlyList<ConstraintViolationDto>)violations;
        }

        public RequestValidationException(string message, ConstraintViolationDto constraintViolationDto)
          : this(message, new List<ConstraintViolationDto>()
          {
        constraintViolationDto
          })
        {
        }

        public RequestValidationException(string message, ConstraintViolationDto constraintViolationDto, short status)
        {
            var message1 = message;
            var violations = new List<ConstraintViolationDto>();
            violations.Add(constraintViolationDto);
            var status1 = (int)status;
        }
    }
}