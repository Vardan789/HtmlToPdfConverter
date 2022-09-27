using System.Net.Http.Headers;
using HtmlToPdfConverter.Service.Abstraction;
using HtmlToPdfConverter.Service.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PuppeteerSharp;

namespace HtmlToPdfConverter.Service.Impl;

public class FileUploaderService : IFileUploaderService
{
    private readonly IWebHostEnvironment hostingEnvironment;
    private readonly IConfiguration configuration;


    public FileUploaderService(IWebHostEnvironment hostingEnvironment,
        IConfiguration configuration)
    {
        this.hostingEnvironment = hostingEnvironment;
        this.configuration = configuration;
    }

    public string Progress()
    {
        return StaticProgress.Progress.ToString();
    }

    public async Task<IReadOnlyCollection<string>> UploadFilesAsync(IList<IFormFile> files)
    {
        StaticProgress.Progress = 0;

        var totalBytes = files.Sum(f => f.Length);
        List<FileHelperModel> filesForTransfer = new();
        foreach (var source in files)
        {
            var filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName?.Trim('"');

            filename = EnsureCorrectFilename(filename);

            var buffer = new byte[16 * 1024];

            await using var output = File.Create(GetPathAndFilename(filename, FilesDirectory.UploadedFile));
            await using var input = source.OpenReadStream();
            long totalReadBytes = 0;
            int readBytes;

            while ((readBytes = await input.ReadAsync(buffer)) > 0)
            {
                await output.WriteAsync(buffer.AsMemory(0, readBytes));
                totalReadBytes += readBytes;
                StaticProgress.Progress = (int) (totalReadBytes / (float) totalBytes * 100.0);
                await Task.Delay(10); // It is only to make the process slower
            }

            if (filename == null) continue;

            var actualPath = Path.Combine(configuration["AppBaseUrl"] ?? string.Empty, "uploads/", filename);
            filesForTransfer.Add(new FileHelperModel
            {
                FilePath = actualPath,
                FileName = filename
            });
        }

        return await HTmlToPdf(filesForTransfer);
    }

    #region Private methods

    private async Task<IReadOnlyCollection<string>> HTmlToPdf(IEnumerable<FileHelperModel> files)
    {
        try
        {
            List<string> filePaths = new();

            using var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            var baseAddress = configuration["AppBaseUrl"];
            var page = await browser.NewPageAsync();

            foreach (var file in files)
            {
                await page.GoToAsync(file.FilePath);

                file.FileName = file.FileName.Replace(".html", ".pdf");

                var actualPath = Path.Combine(baseAddress ?? string.Empty,
                    FilesDirectory.PdfDocumentFile, file.FileName);

                var pdfFile = GetPathAndFilename(file.FileName, FilesDirectory.PdfDocumentFile);

                await page.PdfAsync(pdfFile);
                filePaths.Add(actualPath);
            }

            return filePaths;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static string? EnsureCorrectFilename(string? filename)
    {
        if (filename != null && filename.Contains('\\'))
            filename = filename[(filename.LastIndexOf("\\", StringComparison.Ordinal) + 1)..];

        return filename;
    }

    private string GetPathAndFilename(string? filename, string folder)
    {
        var path = hostingEnvironment.WebRootPath + folder;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        return path + filename;
    }

    #endregion
}