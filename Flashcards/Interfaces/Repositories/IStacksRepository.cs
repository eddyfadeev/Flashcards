using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

public interface IStacksRepository
{
    internal void Insert(IDbEntity<IStack> entity);
    internal IEnumerable<IStack> GetAll();
}