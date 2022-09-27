using Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;
namespace HtmlToPdfConverter.Service.Model;
public record UploadFilesRequestModel(IFormFile Files, string Directory = "");
public class UploadFileRequestModelValidator : AbstractValidator<UploadFilesRequestModel>
{
    public UploadFileRequestModelValidator()
    {
        RuleFor(x => x.Files)
            .NotEmpty()
            .WithMessage(x => ExceptionThrower.RequestValidation(
                message: ErrorMessages.IsNullOrEmpty(nameof(x.Files)), x.Files.ToString(), nameof(x.Files), -2))
            .Must(x => x.Length < 2000000)
            .WithMessage(x => ExceptionThrower.RequestValidation(
                FileManagerErrorMessages.UploadFailedMaxSize, x.Files.ToString(), nameof(x.Files), -3))
            .Must(x => SupportedMimeTypes.GetMimeTypes().Contains(x.ContentType))
            .WithMessage(x => ExceptionThrower.RequestValidation(
                FileManagerErrorMessages.UploadFailedInvalidFileType, x.Files.ToString(), nameof(x.Files), -4));

        When(x => !string.IsNullOrWhiteSpace(x.Directory), () =>
        {
            RuleFor(x => x.Directory)
                .Must(x => x.EndsWith('/'))
                .WithMessage(x => ExceptionThrower.RequestValidation(
                    message: FileManagerErrorMessages.DirectoryMustEndWithSlash, x.Directory, nameof(x.Directory), -5));
        });
    }
}
