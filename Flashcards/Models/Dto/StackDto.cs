using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Dto;

public record StackDto : IStack
{
    public int Id { get; init; }
    public string? Name { get; set; }
}