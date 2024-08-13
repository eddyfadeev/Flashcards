namespace Flashcards.Interfaces.Repositories.Operations;

internal interface IGetAllFromRepository<out TEntity>
{
    internal IEnumerable<TEntity> GetAll();
}