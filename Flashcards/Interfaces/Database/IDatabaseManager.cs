using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Database;

public interface IDatabaseManager
{
    internal int InsertFlashcard(IDbEntity<IFlashcard> flashcard);
    internal int InsertStack(IDbEntity<IStack> stack);
    internal IEnumerable<IStack> GetAllStacks();
    internal IEnumerable<IFlashcard> GetFlashcardsForStack(int stackId);
}