using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

public interface IFlashcardsRepository
{
    internal void Insert(IDbEntity<IFlashcard> entity);
    internal IEnumerable<IFlashcard> GetAll(int stackId);
}