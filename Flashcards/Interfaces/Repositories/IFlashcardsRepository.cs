using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

internal interface IFlashcardsRepository : IRepository<IFlashcard>
{
    internal IEnumerable<IFlashcard> GetAll(int stackId);
}

internal interface IRepository<TEntity>
{
    internal TEntity ChosenEntry { get; set; }
    internal int Insert(IDbEntity<TEntity> entity);
    internal void Delete(int id);
}