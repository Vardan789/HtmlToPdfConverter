namespace Common
{
    public class ConstraintViolationDto
    {
        public string Property { get; set; }

        public string Message { get; set; }

        public string InvalidValue { get; set; }
    }
}