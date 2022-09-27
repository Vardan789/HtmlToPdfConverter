namespace Common
{
    public static class ExceptionThrower
    {
        public static string RequestValidation(
          string message,
          string invalidValue,
          string property,
          short status)
        {
            var message1 = message;
            var constraintViolationDto = new ConstraintViolationDto();
            constraintViolationDto.InvalidValue = invalidValue;
            constraintViolationDto.Message = message;
            constraintViolationDto.Property = property;
            var status1 = (int)status;
            throw new RequestValidationException(message1, constraintViolationDto, (short)status1);
        }
    }
}