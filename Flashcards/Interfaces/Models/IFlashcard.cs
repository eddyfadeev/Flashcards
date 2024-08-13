using Flashcards.Interfaces.Repositories.Operations;

namespace Flashcards.Interfaces.Models;

internal interface IFlashcard : IAssignableStackId
{
    int Id { get; set; }
    string? Question { get; set; }
    string? Answer { get; set; }
}