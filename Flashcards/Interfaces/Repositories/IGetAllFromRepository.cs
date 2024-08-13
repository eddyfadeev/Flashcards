namespace Flashcards.Interfaces.Repositories;

internal interface IGetAllFromRepository<out TEntity>
{
    internal IEnumerable<TEntity> GetAll();
}