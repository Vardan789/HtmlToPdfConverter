namespace HtmlToPdfConverter.Service.Model
{
    public static class FileManagerErrorMessages
    {
        public const string UploadFailedMaxSize = "Upload failed. Allowed max. size is 2 MB.";

        public const string UploadFailedInvalidFileType = "Upload failed. The following extensions only are allowed: PNG, JPG, JPEG, PDF, SVG.";

        public const string DirectoryMustEndWithSlash = "Must end with '/'";
    }
    public static class SupportedMimeTypes
    {
        public static string[] GetMimeTypes() => new string[5]
        {
      "image/svg+xml",
      "image/png",
      "image/jpeg",
      "image/jpg",
      "application/pdf"
        };

        public static string[] GetExtensions() => new string[5]
        {
      "svg",
      "png",
      "jpeg",
      "jpg",
      "pdf"
        };
    }

}
