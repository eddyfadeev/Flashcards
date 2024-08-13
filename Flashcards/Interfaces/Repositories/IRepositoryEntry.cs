namespace Flashcards.Interfaces.Repositories;

internal interface IRepositoryEntry<TEntity>
{
    internal TEntity? ChosenEntry { get; set; }
}