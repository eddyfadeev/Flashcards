using Flashcards.Interfaces.Models;

namespace Flashcards.Models.Dto;

public record FlashcardDto : IFlashcard
{
    public int Id { get; init; }
    public string? Question { get; init; }
    public string? Answer { get; init; }
    public int StackId { get; init; }
}