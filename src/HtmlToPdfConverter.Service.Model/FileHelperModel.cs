namespace HtmlToPdfConverter.Service.Model;

public record FileHelperModel
{
    public string FilePath { get; init; }
    public string FileName{ get; set; }
}
