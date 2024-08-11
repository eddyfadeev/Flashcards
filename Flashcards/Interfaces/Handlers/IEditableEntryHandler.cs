namespace Flashcards.Interfaces.Handlers;

internal interface IEditableEntryHandler<TEntry> where TEntry : class
{
    public TEntry? HandleEditableEntry(List<TEntry> entries);
}