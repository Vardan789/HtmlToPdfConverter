using Microsoft.AspNetCore.Http;

namespace HtmlToPdfConverter.Service.Abstraction;
public interface IFileUploaderService
{
    string Progress();
    Task<IReadOnlyCollection<string>> UploadFilesAsync(IList<IFormFile> files);
}
