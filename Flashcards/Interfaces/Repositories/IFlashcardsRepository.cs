using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

public interface IFlashcardsRepository
{
    internal int Insert(IDbEntity<IFlashcard> entity);
    internal IEnumerable<IFlashcard> GetAll(int stackId);
}