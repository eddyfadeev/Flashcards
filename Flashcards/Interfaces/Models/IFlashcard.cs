namespace Flashcards.Interfaces.Models;

public interface IFlashcard
{
    int Id { get; }
    string? Question { get; }
    string? Answer { get; }
    int StackId { get; }
}