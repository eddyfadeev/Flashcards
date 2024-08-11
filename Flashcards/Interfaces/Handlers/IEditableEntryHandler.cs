namespace Flashcards.Interfaces.Handlers;

internal interface IEditableEntryHandler<TEntry>
{
    public TEntry? HandleEditableEntry(List<TEntry> entries);
}