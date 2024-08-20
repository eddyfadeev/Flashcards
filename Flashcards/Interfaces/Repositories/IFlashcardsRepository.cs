using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories.Operations;

namespace Flashcards.Interfaces.Repositories;

/// <summary>
/// Represents an interface for a flashcard repository.
/// </summary>
internal interface IFlashcardsRepository :
    IInsertIntoRepository<IFlashcard>,
    IDeleteFromRepository<IFlashcard>,
    IUpdateInRepository<IFlashcard>
{
    internal IEnumerable<IFlashcard> GetFlashcards(IDbEntity<IStack> stack);
}