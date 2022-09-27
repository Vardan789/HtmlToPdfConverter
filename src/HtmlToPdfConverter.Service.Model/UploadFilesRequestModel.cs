using Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;
namespace HtmlToPdfConverter.Service.Model;
public class IFormFileValidator : AbstractValidator<IFormFile>
{
    public IFormFileValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage(x => ExceptionThrower.RequestValidation(
                message: ErrorMessages.IsNullOrEmpty(nameof(x)), x.ToString(), nameof(x.Name), -2))
            .Must(x => x.Length < 0)
            .WithMessage(x => ExceptionThrower.RequestValidation(
                FileManagerErrorMessages.UploadFailedMaxSize, x.ToString(), nameof(x.Name), -3))
            .Must(x => SupportedMimeTypes.GetMimeTypes().Contains(x.ContentType))
            .WithMessage(x => ExceptionThrower.RequestValidation(
                FileManagerErrorMessages.UploadFailedInvalidFileType, x.ToString(), nameof(x.Name), -4));
    }
}
