using System.ComponentModel.DataAnnotations;
namespace ShortenIt.Models;

public class Link
{
    public string? Id { get; set; }
    [Url]
    [Required]
    public string Url { get; set; }

}
