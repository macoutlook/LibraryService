using System.Text.Json.Serialization;
using Core.Domain;

namespace Service.Dtos;

public class BookDto
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string Isbn { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BookStatus BookStatus { get; set; }
}