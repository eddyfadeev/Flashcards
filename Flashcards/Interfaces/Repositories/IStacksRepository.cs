using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

public interface IStacksRepository
{
    internal int Insert(IDbEntity<IStack> entity);
    internal IEnumerable<IStack> GetAll();
}