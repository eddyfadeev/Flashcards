using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

internal interface IInsertIntoRepository<in TEntity>
{
    internal int Insert(IDbEntity<TEntity> entity);
}