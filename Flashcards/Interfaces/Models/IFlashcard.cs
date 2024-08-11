namespace Flashcards.Interfaces.Models;

public interface IFlashcard
{
    int Id { get; set; }
    string? Question { get; set; }
    string? Answer { get; set; }
    int StackId { get; set; }
}