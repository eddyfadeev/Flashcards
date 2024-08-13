namespace Flashcards.Interfaces.Models;

internal interface IFlashcard
{
    int Id { get; set; }
    string? Question { get; set; }
    string? Answer { get; set; }
    int StackId { get; set; }
}