namespace Common
{
    public static class ErrorMessages
    {
        public const string NotFound = "Not found";

        public static string IsNotValid(string propName) => propName + " is not valid";


        public static string IsNullOrEmpty(string propName) => propName + " is null or empty";

    }
}