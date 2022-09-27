using System.Net.Mime;

namespace HtmlToPdfConverter.Service.Model
{
    public static class FileManagerErrorMessages
    {
        public const string UploadFailedMaxSize = "Upload failed. Allowed max. size is 5 MB.";

        public const string UploadFailedInvalidFileType =
            "Upload failed. The following extensions only are allowed: HTML";

    }

    public static class SupportedMimeTypes
    {
        public static IEnumerable<string> GetMimeTypes() => new []
        {
            MediaTypeNames.Text.Html,
        };

        public static string[] GetExtensions() => new []
        {
            "html"
        };
    }
}