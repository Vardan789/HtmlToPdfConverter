using HtmlToPdfConverter.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace HtmToPdfConverter.Controllers;

public class UploaderController : Controller
{
    private readonly IFileUploaderService fileUploader;

    public UploaderController(IFileUploaderService fileUploader)
    {
        this.fileUploader = fileUploader;
    }

    [HttpPost]
    public async Task<IActionResult> Index(IList<IFormFile> files)
    {
        return Ok( await fileUploader.UploadFilesAsync(files));
    }

    [HttpPost]
    public ActionResult Progress()
    {
        return this.Content(fileUploader.Progress());
    }
}