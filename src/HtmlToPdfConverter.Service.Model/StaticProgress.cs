namespace HtmlToPdfConverter.Service.Model;

public static class StaticProgress
{
    public static int Progress { get; set; }
}

public static class FilesDirectory
{
    public const string UploadedFile = @"\uploads\";
    public const string PdfDocumentFile = @"\pdfDocumets\";
}