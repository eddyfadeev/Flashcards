namespace Flashcards.Interfaces.Repositories.Operations;

internal interface ISelectableRepositoryEntry<TEntity>
{
    internal TEntity? SelectedEntry { get; set; }
}